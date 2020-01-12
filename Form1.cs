using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Final_project
{
    public partial class Form1 : Form
    {
        enum smer { LEFT, RIGHT };
        int pohyb = 2;
        int timePassed = 0;
        bool levystisk = false;
        bool pravystisk = false;

        Graphics kp;
        Man hrac;
        IndikatorZivotu zivoty;
        HighScore topScore;
        Score currentScore;
        Shield[] stity = new Shield[4];
        List<Invader> invaderi = new List<Invader>();
        List<Bullet> kulky = new List<Bullet>();
        List<Bomba> bomby = new List<Bomba>();

        public Form1()
        {
            InitializeComponent();
        }
        Random rnd = new Random();
        private void Timer1_Tick(object sender, EventArgs e)
        {
            timePassed += 1;
            
            DetekceKolize();
            if (invaderi.Count == 0)
            {
                timer1.Stop();
                kp.DrawString("Vyhrál jsi!", new Font(FontFamily.GenericSerif, (float)20), new SolidBrush(Color.White), 200, 200);
                currentScore.Zapsat();
            }
            if (zivoty.PocetZivotu == 0)
            {
                timer1.Stop();
                kp.DrawString("Prohrál jsi!", new Font(FontFamily.GenericSerif, (float)20), new SolidBrush(Color.White), 200, 100);
                currentScore.Zapsat();
            }

            switch (pohyb)
            {
                case 0:
                    hrac.PohybVlevo();
                    break;
                case 1:
                    hrac.PohybVpravo();
                    break;
            }

            zivoty.Vykresli();
            hrac.Vykresli();
            currentScore.Vykresli();
            topScore.Vykresli();
            if (timePassed % 5 == 0)
            {
                foreach (Invader badguy in invaderi)
                {
                    badguy.Vykresli();
                }
            }
            if (timePassed % 8 == 0)
            {
                foreach (Invader badguy in invaderi.Where(x => x.CanDrop))
                {
                    if (rnd.Next(0, 101) < 2)
                    {
                        Point p = badguy.ZiskejPozici;
                        bomby.Add(new Bomba(kp, new Point(p.X + badguy.RozlohaSPozici.Width / 2, p.Y + badguy.RozlohaSPozici.Height)));
                    }
                }

            }
            foreach (Shield stit in stity)
            {
                stit.Vykresli();
            }
            foreach (Bullet kulka in kulky)
            {
                kulka.PohniSe();
                kulka.Vykresli();
            }
            foreach (Bomba bum in bomby)
            {
                bum.PohniSe();
                bum.Vykresli();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            kp = panMain.CreateGraphics();
            Reset();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A)
            {
                pohyb = (int)smer.LEFT;
                levystisk = true;
            }
            else if (e.KeyCode == Keys.D)
            {
                pohyb = (int)smer.RIGHT;
                pravystisk = true;
            } 
            if (kulky.Count == 0)
            {
                if (e.KeyCode == Keys.L) kulky.Add(new Bullet(kp, hrac.GetBulletStart));
            }            
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A && !pravystisk)
            {
                pohyb = 2;
                levystisk = false;
            }
            else if (e.KeyCode == Keys.A && pravystisk)
            {
                levystisk = false;
            }
            else if (e.KeyCode == Keys.D && !levystisk)
            {
                pohyb = 2;
                pravystisk = false;
            }
            else if (e.KeyCode == Keys.D && levystisk)
            {
                pravystisk = false;
            }
        }

        private void DetekceKolize()
        {
            foreach (Invader badguy in invaderi)
            {
                foreach (Bullet buljet in kulky)
                {
                    Rectangle check = Rectangle.Intersect(badguy.RozlohaSPozici, buljet.RozlohaSPozici);
                    if(check.Width > 0 || check.Height > 0)
                    {
                        buljet.Odstranit = true;
                        badguy.Odstranit = true;
                    }
                }
            }
            foreach (Shield stit in stity)
            {
                foreach (Bullet buljet in kulky)
                {
                    Rectangle check = Rectangle.Intersect(stit.RozlohaSPozici, buljet.RozlohaSPozici);
                    if (check.Width > 0 || check.Height > 0)
                    {
                        buljet.Odstranit = true;
                    }
                }
            }
            foreach (Bomba bum in bomby)
            {
                foreach (Shield stit in stity)
                {
                    Rectangle check2 = Rectangle.Intersect(stit.RozlohaSPozici, bum.RozlohaSPozici);
                    if (check2.Width > 0 || check2.Height > 0)
                    {
                        bum.Odstranit = true;
                    }
                }
                Rectangle check = Rectangle.Intersect(bum.RozlohaSPozici, hrac.RozlohaSPozici);
                if (check.Width > 0 || check.Height > 0)
                {
                    bum.Odstranit = true;
                    zivoty.PocetZivotu--; 
                }
            }

            foreach (Bullet buljet in kulky)
            {
                if (buljet.ZiskejPozici.Y <= 0)
                {
                    buljet.Odstranit = true;
                }
            }
            for (int i = 0; i < invaderi.Count; i++)
            {
                if (invaderi[i].Odstranit)
                {
                    if (invaderi[i].InvaderType == 1)
                    {
                        currentScore.Increase(10);
                    }
                    else if (invaderi[i].InvaderType == 2)
                    {
                        currentScore.Increase(20);
                    }
                    else
                    {
                        currentScore.Increase(30);
                    }
                    var collum = invaderi.Where(x => x.ZiskejPozici.X == invaderi[i].ZiskejPozici.X && x != invaderi[i])
                                         .OrderBy(x => x.ZiskejPozici.Y);
                    if (collum.Count() > 0)collum.Last().CanDrop = true;
                    invaderi[i].CoverUp();
                    invaderi.Remove(invaderi[i]);                   
                }
                
            }
            for (int i = 0; i < kulky.Count; i++)
            {
                if (kulky[i].Odstranit)
                {
                    kulky[i].CoverUp();
                    kulky.Remove(kulky[i]);
                } 

            }
            for (int i = 0; i < bomby.Count; i++)
            {
                if (bomby[i].Odstranit)
                {
                    bomby[i].CoverUp();
                    bomby.Remove(bomby[i]);
                }
            }
        }

        private void ButReset_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void Reset()
        {
            Point[] body = new Point[55];

            kp.FillRectangle(new SolidBrush(Color.Black), 0, 0, 520, 580);
            invaderi.Clear();
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 11; j++)
                {
                    body[j + 10 * i + i * 1] = new Point(20 * j * 2 + 25, 150 + i * 50);
                }
            }
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 11; j++)
                {
                    int k;
                    if (i == 0) k = 3;
                    else if (i < 3) k = 2;
                    else k = 1;
                    invaderi.Add(new Invader(kp, body[j + 10 * i + i * 1], (byte)k));
                    if (i == 4) invaderi[j + 10 * i + i * 1].CanDrop = true;
                }
            }
            for (int i = 0; i < 4; i++)
            {
                stity[i] = new Shield(kp, new Point(60 * i * 2 + 40, 425));
            }

            zivoty = new IndikatorZivotu(kp);
            hrac = new Man(kp);
            topScore = new HighScore(kp);
            currentScore = new Score(kp);
        }
    }
}
