using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CaixaApp
{
    public partial class App : Application
    {
        public static string DataName;
        public static string Path;
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        public App(string name,string path)
        {
            InitializeComponent();
            DataName = name;
            Path = path;
            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
