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
    public partial class Form1 : Form
    {
        Start_form startForm = new Start_form();

        public static Label lbl, lbl_players, lbl_player, lbl_bot;
        PictureBox card_one, card_two, card_third, card_fourth, extra_card, extra_card_2, koloda;
        Button take_card, pass, giveUp, start_game;
        List<string> listOfcards, listOffiles;
        List<int> listOfpoints;
        public int rand_card_one, rand_card_two, rand_card_three, rand_card_four, rand_card_extra, point_player, point_bot, file_three, file_four, file_extra_2;
        Random rnd;

        public Form1()
        {
            this.Height = 600;
            this.Width = 800;

            this.BackgroundImage = Properties.Resources.blackjack_background;
            this.Icon = Properties.Resources.game_playing_cards_card_cards_poker_1488;
            this.Text = "Black Jack";

            listOffiles = new List<string>() {"Bubna", "Chervi", "Kresta", "Pika"};

            listOfcards = new List<string>() {"1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13"};

            listOfpoints = new List<int>() { 11, 2, 3, 4, 5, 6, 7, 8, 9, 10, 2, 3, 4 };


            lbl = new Label()
            {
                Text = "Welcome to Black Jack!",
                AutoSize = true,
                Location = new Point(250, 75),
                Font = new Font(Font.FontFamily, 20),
                BackColor = Color.Transparent
            };

            lbl_player = new Label()
            {
                Text = "sd",
                Location = new Point(150, 120),
                BackColor = Color.Transparent,
                AutoSize = true,
                Font = new Font(Font.FontFamily, 10),
            };

            lbl_bot = new Label() 
            { 
                Text = "Bot Zhora",
                Location = new Point(560, 120),
                BackColor = Color.Transparent,
                AutoSize = true,
                Font = new Font(Font.FontFamily, 10),
            };


            lbl_players = new Label()
            {
                Text = "0 : 0",
                AutoSize = true,
                Location = new Point(350, 190),
                Font = new Font(Font.FontFamily, 30),
                BackColor = Color.Transparent
            };

            card_one = new PictureBox()
            {
                Size = new Size(85, 129),
                Location = new Point(100, 150),
                BackgroundImage = Properties.Resources.player
            };
            card_one.SizeMode = PictureBoxSizeMode.StretchImage;

            card_two = new PictureBox()
            {
                Size = new Size(85, 129),
                Location = new Point(200, 150),
                BackgroundImage = Properties.Resources.player
            };
            card_two.SizeMode = PictureBoxSizeMode.StretchImage;

            card_third = new PictureBox()
            {
                Size = new Size(85, 129),
                Location = new Point(500, 150),
                BackgroundImage = Properties.Resources.bot
            };
            card_third.SizeMode = PictureBoxSizeMode.StretchImage;

            card_fourth = new PictureBox()
            {
                Size = new Size(85, 129),
                Location = new Point(600, 150),
                BackgroundImage = Properties.Resources.bot
            };
            card_fourth.SizeMode = PictureBoxSizeMode.StretchImage;

            extra_card = new PictureBox()
            {
                Size = new Size(85, 129),
                Location = new Point(150, 300),
                BackgroundImage = Properties.Resources.player
            };
            extra_card.AllowDrop = false;
            extra_card.DragDrop += Extra_card_DragDrop;
            extra_card.DragEnter += GovnoKoloda_DragEnter;
            extra_card.SizeMode = PictureBoxSizeMode.StretchImage;

            extra_card_2 = new PictureBox()
            {
                Size = new Size(85, 129),
                Location = new Point(550, 300),
                BackgroundImage = Properties.Resources.bot
            };
            extra_card_2.SizeMode = PictureBoxSizeMode.StretchImage;

            koloda = new PictureBox()
            {
                Size = new Size(150, 120),
                Location = new Point(315, 300),
                BackgroundImage = Properties.Resources.trollface
            };
            koloda.MouseDown += GovnoKoloda_MouseDown;
            koloda.DragEnter += GovnoKoloda_DragEnter;
            koloda.AllowDrop = false;
            koloda.SizeMode = PictureBoxSizeMode.StretchImage;

            start_game = new Button()
            {
                Text = "Start the game",
                Size = new Size(150, 50),
                Location = new Point(315, 450),
                Font = new Font(Font.FontFamily, 15),
                BackColor = Color.Gray
            };
            start_game.Click += Start_game_Click;

            //----------------Main Buttons----------------

            /*take_card = new Button()
            {
                Text = "Take Cards",
                Size = new Size(150, 50),
                Location = new Point(315, 450),
                Font = new Font(Font.FontFamily, 15),
                BackColor = Color.FromArgb(102, 204, 0)
            };
            take_card.Click += Take_card_Click;*/

            pass = new Button()
            {
                Text = "Pass",
                Size = new Size(150, 50),
                Location = new Point(150, 450),
                Font = new Font(Font.FontFamily, 15),
                BackColor = Color.Gray
            };
            pass.Click += Pass_Click;

            giveUp = new Button()
            {
                Text = "Give up",
                Size = new Size(150, 50),
                Location = new Point(480, 450),
                Font = new Font(Font.FontFamily, 15),
                BackColor = Color.Red
            };
            giveUp.Click += GiveUp_Click;

            this.Controls.Add(koloda);

            this.Controls.Add(card_one);
            this.Controls.Add(card_two);
            this.Controls.Add(card_third);
            this.Controls.Add(card_fourth);
            this.Controls.Add(extra_card);
            this.Controls.Add(extra_card_2);
            this.Controls.Add(lbl);
            this.Controls.Add(lbl_players);
            this.Controls.Add(start_game);
            this.Controls.Add(lbl_bot);
            this.Controls.Add(lbl_player);
            this.GiveFeedback += Form1_GiveFeedback;
        }

        private void Form1_GiveFeedback(object sender, GiveFeedbackEventArgs e)
        {
            e.UseDefaultCursors = false;
        }

        private void Extra_card_DragDrop(object sender, DragEventArgs e)
        {
            rnd = new Random();
            //sdelaj chtobi karta pojavlalas
            int file_one = rnd.Next(listOffiles.Count);

            rand_card_one = rnd.Next(listOfcards.Count);
            extra_card.BackgroundImage = new Bitmap($@"../../{listOffiles[file_one]}/{listOfcards[rand_card_one]}.png");

            int bot_chance = rnd.Next(1, 100);

            if (point_bot <= 10)
            {
                if (bot_chance < 100)
                {
                    Bot();
                }
                else
                {
                    card_third.BackgroundImage = Properties.Resources.bot;
                    card_fourth.BackgroundImage = Properties.Resources.bot;
                }
            }

            else if (point_bot > 10)
            {
                if (bot_chance < 85)
                {
                    Bot();
                }
                else
                {
                    card_third.BackgroundImage = Properties.Resources.bot;
                    card_fourth.BackgroundImage = Properties.Resources.bot;
                }
            }

            else if (point_bot >= 15)
            {
                if (bot_chance < 60)
                {
                    Bot();
                }
                else
                {
                    card_third.BackgroundImage = Properties.Resources.bot;
                    card_fourth.BackgroundImage = Properties.Resources.bot;
                }
            }

            point_player += listOfpoints[rand_card_one];
            lbl_players.Text = $"{point_player} : ?";

            if (point_bot >= 21)
            {
                Win();
            }
            else if (point_player >= 21)
            {
                Win();
            }
        }

        private void GovnoKoloda_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Bitmap))
                e.Effect = DragDropEffects.Move;
            Bitmap curs = Properties.Resources.bot;
            curs.MakeTransparent(Color.White);
            Cursor cur = new Cursor(curs.GetHicon());
            Cursor.Current = cur;
        }

        private void GovnoKoloda_MouseDown(object sender, MouseEventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            var img = pb.BackgroundImage;
            //if (img == null) return;
            DoDragDrop(img, DragDropEffects.Move);
        }

        private void Start_game_Click(object sender, EventArgs e)
        {
            extra_card.AllowDrop = true;
            koloda.AllowDrop = true;

            card_third.BackgroundImage = Properties.Resources.bot;
            card_fourth.BackgroundImage = Properties.Resources.bot;
            extra_card.BackgroundImage = Properties.Resources.player;
            extra_card_2.BackgroundImage = Properties.Resources.bot;

            start_game.Hide();
            //take_card.Show();
            pass.Show();
            giveUp.Show();

            rnd = new Random();

            //-------------------------------PictureBox one-------------------------------
            int file_one = rnd.Next(listOffiles.Count);

            rand_card_one = rnd.Next(listOfcards.Count);
            card_one.BackgroundImage = new Bitmap($@"../../{listOffiles[file_one]}/{listOfcards[rand_card_one]}.png");

            //-------------------------------PictureBox two-------------------------------

            int file_two = rnd.Next(listOffiles.Count);

            rand_card_two = rnd.Next(listOfcards.Count);
            card_two.BackgroundImage = new Bitmap($@"../../{listOffiles[file_two]}/{listOfcards[rand_card_two]}.png");

            //-------------------------------PictureBox three-------------------------------

            int file_three = rnd.Next(listOffiles.Count);

            rand_card_three = rnd.Next(listOfcards.Count);
            /*card_third.BackgroundImage = new Bitmap($@"../../{listOffiles[file_three]}/{listOfcards[rand_card_three]}.png");*/

            //-------------------------------PictureBox four-------------------------------

            int file_four = rnd.Next(listOffiles.Count);

            rand_card_four = rnd.Next(listOfcards.Count);
            /*card_fourth.BackgroundImage = new Bitmap($@"../../{listOffiles[file_four]}/{listOfcards[rand_card_four]}.png");*/

            point_bot += listOfpoints[rand_card_three] + listOfpoints[rand_card_four];

            point_player += listOfpoints[rand_card_one] + listOfpoints[rand_card_two];
            lbl_players.Text = $"{point_player} : ?";

            this.Controls.Add(take_card);
            this.Controls.Add(pass);
            this.Controls.Add(giveUp);
        }

        private void Bot()
        {
            if (point_bot < 17)
            {
                file_extra_2 = rnd.Next(listOffiles.Count);
                rand_card_extra = rnd.Next(listOfcards.Count);
                point_bot += listOfpoints[rand_card_extra];
            }            
        }

        private void gusj()
        {
            card_third.BackgroundImage = new Bitmap($@"../../{listOffiles[file_three]}/{listOfcards[rand_card_three]}.png");
            card_fourth.BackgroundImage = new Bitmap($@"../../{listOffiles[file_four]}/{listOfcards[rand_card_four]}.png");
            extra_card_2.BackgroundImage = new Bitmap($@"../../{listOffiles[file_extra_2]}/{listOfcards[rand_card_extra]}.png");

            lbl_players.Text = $"{point_player} : {point_bot}";
            //take_card.Hide();
            pass.Hide();
            giveUp.Hide();
            start_game.Show();

            extra_card.AllowDrop = false;
            koloda.AllowDrop = false;
        }

        private void Pass_Win()
        {
            if (point_player > point_bot)
            {
                gusj();
                MessageBox.Show("You win!");
                point_player = 0; point_bot = 0;
            }

            else if (point_player < point_bot)
            {
                gusj();
                MessageBox.Show("You lose!");
                point_player = 0; point_bot = 0;
            }
            else
            {
                gusj();
                MessageBox.Show("Draw!");
                point_player = 0; point_bot = 0;
            }
        }

        private void Win()
        {
            if ((point_player > 21 && point_bot > 21) || (point_player == point_bot))
            {
                gusj();
                MessageBox.Show("Draw!");
                point_player = 0; point_bot = 0;
            }

            else if((point_player == 21 && point_bot > 21) || (point_player == 21 && point_bot < 21))
            {
                gusj();
                MessageBox.Show("You win!");
                point_player = 0; point_bot = 0;   
            }

            else if ((point_bot == 21 && point_player > 21) || (point_bot == 21 && point_player < 21))
            {
                gusj();
                MessageBox.Show("You lose!");
                point_player = 0; point_bot = 0;
            }
            
            else if (point_player > 21 && point_bot <= 21)
            {
                gusj();
                MessageBox.Show("You lose!");
                point_player = 0; point_bot = 0;
            }

            else if (point_bot > 21 && point_player <= 21)
            {
                gusj();
                MessageBox.Show("You win!");
                point_player = 0; point_bot = 0;
            }           
        }

        /*private void Take_card_Click(object sender, EventArgs e)
        {
            rnd = new Random();

            //-------------------------------PictureBox one-------------------------------
            int file_one = rnd.Next(listOffiles.Count);

            rand_card_one = rnd.Next(listOfcards.Count);
            extra_card.BackgroundImage = new Bitmap($@"../../{listOffiles[file_one]}/{listOfcards[rand_card_one]}.png");           
            
            int bot_chance = rnd.Next(1, 100);

            if (point_bot <= 10)
            {
                if (bot_chance < 100)
                {
                    Bot();
                }
                else
                {
                    card_third.BackgroundImage = Properties.Resources.bot;
                    card_fourth.BackgroundImage = Properties.Resources.bot;
                }
            }

            else if (point_bot > 10)
            {
                if (bot_chance < 85)
                {
                    Bot();
                }
                else
                {
                    card_third.BackgroundImage = Properties.Resources.bot;
                    card_fourth.BackgroundImage = Properties.Resources.bot;
                }
            }

            else if (point_bot >= 15)
            {
                if (bot_chance < 60)
                {
                    Bot();
                }
                else
                {
                    card_third.BackgroundImage = Properties.Resources.bot;
                    card_fourth.BackgroundImage = Properties.Resources.bot;
                }
            }

            point_player += listOfpoints[rand_card_one];
            lbl_players.Text = $"{point_player} : ?";

            if (point_bot >= 21)
            {
                Win();
            }
            else if (point_player >= 21)
            {
                Win();
            }
            
        }*/

        private void Pass_Click(object sender, EventArgs e)
        {
            Pass_Win();
            lbl_players.Text = $"{point_player} : {point_bot}";           
        }

        private void GiveUp_Click(object sender, EventArgs e)
        {
            point_player = 22;
            Win();
        }
    }
}