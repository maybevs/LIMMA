using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using LIMMA.Data;
using LIMMA.Helper;
using LIMMA.Interfaces;
using LIMMA.Models;
using LIMMA.Services;
using LIMMA.ViewModels;
using LIMMA.Views;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.ObjectBuilder;
using Newtonsoft.Json;
using Prism.Unity;
using Xamarin.Forms;
using System.Threading;
using Microsoft.AspNet.SignalR.Client;


namespace LIMMA
{
    public class App : PrismApplication
    {
        static LocalStorage storage;
        public App()
        {
            // The root page of your application
            //MainPage = new ContentPage
            //{
            //    Content = new StackLayout
            //    {
            //        VerticalOptions = LayoutOptions.Center,
            //        Children = {
            //            new Label {
            //                HorizontalTextAlignment = TextAlignment.Center,
            //                Text = "Welcome to Xamarin Forms!"
            //            }
            //        }
            //    }
            //};
        }

        public static LocalStorage Storage => storage ??
                                              (storage =
                                                  new LocalStorage(
                                                      DependencyService.Get<IFileHelper>().GetLocalFilePath("LocalStorageSQLite.db3")));

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        protected override void OnInitialized()
        {
            
            NavigationService.NavigateAsync("MainPage");
        }

        private static ConfigurationService configurator;
        private static ConnectionService connector;
        /// <summary>
        /// In RegisterTypes we'll do everything that needs to be globally available. This includes setting up our Navigation as well as our Backendconnections.
        /// </summary>
        protected override async void RegisterTypes()
        {
            


            //Service Initialisation
            Debug.WriteLine("Setting up Services");
            configurator = new ConfigurationService();
            connector = new ConnectionService();


            //DependencyInjection Init
            // ConnectionService
            Debug.WriteLine("Registering Services");
            Container.RegisterInstance<IConnectionServices>("Connector",connector);
            Container.RegisterType<IConnectionServices, ConnectionService>();

            // ConfigurationService
            Container.RegisterInstance<IConfiguration>("Config", configurator);
            Container.RegisterType<IConfiguration, ConfigurationService>();


            Debug.WriteLine("Registering Navigation");
            //Navigation Registration
            //Todo: Extend for generated Pages
            Container.RegisterTypeForNavigation<MainPage, MainPageViewModel>("MainPage");

            //Test
            var token = await connector.GetCurrentToken(configurator);

            var structure = await connector.GetAppStructure(configurator);

            string s = "";

            GenerateMain(structure);


            //var hubConnection = new HubConnection("http://demo.m2mgo.com");
            //hubConnection.Headers.Add("Authorization",token.TokenPrefix + token.Token);


            ////hubConnection.Headers.Add("Authorization",token.TokenPrefix + token.Token);
            //var proxy = hubConnection.CreateHubProxy("cms");

            //QuerySettings.QuerySettings qs = new QuerySettings.QuerySettings();
            //qs.CurrentPage = 1;
            
            
            //await hubConnection.Start();

            //await proxy.Invoke<Guid>("RegisterPage", "ee59fe53-da64-4345-a349-6b303d5ceb6a",qs);

            //proxy.On<string, string>("datasourceUpdated", (updateType, Values) => updateType)


            //Device.StartTimer(TimeSpan.FromSeconds(5), () =>
            //{
            //    Task.Factory.StartNew(async () =>
            //    {
            //        await Tick();
            //    });
            //    return true;
            //});



        }

        private List<WidgetBinding> widgetBindings;
        private void GenerateMain(AppStructure structure)
        {

            ContentPage generatedMain = new ContentPage()
            {
                Title = "Hallo Welt!"
            };

            StackLayout root = new StackLayout();
            root.Orientation = StackOrientation.Vertical;

            var mainpageChildren =
                structure.Tenant.RootNode.Children.FirstOrDefault().Children.FirstOrDefault().Page.RootWidget.Children;


            /*
             * WidgetTypes:
             * Navigation: 3cb0c700-5b55-4563-81f5-576e1a31271a
             * SingleValue: 57562e4a-8d89-47d1-94ae-a0a1feb206f1
             */

            bool hasGrid = false;
            Dictionary<string,List<Column>> gridDefinitions = new Dictionary<string, List<Column>>();
            GenerateChildren(mainpageChildren, hasGrid, gridDefinitions, root);

            generatedMain.Content = root;
            NodeBindings nbs = new NodeBindings(new Dictionary<Guid, DataModel>(),new Dictionary<Guid, WidgetBinding>() );
            widgetBindings =  GetAllBindings(structure.Tenant.RootNode, nbs);


            MainPage = generatedMain;

            

            foreach (var child in ((StackLayout)((ContentPage)MainPage).Content).Children)
            {
                if (child.GetType() == typeof(SingleValue))
                {
                    if (((SingleValue)child).Name == "c326141e-3e9e-489f-85d2-387208629be0")
                    {
                        ((SingleValue)child).TextDisplay.Text = "Hallo Welt!";
                    }
                }
            }


        }

