using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatSahOOP
{
    internal class Lovac : Piece
    {
        public Lovac(bool beli) : base(beli) { this.T = Tip.Lovac; }
        public override void RacunajPoteze(Board board, Kvadrat k, Kvadrat? EnPassantKV = null, bool TrazimSah = false)
        {
            Potezi.Clear();
            int[] dr = { 1, -1, 1, -1 };
            int[] dc = { 1, -1, -1, 1 };
            Potezi.Clear();
            for (int i = 0; i < 4; i++)
            {
                int r = k.Row + dr[i];
                int c = k.Col + dc[i];

                while (board.Unutar(r, c))
                {
                    Piece ovde = board.GetPiece(r, c);
                    if (ovde == null) this.Potezi.Add(new Kvadrat(r, c));
                    if (ovde != null && ovde.beli == this.beli) break;
                    else if (ovde != null && ovde.beli != this.beli) { this.Potezi.Add(new Kvadrat(r, c)); break; }
                    r += dr[i];
                    c += dc[i];

                }

            }
        }
    }
}
