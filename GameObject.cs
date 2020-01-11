using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Final_project
{
    class GameObject
    {
        protected SolidBrush clear = new SolidBrush(Color.Black);
        protected Point pozice;
        protected Image obrazek;
        protected byte sirka;
        protected byte vyska;
        protected Rectangle rozlohaSPozici;
        public Rectangle RozlohaSPozici { get { return rozlohaSPozici; } }
        protected byte[] rozmeziPohybuXY = new byte[2];
        protected Graphics kp;
        public Point ZiskejPozici { get { return pozice; } }

        public GameObject(Graphics kp)
        {
            rozmeziPohybuXY = new byte[] { (byte)kp.DpiX, (byte)kp.DpiY };
            this.kp = kp;
            obrazek = Properties.Resources._default;
            sirka = (byte)obrazek.Width;
            vyska = (byte)obrazek.Height;
        }

        public virtual void Vykresli()
        {
            kp.DrawImage(obrazek, pozice);
        }
    }
}