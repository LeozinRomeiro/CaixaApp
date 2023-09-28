using CaixaApp.Data;
using CaixaApp.Model;
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
    public partial class CaixaPage : ContentPage
    {
        Context context = new Context(App.Path);
        static Ferramenta Caixa = new Ferramenta();
        static List<Ferramenta> Ferramentas = new List<Ferramenta>();
        public CaixaPage()
        {
            InitializeComponent();
        }
        private async void OnAddStackLayoutClicked(object sender, EventArgs e)
        {
            string codigo = await LerCodigoAsync();
            await DisplayAlert("erro", "nao chega aqui", "tes");
            if (!string.IsNullOrEmpty(codigo))
            {
                CriarStakyLauout(codigo);
            }
            else
            {
                await DisplayAlert("Não encontrado", "Codigo de barras inavalido! por favor verifique se ele está correto", "Ok");
            }
        }
        private async void Button_Clicked(object sender, EventArgs e)
        {
            string codigo = await LerCodigoAsync();
            Caixa = context.LocalizarFerramenta(codigo);
        }

        private async Task<string> LerCodigoAsync()
        {
            await Navigation.PushAsync(new LeitorPage(this));
            CodigoBarras codigo = this.BindingContext as CodigoBarras;

            if (codigo != null && !string.IsNullOrEmpty(codigo.Codigo))
            {
                return codigo.Codigo;
            }

            return string.Empty;
        }

        private void CriarStakyLauout(string codigo)
        {
            try
            {
                Ferramenta ferramenta = new Ferramenta();
                ferramenta = context.LocalizarFerramentaCodigo(codigo);
                if (ferramenta != null)
                {
                    var stackLayoutFerramenta = new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        Children =
                        {
                            new StackLayout
                            {
                                Children =
                                {
                                    new Image
                                    {
                                        Source = "ok",
                                        HorizontalOptions = LayoutOptions.Start
                                    }
                                }
                            },
                            new StackLayout
                            {
                                Orientation = StackOrientation.Horizontal,
                                VerticalOptions = LayoutOptions.CenterAndExpand,
                                HorizontalOptions = LayoutOptions.CenterAndExpand,
                                Children =
                                {
                                    new Label {  Text = ferramenta.Nome, FontSize = 20 },
                                    new Label { Text = ferramenta.Tipo, FontSize = 20 }
                                }
                            }
                        }
                    };
                    var contentStackLayout = Content as StackLayout;
                    if (contentStackLayout != null)
                    {
                        contentStackLayout.Children.Add(stackLayoutFerramenta);
                    }
                    ((StackLayout)Content).Children.Add(stackLayoutFerramenta);
                    Ferramentas.Add(ferramenta);
                }
                else
                {
                    DisplayAlert("Não encontrado", "Codigo de barras não encontrado! por favor verifique se ele está cadastrada", "Ok");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}