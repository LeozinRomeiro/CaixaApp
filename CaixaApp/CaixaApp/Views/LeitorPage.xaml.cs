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
        private Action<String> _onCodeScanned;
        public LeitorPage(Action<String> OnCodeScanned)
        {
            InitializeComponent();
            _onCodeScanned = OnCodeScanned;
        }

        //private void ZXingScannerView_OnScanResult(ZXing.Result result)
        //{
        //    //Atualize o BindingContext na thread principal
        //    Device.BeginInvokeOnMainThread(async () =>
        //    {
        //        resultCodigo.Text = result.Text;
        //        CodigoBarras codigo = new CodigoBarras
        //        {
        //            Codigo = result.Text,
        //        };
        //        DestinoPage.BindingContext = codigo;
        //        await Navigation.PopAsync();
        //    });
        //}

        private void ZXingScannerView_OnScanResult(ZXing.Result result)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                string codigo = result.Text;
                _onCodeScanned?.Invoke(codigo);
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
