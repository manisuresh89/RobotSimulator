using System;
using System.Collections.Generic;
using System.Text;

namespace ANZ.ToyRobot.Model
{
    public class Surface
    {
        public int Length { get; set; }
        public int Width { get; set; }

        public Surface(int width, int length)
        {
            this.Width = width;
            this.Length = length;
        }

        public bool isValidPosition(int positionX, int positionY)
        {

            return ((positionX >= 0 && positionX <= Width) && (positionY >= 0 && positionY <= Length));
        }
    }
}
