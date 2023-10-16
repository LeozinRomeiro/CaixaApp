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
    public partial class ListarPage : ContentPage
    {
        static string Selecionado;
        Context context = new Context(App.Path);
        int teste;

		public ListarPage()
        {
            InitializeComponent();
            EscolherSelecionado();
            stackLayoutTipoCadastro.IsVisible = true;
        }
		private Action<Colaborador> _definirColaborador;
		public ListarPage(Action<Colaborador> DefinirColaborador)
		{
			InitializeComponent();
            Selecionado = "Colaborador";
            TipoCadastroPicker.SelectedItem = Selecionado;
            teste = 1;
            PreencherTela();
            _definirColaborador = DefinirColaborador;
			stackLayoutTipoCadastro.IsVisible = false;
		}
		public async void EscolherSelecionado()
        {
            Selecionado = await DisplayActionSheet("Escolhar o cadastrado:", "Cancelar", null, "Ferramenta", "Colaborador");
            TipoCadastroPicker.SelectedItem = Selecionado;
        }

        public void PreencherTela()
        {
            try
            {
                string textoBuscado = texteBuscado.Text;
                switch (TipoCadastroPicker.SelectedItem.ToString())
                {
                    //case "Ferramenta":

                    //    List<Ferramenta> ferramentasAnalise= context.LocalizarFerramentas(textoBuscado);

                    //    foreach (var ferramenta in ferramentasAnalise)
                    //    {
                    //        InformacoesListadas informacoes = new InformacoesListadas();
                    //        informacoes.Texto = $"{ferramenta.Nome} {ferramenta.Tipo} {ferramenta.Quantidade} "; // Adicione todos os atributos que deseja exibir
                    //        informacoesListadas.Add(informacoes);
                    //    }
                    //    break;
                    //case "Colaborador":

                    //    List<Colaborador> colaboradors = context.LocalizarColaboradores(textoBuscado);

                    //    foreach (var colaborador in colaboradors)
                    //    {
                    //        InformacoesListadas informacoes = new InformacoesListadas();
                    //        informacoes.Texto = $"{colaborador.Nome} {colaborador.Setor} {colaborador.Cargo} "; // Adicione todos os atributos que deseja exibir
                    //        informacoesListadas.Add(informacoes);
                    //    }
                    //    break;
                    case "Ferramenta":

                        List<Ferramenta> ferramentas= context.LocalizarFerramentas(textoBuscado);
                        ListaFerramenta.IsVisible = true;
                        ListaColaborador.IsVisible = false;
                        ListaFerramenta.ItemsSource= ferramentas;
                        //foreach (var ferramenta in ferramentasAnalise)
                        //{
                        //    InformacoesListadas informacoes = new InformacoesListadas();
                        //    informacoes.Texto = $"{ferramenta.Nome} {ferramenta.Tipo} {ferramenta.Quantidade} "; // Adicione todos os atributos que deseja exibir
                        //    informacoesListadas.Add(informacoes);
                        //}
                        break;
                    case "Colaborador":

                        List<Colaborador> colaboradors = context.LocalizarColaboradores(textoBuscado);
                        ListaColaborador.IsVisible = true;
                        ListaFerramenta.IsVisible = false;
                        ListaColaborador.ItemsSource = colaboradors;
                        //List<Colaborador> colaboradors = context.LocalizarColaboradores(textoBuscado);

                        //foreach (var colaborador in colaboradors)
                        //{
                        //    InformacoesListadas informacoes = new InformacoesListadas();
                        //    informacoes.Texto = $"{colaborador.Nome} {colaborador.Setor} {colaborador.Cargo} "; // Adicione todos os atributos que deseja exibir
                        //    informacoesListadas.Add(informacoes);
                        //}
                        break;
                    default:
                        // Trate qualquer seleção inválida aqui
                        break;
                }
            }
            catch (Exception)
            {
                throw ;
            }
            //if (int.TryParse(textoBuscado, out int NumerBuscado))
            //{
            //}
        }

        private void buttonLocalizar_Clicked(object sender, EventArgs e)
        {
            PreencherTela();
        }

        private void ListaFerramenta_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                if (e.SelectedItem == null)
                    return;
                if (e.SelectedItem is Ferramenta ferramenta)
                {
                    Navigation.PushAsync(new CadastrarPage(ferramenta));
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void ListaColaborador_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                if (e.SelectedItem == null)
                    return;
                if (e.SelectedItem is Colaborador colaborador)
                {
                    if (teste==1)
                    {
                        _definirColaborador.Invoke(colaborador);
					    Navigation.PopAsync();
                    }
                    else
                    {
                        Navigation.PushAsync(new CadastrarPage(colaborador));
                    }
				}
			}
            catch (Exception)
            {

                throw;
            }
        }
        private void TipoCadastroPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            PreencherTela();
        }
    }
}