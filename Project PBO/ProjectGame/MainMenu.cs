using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGame
{
    public class MainMenu : Form
    {
        private Button startButton;
        private Button ExitButton;

        public MainMenu()
        {
            InitializeControls();
            InitializeForm();
        }

        public void InitializeForm()
        {
            this.Text = "Main Menu";
            this.Size = new Size(1366, 768);
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        public void InitializeControls()
        {
            startButton = new Button
            {
                Text = "Start Game",
                Location = new Point(600, 300),
                Size = new Size(150, 50)
            };
            startButton.Click += StartButton_Click;
            Controls.Add(startButton);

            ExitButton = new Button
            {
                Text = "Exit",
                Location = new Point(600, 400),
                Size = new Size(150, 50)
            };
            ExitButton.Click += ExitButton_Click;
            Controls.Add(ExitButton);

        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            InGameForm inGameForm = new InGameForm();
            inGameForm.ShowDialog();
            this.Close();
        }
    }
}
