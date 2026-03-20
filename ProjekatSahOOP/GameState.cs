using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatSahOOP
{
    public enum Status { Mat, Pat, Sah, Normal };
    public class GameState
    {
        public Board Board { get; set; }
        public Status St { get; set; }
        public bool CijiPotez { get; set; }
        public bool Flipped { get; set; } = true;
        public Kvadrat? EnPassantKV { get; set; } = null;
        public bool BeliRokadaDozvoljenaDesno { get; set; } = true;
        public bool CrniRokadaDozvoljenaDesno { get; set; } = true;
        public bool BeliRokadaDozvoljenaLevo { get; set; } = true;
        public bool CrniRokadaDozvoljenaLevo { get; set; } = true;
        public List<Potez> MoveHistory { get; set; } = new List<Potez>();
        public List<Piece> BeliUzeo, CrniUzeo;
        Dictionary<Kvadrat, List<Kvadrat>> LegalniSt = new Dictionary<Kvadrat, List<Kvadrat>>();
        public GameState()
        {
            BeliUzeo = new List<Piece>();
            CrniUzeo = new List<Piece>();
            Board = new Board();
            Board.Pocetno();
            CijiPotez = true;
            St = Status.Normal;
        }
        public int BeliP => Board.BeliVal();
        public int CrniP => Board.CrniVal();
        public event Action<Kvadrat, Kvadrat> PromocijaObavezna;
        bool BiceSah(Kvadrat P, Kvadrat O)
        {
            Piece p = Board.GetPiece(P);
            Piece o = Board.GetPiece(O);
            bool pomeren = p.pomeren;
            Board.SetPiece(O, p);
            Board.SetPiece(P, null);
            p.pomeren = true;
            Kvadrat? EPKV = null;
            Piece EPPesak = null;
            if (p.T == Tip.Pesak && EnPassantKV.HasValue && EnPassantKV.Value == O)
            {
                EPKV = new Kvadrat(P.Row, O.Col);
                EPPesak = Board.GetPiece(EPKV.Value);
                Board.SetPiece(EPKV.Value, null);
            }
            bool sah = Board.SAH(CijiPotez);
            Board.SetPiece(P, p);
            Board.SetPiece(O, o);
            p.pomeren = (p.T == Tip.Kralj || p.T == Tip.Top) ? pomeren : pomeren;
            if (EPKV.HasValue) Board.SetPiece(EPKV.Value, EPPesak);
            return sah;
        }
        public List<Kvadrat> LegalMoves(Kvadrat K)
        {
            if (LegalniSt.TryGetValue(K, out List<Kvadrat> leg)) return leg;
            List<Kvadrat> legal = new List<Kvadrat>();
            Piece p = Board.GetPiece(K);
            if (p == null || p.beli != CijiPotez) return legal;
            p.RacunajPoteze(Board, K, EnPassantKV);
            foreach (Kvadrat x in p.Potezi)
            {
                if (p.T == Tip.Kralj && DaLiJeRokada(K, x) && !DozvoljenaRokada(K, x)) continue;
                if (!BiceSah(K, x)) legal.Add(x);
            }
            LegalniSt[K] = legal;
            return legal;

        }
        void PotegniPotez(Board board, Kvadrat P, Kvadrat O, Kvadrat? EnPassantKV, Piece Promotion = null)
        {
            Piece p = board.GetPiece(P);
            Piece uhvacen = board.GetPiece(O);
            board.SetPiece(O, p);
            board.SetPiece(P, null);
            p.pomeren = true;
            if (p.T == Tip.Kralj && DaLiJeRokada(P, O))
            {
                bool desno = (O.Col == 6);
                int topCol = desno ? 7 : 0;
                int noviTopCol = desno ? 5 : 3;
                Piece top = board.GetPiece(P.Row, topCol);
                board.SetPiece(new Kvadrat(P.Row, noviTopCol), top);
                board.SetPiece(P.Row, topCol, null);
                if (top != null) top.pomeren = true;
            }
            if (p.T == Tip.Pesak && EnPassantKV != null && O.Equals(EnPassantKV.Value))
            {
                int R = P.Row;
                uhvacen = board.GetPiece(new Kvadrat(R, O.Col));
                board.SetPiece(new Kvadrat(R, O.Col), null);
            }
            if (p.T == Tip.Pesak)
            {
                int promoteRow = p.beli ? 0 : 7;
                if (O.Row == promoteRow && Promotion != null) board.SetPiece(O, Promotion);
            }
            if (uhvacen != null && board == Board)
            {
                if (CijiPotez) BeliUzeo.Add(uhvacen);
                else CrniUzeo.Add(uhvacen);
            }
        }
        Potez Sklopi(Kvadrat P, Kvadrat O, Piece Promotion = null)
        {
            Piece p = Board.GetPiece(P);
            Potez m = new Potez(P, O);
            if (p.T == Tip.Kralj && DaLiJeRokada(P, O))
            {
                m.Rokada = true;
                bool desno = (O.Col == 6);
                int TopCol = desno ? 7 : 0;
                int noviTopCol = desno ? 5 : 3;
                m.TopPKV = new Kvadrat(P.Row, TopCol);
                m.TopOKV = new Kvadrat(P.Row, noviTopCol);
            }
            if (p.T == Tip.Pesak && EnPassantKV != null && O.Equals(EnPassantKV.Value))
            {
                m.EnPassant = true;
                m.EnPassantKV = new Kvadrat(P.Row, O.Col);
            }
            int PromoteRow = p.beli ? 0 : 7;
            if (p.T == Tip.Pesak && O.Row == PromoteRow)
            {
                m.Promo = true;
                m.Promocija = Promotion;

            }
            return m;
        }
        public bool PokusajPotez(Kvadrat P, Kvadrat O)
        {

            if (!LegalMoves(P).Contains(O)) return false;
            Potez m = Sklopi(P, O);
            if (m.Promo && m.Promocija == null)
            {
                PromocijaObavezna?.Invoke(P, O);
                return false;
            }
            PotegniPotez(Board, P, O, EnPassantKV, m.Promocija);
            AzurirajEnPassant(P, O);
            AzurirajRokada(P, O);

            MoveHistory.Add(m);
            CijiPotez = !CijiPotez;
            AzurirajStatus();

            return true;

        }
        static Piece Promeni(Tip T, bool beli)
        {
            if (T == Tip.Kraljica) return new Kraljica(beli);
            if (T == Tip.Top) return new Top(beli);
            if (T == Tip.Lovac) return new Lovac(beli);
            if (T == Tip.Skakac) return new Skakac(beli);
            throw new Exception("Ne moze taj tip");
        }
        public bool Unapredjenje(Kvadrat P, Kvadrat O, Tip Promotion)
        {
            Piece p = Promeni(Promotion, CijiPotez);
            Potez m = Sklopi(P, O, p);
            PotegniPotez(Board, P, O, EnPassantKV, p);
            AzurirajEnPassant(P, O);
            AzurirajRokada(P, O);
            MoveHistory.Add(m);
            CijiPotez = !CijiPotez;
            AzurirajStatus();
            return true;
        }
        bool DaLiJeRokada(Kvadrat P, Kvadrat O)
        {
            return Math.Abs(P.Col - O.Col) > 1;
        }
        bool DozvoljenaRokada(Kvadrat P, Kvadrat O)
        {
            bool desno = (P.Col == 6);
            if (CijiPotez && desno) return BeliRokadaDozvoljenaDesno;
            if (CijiPotez && !desno) return BeliRokadaDozvoljenaLevo;
            if (!CijiPotez && desno) return CrniRokadaDozvoljenaDesno;
            if (!CijiPotez && !desno) return CrniRokadaDozvoljenaLevo;
            return false;
        }
        void AzurirajStatus()
        {
            bool Legalni = false;
            LegalniSt.Clear();
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Piece p = Board.GetPiece(i, j);
                    if (p != null && p.beli == CijiPotez && LegalMoves(new Kvadrat(i, j)).Count > 0)
                    {
                        Legalni = true;
                        break;
                    }
                }
                if (Legalni) break;
            }
            bool sah = Board.SAH(CijiPotez);
            if (!Legalni && sah) St = Status.Mat;
            else if (!Legalni && !sah) St = Status.Pat;
            else if (sah) St = Status.Sah;
            else St = Status.Normal;
        }
        void AzurirajEnPassant(Kvadrat P, Kvadrat O)
        {
            Piece p = Board.GetPiece(O);
            if (p != null && p.T == Tip.Pesak && Math.Abs(P.Row - O.Row) == 2)
            {
                EnPassantKV = new Kvadrat((P.Row + O.Row) / 2, P.Col);
            }
            else
            {
                EnPassantKV = null;
            }
        }
        void AzurirajRokada(Kvadrat P, Kvadrat O)
        {
            Piece p = Board.GetPiece(O);
            if (p != null && p.T == Tip.Kralj)
            {
                if (CijiPotez && P.Col == 4 && P.Row == 0)
                {
                    if (O.Col == 6) BeliRokadaDozvoljenaDesno = false;
                    else if (O.Col == 2) BeliRokadaDozvoljenaLevo = false;
                }
                else if (!CijiPotez && P.Col == 4 && P.Row == 7)
                {
                    if (O.Col == 6) CrniRokadaDozvoljenaDesno = false;
                    else if (O.Col == 2) CrniRokadaDozvoljenaLevo = false;
                }
            }
            else if (p != null && p.T == Tip.Top)
            {
                if (CijiPotez && P.Row == 0)
                {
                    if (P.Col == 0) BeliRokadaDozvoljenaLevo = false;
                    else if (P.Col == 7) BeliRokadaDozvoljenaDesno = false;
                }
                else if (!CijiPotez && P.Row == 7)
                {
                    if (P.Col == 0) CrniRokadaDozvoljenaLevo = false;
                    else if (P.Col == 7) CrniRokadaDozvoljenaDesno = false;
                }
            }
        }
    }
}