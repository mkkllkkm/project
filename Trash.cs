using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace Project
{
    internal class Trash
    { 
        public int id;
        public int startSide;
        public int x, y;
        public int size_x, size_y;
        public int velocity_x, velocity_y;
        public int bonus;

        public string name;
        Random rnd = new Random();


        public Trash(int score)
        {
            name = "";
            DrawTrash(score);
        }


        public void DrawTrash(int score)
        {
            id = rnd.Next(1, 6);

            if (id == 1)
                name = "newspaper.png";

            else if (id == 2)
                name = "apple_core.png";

            else if (id == 3)
                name = "glass_bottle.png";

            else if (id == 4)
                name = "can.png";

            else if (id == 5)
                name = "plastic_bottle.png";



            startSide = rnd.Next(1, 5);
            bonus = score / 5;

            switch (startSide)
            {
                case 1:
                    x = rnd.Next(100, 600);
                    y = -5;
                    velocity_x = 0 ;
                    velocity_y = rnd.Next(5 + bonus, 10 + bonus);
                    break;

                case 2:
                    x = 750;
                    y = rnd.Next(100, 300);
                    velocity_x = -rnd.Next(10 + bonus, 15 + bonus);
                    velocity_y = 0;
                    break;

                case 3:
                    x = rnd.Next(100, 600);
                    y = 410;
                    velocity_x = 0;
                    velocity_y = -rnd.Next(5 + bonus, 10 + bonus);
                    break;

                case 4:
                    x = 0;
                    y = rnd.Next(100, 300);
                    velocity_x = rnd.Next(10 + bonus, 15 + bonus);
                    velocity_y = 0;
                    break;

            }

            size_x = rnd.Next(50, 100);
            size_y = rnd.Next(50, 100);
        }
    }
}
