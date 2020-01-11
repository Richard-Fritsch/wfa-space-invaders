using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;

namespace Final_project
{
    class HighScore : Score
    {
        string cesta = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()))+"/highscore.txt";

        public HighScore(Graphics kp) : base(kp)
        {           
            skore = int.Parse(File.ReadAllText(cesta));
        }

        //public void Write()
        //{
        //    File.WriteAllText(cesta, skore.ToString());
        //}

        public override void Vykresli()
        {
            string text = "High Score: " + skore.ToString();

            kp.DrawString(text, scoreFont, new SolidBrush(Color.Green), 15, 10);
        }
    }
}
