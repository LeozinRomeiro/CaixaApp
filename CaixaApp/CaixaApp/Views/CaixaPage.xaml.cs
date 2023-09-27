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
        static Ferramenta Caixa;
        static List<Ferramenta> Ferramentas;
        public CaixaPage()
        {
            InitializeComponent();
        }
        private async void OnAddStackLayoutClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LeitorPage(this));
            codigo.SetBinding(Entry.TextProperty, new Binding("Codigo"));
        }
        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LeitorPage(this));
            codigo.SetBinding(Entry.TextProperty, new Binding("Codigo"));
            Caixa = context.LocalizarFerramenta(codigo.Text);
        }
        public void AddStackLayoutClicked()
        {
            Ferramenta ferramenta = new Ferramenta();
            string _codigo = codigo.ToString();
            ferramenta = context.LocalizarFerramentaCodigo(codigo.ToString());
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
            ((StackLayout)Content).Children.Add(stackLayoutFerramenta);
            Ferramentas.Add(ferramenta);
        }
    }
}