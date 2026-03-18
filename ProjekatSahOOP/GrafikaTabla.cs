using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
namespace ProjekatSahOOP
{
    public class GrafikaTabla : Panel
    {
        public GameState GS { get; set; }
        public Kvadrat? Selected { get; set; }
        public List<Kvadrat>Legalni { get; set; }
        public Potez LastMove { get; set; } = null;
        public event Action<Kvadrat> Klik;
        static Color Beli = Color.FromArgb(240, 240, 210);
        static Color Crni = Color.FromArgb(40, 164, 221);
        static Color Sel = Color.FromArgb(120, 40, 164, 221);
        static Color Tac = Color.FromArgb(65, 0, 0, 0);
        static Color Pos = Color.FromArgb(45, 0, 0, 0) ;
        static SolidBrush cetka = new SolidBrush(Tac);
        int KVSize => Width / 8;
        public GrafikaTabla()
        {
            DoubleBuffered = true;
            ResizeRedraw = true;
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (GS == null) return;
            Graphics g = e.Graphics;
            int sz = KVSize;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    int r = GS.Flipped ? 7 - i :  i;
                    int c = GS.Flipped ? 7 - j : j;
                    bool beli = !(((r + c) & 1) == 1);
                    Color color = beli ? Beli : Crni;
                    g.FillRectangle(new SolidBrush(color), c * sz, r * sz, sz, sz);
                }
            }
            if(Selected != null)
            {
                int r = GS.Flipped ? 7 - Selected.Value.Row : Selected.Value.Row;
                int c = GS.Flipped ? 7 - Selected.Value.Col : Selected.Value.Col;
                g.FillRectangle(new SolidBrush(Sel), c * sz, r * sz, sz, sz);
            }
            if(LastMove != null)
            {
                int r = GS.Flipped ? 7 - LastMove.Polazno.Row :  LastMove.Polazno.Row;
                int c = GS.Flipped ? 7 - LastMove.Polazno.Col :  LastMove.Polazno.Col;
                g.FillRectangle(new SolidBrush(Pos), c * sz, r * sz, sz, sz);
                r = GS.Flipped ? 7 - LastMove.Odredisno.Row :  LastMove.Odredisno.Row;
                c = GS.Flipped ? 7 - LastMove.Odredisno.Col :  LastMove.Odredisno.Col;
                g.FillRectangle(new SolidBrush(Pos), c * sz, r * sz, sz, sz);
            }
            Board b = GS.Board;
            if(GS.St == Status.Sah || GS.St == Status.Mat)
            {
                Kvadrat k = b.GdeKralj(GS.CijiPotez);
                g.FillRectangle(new SolidBrush(Color.FromArgb(120, 255, 0, 0)), k.Col * sz, k.Row * sz, sz, sz);
            }
            for(int i = 0; i < 8; i++)
            {
                for(int j = 0; j < 8; j++)
                {
                    int r = GS.Flipped ? 7 - i :  i;
                    int c = GS.Flipped ? 7 - j :  j;
                    Piece p = b.GetPiece(i, j);
                    if (p == null) continue;
                    Image img = ImageMapping.Get(p.beli, p.T);
                    int razmak = (sz >> 3);
                    g.DrawImage(img, c * sz + razmak, r * sz + razmak, sz - razmak * 2, sz - razmak * 2);
                }
            }
            foreach(Kvadrat k in Legalni)
            {
                bool jede = (b.GetPiece(k) != null || (GS.EnPassantKV.HasValue && GS.EnPassantKV == k));
                Tackica(g, k, jede);
            }
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            int col = e.X / KVSize;
            int row = e.Y / KVSize;
            row = GS.Flipped ?  7 - row :  row;
            col = GS.Flipped ? 7 - col :  col;
            if (GS.Board.Unutar(row, col)) Klik?.Invoke(new Kvadrat(row, col));
        }
        void Tackica(Graphics g, Kvadrat k, bool jede)
        {
            k = new Kvadrat(GS.Flipped ? 7 - k.Row :  k.Row, GS.Flipped ? 7 - k.Col :  k.Col);
            if (jede)
            {
                int size = KVSize / 8;
                Pen olovka = new Pen(Tac, size);
                
                g.DrawEllipse(olovka, k.Col * KVSize + size, k.Row * KVSize + size, KVSize - size * 2, KVSize - size * 2);
            }
            else
            {
                int size = KVSize / 3;
                int x = k.Col * KVSize + (KVSize - size) / 2;
                int y = k.Row * KVSize + (KVSize - size) / 2;
                g.FillEllipse(cetka, x, y, size, size);
                
            }
        }
        
        
    }
}
