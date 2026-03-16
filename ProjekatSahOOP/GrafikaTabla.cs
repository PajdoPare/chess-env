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
            Board b = GS.Board;
            if(GS.St == Status.Sah || GS.St == Status.Mat)
            {
                Kvadrat k = b.GdeKralj(GS.CijiPotez);
                g.FillRectangle(new SolidBrush(Color.FromArgb(120, 255, 0, 0)), k.Col * sz, k.Row * sz, sz, sz);
            }
        }
        
        
    }
}
