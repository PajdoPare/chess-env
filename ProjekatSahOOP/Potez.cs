using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatSahOOP
{
    internal class Potez
    {
        public Kvadrat Polazno { get; set; }
        public Kvadrat Odredisno { get; set; }
        public Piece Promocija { get; set; } // null ako nije promocija
        public bool Rokada { get; set; } 
        public bool EnPassant { get; set; }
        public Kvadrat EnPassantKV { get; set; } 
        public Potez(Kvadrat a, Kvadrat b)
        {
            Polazno = a;
            Odredisno = b;
        }
    }
}
