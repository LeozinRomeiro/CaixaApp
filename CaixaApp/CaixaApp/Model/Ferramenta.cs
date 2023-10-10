using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace CaixaApp.Model
{
    public class Ferramenta
    {
        [NotNull] public string Codigo { get; set; }
        [NotNull] public string Nome { get; set; }
        [NotNull] public string Tipo { get; set; }
        [NotNull] public int Quantidade { get; set; }
        [PrimaryKey, AutoIncrement] public int Id { get; set; }
        public int IdCaixa { get; set; } = 0;
    }
}
