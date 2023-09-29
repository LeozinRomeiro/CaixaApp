using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CaixaApp.Model
{
    internal class ColaboradorList
    {
        [NotNull] public string Nome { get; set; } = string.Empty;
        [NotNull] public string Setor { get; set; } = string.Empty;
        [NotNull] public string Cargo { get; set; } = string.Empty;
    }
}
