﻿using System;
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
        //public List<Pawn> pawns = new List<Pawn>();

        public Square currentlySelectedSquare;

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
                var whitePawns = new List<WhiteMan>();
                for (int i = 0; i < 12; i++)
                    whitePawns.Add(new WhiteMan(squares));

                (squares[0][0] as BrownSquare).Pawn = whitePawns[0];
                (squares[0][2] as BrownSquare).Pawn = whitePawns[1];
                (squares[0][4] as BrownSquare).Pawn = whitePawns[2];
                (squares[0][6] as BrownSquare).Pawn = whitePawns[3];

                (squares[1][1] as BrownSquare).Pawn = whitePawns[4];
                (squares[1][3] as BrownSquare).Pawn = whitePawns[5];
                (squares[1][5] as BrownSquare).Pawn = whitePawns[6];
                (squares[1][7] as BrownSquare).Pawn = whitePawns[7];

                (squares[2][0] as BrownSquare).Pawn = whitePawns[8];
                (squares[2][2] as BrownSquare).Pawn = whitePawns[9];
                (squares[2][4] as BrownSquare).Pawn = whitePawns[10];
                (squares[2][6] as BrownSquare).Pawn = whitePawns[11];

                //pawns.AddRange(whitePawns);
            }
            else
            {
                throw new NotImplementedException("This settup (black starting on top) not coded");
            }
        }

        /// <summary>
        /// Returns if move was valid
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public bool MovePawn(Square start, Square end)
        {
            //Brown square can not contain pawns
            if(!(start is BrownSquare))
            {
                return false;
            }
            BrownSquare s = (start as BrownSquare);
            //No pawn in start square
            if (s.Pawn == null)
                return false;
            return s.Pawn.Move(end);
            //s.pawn.Move()
        }

        private void AddBlackPawns()
        {
            if(blackPawnsAtBeginningOnBottom)
            {
                var blackPawns = new List<BlackMan>();
                for (int i = 0; i < 12; i++)
                    blackPawns.Add(new BlackMan(squares));

                (squares[7][1] as BrownSquare).Pawn = blackPawns[0];
                (squares[7][3] as BrownSquare).Pawn = blackPawns[1];
                (squares[7][5] as BrownSquare).Pawn = blackPawns[2];
                (squares[7][7] as BrownSquare).Pawn = blackPawns[3];

                (squares[6][0] as BrownSquare).Pawn = blackPawns[4];
                (squares[6][2] as BrownSquare).Pawn = blackPawns[5];
                (squares[6][4] as BrownSquare).Pawn = blackPawns[6];
                (squares[6][6] as BrownSquare).Pawn = blackPawns[7];

                (squares[5][1] as BrownSquare).Pawn = blackPawns[8];
                (squares[5][3] as BrownSquare).Pawn = blackPawns[9];
                (squares[5][5] as BrownSquare).Pawn = blackPawns[10];
                (squares[5][7] as BrownSquare).Pawn = blackPawns[11];

                //pawns.AddRange(blackPawns);
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
