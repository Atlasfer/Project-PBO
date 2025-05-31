using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGame
{
    public class InGameForm : Form
    {
        private Button menuButton;
        Level currentLevel;
        int tileSize = 50;
        System.Windows.Forms.Timer gameTimer = new System.Windows.Forms.Timer();
        HashSet<Keys> keysPressed = new HashSet<Keys>();
        Player player;
        List<Projectile> projectiles = new List<Projectile>();
        List<Enemy> enemies = new List<Enemy>();
        Label healthPoint;



        public InGameForm()
        {
            InitializeControls();
            InitializeForm();
        }

        public void InitializeForm()
        {
            this.Text = "In-Game";
            this.ClientSize = new Size(1366, 768);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.Black;
            this.DoubleBuffered = true;
            enemies.Add(new Enemy(new PointF(400, 300)));
            enemies.Add(new Enemy(new PointF(700, 500)));

            int[,] mapData = new int[,]
{
                {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
            };

            currentLevel = new Level(mapData, tileSize);
            player = new Player(new PointF(1 * tileSize + 10, 1 * tileSize + 10));

            gameTimer.Interval = 20;
            gameTimer.Tick += GameLoop;
            gameTimer.Start();

            this.Paint += DrawGame;
            this.KeyDown += (s, e) => keysPressed.Add(e.KeyCode);
            this.KeyUp += (s, e) => keysPressed.Remove(e.KeyCode);

            this.KeyPreview = true;
            this.Focus();

            this.MouseDown += (s, e) =>
            {
                if (e.Button == MouseButtons.Left)
                {
                    PointF playerCenter = new PointF(player.Position.X + 15, player.Position.Y + 15);
                    PointF mousePos = e.Location;
                    projectiles.Add(new Projectile(playerCenter, mousePos));
                }
            };


        }

        public void InitializeControls()
        {
            menuButton = new Button
            {
                Text = "Menu",
                Location = new Point(1150, 30),
                Size = new Size(150, 50),
                BackColor = Color.Black,
                ForeColor = Color.White,
            };
            menuButton.Click += MenuButton_Click;
            Controls.Add(menuButton);



        }

        private void MenuButton_Click(object sender, EventArgs e)
        {
            //this.Hide();
            MenuForm menuForm = new MenuForm();
            menuForm.ShowDialog();
            //this.Close();
        }

        void GameLoop(object sender, EventArgs e)
        {
            player.Move(keysPressed, this.ClientSize, p => currentLevel.IsWalkable(p, tileSize));

            // Update peluru
            for (int i = projectiles.Count - 1; i >= 0; i--)
            {
                projectiles[i].Update();
                if (projectiles[i].IsOffScreen(this.ClientSize))
                    projectiles.RemoveAt(i);
            }

            for (int i = projectiles.Count - 1; i >= 0; i--)
            {
                projectiles[i].Update();

                if (projectiles[i].IsOffScreen(this.ClientSize) || currentLevel.IsWall(projectiles[i].Position, tileSize))
                {
                    projectiles.RemoveAt(i);
                }
            }

            for (int i = projectiles.Count - 1; i >= 0; i--)
            {
                projectiles[i].Update();

                // Cek tembok
                if (projectiles[i].IsOffScreen(this.ClientSize) || currentLevel.IsWall(projectiles[i].Position, tileSize))
                {
                    projectiles.RemoveAt(i);
                    continue;
                }

                // Cek musuh
                bool hitEnemy = false;
                for (int j = enemies.Count - 1; j >= 0; j--)
                {
                    if (enemies[j].GetBounds().Contains(projectiles[i].Position))
                    {
                        enemies[j].Health -= 1;
                        if (enemies[j].Health <= 0)
                            enemies.RemoveAt(j);

                        hitEnemy = true;
                        break;
                    }
                }

                if (hitEnemy)
                {
                    projectiles.RemoveAt(i);
                }
            }

            foreach (var enemy in enemies)
            {
                enemy.Update(player.Position);
            }

            foreach (var enemy in enemies)
            {
                enemy.Update(player.Position, p => currentLevel.IsWalkable(p, tileSize));
            }

            foreach (var enemy in enemies)
            {
                enemy.Update(player.Position, p => currentLevel.IsWalkable(p, tileSize));

                if (CheckCollision(player.GetBounds(), enemy.GetBounds()))
                {
                    player.TakeDamage(1);
                }
            }


            this.Invalidate();
        }

        void DrawGame(object sender, PaintEventArgs e)
        {
            currentLevel.Draw(e.Graphics);
            player.Draw(e.Graphics);

            foreach (var bullet in projectiles)
            {
                bullet.Draw(e.Graphics);
            }

            foreach (var enemy in enemies)
            {
                enemy.Draw(e.Graphics);
            }

            DrawHUD(e.Graphics);
        }

        private bool CheckCollision(RectangleF a, RectangleF b)
        {
            return a.IntersectsWith(b);
        }

        void DrawHUD(Graphics g)
        {
            string hpText = $"HP: {player.Health}";
            Font font = new Font("Arial", 16);
            Brush brush = Brushes.White;

            g.DrawString(hpText, font, brush, new PointF(10, 10));
        }

    }
}
