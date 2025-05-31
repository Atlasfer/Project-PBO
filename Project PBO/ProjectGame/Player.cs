using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ProjectGame
{
    public class Player
    {
        public PointF Position;
        public int Health = 3;
        public int Damage = 1;
        public int Ammo = 10;

        private float speed = 3;
        private Size size = new Size(30, 30);

        public Player(PointF startPos)
        {
            Position = startPos;
        }

        public void Move(HashSet<Keys> keys, Size screenSize, Func<PointF, bool> isWalkable)
        {
            PointF nextPos = Position;

            if (keys.Contains(Keys.W) || keys.Contains(Keys.Up))
                nextPos.Y -= speed;
            if (keys.Contains(Keys.S) || keys.Contains(Keys.Down))
                nextPos.Y += speed;
            if (keys.Contains(Keys.A) || keys.Contains(Keys.Left))
                nextPos.X -= speed;
            if (keys.Contains(Keys.D) || keys.Contains(Keys.Right))
                nextPos.X += speed;

            // Hitbox pusat
            PointF centerPoint = new PointF(nextPos.X + size.Width / 2, nextPos.Y + size.Height / 2);

            if (isWalkable(centerPoint))
                Position = nextPos;
        }

        public void Draw(Graphics g)
        {
            g.FillRectangle(Brushes.Green, Position.X, Position.Y, size.Width, size.Height);
        }

        public void TakeDamage(int amount)
        {
            Health -= amount;
            if (Health <= 0)
            {
                // TODO: Tambahkan logic kematian / game over
                Health = 0;
                Console.WriteLine("Player mati!");
            }
        }
        public RectangleF GetBounds()
        {
            return new RectangleF(Position.X, Position.Y, size.Width, size.Height);
        }

    }
}
