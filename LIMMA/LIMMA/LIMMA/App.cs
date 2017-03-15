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
using LIMMA.QuerySettings;
using Microsoft.AspNet.SignalR.Client;
using Newtonsoft.Json.Linq;


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

            deviceBindings = await GetAllDeviceBindings(widgetBindings);


            foreach (var pageBinding in pageBindings)
            {
                var dm = await GetDataModel(pageBinding.Key.NodeID);
            }
            



            var hubConnection = new HubConnection("https://demo.m2mgo.com");
            hubConnection.Headers.Add("Authorization", token.TokenPrefix + token.Token);


            
            var proxy = hubConnection.CreateHubProxy("cms");

            QuerySettings.QuerySettings qs = new QuerySettings.QuerySettings();
            //qs.CurrentPage = 1;
            qs.Parameter = new Dictionary<string, List<string>>();

            hubConnection.Received += HubConnectionOnReceived;

            await hubConnection.Start();

            await proxy.Invoke("registerPage", "ee59fe53-da64-4345-a349-6b303d5ceb6a",qs);
            SensorDataSaved test;
            SensorValue[] Values;
            //proxy.On<SensorDataSaved, Value>("datasourceUpdated", (message, values) => Debug.WriteLine("Data Received:"+message.ToString()));

            //proxy.On<string, string>("datasourceUpdated", (updateType, Values) => updateType)

        }

        private void HubConnectionOnReceived(string s)
        {
            Debug.WriteLine("Data Received");
            dynamic message = JsonConvert.DeserializeObject(s);

            if (message.M == "datasourceUpdated")
            {
                Debug.WriteLine("Datasource Updated");

                var sensordataSaved = message.A;
                var blubb = sensordataSaved[1];
                var value = blubb.SensorValueSeries[0].Values[0].Value;
                var deviceID = blubb.SensorValueSeries[0].Values[0].DeviceIdentifier.ID;

                UpdateDisplay(deviceID, value);

                //Debug.WriteLine(sensordataSaved[1].EventID);


            }
            
        }

        private void UpdateDisplay(object deviceID, object value)
        {
            foreach (var devicebinding in deviceBindings)
            {
                if (devicebinding.DeviceId == Guid.Parse(deviceID.ToString()))
                {
                    foreach (var child in ((StackLayout)((ContentPage)MainPage).Content).Children)
                    {
                        if (child.GetType() == typeof(SingleValue))
                        {
                            if (((SingleValue)child).Name == devicebinding.WidgetId.ToString())
                            {
                                Device.BeginInvokeOnMainThread(() => ((SingleValue)child).TextDisplay.Text = value.ToString());

                            }
                        }
                        if (child.GetType() == typeof(DisplayGrid))
                        {
                            foreach (var gridChild in ((DisplayGrid)child).Grid.Children)
                            {
                                if (gridChild.GetType() == typeof(SingleValue))
                                {
                                    if (((SingleValue)gridChild).Name == devicebinding.WidgetId.ToString())
                                    {
                                        Device.BeginInvokeOnMainThread(() => ((SingleValue)gridChild).TextDisplay.Text = value.ToString());

                                    }
                                }
                            }
                        }
                    }
                }



                //foreach (var target in widgetBinding.Targets)
                //{
                //    var datamodel = await GetDataModel(target.DatasourceID);
                //}
                //foreach (var child in ((StackLayout)((ContentPage)MainPage).Content).Children)
                //{
                //    if (child.GetType() == typeof(SingleValue))
                //    {
                //        if (((SingleValue)child).Name == widgetBinding.WidgetID.ToString())
                //        {
                //            Device.BeginInvokeOnMainThread(() => ((SingleValue)child).TextDisplay.Text = value.ToString());
                            
                //        }
                //    }
                //    if (child.GetType() == typeof(DisplayGrid))
                //    {
                //        foreach (var gridChild in ((DisplayGrid)child).Grid.Children)
                //        {
                //            if (gridChild.GetType() == typeof(SingleValue))
                //            {
                //                if (((SingleValue)gridChild).Name == widgetBinding.WidgetID.ToString())
                //                {
                //                    Device.BeginInvokeOnMainThread(() => ((SingleValue)gridChild).TextDisplay.Text = value.ToString());

                //                }
                //            }
                //        }
                //    }
                //}

            }
        }

        private List<WidgetBinding> widgetBindings;
        private NodeBindings nbs;
        private List<DeviceBinding> deviceBindings;
        private void GenerateMain(AppStructure structure)
        {

            ContentPage generatedMain = new ContentPage()
            {
                Title = "Hallo Welt!"
            };

            StackLayout root = new StackLayout();
            root.Orientation = StackOrientation.Vertical;


            //Todo: Add recursiveness here to generate multiple pages.
            var mainpageChildren =
                structure.Tenant.RootNode.Children.FirstOrDefault().Children.FirstOrDefault().Page.RootWidget.Children;
            Guid nodeID = Guid.Parse(structure.Tenant.RootNode.Children.FirstOrDefault().Children.FirstOrDefault().Id);



            deviceBindings = new List<DeviceBinding>();
            bool hasGrid = false;
            Dictionary<string,List<Column>> gridDefinitions = new Dictionary<string, List<Column>>();
            GenerateChildren(mainpageChildren, hasGrid, gridDefinitions, root, nodeID);

            generatedMain.Content = root;
            nbs = new NodeBindings(new Dictionary<Guid, DataModel>(),new Dictionary<Guid, WidgetBinding>() );
            pageBindings = new Dictionary<DataPage, List<WidgetBinding>>();
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

        private Guid GetNodeIDfromPageID(Guid guid)
        {
            return pageBindings.Where(pb => pb.Key.PageID == guid).Select(pb => pb.Key.NodeID).FirstOrDefault();
        }

        private Guid GetPageIDfromNodeID(Guid guid)
        {
            return pageBindings.Where(pb => pb.Key.NodeID == guid).Select(pb => pb.Key.PageID).FirstOrDefault();
        }


        private async Task<List<DeviceBinding>> GetAllDeviceBindings(List<WidgetBinding> list)
        {
            foreach (var deviceBinding in deviceBindings)
            {
                var dm = await GetDataModel(deviceBinding.NodeId);
                deviceBinding.DeviceId = dm;
            }




            //Dictionary<Guid,Guid> bindings = new Dictionary<Guid, Guid>();
            //foreach (var widgetBinding in list)
            //{
            //    foreach (var widgetBindingTarget in widgetBinding.Targets)
            //    {
            //        var dm = await GetDataModel(widgetBindingTarget.DatasourceID);
            //        if (dm != null)
            //        {
            //            dynamic whatIreallyneed = JsonConvert.DeserializeObject(dm.Data.ToString());
            //        }
            //    }
            //}


            return deviceBindings;
        }

        private void GenerateChildren(List<Widget> mainpageChildren, bool hasGrid, Dictionary<string, List<Column>> gridDefinitions, Layout<View> root, Guid nodeId)
        {
            
            foreach (var mainpageChild in mainpageChildren)
            {

             /*
             * WidgetTypes:
             * Navigation: 3cb0c700-5b55-4563-81f5-576e1a31271a
             * SingleValue: 57562e4a-8d89-47d1-94ae-a0a1feb206f1
             */
                switch (mainpageChild.WidgetTypeID)
                {
                    case "57562e4a-8d89-47d1-94ae-a0a1feb206f1":
                        SingleValue sv = new SingleValue(mainpageChild.ID, mainpageChild.Model.Settings);
                        DeviceBinding db = new DeviceBinding();
                        db.WidgetId = Guid.Parse(mainpageChild.ID);
                        db.PageId = Guid.Parse(mainpageChild.ParentID);
                        db.NodeId = nodeId;
                        deviceBindings.Add(db);

                        
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
                        
                        GenerateChildren(mainpageChild.Children,true,gridDefinitions,dg.Grid, nodeId);
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

        private async Task<Guid> GetDataModel(Guid targetDatasourceID)
        {
            Guid dm = await connector.GetDeviceIdFromDataModel(targetDatasourceID, configurator);

            return dm;
        }

        private Dictionary<DataPage, List<WidgetBinding>> pageBindings;

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
            pageBindings.Add(new DataPage {NodeID = Guid.Parse(root.Id), PageID = Guid.Parse(root.PageID)}, bindings);
            return bindings;
        }

        private async void GetData(Settings RootModelSettings)
        {
            //
        }
    }

    internal class DataPage
    {
        public Guid PageID { get; set; }
        public Guid NodeID { get; set; }
    }
}
