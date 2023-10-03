using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace CaixaApp.Model
{
    public class Ferramenta
    {
        [PrimaryKey, AutoIncrement] public int Id { get; set; }
        [NotNull] public string Codigo { get; set; }
        [NotNull] public int IdCaixa { get; set; }
        [NotNull] public string Tipo { get; set; }
        [NotNull] public string Nome { get; set; }
        [NotNull] public int Quantidade { get; set; }
    }
}
