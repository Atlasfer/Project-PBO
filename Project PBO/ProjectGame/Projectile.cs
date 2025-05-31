using System;
using System.Drawing;

namespace ProjectGame
{
    public class Projectile
    {
        public PointF Position;
        public PointF Velocity;
        public float Speed = 10;
        public Size Size = new Size(10, 10);

        public Projectile(PointF startPos, PointF targetPos)
        {
            Position = startPos;

            // Hitung arah (normalized vector)
            float dx = targetPos.X - startPos.X;
            float dy = targetPos.Y - startPos.Y;
            float length = (float)Math.Sqrt(dx * dx + dy * dy);
            Velocity = new PointF(dx / length * Speed, dy / length * Speed);
        }

        public void Update()
        {
            Position = new PointF(Position.X + Velocity.X, Position.Y + Velocity.Y);
        }

        public void Draw(Graphics g)
        {
            g.FillEllipse(Brushes.Yellow, Position.X, Position.Y, Size.Width, Size.Height);
        }

        public bool IsOffScreen(Size screenSize)
        {
            return Position.X < 0 || Position.Y < 0 || Position.X > screenSize.Width || Position.Y > screenSize.Height;
        }
    }
}
