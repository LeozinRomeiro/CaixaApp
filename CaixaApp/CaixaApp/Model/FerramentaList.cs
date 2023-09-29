using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace CaixaApp.Model
{
    public class FerramentaList
    {
        [NotNull] public string Tipo { get; set; }
        [NotNull] public string Nome { get; set; }
        [NotNull] public int Quantidade { get; set; }
    }
}
