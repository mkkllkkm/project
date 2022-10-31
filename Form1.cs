using System.Media;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;
using System.Windows.Forms.Automation;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Application = System.Windows.Forms.Application;

namespace Project
{
    enum Position
    {
        None, Left, Right, Up, Down
    }


    public partial class Form1 : Form
    {
        static int score;        
        bool status;
        
        Position objPosition;
        
        System.Media.SoundPlayer soundPlay = new System.Media.SoundPlayer();

        Player player = new Player();
        Trash trash = new Trash(score);
        System.Drawing.Image playerImage;
        System.Drawing.Image trashImage;


        public Form1()
        {
            InitializeComponent();

            status = true;
            score = 0;

            soundPlay.SoundLocation = "music.mp4";

            try
            {
                soundPlay.Play();
            }

            catch
            {
                MessageBox.Show("Couldn't find music file", "Error");
            }

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

                else if (objPosition == Position.Up && player.y > 35)
                    player.y -= player.velocity_y + player.bonus;

                else if (objPosition == Position.Down && player.y < 305)
                    player.y += player.velocity_y + player.bonus;

                trash.x += trash.velocity_x;
                trash.y += trash.velocity_y;

                if (trash.x > 750 || trash.x < 0 - trash.size_x || trash.y > 450 || trash.y < 0 - trash.size_x)
                {
                    if (player.id == trash.id)
                        score -= 5;

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
            if (player.CollisionDetected(trash, status) == false)
            {
              
                if (player.id == trash.id)
                    score += 5;

                else
                {
                    --player.lives;

                    if (player.lives == 0)
                    {
                        status = false;
                        label2.Text = "Lives: " + player.lives.ToString();

                        MessageBox.Show("GAME OVER" + "\n" + "---------------" + "\n" + "Score: " + score.ToString(), ":(");
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


        private void restartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            status = false;
            DialogResult dialog = MessageBox.Show("Are you sure you want to restart?", "Restart", MessageBoxButtons.YesNo);

            if (dialog == DialogResult.Yes)
                Application.Restart();

            else if (dialog == DialogResult.No)
                status = true;
        }


        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            status = false;
            DialogResult dialog = MessageBox.Show("Are you sure you want to quit?", "Exit", MessageBoxButtons.YesNo);

            if (dialog == DialogResult.Yes)
                Application.Exit();

            else if (dialog == DialogResult.No)
                status = true;
        }

    }
}