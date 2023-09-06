using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace CaixaApp.Model
{
    internal class Ferramenta
    {
        [PrimaryKey, AutoIncrement] public int Id { get; set; } = 0;
        [NotNull] public string Tipo { get; set; }
        [NotNull] public string Nome { get; set; }
        [NotNull] public int Quantidade { get; set; }
        [NotNull] public int Codigo { get; set; }
    }
}
