using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace ProjekatSahOOP
{
    public static class ImageMapping
    {
        static Dictionary<(bool, Tip), Image> Dic = new Dictionary<(bool, Tip), Image>();
        public static void Load()
        {
            Dic[(true, Tip.Pesak)] = Properties.Resources.PesakB;
            Dic[(false, Tip.Pesak)] = Properties.Resources.PesakC;
            Dic[(true, Tip.Top)] = Properties.Resources.TopB;
            Dic[(false, Tip.Top)] = Properties.Resources.TopC;
            Dic[(true, Tip.Lovac)] = Properties.Resources.LovacB;
            Dic[(false, Tip.Lovac)] = Properties.Resources.LovacC;
            Dic[(true, Tip.Skakac)] = Properties.Resources.SkakacB;
            Dic[(false, Tip.Skakac)] = Properties.Resources.SkakacC;
            Dic[(true, Tip.Kraljica)] = Properties.Resources.KraljicaB;
            Dic[(false, Tip.Kraljica)] = Properties.Resources.KraljicaC;
            Dic[(true, Tip.Kralj)] = Properties.Resources.KraljB;
            Dic[(false, Tip.Kralj)] = Properties.Resources.KraljC;
        }
        public static Image Get(bool beli, Tip t) => Dic[(beli, t)];
    } 
}
