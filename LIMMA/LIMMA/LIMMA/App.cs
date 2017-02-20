using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LIMMA.Interfaces;
using LIMMA.Services;
using LIMMA.ViewModels;
using LIMMA.Views;
using Microsoft.Practices.Unity;
using Prism.Unity;
using Xamarin.Forms;

namespace LIMMA
{
    public class App : PrismApplication
    {
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
            //Navigation Registration
            //Todo: Extend for generated Pages
            Container.RegisterTypeForNavigation<MainPage, MainPageViewModel>("MainPage");


            //Service Initialisation
            ConnectionService connector = new ConnectionService();
            ConfigurationService configurator = new ConfigurationService();


            //DependencyInjection Init
            // ConnectionService
            Container.RegisterInstance<IConnectionServices>("Connector",connector);
            Container.RegisterType<IConnectionServices, ConnectionService>();

            // ConfigurationService
            Container.RegisterInstance<IConfiguration>("Config", configurator);
            Container.RegisterType<IConfiguration, ConfigurationService>();


            //Test

            



        }
    }

}
