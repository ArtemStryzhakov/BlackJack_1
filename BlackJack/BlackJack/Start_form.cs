using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlackJack
{
    public partial class Start_form : Form
    {
        public TextBox txt;

        public Start_form()
        {
            this.Height = 400;
            this.Width = 500;

            this.Icon = Properties.Resources.icon_log;
            this.BackgroundImage = Properties.Resources.blackjack_background;
            this.Text = "Welcome!";

            Label lbl = new Label()
            {
                Text = "Enter your name:",
                AutoSize = true,
                Location = new Point(130, 80),
                Font = new Font(Font.FontFamily, 20),
                BackColor = Color.Transparent
            };

            txt = new TextBox()
            {
                Width = 200,
                Location = new Point(138, 140),
                MaxLength = 10
            };

            Button cont = new Button()
            {
                Text = "Continue",
                Location = new Point(160, 190),
                Size = new Size(150, 50),
                Font = new Font(Font.FontFamily, 15),
                BackColor = Color.FromArgb(102, 204, 0)
            };
            cont.Click += Cont_Click;

            this.Controls.Add(lbl);
            this.Controls.Add(txt);
            this.Controls.Add(cont);
        }

        private void Cont_Click(object sender, EventArgs e)
        {
            if (txt.Text != "")
            {
                Form1 form = new Form1();
                form.Show();
                this.Hide();

                Form1.lbl_player.Text = txt.Text;
            }
            else
            {
                MessageBox.Show("You need to write your name!");
            }
        }
    }
}
