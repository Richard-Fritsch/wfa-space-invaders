using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Final_project
{
    class Bomba : GameObject
    {
        bool remove = false;
        public bool Odstranit { get { return remove; } set { remove = value; } }

        public Bomba(Graphics kp, Point poz) : base(kp)
        {
            obrazek = Properties.Resources.bomb;
            sirka = (byte)obrazek.Width;
            vyska = (byte)obrazek.Height;
            pozice = poz;
            rozlohaSPozici = new Rectangle(pozice, new Size(sirka, vyska));
        }

        public override void Vykresli()
        {
            kp.FillRectangle(clear, new Rectangle(pozice.X, pozice.Y - 2, 6, 2));
            base.Vykresli();
        }

        public void PohniSe()
        {
            pozice = new Point(pozice.X, pozice.Y + 2);
            rozlohaSPozici = new Rectangle(pozice, new Size(sirka, vyska));
        }

        public void CoverUp()
        {
            kp.FillRectangle(new SolidBrush(Color.Black), pozice.X, pozice.Y, sirka, vyska);
        }
    }
}
