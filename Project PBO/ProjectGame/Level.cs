using System.Drawing;

namespace ProjectGame
{
    public class Level
    {
        private int[,] map;
        private int tileSize;

        public Level(int[,] mapData, int tileSize)
        {
            this.map = mapData;
            this.tileSize = tileSize;
        }

        public void Draw(Graphics g)
        {
            for (int y = 0; y < map.GetLength(0); y++)
            {
                for (int x = 0; x < map.GetLength(1); x++)
                {
                    Rectangle rect = new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize);
                    Brush b = map[y, x] == 1 ? Brushes.DarkGray : Brushes.LightGray;
                    g.FillRectangle(b, rect);
                    g.DrawRectangle(Pens.Black, rect);
                }
            }
        }

        public bool IsWalkable(PointF pos, int tileSize)
        {
            int x = (int)(pos.X / tileSize);
            int y = (int)(pos.Y / tileSize);

            if (x < 0 || y < 0 || y >= map.GetLength(0) || x >= map.GetLength(1))
                return false;

            return map[y, x] == 0;
        }

        public bool IsWall(PointF pos, int tileSize)
        {
            int x = (int)(pos.X / tileSize);
            int y = (int)(pos.Y / tileSize);

            if (x < 0 || y < 0 || y >= map.GetLength(0) || x >= map.GetLength(1))
                return true;

            return map[y, x] == 1;
        }

    }
}
