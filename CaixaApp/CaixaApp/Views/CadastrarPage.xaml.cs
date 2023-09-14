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
        public CadastrarPage()
        {
            InitializeComponent();
        }

        private async void buttontCancelar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }

        private async void buttonSalvar_Clicked(object sender, EventArgs e)
        {
            try
            {
                Colaborador colaborador = new Colaborador();
                colaborador.Nome = NomeEntry.Text;
                colaborador.Setor = SetorEntry.Text;
                colaborador.Cargo = CargoEntry.Text;
                Context context = new Context(App.Path);
                if (buttonSalvar.Text == "Inserir")
                {
                    context.Inserir(colaborador);
                    await DisplayAlert("Resultado", NomeEntry.Text + " inserido com sucesso!", "OK");
                }
                else
                {
                    colaborador.Id = Convert.ToInt32(IdEntry.Text);
                    context.Inserir(colaborador);
                    await DisplayAlert("Resultado", NomeEntry.Text + " alterado com sucesso!", "OK");
                }
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
                int id = Convert.ToInt32(IdEntry.Text);
                context.Excluir(id);
                await DisplayAlert("Sucesso", "Nota excluída com sucesso", "OK");
                await Navigation.PushAsync(new MainPage());
            }
        }
    }

}