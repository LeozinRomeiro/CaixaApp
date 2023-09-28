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
        //public List<Ferramenta> LocalizarFerramentasNaCaixa(int id)
        //{
        //    try
        //    {
        //        Caixa caixa = con.Table<Caixa>().FirstOrDefault(x => x.Id == id);
        //        if (caixa != null)
        //        {
        //            return con.Table<Ferramenta>().Where(x => x.IdCaixa == id).ToList();
        //        }
        //        else
        //        {
        //            return new List<Ferramenta>();
        //        }
        //    }
        //    catch (Exception)
        //    {

        ///        throw;
        //    }
        //}

        public Ferramenta LocalizarFerramentaCodigo(string codigo)
        {
            try
            {
                return con.Table<Ferramenta>().FirstOrDefault(x => x.Codigo == codigo);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Ferramenta LocalizarCaixa(int id)
        {
            try
            {
                return con.Table<Ferramenta>().FirstOrDefault(x => x.IdCaixa == id);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public Ferramenta LocalizarFerramenta(int id)
        {
            Ferramenta ferramenta= new Ferramenta();
            try
            {
                return con.Table<Ferramenta>().FirstOrDefault(x => x.Id == id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Ferramenta LocalizarFerramenta(string codigo)
        {
            Ferramenta ferramenta = new Ferramenta();
            try
            {
                return con.Table<Ferramenta>().FirstOrDefault(x => x.Codigo == codigo);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public Colaborador LocalizarColaborador(int id)
        {
            Colaborador ferramenta = new Colaborador ();
            try
            {
                return con.Table<Colaborador >().FirstOrDefault(x => x.Id == id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Colaborador LocalizarColaboradorCaixa(int id)
        {
            Colaborador ferramenta = new Colaborador();
            try
            {
                return con.Table<Colaborador>().FirstOrDefault(x => x.IdCaixa == id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Colaborador> LocalizarColaboradores(string nome)
        {
            List<Colaborador> lista = new List<Colaborador>();
            try
            {
                if (nome != null)
                {
                    lista = (con.Table<Colaborador>().Where(p => p.Nome.ToLower().Contains(nome.ToLower()))).ToList();
                }
                else
                {
                    lista = (from p in con.Table<Colaborador>() select p).ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return lista;
        }
        public List<Ferramenta> LocalizarFerramentas(string nome)
        {
            List<Ferramenta> lista = new List<Ferramenta>();
            try
            {
                if (nome != null)
                {
                    lista = (con.Table<Ferramenta>().Where(p => p.Nome.ToLower().Contains(nome.ToLower()))).ToList();
                }
                else
                {
                    lista = (from p in con.Table<Ferramenta>() select p).ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return lista;
        }
        public List<Caixa> LocalizarCaixa(string nome)
        {
            List<Caixa> lista = new List<Caixa>();
            try
            {
                if (nome != null)
                {
                    List<Colaborador> listaColaboradoes = LocalizarColaboradores(nome);
                    foreach (var colaborador in listaColaboradoes)
                    {
                        lista.Add(con.Table<Caixa>().First(p => p.IdColaborador == colaborador.Id));
                    }
                }
                else
                {
                    lista = (from p in con.Table<Caixa>() select p).ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return lista;
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
