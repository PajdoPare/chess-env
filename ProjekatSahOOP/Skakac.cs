using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatSahOOP
{
    internal class Skakac : Piece
    {
        public Skakac(bool beli) : base(beli) { this.T = Tip.Skakac; }
        public override void RacunajPoteze(Board board, Kvadrat k, Kvadrat? EnPassantKV = null)
        {
            int[] dr = {-2, 2, -2, 2, -1, 1, -1, 1 };
            int[]dc = {-1, -1, 1, 1, -2, -2, 2, 2 };
            for(int i = 0; i < 8; i++)
            {
                int r = k.Row + dr[i];
                int c = k.Col + dc[i];
                if(board.Unutar(r, c) && (board.GetPiece(r, c) == null || board.GetPiece(r, c).beli != beli))
                    Potezi.Add(new Kvadrat(r, c));
            }
        }
    }
}
