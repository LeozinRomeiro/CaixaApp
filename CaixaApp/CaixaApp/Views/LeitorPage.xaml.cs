using CaixaApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Mobile;

namespace CaixaApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LeitorPage : ContentPage
    {
        private ContentPage DestinoPage;
        public LeitorPage(ContentPage page)
        {
            InitializeComponent();
            DestinoPage = page;
        }

        private async void ZXingScannerView_OnScanResult(ZXing.Result result)
        {
            // Atualize o BindingContext na thread principal
            await Device.BeginInvokeOnMainThread(async () =>
            {
                resultCodigo.Text = result.Text;
                CodigoBarras codigo = new CodigoBarras
                {
                    Codigo = result.Text,
                };
                DestinoPage.BindingContext = codigo;
                await Navigation.PopAsync();
            });

        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            scannerView.OnScanResult -= ZXingScannerView_OnScanResult;
        }
    }
}
