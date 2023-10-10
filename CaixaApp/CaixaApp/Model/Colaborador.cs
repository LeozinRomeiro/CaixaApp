using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CaixaApp.Model
{
    public class Colaborador
    {
        [PrimaryKey, AutoIncrement] public int Id { get; set; }
        public int IdCaixa { get; set; } = 0;
        [NotNull] public string Nome { get; set; } = string.Empty;
        [NotNull] public string Setor { get; set; } = string.Empty;
        [NotNull] public string Cargo { get; set; } = string.Empty;
    }
}
