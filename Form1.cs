using System.Media; //audio files
//using System.Windows.Forms;
//using System.Windows.Forms.Automation;
//using System.IO;    // read/write file
//using static System.Net.Mime.MediaTypeNames;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement;
//using Application = System.Windows.Forms.Application;

namespace Project
{
    enum Position
    {
        None, Left, Right, Up, Down
    }


    public partial class Form1 : Form
    {
        static int score;
        int bestScore, clickCounter;        
        bool status;
        
        Position objPosition;
        
        SoundPlayer soundPlayer = new SoundPlayer();

        Player player = new Player();
        Trash trash = new Trash(score);
        System.Drawing.Image playerImage;
        System.Drawing.Image trashImage;


        public Form1()
        {
            InitializeComponent();

            status = true;
            score = 0;
            bestScore = 0;
            clickCounter = 0;

            label1.Text = "Score: " + score.ToString();
            label2.Text = "Lives: " + player.lives.ToString();

            playerImage = new Bitmap("graphics/" + player.name);
            trashImage = new Bitmap("graphics/" + trash.name);
        }


        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(playerImage, player.x, player.y, player.size_x, player.size_y);
            e.Graphics.DrawImage(trashImage, trash.x, trash.y, trash.size_x, trash.size_y);
        }


        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
                objPosition = Position.Left; 

            else if (e.KeyCode == Keys.Right)
                objPosition = Position.Right; 

            else if (e.KeyCode == Keys.Up)
                objPosition = Position.Up; 

            else if (e.KeyCode == Keys.Down)
                objPosition = Position.Down;
        }


        private void timer_Tick(object sender, EventArgs e)
        {
            if (status == true)
            {

                if (objPosition == Position.Right && player.x < 665)                
                    player.x += player.velocity_x + player.bonus;

                else if (objPosition == Position.Left && player.x > 5)
                    player.x -= player.velocity_x + player.bonus;

                else if (objPosition == Position.Up && player.y > 10)
                    player.y -= player.velocity_y + player.bonus;

                else if (objPosition == Position.Down && player.y < 305)
                    player.y += player.velocity_y + player.bonus;

                trash.x += trash.velocity_x;
                trash.y += trash.velocity_y;

                if (trash.x > 750 || trash.x < 0 - trash.size_x || trash.y > 450 || trash.y < 0 - trash.size_x)
                {
                    if (player.id == trash.id)
                        score -= 3;

                    else
                        ++score;

                    player.bonus = score / 5;
                    Renew();
                }

                Collision();
                Invalidate();
            }
        }


        public void Collision()
        {
            if (player.CollisionDetector(trash, status) == false)
            {
              
                if (player.id == trash.id)
                {
                    score += 5;

                    soundPlayer.SoundLocation = "sounds/sound1.wav";

                    try
                    {
                        soundPlayer.Play();
                    }

                    catch
                    {
                        MessageBox.Show("Couldn't find music file", "Error");
                    }
                }
                    

                else
                {
                    --player.lives;

                    soundPlayer.SoundLocation = "sounds/sound2.wav";

                    try
                    {
                        soundPlayer.Play();
                    }

                    catch
                    {
                        MessageBox.Show("Couldn't find music file", "Error");
                    }

                    if (player.lives == 0)
                    {
                        status = false;
                        label2.Text = "Lives: " + player.lives.ToString();

                        SetBestScore();

                        MessageBox.Show("GAME OVER" + "\n" + "---------------" + "\n" + "Best score: " + bestScore.ToString() + "\n" + "Score: " + score.ToString(), ":(");
                        DialogResult dialog = MessageBox.Show("Do you want to play again?", "Restart", MessageBoxButtons.YesNo);

                        if (dialog == DialogResult.Yes)                     
                             Application.Restart();

                        else if (dialog == DialogResult.No)
                            Application.Exit();
                    }

                }

                Renew();
                status = true;
            }
        }


        public void Renew()
        {
            if (score < 0)
                score = 0;

            label1.Text = "Score: " + score.ToString();
            label2.Text = "Lives: " + player.lives.ToString();

            trash.DrawTrash(score);
            trashImage = new Bitmap("graphics/" + trash.name);

            GC.Collect();
        }


        public void SetBestScore()
        {
            try
            {
                StreamReader sr = new StreamReader("data.txt");
                
                bestScore = Int32.Parse(sr.ReadLine() ?? string.Empty);
                sr.Close();
                
                StreamWriter sw = new StreamWriter("data.txt");

                if (score > bestScore)
                    bestScore = score;

                sw.WriteLine(bestScore);
                sw.Close();
            }

            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }


        private void label3_Click(object sender, EventArgs e)
        {
            if(clickCounter % 2 == 0)
            {
                label3.Text = "Play";
                pictureBox1.Visible = true;
                status = false;
                
            }

            else
            {
                label3.Text = "Tutorial";
                pictureBox1.Visible = false;
                status = true;
            }

            ++clickCounter;
        }
        


    }
}