using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LIMMA.Data;
using LIMMA.Helper;
using LIMMA.Interfaces;
using LIMMA.Services;
using LIMMA.ViewModels;
using LIMMA.Views;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.ObjectBuilder;
using Newtonsoft.Json;
using Prism.Unity;
using Xamarin.Forms;

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

        /// <summary>
        /// In RegisterTypes we'll do everything that needs to be globally available. This includes setting up our Navigation as well as our Backendconnections.
        /// </summary>
        protected override async void RegisterTypes()
        {
            


            //Service Initialisation
            Debug.WriteLine("Setting up Services");
            ConfigurationService configurator = new ConfigurationService();
            ConnectionService connector = new ConnectionService();


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



        }

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


            foreach (var mainpageChild in mainpageChildren)
            {
                switch (mainpageChild.WidgetTypeID)
                {
                    case "57562e4a-8d89-47d1-94ae-a0a1feb206f1":
                        SingleValue sv = new SingleValue("c326141e-3e9e-489f-85d2-387208629be0",mainpageChild.Model.Settings);

                        //Label l = new Label();
                        
                        //l.BackgroundColor = GetColor(mainpageChild.Model.Settings.BackgroundColor);
                        //l.Text = "1234567890ßßßßßßßßßßßß";
                        root.Children.Add(sv);
                        break;
                }
            }

            generatedMain.Content = root;

            

            
            MainPage = generatedMain;

            foreach (var child in ((StackLayout)((ContentPage)MainPage).Content).Children)
            {
                if (child.GetType() == typeof(SingleValue))
                {
                    if (((SingleValue)child).Name == "c326141e-3e9e-489f-85d2-387208629be0")
                    {
                        ((SingleValue)child).TextDisplay.Text = "trolololollolollolo";
                    }
                }
            }


        }
    }

}
