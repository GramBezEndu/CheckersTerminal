using System;
using System.Collections.Generic;
using System.Text;

namespace CheckersLogic
{
    public class Board
    {
        /// <summary>
        /// Starting from top left corner
        /// </summary>
        public Square[][] squares = new Square[8][];
        public List<Pawn> pawns = new List<Pawn>();
        public bool blackPawnsAtBeginningOnBottom = true;
        public Board()
        {
            CreateSquares();
            AddBlackPawns();
            AddWhitePawns();
        }

        private void AddWhitePawns()
        {
            if(blackPawnsAtBeginningOnBottom)
            {
                var whitePawns = new List<WhiteMan>()
                {
                    new WhiteMan(),
                    new WhiteMan(),
                    new WhiteMan(),
                    new WhiteMan(),
                    new WhiteMan(),
                    new WhiteMan(),
                    new WhiteMan(),
                    new WhiteMan(),
                    new WhiteMan(),
                    new WhiteMan(),
                    new WhiteMan(),
                    new WhiteMan(),
                };
                whitePawns[0].position = squares[0][0];
                whitePawns[1].position = squares[0][2];
                whitePawns[2].position = squares[0][4];
                whitePawns[3].position = squares[0][6];

                whitePawns[4].position = squares[1][1];
                whitePawns[5].position = squares[1][3];
                whitePawns[6].position = squares[1][5];
                whitePawns[7].position = squares[1][7];

                whitePawns[8].position = squares[2][0];
                whitePawns[9].position = squares[2][2];
                whitePawns[10].position = squares[2][4];
                whitePawns[11].position = squares[2][6];

                pawns.AddRange(whitePawns);
            }
            else
            {
                throw new NotImplementedException("This settup (black starting on top) not coded");
            }
        }

        private void AddBlackPawns()
        {
            if(blackPawnsAtBeginningOnBottom)
            {
                var blackPawns = new List<BlackMan>()
                {
                    new BlackMan(),
                    new BlackMan(),
                    new BlackMan(),
                    new BlackMan(),
                    new BlackMan(),
                    new BlackMan(),
                    new BlackMan(),
                    new BlackMan(),
                    new BlackMan(),
                    new BlackMan(),
                    new BlackMan(),
                    new BlackMan(),
                };
                blackPawns[0].position = squares[7][1];
                blackPawns[1].position = squares[7][3];
                blackPawns[2].position = squares[7][5];
                blackPawns[3].position = squares[7][7];

                blackPawns[4].position = squares[6][0];
                blackPawns[5].position = squares[6][2];
                blackPawns[6].position = squares[6][4];
                blackPawns[7].position = squares[6][6];

                blackPawns[8].position = squares[5][1];
                blackPawns[9].position = squares[5][3];
                blackPawns[10].position = squares[5][5];
                blackPawns[11].position = squares[5][7];

                pawns.AddRange(blackPawns);
            }
            else
            {
                throw new NotImplementedException("This settup (black starting on top) not coded");
            }
        }

        private void CreateSquares()
        {
            for (int i = 0; i < 8; i++)
            {
                squares[i] = new Square[8];
                for (int j = 0; j < 8; j++)
                {
                    //if this row is odd number (starting from top)
                    if(i % 2 ==0)
                    {
                        //brown first
                        if (j % 2 == 0)
                            squares[i][j] = new BrownSquare(i, j);
                        //then white
                        else
                            squares[i][j] = new WhiteSquare(i, j);
                    }
                    //this row is even number
                    else
                    {
                        //white first
                        if (j % 2 == 0)
                            squares[i][j] = new WhiteSquare(i, j);
                        //then brown
                        else
                            squares[i][j] = new BrownSquare(i, j);
                    }
                }
            }
        }
    }
}
