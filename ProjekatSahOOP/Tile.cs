using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjekatSahOOP
{
    public class Tile : PictureBox
    {
        Point Koor;
        bool beli;
        Piece Figura;
        public Tile(int r, int c, int x, int y,int sz, bool beli)
        {
            Koor = new Point(r, c);
            this.beli = beli;
            this.Location = new Point(x, y);
            this.Size = new Size(sz, sz);
            this.SizeMode = PictureBoxSizeMode.Zoom;
            if (beli)
            {
                this.BackColor = Color.FromArgb(240, 240, 210);
            }
            else
            {
                this.BackColor = Color.FromArgb(40, 164, 221);
            }
        }
        public void Jedi(Piece novi)
        {
            if(novi != null)
            {
                this.Figura = novi;
                this.Image = novi.Image;
            }
            else
            {
                Ukloni();
            }
        }
        public Piece Ukloni()
        {
            Piece pom = this.Figura;
            Figura = null;
            Image = null;
            return pom;
        }
    }
}
