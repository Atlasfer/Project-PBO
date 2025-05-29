using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGame
{
    public class InGameForm : Form
    {
        private Button menuButton;

        public InGameForm()
        {
            InitializeControls();
            InitializeForm();
        }

        public void InitializeForm()
        {
            this.Text = "In-Game";
            this.Size = new Size(1366, 768);
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        public void InitializeControls()
        {
            menuButton = new Button
            {
                Text = "Menu",
                Location = new Point(1150, 30),
                Size = new Size(150, 50)
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

    }
}
