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
    public partial class CadastrarPage : ContentPage
    {
        static string Selecionado;
        public CadastrarPage()
        {
            InitializeComponent();
            EscolherSelecionado();
        }

        public async void EscolherSelecionado()
        {
            Selecionado = await DisplayActionSheet("Escolhar o cadastrado:", "Cancelar", null, "Caixa", "Ferramenta", "Colaborador");
            TipoCadastroPicker.SelectedItem = Selecionado;
            PreencherCamps();
        }

        public void PreencherCamps()
        {
            Selecionado = TipoCadastroPicker.SelectedItem.ToString();
            switch (Selecionado)
            {
                case "Caixa":
                    Camp1Entry.Placeholder = "Codigo";
                    Camp2Entry.Placeholder = "Id";
                    Camp3Entry.Placeholder = "IdColaborador";
                    Camp4Entry.Placeholder = "";
                    Camp5Entry.Placeholder = "";
                    Camp6Entry.Placeholder = "";
                    break;
                case "Ferramenta":
                    Camp1Entry.Placeholder = "Codigo";
                    Camp2Entry.Placeholder = "Id";
                    Camp3Entry.Placeholder = "IdCaixa";
                    Camp4Entry.Placeholder = "Tipo";
                    Camp5Entry.Placeholder = "Nome";
                    Camp6Entry.Placeholder = "Quantidade";
                    break;
                case "Colaborador":
                    Camp1Entry.Placeholder = "";
                    Camp2Entry.Placeholder = "Id";
                    Camp3Entry.Placeholder = "Nome";
                    Camp4Entry.Placeholder = "Setor";
                    Camp5Entry.Placeholder = "Cargo";
                    Camp6Entry.Placeholder = "";
                    break;
                default:
                    // Trate qualquer seleção inválida aqui
                    break;
            }
        }

        private async void buttontCancelar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }

        private async void buttonSalvar_Clicked(object sender, EventArgs e)
        {
            try
            {
                Context context = new Context(App.Path);
                Selecionado = TipoCadastroPicker.SelectedItem.ToString();
                switch (Selecionado)
                {
                    case "Caixa":
                        Caixa caixa = new Caixa();
                        caixa.Id = int.Parse(Camp1Entry.Text);
                        caixa.IdColaborador = int.Parse(Camp2Entry.Text);
                        caixa.Codigo = Camp3Entry.Text;
                        context.Inserir(caixa);
                        break;
                    case "Ferramenta":
                        Ferramenta ferramenta = new Ferramenta();
                        ferramenta.Codigo = Camp1Entry.Text;
                        ferramenta.Id = int.Parse(Camp2Entry.Text);
                        ferramenta.IdCaixa = int.Parse(Camp3Entry.Text);
                        ferramenta.Tipo = Camp4Entry.Text;
                        ferramenta.Nome = Camp5Entry.Text;
                        ferramenta.Quantidade = int.Parse(Camp6Entry.Text);
                        context.Inserir(ferramenta);
                        break;
                    case "Colaborador":
                        Colaborador colaborador = new Colaborador();
                        colaborador.Id = int.Parse(Camp2Entry.Text);
                        colaborador.Nome = Camp3Entry.Text;
                        colaborador.Setor = Camp4Entry.Text;
                        colaborador.Cargo = Camp5Entry.Text;
                        context.Inserir(colaborador);
                        break;
                    default:
                        await DisplayAlert("Erro", "Por favor selecione um tipo de cadastro!", "OK");
                        break;
                }
                await DisplayAlert("Resultado", Camp2Entry.Text + " inserido com sucesso!", "OK");

                //if (buttonSalvar.Text == "Inserir")
                //{
                //    context.Inserir(colaborador);
                //       }
                //else
                //{
                //    colaborador.Id = Convert.ToInt32(IdEntry.Text);
                //    context.Inserir(colaborador);
                //    await DisplayAlert("Resultado", NomeEntry.Text + " alterado com sucesso!", "OK");
                //}
                await Navigation.PushAsync(new MainPage());
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", ex.Message, "OK");
            }
        }

        private async void buttonExcluir_Clicked(object sender, EventArgs e)
        {
            var resp = await DisplayAlert("Excluir registro", "Deseja excluir a nota atual?", "Sim", "Não");
            if (resp == true)
            {
                Context context = new Context(App.DataName);
                int id = Convert.ToInt32(Camp1Entry.Text);
                context.Excluir(id);
                await DisplayAlert("Sucesso", "Nota excluída com sucesso", "OK");
                await Navigation.PushAsync(new MainPage());
            }
        }

        private async void Leitor_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LeitorPage(OnCodeScanned));
            Camp1Entry.SetBinding(Entry.TextProperty, new Binding("Codigo"));
        }

        private void OnCodeScanned(string codigo)
        {
            Camp1Entry.Text= codigo.ToString();
        }

        private void TipoCadastroPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            PreencherCamps();
        }
    }

}