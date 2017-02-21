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

            string s = "";



        }

        
    }

}
