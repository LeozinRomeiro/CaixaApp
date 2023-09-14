using CaixaApp.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace CaixaApp.Data
{
    internal class Context
    {
        SQLiteConnection con;
        public Context(string path)
        {
            con = new SQLiteConnection(path);
            con.CreateTable<Ferramenta>();
            con.CreateTable<Colaborador>();
            con.CreateTable<Caixa>();
        }
        public void Inserir<Entidade>(Entidade entidade)
        {
            try
            {
                con.Insert(entidade);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void Excluir(int id)
        {
            try
            {
                con.Table<Caixa>().Delete(e => e.Id == id);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<Ferramenta> LocalizarFerramentasNaCaixa(int id)
        {
            try
            {
                Caixa caixa = con.Table<Caixa>().FirstOrDefault(x => x.IdFerramenta == id);
                if (caixa!=null)
                {
                    return con.Table<Ferramenta>().Where(x=>x.Id==caixa.IdFerramenta).ToList();
                }
                else
                {
                    return new List<Ferramenta>();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public Colaborador LocalizarColaborador(int id)
        {
            try
            {
                return con.Table<Colaborador>().FirstOrDefault(x => x.Id == id);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public Caixa LocalizarCaixa(int id)
        {
            try
            {
                return con.Table<Caixa>().FirstOrDefault(x => x.Id == id);
            }
            catch (Exception)
            {

                throw;
            }
        }
        //public List<Ferramenta> EncontrarFerramentas(int id)
        //{
        //    List<Ferramenta> ferramentas = new List<Ferramenta>();
        //    try
        //    {
        //        var resposta = from caixa in con.Table<Caixa>() 
        //                       join ferramenta in con.Table<Ferramenta>()
        //                       on caixa.IdFerramenta equals ferramenta.Id
        //                       where caixa.Id == id select p;
        //        ferramentas = resposta.ToList();
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    return ferramentas;
        //}
    }
}
