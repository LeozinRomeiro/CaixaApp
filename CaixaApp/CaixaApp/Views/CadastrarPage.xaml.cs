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

        public CadastrarPage(Ferramenta ferramenta)
        {
            InitializeComponent();
            Selecionado = "Ferramenta";
            TipoCadastroPicker.SelectedItem = Selecionado;
            Camp1Entry.Text = ferramenta.Codigo.ToString();
			Camp2Entry.Text = ferramenta.Nome;
            Camp3Entry.Text = ferramenta.Tipo;
            Camp4Entry.Text = ferramenta.Quantidade.ToString();
            Camp5Entry.Text = ferramenta.Id.ToString();
            Camp6Entry.Text = ferramenta.IdCaixa.ToString();
            PreencherCamps();
			buttonSalvar.Text = "Atualizar";
            buttonExcluir.IsVisible = true;
        }

        public CadastrarPage(Colaborador colaborador)
        {
            InitializeComponent();
            TipoCadastroPicker.SelectedItem = "Colaborador";
            Camp1Entry.IsVisible = false;
            Camp2Entry.Text = colaborador.Nome;
            Camp3Entry.Text = colaborador.Setor;
            Camp4Entry.Text = colaborador.Cargo;
            Camp5Entry.Text = colaborador.Id.ToString();
            Camp6Entry.Text = colaborador.IdCaixa.ToString();
            PreencherCamps();
			buttonSalvar.Text = "Atualizar";
            buttonExcluir.IsVisible = true;
        }

        public async void EscolherSelecionado()
        {
            Selecionado = await DisplayActionSheet("Escolhar o cadastrado:", "Cancelar", null, "Ferramenta", "Colaborador");
            TipoCadastroPicker.SelectedItem = Selecionado;
            PreencherCamps();
        }

        public void PreencherCamps()
        {
            Selecionado = TipoCadastroPicker.SelectedItem.ToString();
			Camp1Entry.IsVisible = true;
			Camp2Entry.IsVisible = true;
			Camp3Entry.IsVisible = true;
			Camp4Entry.IsVisible = true;
			Camp5Entry.IsVisible = true;
			Camp6Entry.IsVisible = true;
			switch (Selecionado)
            {
                case "Ferramenta":
                    Camp1Entry.Text = "Aperte no leitor para ler o cidgo";
                    Camp2Entry.Placeholder = "Nome";
                    Camp3Entry.Placeholder = "Tipo";
                    Camp4Entry.Placeholder = "Quantidade";
					Camp5Entry.IsVisible = false;
					Camp6Entry.IsVisible = false;
					break;
                case "Colaborador":
                    Camp1Entry.IsVisible = false;
                    frameCodigo.IsVisible = false;
                    Camp2Entry.Placeholder = "Nome";
                    Camp3Entry.Placeholder = "Setor";
                    Camp4Entry.Placeholder = "Cargo";
                    Camp5Entry.IsVisible = false;
                    Camp6Entry.IsVisible = false;
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
                    case "Ferramenta":
                        Ferramenta ferramenta = new Ferramenta();
                        ferramenta.Codigo = Camp1Entry.Text;
                        ferramenta.Nome = Camp2Entry.Text;
                        ferramenta.Tipo = Camp3Entry.Text;
                        ferramenta.Quantidade = int.Parse(Camp4Entry.Text);
                        if (buttonSalvar.Text=="Atualizar")
                        {
                            context.Atualizar(ferramenta);
                            await DisplayAlert("Resultado", Camp2Entry.Text + " atualizado com sucesso!", "OK");
                            break;
                        }
						context.Inserir(ferramenta);
                        await DisplayAlert("Resultado", Camp2Entry.Text + " inserido com sucesso!", "OK");
                        break;
                    case "Colaborador":
                        Colaborador colaborador = new Colaborador();
                        colaborador.Nome = Camp2Entry.Text;
                        colaborador.Setor = Camp3Entry.Text;
                        colaborador.Cargo = Camp4Entry.Text;
                        if (buttonSalvar.Text == "Atualizar")
                        {
                            context.Atualizar(colaborador);
                            await DisplayAlert("Resultado", Camp2Entry.Text + " atualizado com sucesso!", "OK");
                            break;
                        }
                        context.Inserir(colaborador);
                        await DisplayAlert("Resultado", Camp2Entry.Text + " inserido com sucesso!", "OK");
                        break;
                    default:
                        await DisplayAlert("Erro", "Por favor selecione um tipo de cadastro!", "OK");
                        break;
                }

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
            var resp = await DisplayAlert("Excluir registro", "Deseja excluir o cadastro atual?", "Sim", "Não");
            if (resp == true)
            {
                Context context = new Context(App.Path);
                if (Selecionado== "Colaborador")
                {
                    int id = Convert.ToInt32(Camp5Entry.Text);
                    Colaborador colaborador = context.LocalizarColaborador(id);
                    context.Excluir(colaborador);
                }
                else
                {
                    int id = Convert.ToInt32(Camp5Entry.Text);
                    Ferramenta colaborador = context.LocalizarFerramenta(id);
                    context.Excluir(colaborador);
                }
                await DisplayAlert("Sucesso", "Cadastro excluído com sucesso", "OK");
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