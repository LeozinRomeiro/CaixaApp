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
        string CodigoLido;
        Context context = new Context(App.Path);
        static Ferramenta Caixa = new Ferramenta();
        static List<Ferramenta> Ferramentas = new List<Ferramenta>();
        public CaixaPage()
        {
            InitializeComponent();
        }

        private async void AdicionarStackLayoutClicked(object sender, EventArgs e)
        {
			await LerCodigoFerramentaAsync();
            if (!string.IsNullOrEmpty(Caixa.Codigo))
            {
                await DisplayAlert("Opa", Caixa.Codigo, "ok");
                await CriarStakyLauout(Caixa.Codigo);
            }
        }
        private async void LerCaixaClicked(object sender, EventArgs e)
        {
            await LerCodigoCaixaAsync();
		}

        private async Task DeterminarCaixa(string codigoLido)
        {
			Caixa = context.LocalizarFerramenta(codigoLido);
			labelColaborador.Text = (context.LocalizarColaboradorCaixa(Caixa.IdCaixa)).Nome;
        }

        private async Task LerCodigoCaixaAsync()
        {
            var leitorPage = new LeitorPage(OnCodeScanned);
			Caixa.Codigo = CodigoLido;
			leitorPage.Disappearing += async (sender, e) =>
            {
                if (!string.IsNullOrEmpty(CodigoLido))
                {
                    await DeterminarCaixa(CodigoLido);
                }
                else
                {
                    await DisplayAlert("Errado", "Codigo invalido", "ok");
                }
            };
            await Navigation.PushAsync(leitorPage);
        }

		private async Task LerCodigoFerramentaAsync()
		{
			var leitorPage = new LeitorPage(OnCodeScanned);
            Caixa.Codigo = CodigoLido;
			leitorPage.Disappearing += async (sender, e) =>
			{
				if (!string.IsNullOrEmpty(Caixa.Codigo))
				{
					await DeterminarCaixa(Caixa.Codigo);
				}
				else
				{
					await DisplayAlert("Errado", "Codigo invalido", "ok");
				}
			};
			await Navigation.PushAsync(leitorPage);
		}

		private void OnCodeScanned(string codigo)
        {
            CodigoLido = codigo;
        }

        private async Task CriarStakyLauout(string codigo)
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
                    await DisplayAlert("Não encontrado", "Codigo de barras não encontrado! por favor verifique se ele está cadastrada", "Ok");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}