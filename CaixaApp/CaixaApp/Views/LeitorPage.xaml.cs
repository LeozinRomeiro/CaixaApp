using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CaixaApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LeitorPage : ContentPage
    {
        public LeitorPage()
        {
            InitializeComponent();
        }

        private async void ZXingScannerView_OnScanResult(ZXing.Result result)
        {
            string codigo;
            Device.BeginInvokeOnMainThread(() =>
            {
                resultCodigo.Text = result.Text + " (type: " + result.BarcodeFormat.ToString() + ")";
                codigo = result.Text;
            });
            codigo=resultCodigo.Text;
            await Navigation.PopAsync();
            Codigo?.Invoke(this, codigo);
        }
        public event EventHandler<string> Codigo;
    }
}