using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Project
{
    class Player
    {
        public int id;
        public int x, y;
        public int size_x, size_y;
        public int velocity_x, velocity_y;
        public int lives;
        public int bonus;

        public string name;
        Random rnd = new Random();


        public Player()
        {
            x = 350;
            y = 200;

            size_x = 60;
            size_y = 90;

            velocity_x = 12;
            velocity_y = 12;

            lives = 3;
            bonus = 0;

            name = "";

            DrawBin();
        }


        public void DrawBin()
        {
            id = rnd.Next(1, 6);


            if (id == 1)
                name = "blue_bin.png";

            else if (id == 2)
                name = "brown_bin.png";

            else if (id == 3)
                name = "green_bin.png";

            else if (id == 4)
                name = "red_bin.png";

            else if (id == 5)
                name = "yellow_bin.png";
        }

        
        public bool CollisionDetected (Trash trash, bool status)
        {
            // top left corner
            if ((y > trash.y && y < trash.y + trash.size_y) && (x > trash.x && x < trash.x + trash.size_x))
                status = false;

            // top right corner
            else if ((y > trash.y && y < trash.y + trash.size_y) && (x + size_y > trash.x && x + size_y < trash.x + trash.size_x))
                status = false;

            // bottom left corner
            else if ((y + size_y > trash.y && y + size_y < trash.y + trash.size_y) && (x > trash.x && x < trash.x + trash.size_x))
                status = false;

            // bottom right corner
            else if ((y + size_y > trash.y && y + size_y < trash.y + trash.size_y) && (x + size_x > trash.x && x + size_x < trash.x + trash.size_x))
                status = false;
            
            // center
            else if ((x + size_x/2 > trash.x && x + size_x/2 < trash.x + trash.size_x) && (y + size_y / 2 > trash.y && y + size_y / 2 < trash.y + trash.size_y))
                status = false;

            return status;
        }

    } 
}
