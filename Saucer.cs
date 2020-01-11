using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Final_project
{
    class Saucer : GameObject
    {
        bool dead = true;
        public bool Dead { get { return dead; } set { dead = value; } }

        public Saucer(Graphics kp) : base(kp)
        {
            obrazek = Properties.Resources.saucer;
            sirka = (byte)obrazek.Width;
            vyska = (byte)obrazek.Height;
            pozice = new Point(-72, 70);
            rozlohaSPozici = new Rectangle(pozice, new Size(sirka, vyska));
        }

        public override void Vykresli()
        {           
            pozice = new Point(pozice.X + 2, pozice.Y);
            kp.FillRectangle(clear, pozice.X - 2, pozice.Y + 17, 2, 4);
            rozlohaSPozici = new Rectangle(pozice, new Size(sirka, vyska));

            base.Vykresli();            
        }
    }
}
