using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace sim
{
    public partial class Form1 : Form
    {
        static vektor álló=new vektor(0,0);
        bool fut = false;               
        List<particle> reszek = new List<particle>();
        List<particle> mozgoreszek = new List<particle>();
        Brush black = new SolidBrush(Color.Black);
        Brush red = new SolidBrush(Color.Red);
        public Form1()
        {
            InitializeComponent();
            Random rand = new Random();            
            for (int i = 0; i < 150; i++)
            {
                int x = rand.Next(0,1500);
                int y = rand.Next(0,1000);
                particle temp = new particle(x, y, 20, álló);
                reszek.Add(temp);
            }
            particle neutron = new particle(0, 30, 10, new vektor(3, 2));                  
            mozgoreszek.Add(neutron);
            rajzol();           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            fut = true;
            reakció(reszek);
        }
        void rajzol()
        {
            Size vaszonmeret = pictureBox1.Size;
            Bitmap bmp = new Bitmap(vaszonmeret.Width, vaszonmeret.Height);
            pictureBox1.Image = bmp;
            using (Graphics g = Graphics.FromImage(bmp))
            {
                foreach (particle p in reszek)
                {                
                    Rectangle rect = new Rectangle(p.x, p.y, p.size, p.size);                  
                    g.FillEllipse(black, rect);
                }
                foreach (particle p in mozgoreszek)
                {
                    Rectangle rect = new Rectangle(p.x, p.y, p.size, p.size);
                    g.FillEllipse(red, rect);
                }
            }
            pictureBox1.Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            fut = false;
        }
        void reakció(List<particle> reszek)
        {          
            while(fut)
            {
                Thread.Sleep(3);
                léptetés(reszek);
                rajzol();
                Application.DoEvents();
            }
        }
        void léptetés(List<particle> reszek)
        {
            for (int i = 0; i < mozgoreszek.Count(); i++)
            {
                mozgoreszek[i].lép();                
                for (int j = 0; j < reszek.Count(); j++)
                {
                    if (mozgoreszek[i].collides(reszek[j]))
                    {                      
                        explode(reszek[j], mozgoreszek[i]);
                        break; //enélkül nem megy xd
                    }
                }
            }                  
        }
        public void explode(particle reszecske,particle reszecske2)
        {
            mozgoreszek.Remove(reszecske2); 
            reszek.Remove(reszecske);      
            Random rand = new Random();                          
            int num = rand.Next(10);
            for (int i = 0; i < num; i++)
            {
                int xvel = rand.Next(-3, 3);
                int yvel = rand.Next(-3, 3);
                if (xvel==0&&yvel==0)             
                    break;              //enélkül álló neutron
                mozgoreszek.Add(new particle(reszecske.x, reszecske.y, 10, new vektor(xvel, yvel)));
            }
        }
    }
}
