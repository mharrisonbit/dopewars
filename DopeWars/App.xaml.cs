﻿using Prism;
using Prism.Ioc;
using Prism.Modularity;
using Xamarin.Forms;

namespace DopeWars
{
    [AutoRegisterForNavigation]
    public partial class App
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("TabbedView?selectedTab=HomeView");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {

        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Support IApplicationLifecycleAware
            base.OnSleep();

            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Support IApplicationLifecycleAware
            base.OnResume();

            // Handle when your app resumes
        }
    }
}