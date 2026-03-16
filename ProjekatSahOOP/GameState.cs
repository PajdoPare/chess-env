using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatSahOOP
{
    public enum Status {Mat, Pat, Sah, Normal};
    public class GameState
    {
        public Status St { get; set; }
        public bool CijiPotez {  get; set; }
        public Kvadrat? EnPassantKV { get; set; } = null;
        public bool BeliRokadaDozvoljenaDesno {  get; set; }
        public bool CrniRokadaDozvoljenaDesno {  get; set; }
        public bool BeliRokadaDozvoljenaLevo {  get; set; }
        public bool CrniRokadaDozvoljenaLevo {  get; set; }
        List<Kvadrat> LegalMoves(Board board, Kvadrat K)
        {
            List<Kvadrat> legal = new List<Kvadrat> ();
            Piece p = board.GetPiece(K);
            if (p == null || p.beli != CijiPotez)return legal;
            p.RacunajPoteze(board, K, EnPassantKV);
            foreach(Kvadrat x in p.Potezi)
            {
                Potez Move = new Potez(K, x);
                if (p.T == Tip.Kralj && DaLiJeRokada(K, x) && )
                Board b = board.Clone();
                
            }
            return legal;

        }
        bool DaLiJeRokada(Kvadrat P, Kvadrat O)
        {
            return Math.Abs(P.Col - O.Col) > 1;
        }
        bool DozvoljenaRokada(Kvadrat P, Kvadrat O)
        {
            bool desno = (P.Col == 6);
            return (CijiPotez && desno) switch
            {
                ()
            }
            
        }
    }
}
