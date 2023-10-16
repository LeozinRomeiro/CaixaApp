using CaixaApp.Data;
using CaixaApp.Model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Datamatrix.Encoder;

namespace CaixaApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CaixaPage : ContentPage
    {
        string CodigoLido;
        bool Repetido;
        string Processo;
        Context context = new Context(App.Path);
        static Ferramenta Caixa = new Ferramenta();
        static Colaborador colaborador = new Colaborador();
        static List<Ferramenta> ferramentasAnalise = new List<Ferramenta>();
		static List<Ferramenta> ferramentasRegistras = new List<Ferramenta>();
		public CaixaPage()
        {
            InitializeComponent();
            EscolherProcesso();
        }
        public CaixaPage(string processo)
        {
            Processo = processo;
            EscolherProcesso();
        }
        public async void EscolherProcesso()
		{
            while (Processo==null)
            {
                Processo = await DisplayActionSheet("Escolhar o cadastrado:", "Cancelar", null, "Montar caixa", "Verificar caixa");
            }
			if (Processo == "Montar caixa")
			{
				await Navigation.PushAsync(new ListarPage(DefinirColaborador));
                MostrarButton(buttonCaixa);
				buttonFinalizar.Text = "Registrar nessa caixa";
			}
            else
            {
				MostrarButton(buttonCaixa);
                buttonFinalizar.Text = "Finalizar verificacão";
                buttonAdicionarStackLayout.Text = "Verificar ferramenta";
				//if (await DisplayAlert("Verificar caixa", "Por favor leia o QRcode da caixa do funcionario...", "Abrir leitor", "Cancelar"))
				//{
				//}
				//if (Caixa.Codigo != null)
				//         {
				//             await LerCodigoCaixaAsync();
				//         }
				//         else
				//         {
				//             bool resposta = await DisplayAlert("Substituir", "O dono da caixa já foi selecionado, quer alterar?", "Sim", "Não");
				//             if (resposta)
				//             {
				//		await LerCodigoCaixaAsync();
				//	}
				//         }
			}
        }
		private async void AdicionarStackLayoutClicked(object sender, EventArgs e)
        {
			await LerCodigoFerramentaAsync();
        }
        private async void LerCaixaClicked(object sender, EventArgs e)
        {
			await LerCodigoCaixaAsync();
            buttonCaixa.IsVisible = false;
            MostrarButton(buttonAdicionarStackLayout);
		}

        private async Task LerCodigoCaixaAsync()
        {
			CodigoLido = string.Empty;
			var leitorPage = new LeitorPage(OnCodeScanned);
			leitorPage.Disappearing += async (sender, e) =>
            {
                if (!string.IsNullOrEmpty(CodigoLido))
                {
					Caixa.Codigo = CodigoLido;
					await DefinirCaixa(Caixa.Codigo);
                    BuscarFerramentasCaixa(Caixa);
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
			CodigoLido = string.Empty;
			var leitorPage = new LeitorPage(OnCodeScanned);
			leitorPage.Disappearing += async (sender, e) =>
			{
				if (!string.IsNullOrEmpty(CodigoLido))
				{
					if (Repetido||!string.IsNullOrEmpty(CodigoLido))
                    {
                        if (CodigoLido!=Caixa.Codigo.ToString())
                        {
						    await CriarStakyLauout(CodigoLido);
                        }
                        else
                        {
                            await DisplayAlert("Já foi", "A caixa já foi apontada...", "ok");
                        }
                    }
                    else
                    {
					    await DisplayAlert("Já foi", "Essa ferramenta já foi apontada...", "ok");
                    }
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
			Repetido = VerificarRepeticao(CodigoLido);
		}
		private void DefinirColaborador(Colaborador _colaborador)
        {
            colaborador = _colaborador;
            labelColaborador.Text = colaborador.Nome;
        }
        private async Task DefinirCaixa(string codigoLido)
        {
			Caixa = context.LocalizarFerramenta(codigoLido);
            colaborador.IdCaixa = Caixa.Id;
            DefinirColaborador(context.LocalizarColaboradorCaixa(Caixa.IdCaixa));
			string texto = Caixa.Nome +" " + Caixa.Tipo;
			labelCaixa.Text = texto;
		}
        private bool VerificarRepeticao(string codigoLido)
        {
            foreach (var ferramenta in ferramentasAnalise)
            {
                if(ferramenta.Codigo == codigoLido)
                {
                    return true;
                }
            }
            return false;
        }

        private void BuscarFerramentasCaixa(Ferramenta caixa)
        {
            ferramentasRegistras = context.LocalizarFerramentasNaCaixa(caixa);
        }

        private void MostrarButton(Button button)
        {
			button.BackgroundColor = Color.Green;
			button.TextColor = Color.White;
		}

        private async Task CriarStakyLauout(string codigo)
        {
            try
            {
                Ferramenta ferramenta = new Ferramenta();
                ferramenta = context.LocalizarFerramenta(codigo);
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
                                    new Label {  Text = ferramenta.Nome, FontSize = 18, TextColor=Color.Black },
                                    new Label { Text = ferramenta.Tipo, FontSize = 18, TextColor=Color.Black }
                                }
                            }
                        }
                    };
                    var scrollView = new ScrollView
                    {
                        VerticalOptions = LayoutOptions.CenterAndExpand,
                        Content = stackLayoutFerramenta,
                    };
                    var contentStackLayout = Content as StackLayout;
                    if (contentStackLayout != null)
                    {
                        contentStackLayout.Children.Add(scrollView);
                    }
                    //((StackLayout)Content).Children.Add(stackLayoutFerramenta);
                    ferramentasAnalise.Add(ferramenta);
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

		private async void buttonFinalizar_Clicked(object sender, EventArgs e)
		{
            try
            {
                if (await DisplayAlert("Confirmação", "Tem certeza que deseja finalizar?", "Sim", "Não"))
                {
                    if (Processo == "Montar caixa")
                    {
                        foreach (var ferramenta in ferramentasAnalise)
                        {
                            ferramenta.IdCaixa = Caixa.Id;
                            context.Atualizar(ferramenta);
                        }
                        context.Atualizar(colaborador);
                        await DisplayAlert("Sucesso", "Sua caixa foi montada com sucesso!!", "Concluir");
                    }
                    else
                    {
						var ferramentasToRemove = new List<Ferramenta>();

						foreach (var ferramentaEsperada in ferramentasRegistras)
						{
							foreach (var ferramenta in ferramentasAnalise)
							{
								if (ferramentaEsperada.Id == ferramenta.Id)
								{
									ferramentasToRemove.Add(ferramentaEsperada);
								}
							}
						}

						foreach (var ferramentaToRemove in ferramentasToRemove)
						{
							ferramentasRegistras.Remove(ferramentaToRemove);
						}
						if (ferramentasRegistras.Count == 0)
                        {
                            await DisplayAlert("Sucesso", "Todas as ferramentas estão presentes!", "Concluir");
                        }
                        else
                        {
                            string Faltantes = string.Empty;
                            foreach (var ferramentaFaltante in ferramentasRegistras)
                            {
                                Faltantes += ferramentaFaltante.Nome + "\n";
                            }
                            await DisplayAlert("Perai", "As seguintes ferramentas estão faltando...\n" + Faltantes, "Concluir");
                        }
                    }
                }
				await Navigation.PushAsync(new Views.CaixaPage());
			}
			catch (Exception)
            {
                throw;
            }
        }
	}
}