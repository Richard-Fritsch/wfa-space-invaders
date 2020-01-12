using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Final_project
{
    class Invader : GameObject
    {
        int pocetpohybu = 0;
        bool doleva = false;
        bool canDrop = false;
        public bool CanDrop { get { return canDrop; } set { canDrop = value; } }
        byte invaderType;
        public byte InvaderType { get { return invaderType; } }
        bool remove = false;
        public bool Odstranit { get { return remove; } set { remove = value; } }

        public Invader(Graphics kp, Point poz, byte invaderType) : base(kp)
        {
            switch (invaderType)
            {
                case 1:
                    this.invaderType = 1;
                    obrazek = Properties.Resources.invader1_1;
                    break;
                case 2:
                    this.invaderType = 2;
                    obrazek = Properties.Resources.invader2;
                    break;
                case 3:
                    this.invaderType = 3;
                    obrazek = Properties.Resources.invader3;
                    break;
                default:
                    obrazek = Properties.Resources._default;
                    break;
            }           
            sirka = (byte)obrazek.Width;
            vyska = (byte)obrazek.Height;
            pozice = poz;
            rozlohaSPozici = new Rectangle(pozice, new Size(sirka, vyska));
        }

        public override void Vykresli()
        {
            if (pocetpohybu == 30)
            {
                pozice = new Point(pozice.X, pozice.Y + 5);
                kp.FillRectangle(new SolidBrush(Color.Black), pozice.X, pozice.Y - 5, 36, 5);
                pocetpohybu = 0;
                doleva = !doleva;
            }

            if (doleva)
            {
                pozice = new Point(pozice.X - 2, pozice.Y);
                if (invaderType == 1)
                {
                    kp.FillRectangle(new SolidBrush(Color.Black), pozice.X + 25, pozice.Y, 2, 24);
                }
                else if (invaderType == 2)
                {
                    kp.FillRectangle(new SolidBrush(Color.Black), pozice.X + 22, pozice.Y, 3, 24);
                }
                else
                {
                    kp.FillRectangle(new SolidBrush(Color.Black), pozice.X + 17, pozice.Y, 3, 24);
                }
                pocetpohybu++;
            }
            else
            {
                pozice = new Point(pozice.X + 2, pozice.Y);
                kp.FillRectangle(new SolidBrush(Color.Black), pozice.X - 2, pozice.Y, 2, 24);
                pocetpohybu++;
            }
            rozlohaSPozici = new Rectangle(pozice, new Size(sirka, vyska));
            base.Vykresli();
        }

        public void CoverUp()
        {
            kp.FillRectangle(new SolidBrush(Color.Black), pozice.X, pozice.Y, sirka, vyska);
        }
    }
}
