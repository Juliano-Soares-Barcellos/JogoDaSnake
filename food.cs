using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    class Food
    {
        public Point Location { get; private set; }
        public void CreateFood()
        {
            Random randown = new Random();
            Location = new Point(randown.Next(0, 27), randown.Next(0, 27));
        }
    }
}
