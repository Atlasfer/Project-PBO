using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGame
{
    public class Tile
    {
        public Rectangle Area;
        public bool IsCollider;

        public Tile(int x, int y, int size, bool isCollider)
        {
            Area = new Rectangle(x*size, y*size, size, size);
            IsCollider = isCollider;
        }

        public void Draw(Graphics g)
        {
            Brush brush = IsCollider ? Brushes.Gray : Brushes.Green;
            g.FillRectangle(brush, Area);
            g.DrawRectangle(Pens.Black, Area);
        }
    }
}
