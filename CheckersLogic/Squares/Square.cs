using System;
using System.Collections.Generic;
using System.Text;

namespace CheckersLogic
{
    public abstract class Square : DrawableComponent
    {
        //(x, y) indicates where the square is located on the board
        public int xIndex { get; private set; }
        public int yIndex { get; private set; }
        public Square(int x, int y)
        {
            xIndex = x;
            yIndex = y;
        }
    }
}
