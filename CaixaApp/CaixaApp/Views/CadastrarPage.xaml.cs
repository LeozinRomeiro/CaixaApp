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
            Selecionado = await DisplayActionSheet("Escolhar o cadastrado:", "Cancelar", null, "Caixa", "Ferramenta", "");
            TipoCadastroPicker.SelectedItem = Selecionado;
            PreencherCamps();
        }

        public void PreencherCamps()
        {
            Selecionado = TipoCadastroPicker.SelectedItem.ToString();
            switch (Selecionado)
            {
                case "Caixa":
                    Camp1Entry.Placeholder = "Id";
                    Camp2Entry.Placeholder = "IdColaborador";
                    Camp3Entry.Placeholder = "Codigo";
                    break;
                case "Ferramenta":
                    Camp1Entry.Placeholder = "Id";
                    Camp3Entry.Placeholder = "IdCaixa";
                    Camp4Entry.Placeholder = "Tipo";
                    Camp5Entry.Placeholder = "Nome";
                    Camp6Entry.Placeholder = "Quantidade";
                    Camp2Entry.Placeholder = "Codigo";
                    break;
                case "Colaborador":
                    // Lógica de cadastro de colaborador
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
                        ferramenta.Id = int.Parse(Camp1Entry.Text);
                        ferramenta.Codigo = Camp2Entry.Text;
                        ferramenta.Tipo = Camp3Entry.Text;
                        ferramenta.Nome = Camp4Entry.Text;
                        ferramenta.Quantidade = int.Parse(Camp5Entry.Text);
                        context.Inserir(ferramenta);
                        break;
                    case "Colaborador":
                        // Lógica de cadastro de colaborador
                        break;
                    default:
                        // Trate qualquer seleção inválida aqui
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
            await Navigation.PushAsync(new LeitorPage(this));
        }
    }

}