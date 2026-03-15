using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatSahOOP
{
    
  
    public class Board
    {
        Piece[,] board = new Piece[8,8];
        public Piece GetPiece(int R, int C) => board[R,C];
        public Piece GetPiece(Kvadrat K) => board[K.Row,K.Col];
        public void SetPiece(int R, int C, Piece piece) => board[R,C] = piece;
        public void SetPiece(Kvadrat K, Piece piece) => board[K.Row, K.Col] = piece;
        public bool Unutar(int R, int C)
        {
            return R >= 0 && C >= 0 && R < 8 && C < 8;
        }
        public Kvadrat GdeKralj(bool beli)
        {
            for(int i = 0; i < 8; i++)
            {
                for(int j = 0; j < 8; j++)
                {
                    if (board[i,j] is Kralj k && k.beli == beli)return new Kvadrat(i, j);
                }
            }
            throw new Exception("NEMA KRALJA!!!");
        }
    }
}
