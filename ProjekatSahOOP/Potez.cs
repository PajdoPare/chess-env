using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatSahOOP
{
    public class Potez
    {
        public Kvadrat Polazno { get; set; }
        public Kvadrat Odredisno { get; set; }
        public Piece Promocija { get; set; }
        public bool Promo { get; set; }
        public bool Rokada { get; set; } 
        public bool EnPassant { get; set; }
        public Kvadrat EnPassantKV { get; set; } 
        public Kvadrat TopPKV { get; set; } 
        public Kvadrat TopOKV { get; set; } 
        public Potez(Kvadrat a, Kvadrat b)
        {
            Polazno = a;
            Odredisno = b;
        }
    }
}
