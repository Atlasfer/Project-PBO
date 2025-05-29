using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGame
{
    public class MenuForm : Form
    {
        private Button MainMenuButton;
        private Button ExitButton;
        private Button OptionButton;

        public MenuForm()
        {
            InitializeControls();
            InitializeForm();
        }

        public void InitializeForm()
        {
            this.Text = "Menu";
            this.Size = new Size(200, 300);
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        public void InitializeControls()
        {
            MainMenuButton = new Button
            {
                Text = "Menu",
                Location = new Point(40, 25),
                Size = new Size(100, 50)
            };
            MainMenuButton.Click += MainMenuButton_Click;
            Controls.Add(MainMenuButton);

            OptionButton = new Button
            {
                Text = "Options",
                Location = new Point(40, 95),
                Size = new Size(100, 50)
            };
            //OptionButton.Click += OptionButton_Click;
            Controls.Add(OptionButton);

            ExitButton = new Button
            {
                Text = "Exit",
                Location = new Point(40, 165),
                Size = new Size(100, 50)
            };
            ExitButton.Click += ExitButton_Click;
            Controls.Add(ExitButton);
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MainMenuButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainMenu mainMenu = new MainMenu();
            mainMenu.ShowDialog();
            this.Close();
        }


    }
}
