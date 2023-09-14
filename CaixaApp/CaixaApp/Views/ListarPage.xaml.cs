using CaixaApp.Data;
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
        public ListarPage()
        {
            InitializeComponent();
        }

        public void PreencherTela()
        {
            string textoBuscado = texteBuscado.Text;
            if (int.TryParse(textoBuscado, out int NumerBuscado))
            {
                Context context = new Context(App.Path);
                ListaFerramenta.ItemsSource = context.LocalizarFerramentasNaCaixa(NumerBuscado);
            }
            
        }

        private void buttonLocalizar_Clicked(object sender, EventArgs e)
        {

        }

        private void ListaFerramenta_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }
    }
}