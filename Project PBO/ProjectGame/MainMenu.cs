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
            this.ClientSize = new Size(1366, 768);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.Black; // Set a background color for better visibility
            //this.BackgroundImage = Image.FromFile("path_to_your_background_image.jpg"); // Set your background image path here
        }

        public void InitializeControls()
        {
            startButton = new Button
            {
                Text = "START",
                Size = new Size(150, 50),
                Location = new Point(600, 300),
                ForeColor = Color.White,
            };
            //startButton.Location = new Point(
            //    (this.ClientSize.Width - startButton.Width) / 2,
            //    (this.ClientSize.Height - startButton.Height) / 2
            //);
            startButton.Click += StartButton_Click;
            Controls.Add(startButton);

            ExitButton = new Button
            {
                Text = "QUIT GAME",
                Location = new Point(600, 400),
                Size = new Size(150, 50),
                ForeColor = Color.White,
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
