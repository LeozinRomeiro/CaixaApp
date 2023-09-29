﻿using CaixaApp.Data;
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
        public ListarPage()
        {
            InitializeComponent();
            EscolherSelecionado();
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

                    //    List<Ferramenta> ferramentas= context.LocalizarFerramentas(textoBuscado);

                    //    foreach (var ferramenta in ferramentas)
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

                        List<FerramentaList> ferramentas= context.LocalizarFerramentas(textoBuscado);
                        ListaFerramenta.ItemsSource= ferramentas;
                        //foreach (var ferramenta in ferramentas)
                        //{
                        //    InformacoesListadas informacoes = new InformacoesListadas();
                        //    informacoes.Texto = $"{ferramenta.Nome} {ferramenta.Tipo} {ferramenta.Quantidade} "; // Adicione todos os atributos que deseja exibir
                        //    informacoesListadas.Add(informacoes);
                        //}
                        break;
                    case "Colaborador":

                        List<ColaboradorList> colaboradors = context.LocalizarColaboradores(textoBuscado);
                        ListaFerramenta.ItemsSource = colaboradors;
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
            catch (Exception e )
            {
                DisplayAlert("Erro",e.Message,"Cancelar");
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
                Ferramenta ferramenta = (Ferramenta)ListaFerramenta.SelectedItem;
                new NavigationPage(new CadastrarPage(ferramenta));
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