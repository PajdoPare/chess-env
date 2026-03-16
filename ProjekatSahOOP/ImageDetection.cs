using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace ProjekatSahOOP
{
    public class ImageDetection
    {
        static Dictionary<(bool, Tip), Image> Dic = new Dictionary<(bool, Tip), Image>();
        public static void Load()
        {
            Dic[(true, Tip.Pesak)] = Properties.Resources.BeliPesak;
            Dic[(false, Tip.Pesak)] = Properties.Resources.CrniPesak;
            Dic[(true, Tip.Top)] = Properties.Resources.BeliTop;
            Dic[(false, Tip.Top)] = Properties.Resources.CrniTop;
            Dic[(true, Tip.Lovac)] = Properties.Resources.BeliLovac;
            Dic[(false, Tip.Lovac)] = Properties.Resources.CrniLovac;
            Dic[(true, Tip.Skakac)] = Properties.Resources.BeliSkakac;
            Dic[(false, Tip.Skakac)] = Properties.Resources.CrniSkakac;
            Dic[(true, Tip.Kraljica)] = Properties.Resources.BelaKraljica;
            Dic[(false, Tip.Kraljica)] = Properties.Resources.CrnaKraljica;
            Dic[(true, Tip.Kralj)] = Properties.Resources.BeliKralj;
            Dic[(false, Tip.Kralj)] = Properties.Resources.CrniKralj;
        }
        public static Image Get(bool beli, Tip t) => Dic[(beli, t)];
    } 
}
