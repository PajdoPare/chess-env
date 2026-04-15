using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
namespace ProjekatSahOOP
{
    public class PromotionForm : Form
    {
        public Tip Chosen;
        public PromotionForm(bool beli)
        {
            Bitmap bmp1 = new Bitmap(ImageMapping.Get(beli, Tip.Kraljica));
            IntPtr hIcon = bmp1.GetHicon();
            Icon = Icon.FromHandle(hIcon);
            Tip[] tipovi = new Tip[] { Tip.Kraljica, Tip.Top, Tip.Lovac, Tip.Skakac };
            Size = new Size(350, 150);
            Text = "Promocija";
            StartPosition = FormStartPosition.CenterParent;
            int x = 10;
            foreach (Tip t in tipovi)
            {
                Bitmap bmp = new Bitmap(ImageMapping.Get(beli, t), new Size(50, 50));
                Button b = new Button
                {
                    
                    Size = new Size(60, 60),
                    Location = new Point(x, 10),
                    Image = bmp,
                    ImageAlign = ContentAlignment.MiddleCenter
                    
                };
                Label lbl = new Label
                {
                    Text = t.ToString(),
                    Location = new Point(x, 80),
                    Size = new Size(60, 20),
                    TextAlign = ContentAlignment.MiddleCenter
                };
                b.Click += (s, e) =>
                {
                    Chosen = t;
                    Close();
                };
                Controls.Add(lbl);
                Controls.Add(b);
                x += 70;
            }
        }
    }
}
