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
                    bool beli = !(((i + j) & 1) == 1);
                    Color color = beli ? Beli : Crni;
                    g.FillRectangle(new SolidBrush(color), j * sz, i * sz, sz, sz);
                }
            }
            if(Selected != null)
            {
                g.FillRectangle(new SolidBrush(Sel), Selected.Value.Col * sz, Selected.Value.Row * sz, sz, sz);
            }
            if(LastMove != null)
            {
                g.FillRectangle(new SolidBrush(Pos), LastMove.Polazno.Col * sz, LastMove.Polazno.Row * sz, sz, sz);
                g.FillRectangle(new SolidBrush(Pos), LastMove.Odredisno.Col * sz, LastMove.Odredisno.Row * sz, sz, sz);
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
                    Piece p = b.GetPiece(i, j);
                    if (p == null) continue;
                    Image img = ImageMapping.Get(p.beli, p.T);
                    int razmak = (sz >> 3);
                    g.DrawImage(img, j * sz + razmak, i * sz + razmak, sz - razmak * 2, sz - razmak * 2);
                }
            }
            foreach(Kvadrat k in Legalni)
            {
                bool jede = (b.GetPiece(k) != null);
                Tackica(g, k, jede);
            }
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            int col = e.X / KVSize;
            int row = e.Y / KVSize;
            if (GS.Board.Unutar(row, col)) Klik?.Invoke(new Kvadrat(row, col));
        }
        void Tackica(Graphics g, Kvadrat k, bool jede)
        {
            
            
            if(jede)
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
