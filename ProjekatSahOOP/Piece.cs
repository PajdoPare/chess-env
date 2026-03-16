using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatSahOOP
{
    public enum Tip { Pesak, Top, Lovac, Skakac, Kraljica, Kralj };
    public abstract class Piece
    {
        public bool alive, pomeren;
        public Tip T { get; set; }
        public bool beli;
        public Image Image;
        public List<Kvadrat> Potezi;
        public abstract void RacunajPoteze(Board board, Kvadrat k, Kvadrat? EnPassantKV);
        public Piece(bool beli)
        {
            this.alive = true;
            this.beli = beli;
            Potezi = new List<Kvadrat>();
        }
        public void Pojeden()
        {
            alive = false;
        }

    }
}
