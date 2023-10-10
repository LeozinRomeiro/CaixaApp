using CaixaApp.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
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
        public void Excluir<Entidade>(Entidade entidade)
        {
            try
            {
                con.Delete(entidade);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void Atualizar<Entidade>(Entidade entidade)
        {
            try
            {
                con.Update(entidade);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void DeleteDataBase()
        {
            try
            {
                con.Close();
                File.Delete("dbCaixaApp.db3");
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<Ferramenta> LocalizarFerramentasNaCaixa(Ferramenta caixa)
        {
            try
            {
                if (caixa != null)
                {
                    return con.Table<Ferramenta>().Where(x => x.IdCaixa == caixa.Id).ToList();
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

        //public List<ColaboradorList> LocalizarColaboradores(string nome)
        //{
        //    List<Colaborador> lista = new List<Colaborador>();
        //    List<ColaboradorList> lista_= new List<ColaboradorList>();
        //    try
        //    {
        //        if (nome != null)
        //        {
        //            lista = (con.Table<Colaborador>().Where(p => p.Nome.ToLower().Contains(nome.ToLower()))).ToList();
        //            foreach (var colaborador in lista)
        //            {
        //                ColaboradorList colaboradorList = new ColaboradorList
        //                {
        //                    Nome = colaborador.Nome,
        //                    Setor = colaborador.Setor,
        //                    Cargo = colaborador.Cargo,
        //                };
        //            }
        //        }
        //        else
        //        {
        //            lista = (from p in con.Table<Colaborador>() select p).ToList();
        //            foreach (var colaborador in lista)
        //            {
        //                ColaboradorList colaboradorList = new ColaboradorList
        //                {
        //                    Nome = colaborador.Nome,
        //                    Setor = colaborador.Setor,
        //                    Cargo = colaborador.Cargo,
        //                };
        //                lista_.Add(colaboradorList);
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //    return lista_;
        //}
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
        //public List<FerramentaList> LocalizarFerramentas(string nome)
        //{
        //    List<Ferramenta> lista = new List<Ferramenta>();
        //    List<FerramentaList> ferramentasListadas = new List<FerramentaList>();
        //    try
        //    {
        //        if (nome != null)
        //        {
        //            lista = (con.Table<Ferramenta>().Where(p => p.Nome.ToLower().Contains(nome.ToLower()))).ToList();
        //            foreach (var ferramenta in lista)
        //            {
        //                FerramentaList ferramentaList = new FerramentaList
        //                {
        //                    Nome = ferramenta.Nome,
        //                    Tipo = ferramenta.Tipo,
        //                    Quantidade = ferramenta.Quantidade,
        //                };
        //                ferramentasListadas.Add(ferramentaList);
        //            }
        //        }
        //        else
        //        {
        //            lista = (from p in con.Table<Ferramenta>() select p).ToList();
        //            foreach (var ferramenta in lista)
        //            {
        //                FerramentaList ferramentaList = new FerramentaList
        //                {
        //                    Nome = ferramenta.Nome,
        //                    Tipo = ferramenta.Tipo,
        //                    Quantidade = ferramenta.Quantidade,
        //                };
        //                ferramentasListadas.Add(ferramentaList);
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //    return ferramentasListadas;
        //}

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
