using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatSahOOP
{
    internal class Kralj : Piece
    {
        bool pomeren;
        public Kralj(bool beli) : base(beli)
        {
            pomeren = false;
            this.T = Tip.Kralj;
        }
        public override void RacunajPoteze(Board board, Kvadrat k, Kvadrat? EnPassantKV = null)
        {
            int[] dr = { -1, -1, -1, 0, 0, 1, 1, 1 };
            int[] dc = { -1, 0, 1, -1, 1, -1, 0, 1 };
            Potezi.Clear();
            for (int i = 0; i < 8; i++)
            {
                int r = k.Row + dr[i];
                int c = k.Col + dc[i];
                if (board.Unutar(r, c) && (board.GetPiece(r, c) == null || board.GetPiece(r, c).beli != beli))
                    Potezi.Add(new Kvadrat(r, c));
                if(!pomeren && !board.SAH(beli))
                {
                    Rokada(board, k, true);
                    Rokada(board, k, false);
                }
            }
        }
        void Rokada(Board board, Kvadrat odakle, bool desno)
        {
            int topCol = desno ? 7 : 0;
            int OCol = desno ? 6 : 2;
            int[] OpasneKol = desno ? new[] { 5, 6 } : new[] { 3, 2 };  
            Piece top = board.GetPiece(odakle.Row, topCol);
            if(top == null || top.T != Tip.Top || ((Top)top).pomeren || top.beli != beli)
            {
                return;
            }
            for (int c = Math.Min(odakle.Col, topCol) + 1; c < Math.Max(odakle.Col, topCol); c++)
            {
                if (board.GetPiece(odakle.Row, c) != null)
                    return;
            }
            foreach(int o in OpasneKol)
            {
                if(board.Napadnut( new Kvadrat(odakle.Row, o), !beli))
                {
                    return;
                }
            }
            Potezi.Add(new Kvadrat(odakle.Row, OCol));
        }
    }
}
