using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Final_project
{
    class IndikatorZivotu : GameObject
    {
        byte pocetZivotu = 3;
        public byte PocetZivotu { get { return pocetZivotu; } set { pocetZivotu = value; } }

        public IndikatorZivotu(Graphics kp) : base(kp)
        {
            obrazek = Properties.Resources.player;
            sirka = (byte)obrazek.Width;
            vyska = (byte)obrazek.Height;
            pozice = new Point(20, 550);
            rozlohaSPozici = new Rectangle(pozice, new Size(sirka, vyska));
        }

        public override void Vykresli()
        {
            kp.FillRectangle(clear, pozice.X + 40, pozice.Y, 100, 30);
            for (int i = 0; i < pocetZivotu; i++)
            {                
                kp.DrawImage(obrazek, new Point(pozice.X + 40 * i, pozice.Y));
            }            
        }
    }
}
