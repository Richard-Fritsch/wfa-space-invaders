using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;

namespace Final_project
{
    class Score
    {
        protected int skore = 0;
        protected Font scoreFont = new Font(FontFamily.GenericSerif, (float)20);
        protected Graphics kp;

        public Score(Graphics kp)
        {
            this.kp = kp;           
        }

        public virtual void Vykresli()
        {
            string text = "Score: " + skore.ToString();
            kp.DrawString(text, scoreFont, new SolidBrush(Color.Green), 200, 10);
        }

        public void Increase(int amount)
        {
            string text = "Score: " + skore.ToString();
            kp.DrawString(text, scoreFont, new SolidBrush(Color.Black), 200, 10);
            skore += amount;            
        }

        public void Zapsat()
        {
            //Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())) + "/highscore.txt";
            File.WriteAllText(Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())) + "/highscore.txt", skore.ToString());
        }
    }
}