        private void GenerateChildren(List<Widget> mainpageChildren, bool hasGrid, Dictionary<string, List<Column>> gridDefinitions, Layout<View> root)
        {
            foreach (var mainpageChild in mainpageChildren)
            {
                switch (mainpageChild.WidgetTypeID)
                {
                    case "57562e4a-8d89-47d1-94ae-a0a1feb206f1":
                        SingleValue sv = new SingleValue(mainpageChild.ID, mainpageChild.Model.Settings);

                        if (hasGrid)
                        {
                            var gridInfo = CheckGrid(mainpageChild.ID, gridDefinitions);
                            if (gridInfo != null)
                            {
                                Grid grid = (Grid) root;

                                Grid.SetColumn(sv, gridInfo.Offset);
                                Grid.SetColumnSpan(sv, gridInfo.Span);
                                grid.Children.Add(sv);
                                break;
                            }
                        }

                        root.Children.Add(sv);
                        break;
                    case "9a638f0b-99cd-4312-9b78-0c7872d7cb75":
                        gridDefinitions.Add(mainpageChild.ID, mainpageChild.Model.Settings.Columns);
                        hasGrid = true;
                        DisplayGrid dg = new DisplayGrid(mainpageChild.ID, mainpageChild.Model.Settings);
                        
                        GenerateChildren(mainpageChild.Children,true,gridDefinitions,dg.Grid);
                        root.Children.Add(dg);
                        break;
                }
            }
        }

        private View FindChild(string id, Type type)
        {
            foreach (var child in ((StackLayout) ((ContentPage) MainPage).Content).Children)
            {
                if (child.GetType() == type)
                {
                    if (type == typeof(SingleValue))
                    {
                        if (((SingleValue)child).Name == id)
                        {
                            return child;
                        }
                    }
                    else if (type == typeof(DisplayGrid))
                    {
                        if (((DisplayGrid)child).Name == id)
                        {
                            return child;
                        }
                    }
                }
            }

            return null;
        }

        private ColumnCreationInformation CheckGrid(string id,Dictionary<string, List<Column>> gridDefinitions)
        {
            foreach (var gridDefinition in gridDefinitions)
            {
                foreach (var column in gridDefinition.Value)
                {
                    foreach (var nodeReference in column.Content)
                    {
                        if (id == nodeReference.ID)
                        {
                            return new ColumnCreationInformation
                            {
                                GridID = gridDefinition.Key,
                                Offset = column.Offset,
                                Span = column.Span
                            };
                        }
                    }
                }
            }
            return null;
        }

        private async Task<bool> Tick()
        {

            foreach (var widgetBinding in widgetBindings)
            {
                foreach (var target in widgetBinding.Targets)
                {
                    var datamodel = await GetDataModel(target.DatasourceID);
                }
               //foreach (var child in ((StackLayout)((ContentPage)MainPage).Content).Children)
               //{
               //    if (child.GetType() == typeof(SingleValue))
               //    {
               //        if (((SingleValue)child).Name == widgetBinding.WidgetID.ToString())
               //        {
               //            ((SingleValue)child).TextDisplay.Text = DateTime.Now.ToString();
               //        }
               //    }
               //}
                
            }

            return true;
        }

        private async Task<DataModel> GetDataModel(Guid targetDatasourceID)
        {
            DataModel dm = await connector.GetDataModel(targetDatasourceID, configurator);

            return dm;
        }

        private List<WidgetBinding> GetAllBindings(Node root, NodeBindings nbs)
        {
            List<WidgetBinding> bindings = new List<WidgetBinding>();

            
            var nodebindings = root.NodeDataSources.Bindings;
            var nodeSources = root.NodeDataSources.Sources;

            if (nodeSources != null)
            {
                foreach (var nodeSource in nodeSources)
                {
                    DataModel dm = new DataModel();
                    dm.DatasourceID = Guid.Parse(nodeSource.ID);
                    //dm.SourceTypeID = Guid.Parse(nodeSource.Type);
                }
            }

            if (nodebindings != null)
            {
                

                foreach (var nodebinding in nodebindings)
                {
                    WidgetBinding wb = new WidgetBinding(Guid.Parse(nodebinding.WidgetID), new List<TargetBinding>
                    {
                        new TargetBinding(Guid.Parse(nodebinding.DataSourceID), Guid.Parse(nodebinding.BindingTargetID),
                            nodebinding.Expression)
                    });
                    nbs.WidgetBindings.Add(wb.WidgetID, wb);
                    bindings.Add(wb);
                }
            }

            if (root.Children.Count > 0)
            {
                foreach (var child in root.Children)
                {
                    bindings.AddRange(GetAllBindings(child,nbs));
                }
            }
            
            return bindings;
        }

        private async void GetData(Settings RootModelSettings)
        {
            //
        }
    }

}
