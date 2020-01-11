using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Final_project
{
    class Man : GameObject
    {
        public Point GetBulletStart { get { return new Point(pozice.X + 10, pozice.Y); } }

        public Man(Graphics kp) : base(kp)
        {
            obrazek = Properties.Resources.player;
            sirka = (byte)obrazek.Width;
            vyska = (byte)obrazek.Height;
            pozice = new Point(20, 500);
            rozlohaSPozici = new Rectangle(pozice, new Size(sirka, vyska));
        }

        public void PohybVlevo()
        {
            if (pozice.X - 2 < 0) return;
            kp.FillRectangle(new SolidBrush(Color.Black), pozice.X + 25, pozice.Y + 8, 2, 19);
            //kp.DrawLine(new Pen(Color.Black), pozice.X + 26, pozice.Y + 8, pozice.X + 26, pozice.Y + 19);
            pozice = new Point(pozice.X - 2, pozice.Y);
            rozlohaSPozici = new Rectangle(pozice, new Size(sirka, vyska));
        }

        public void PohybVpravo()
        {
            if (pozice.X + 2 > 486) return;
            kp.FillRectangle(new SolidBrush(Color.Black), pozice.X, pozice.Y, 2, 19);
            //kp.DrawLine(new Pen(Color.Black), pozice.X, pozice.Y + 8, pozice.X, pozice.Y + 19);
            pozice = new Point(pozice.X + 2, pozice.Y);
            rozlohaSPozici = new Rectangle(pozice, new Size(sirka, vyska));
        }

        //public void VykresliExplozi()
        //{
        //    kp.DrawImage(Properties.Resources.playerdeath, pozice);            
        //}
    }
}
