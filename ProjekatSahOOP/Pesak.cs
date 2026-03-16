using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Activation;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatSahOOP
{
    internal class Pesak : Piece
    {
        
        public Pesak(bool beli) : base(beli)
        {
            pomeren = false;
            this.T = Tip.Pesak;
        }
        public void KogaNapadam(Board board, Kvadrat k, Kvadrat? EnPassantKV = null)
        {
            Potezi.Clear();
            int smer = beli ? -1 : 1;
            int jedanKorak = k.Row + smer;
            int[] dc = {k.Col -1,k.Col+ 1 };
            for (int i = 0; i < 2; i++)
            {
                if (board.Unutar(jedanKorak, dc[i])) Potezi.Add(new Kvadrat(jedanKorak, dc[i]));
            }
        }

        public override void RacunajPoteze(Board board, Kvadrat k, Kvadrat? EnPassantKV = null)
        {
            Potezi.Clear();
            int smer = beli ? -1 : 1;
            int jedanKorak = k.Row + smer;
            if(board.Unutar(jedanKorak, k.Col) && board.GetPiece(jedanKorak, k.Col) == null)
            {
                Potezi.Add(new Kvadrat(jedanKorak, k.Col));
                int Start = beli ? 6 : 1;
                if (k.Row == Start)
                {
                    int dvaKoraka = k.Row + 2 * smer;
                    if (board.Unutar(dvaKoraka, k.Col) && board.GetPiece(dvaKoraka, k.Col) == null)
                        Potezi.Add(new Kvadrat(dvaKoraka, k.Col));
                }
            }
            int[] dc = { -1, 1 };
            for (int i = 0; i < 2; i++)
            {
                int c = k.Col + dc[i];
                if (board.Unutar(jedanKorak, c) && board.GetPiece(jedanKorak, c) != null && board.GetPiece(jedanKorak, c).beli != beli)
                    Potezi.Add(new Kvadrat(jedanKorak, c));
                if (EnPassantKV.HasValue && EnPassantKV.Value == new Kvadrat(jedanKorak, c))Potezi.Add(new Kvadrat(jedanKorak, c));
            }
            
        }
    }
}
