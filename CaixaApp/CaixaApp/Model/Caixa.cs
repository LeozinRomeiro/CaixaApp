using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CaixaApp.Model
{
    internal class Caixa
    {
        [PrimaryKey, AutoIncrement] public int Id { get; set; } = 0;
        [NotNull] public int IdColaborador { get; set; } = 0;
        [NotNull] public string Codigo { get; set; }
    }
}
