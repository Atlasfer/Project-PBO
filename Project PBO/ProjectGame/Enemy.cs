using System.Drawing;
using System.Security.Policy;

namespace ProjectGame
{
    public class Enemy
    {
        public PointF Position;
        public Size Size = new Size(30, 30);
        public int Health = 3;

        public Enemy(PointF pos)
        {
            Position = pos;
        }

        public RectangleF GetBounds()
        {
            return new RectangleF(Position.X, Position.Y, Size.Width, Size.Height);
        }

        public void Draw(Graphics g)
        {
            g.FillRectangle(Brushes.Red, Position.X, Position.Y, Size.Width, Size.Height);
        }

        public void Update(PointF playerPos, float speed = 1.5f)
        {
            // Hitung vektor arah ke player
            float dx = playerPos.X - Position.X;
            float dy = playerPos.Y - Position.Y;
            float distance = (float)Math.Sqrt(dx * dx + dy * dy);

            if (distance > 0)
            {
                dx /= distance;
                dy /= distance;

                // Gerakkan musuh
                Position = new PointF(Position.X + dx * speed, Position.Y + dy * speed);
            }
        }

        public void Update(PointF playerPos, Func<PointF, bool> isWalkable, float speed = 1.5f)
        {
            float dx = playerPos.X - Position.X;
            float dy = playerPos.Y - Position.Y;
            float distance = (float)Math.Sqrt(dx * dx + dy * dy);

            if (distance > 0)
            {
                dx /= distance;
                dy /= distance;

                PointF nextPos = new PointF(Position.X + dx * speed, Position.Y + dy * speed);
                PointF center = new PointF(nextPos.X + Size.Width / 2, nextPos.Y + Size.Height / 2);

                if (isWalkable(center))
                {
                    Position = nextPos;
                }
            }
        }


    }
}
