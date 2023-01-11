using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sim
{
    public class particle
    {
        public int x;
        public int y;
        public int size;
        public Rectangle rect = new Rectangle();
        vektor sebesseg;
        public particle(int x, int y, int size, vektor sebesseg)
        {
            this.x = x;
            this.y = y;
            this.size = size;
            rect.X = x;
            rect.Y = y;
            rect.Height = size;
            rect.Width = size;
            this.sebesseg = sebesseg;
        }
        public void lép() //collision box + visual
        {
            x += sebesseg.x;
            rect.X += sebesseg.x;
            y += sebesseg.y;
            rect.Y += sebesseg.y;
        }
        public bool collides(particle atom) //lazy fos négyzetre értelmezett beépitett collision
        {
            bool collision = false;
            if(this.rect.IntersectsWith(atom.rect)) { return true; }
            return collision;
        }
        
        

    }
}
