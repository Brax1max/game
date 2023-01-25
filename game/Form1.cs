using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace game
{
    public partial class Form1 : Form
    {
        Random rangen = new Random();
        Rectangle player1 = new Rectangle(295, 195, 10, 10);
        
        Rectangle Point = new Rectangle(295, 150, 10, 10);
        Rectangle Speed = new Rectangle(295, 50, 10, 10);

        int player1Score = 0;
    

        int playerSpeed = 4;
       
        int ballXSpeed = -6;
        int ballYSpeed = 6;

        bool wDown = false;
        bool sDown = false;
        bool upArrowDown = false;
        bool downArrowDown = false;
        bool aDown = false;
        bool dDown = false;
        bool rightArrowDown = false;
        bool leftArrowDown = false;

        SolidBrush blueBrush = new SolidBrush(Color.DodgerBlue);
        SolidBrush whiteBrush = new SolidBrush(Color.White);
        SolidBrush greenBrush = new SolidBrush(Color.Green);
        SolidBrush redBrush = new SolidBrush(Color.Red);

        string gameState = "waiting";
        public Form1()
        {
            InitializeComponent();
        }

        public void GameSetup()
        {
            gameState = "running";

             titleLabel.Text = "";
            subtitleLabel.Text = "";

            timer1.Enabled = true;
            //time = 500;
            //score = 0;

            //hero.X = 280;

            //balls.Clear();
            //ballSpeeds.Clear();
            //ballColours.Clear();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wDown = false;
                    break;
                case Keys.S:
                    sDown = false;
                    break;
                case Keys.Up:
                    upArrowDown = false;
                    break;
                case Keys.Down:
                    downArrowDown = false;
                    break;
                case Keys.D:
                    dDown = false;
                    break;
                case Keys.Right:
                    rightArrowDown = false;
                    break;
                case Keys.A:
                    aDown = false;
                    break;
                case Keys.Left:
                    leftArrowDown = false;
                    break;
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wDown = true;
                    break;
                case Keys.S:
                    sDown = true;
                    break;
                case Keys.Up:
                    upArrowDown = true;
                    break;
                case Keys.Down:
                    downArrowDown = true;
                    break;
                case Keys.Right:
                    rightArrowDown = true;
                    break;
                case Keys.D:
                    dDown = true;
                    break;
                case Keys.A:
                    aDown = true;
                    break;
                case Keys.Left:
                    leftArrowDown = true;
                    break;
                case Keys.Space:
                    if (gameState == "waiting" || gameState == "over")
                    {
                        GameSetup();
                    }
                    break;
                case Keys.Escape:
                    if (gameState == "waiting" || gameState == "over")
                    {
                        this.Close();
                    }
                    break;

            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (gameState == "waiting")
            {



                titleLabel.Text = "Catch Game";
                subtitleLabel.Text = "Press Space to Start or Esc to Exit";
            }
            else if (gameState == "running")
            {
                e.Graphics.FillRectangle(blueBrush, player1);
               
                e.Graphics.FillRectangle(whiteBrush, Point);
                e.Graphics.FillRectangle(redBrush, Speed);
            }
            else if (gameState == "over")
            {


                titleLabel.Text = "Game Over";
                subtitleLabel.Text = "Press Space to Start or Esc to Exit";
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //move player 1 
            if (wDown == true && player1.Y > 0)
            {
                player1.Y -= playerSpeed;
            }

            if (sDown == true && player1.Y < this.Height - player1.Height)
            {
                player1.Y += playerSpeed;
            }
            if (dDown == true && player1.X < this.Width - player1.Width)
            {
                player1.X += playerSpeed;
            }
            if (aDown == true && player1.X > 0)
            {
                player1.X -= playerSpeed;
            }


           

            //check if ball hit top or bottom wall and change direction if it does 
            if (Point.Y < 0 || Point.Y > this.Height - Point.Height)
            {
                ballYSpeed *= -1;  // or: ballYSpeed = -ballYSpeed; 
            }

            //check if ball hits either player. If it does change the direction 
            //and place the ball in front of the player hit 
            if (player1.IntersectsWith(Point))
            {
                int POINTX = rangen.Next(0, 662);
                int POINTY = rangen.Next(0, 449);
                Point.X = POINTX;
                Point.Y = POINTY;
                player1Score++;
                p1ScoreLabel.Text = $"{player1Score}";
            }
           
            if (player1.IntersectsWith(Speed))
            {
                int sppedX = rangen.Next(0, 662);
                int speedY = rangen.Next(0, 449);
                Speed.X = sppedX;
                Speed.Y = speedY;
                playerSpeed++;
            }

            //check if a player missed the ball and if true add 1 to score of other player  
            if (Point.X < 0)
            {
                player1Score++;
                p1ScoreLabel.Text = $"{player1Score}";

                Point.X = 295;
                Point.Y = 195;

                player1.Y = 170;
                

            }
            else if (Point.X > 600)
            {
                
            }

            // check score and stop game if either player is at 3 
            if (player1Score == 10)
            {
                timer1.Enabled = false;
                winLabel.Visible = true;
                winLabel.Text = "Player 1 Wins!!";
            }
           
            



            Refresh();
        }

        
        
    }
}
