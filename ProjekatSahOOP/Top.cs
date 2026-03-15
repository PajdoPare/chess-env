using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatSahOOP
{
    internal class Top : Piece
    {
        bool pomeren;
        public Top(bool beli): base (beli)
        {
            pomeren = false;
        }
        public override void RacunajPoteze(Board board, Kvadrat k)
        {
            Potezi.Clear();
            int[] dr = { 1, 0, -1, 0 };
            int[] dc = { 0, 1, 0, -1 };
            for (int i = 0; i < 4; i++)
            {
                int r = k.Row + dr[i];
                int c = k.Col + dc[i];

                while (board.Unutar(r, c))
                {
                    Piece ovde = board.GetPiece(r, c);
                    if (ovde == null) this.Potezi.Add(new Kvadrat(r, c));
                    else if (ovde.beli != this.beli) { this.Potezi.Add(new Kvadrat(r, c)); break; }
                    r += dr[i];
                    c += dc[i];

                }

            }
        }
    }
}
