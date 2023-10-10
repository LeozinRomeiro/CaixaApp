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
        string Processo;
        Context context = new Context(App.Path);
        static Ferramenta Caixa = new Ferramenta();
        static Colaborador colaborador = new Colaborador();
        static List<Ferramenta> ferramentas = new List<Ferramenta>();
		static List<Ferramenta> ferramentasCaixa = new List<Ferramenta>();
		public CaixaPage()
        {
            InitializeComponent();
            EscolherProcesso();
        }
        public CaixaPage(string processo)
        {
            Processo = processo;
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
                buttonCaixa.BackgroundColor = Color.Green;
                buttonCaixa.TextColor = Color.White;
            }
            else
            {
                buttonCaixa.BackgroundColor = Color.Green;
                buttonCaixa.TextColor = Color.White;
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
                    if (!VerificarRepeticao(CodigoLido))
                    {
                        if (CodigoLido!=Caixa.Codigo.ToString())
                        {
						    await CriarStakyLauout(CodigoLido);
						    ferramentas.Add(context.LocalizarFerramenta(CodigoLido));
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
            colaborador = (context.LocalizarColaboradorCaixa(Caixa.IdCaixa));
            labelColaborador.Text = colaborador.Nome;
        }
        private bool VerificarRepeticao(string codigoLido)
        {
            foreach (var ferramenta in ferramentas)
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
            ferramentasCaixa = context.LocalizarFerramentasNaCaixa(caixa);
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
                                    new Label {  Text = ferramenta.Nome, FontSize = 18 },
                                    new Label { Text = ferramenta.Tipo, FontSize = 18 }
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
                    ferramentas.Add(ferramenta);
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
                        foreach (var ferramenta in ferramentas)
                        {
                            ferramenta.IdCaixa = Caixa.Id;
                            context.Atualizar(ferramenta);
                        }
                        context.Atualizar(colaborador);
                        await DisplayAlert("Sucesso", "Sua caixa foi montada com sucesso!!", "Concluir");
                    }
                    else
                    {
                        foreach (var ferramentaEsperada in ferramentasCaixa)
                        {
                            foreach (var ferramenta in ferramentas)
                            {
                                if (ferramentaEsperada.Id == ferramenta.Id)
                                {
                                    ferramentasCaixa.Remove(ferramentaEsperada);
                                }
                            }
                        }
                        if (ferramentasCaixa.Count == 0)
                        {
                            await DisplayAlert("Sucesso", "Todas as ferramentas estão presentes!", "Concluir");
                        }
                        else
                        {
                            string Faltantes = string.Empty;
                            foreach (var ferramentaFaltante in ferramentasCaixa)
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