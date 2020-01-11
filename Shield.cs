using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Final_project
{
    class Shield : GameObject
    {
        public Shield(Graphics kp, Point poz) : base(kp)
        {
            obrazek = Properties.Resources.shield;
            sirka = (byte)obrazek.Width;
            vyska = (byte)obrazek.Height;
            pozice = poz;
            rozlohaSPozici = new Rectangle(pozice, new Size(sirka, vyska));
        }
    }
}
