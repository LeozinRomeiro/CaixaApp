using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CaixaApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void buttonCadastrar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Views.CadastrarPage());
        }

        private async void buttonListar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Views.ListarPage());
        }

        private async void buttonVer_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Views.CaixaPage());
        }

        private async void ImageButtonLeitor_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Views.CaixaPage());
        }
    }
}
