using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatSahOOP
{
    public class Board
    {
        Piece[,] board = new Piece[8, 8];
        public Piece GetPiece(int R, int C) => board[R, C];
        public Piece GetPiece(Kvadrat K) => board[K.Row, K.Col];
        public void SetPiece(int R, int C, Piece piece) => board[R, C] = piece;
        public void SetPiece(Kvadrat K, Piece piece) => board[K.Row, K.Col] = piece;
        public bool Unutar(int R, int C)
        {
            return R >= 0 && C >= 0 && R < 8 && C < 8;
        }
        public void NapraviPotez(Potez x)
        {
            Piece p = GetPiece(x.Polazno);
            if (p == null) throw new Exception("NEMA FIGURE NA POLAZNOJ POZICIJI!!!");
            SetPiece(x.Odredisno, p);
            SetPiece(x.Polazno, null);
            if (x.EnPassant) SetPiece(x.EnPassantKV, null);
            if(x.Rokada)
            {
                SetPiece(x.TopOKV, GetPiece(x.TopPKV));
                SetPiece(x.TopPKV, null);
                if (x.Promocija != null) SetPiece(x.Odredisno, x.Promocija);
            }
        }
        public Kvadrat GdeKralj(bool beli)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (board[i, j] is Kralj k && k.beli == beli) return new Kvadrat(i, j);
                }
            }
            throw new Exception("NEMA KRALJA!!!");
        }
        public bool Napadnut(Kvadrat K, bool beli)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Piece p = board[i, j];
                    if (p == null || p.beli != beli) continue;
                    if(p is Pesak)
                    {
                        Pesak pesak = (Pesak)p;
                        pesak.KogaNapadam(this, new Kvadrat(i, j));
                        if (pesak.Potezi.Contains(K)) return true;
                        continue;
                    }
                    p.RacunajPoteze(this, new Kvadrat(i, j), null, true);
                    if (p.Potezi.Contains(K)) return true;
                }
            }
            return false;
        }
        public bool SAH(bool beli)
        {
            Kvadrat kralj = GdeKralj(beli);
            return Napadnut(kralj, !beli);
        }
        public void Pocetno()
        {
            bool t = false;
            SetPiece(0, 0, new Top(t));
            SetPiece(0, 1, new Skakac(t));
            SetPiece(0, 2, new Lovac(t));
            SetPiece(0, 3, new Kraljica(t));
            SetPiece(0, 4, new Kralj(t));
            SetPiece(0, 5, new Lovac(t));
            SetPiece(0, 6, new Skakac(t));
            SetPiece(0, 7, new Top(t));

            SetPiece(7, 0, new Top(!t));
            SetPiece(7, 1, new Skakac(!t));
            SetPiece(7, 2, new Lovac(!t));
            SetPiece(7, 3, new Kraljica(!t));
            SetPiece(7, 4, new Kralj(!t));
            SetPiece(7, 5, new Lovac(!t));
            SetPiece(7, 6, new Skakac(!t));
            SetPiece(7, 7, new Top(!t));
            for (int i = 0; i <= 7; i++)
            {
                SetPiece(1, i, new Pesak(t));
                SetPiece(6, i, new Pesak(!t));
            }
        }
        public Board Clone()
        {
            Board clone = new Board();
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    clone.board[i, j] = board[i, j];
                }
            }
            return clone;
        }
    } 
}
