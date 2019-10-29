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
        //public List<Pawn> pawns = new List<Pawn>();

        private BrownSquare selectedSquareAsStart;
        public List<BrownSquare> selectedSquaresToEnd { get; private set; } = new List<BrownSquare>();
        /// <summary>
        /// Indicates which (black or white) turn it is now
        /// </summary>
        public bool IsBlackTurn = true;
        public string TurnMessage
        {
            get
            {
                if (IsBlackTurn)
                    return blackTurnText;
                else
                    return whiteTurnText;
            }
        }

        public BrownSquare GetSelectedSquareAsStart()
        {
            return selectedSquareAsStart;
        }

        public void SetSelectedSquareAsStart(BrownSquare value)
        {
            if (value == null)
                selectedSquareAsStart = null;
            else if (IsBlackTurn && (value.Pawn is BlackDame || value.Pawn is BlackMan))
                selectedSquareAsStart = value;
            else if (IsBlackTurn == false && (value.Pawn is WhiteDame || value.Pawn is WhiteMan))
                selectedSquareAsStart = value;
        }

        private const string blackTurnText = "BLACK TURN";
        private const string whiteTurnText = "WHITE TURN";

        private const string notValidMoveText = "NOT VALID MOVE";

        public bool blackPawnsAtBeginningOnBottom = true;
        public Board()
        {
            CreateSquares();
            AddBlackPawns();
            AddWhitePawns();
        }

        public void ResetMove()
        {
            SetSelectedSquareAsStart(null);
            selectedSquaresToEnd = new List<BrownSquare>();
        }

        public void AcceptMove()
        {
            if(GetSelectedSquareAsStart() != null && selectedSquaresToEnd.Count > 0)
            {
                //First case: accepting only one move
                if(selectedSquaresToEnd.Count == 1)
                {
                    if(CanMovePawn(GetSelectedSquareAsStart(), selectedSquaresToEnd[0]))
                        MovePawn(GetSelectedSquareAsStart(), selectedSquaresToEnd[0]);
                }
                //More than 1 move in one turn -> every move HAS TO be a takedown
                else
                {
                    //ten przypadek do przemyślenia
                    bool correctMove = true;
                    BrownSquare firstSquare = GetSelectedSquareAsStart();

                    Pawn pawn = selectedSquareAsStart.Pawn;
                    //keep the reference to pawn position
                    //Square pawnPos = pawn.position;
                    BrownSquare toBeRemoved = firstSquare;
                    for(int i = 0;i<selectedSquaresToEnd.Count;i++)
                    {
                        if(pawn.IsTakedownMove(selectedSquaresToEnd[i]))
                        {
                            //if (i == 0)
                            //    firstSquare.Pawn = null;
                            //else
                            //    selectedSquaresToEnd[i - 1].Pawn = null;
                            toBeRemoved.Pawn = null;
                            selectedSquaresToEnd[i].Pawn = pawn;
                            toBeRemoved = selectedSquaresToEnd[i];
                        }
                        //Not valid move
                        else
                        {
                            //if (i != 0)
                            //    selectedSquaresToEnd[i - 1].Pawn = null;
                            correctMove = false;
                            break;
                        }
                    }
                    
                    //Trzeba przywrócić pozycję pionka
                    if(correctMove == false)
                    {
                        toBeRemoved.Pawn = null;
                        firstSquare.Pawn = pawn;
                        pawn.takedown = new List<Pawn>();
                    }
                    //Trzeba usunąć pionki przez które przeskakiwał (o ile nie będzie tego w metodzie)
                    else
                    {
                        IsBlackTurn = !IsBlackTurn;
                        Takedown(pawn.takedown);
                        pawn.takedown = new List<Pawn>();
                    }
                    //Create another method Pawn.MoveIsTakeDown and iterate
                    //+ change every move start and end square
                }
            }
            //Reset selected squares no matter the result
            SetSelectedSquareAsStart(null);
            selectedSquaresToEnd = new List<BrownSquare>();
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
        private bool CanMovePawn(BrownSquare start, BrownSquare end)
        {
            //No pawn in start square
            if (start.Pawn == null)
                return false;
            if(IsBlackTurn)
            {
                if (start.Pawn is WhiteDame || start.Pawn is WhiteMan)
                    return false;
            }
            else
            {
                if (start.Pawn is BlackDame || start.Pawn is BlackMan)
                    return false;
            }
            if (start.Pawn.position == end)
                return false;
            return start.Pawn.CanMove(end);
        }

        public void OnInteraction(BrownSquare square)
        {
            if (GetSelectedSquareAsStart() == null)
                SetSelectedSquareAsStart(square);
            else
            {
                selectedSquaresToEnd.Add(square);
            }
        }

        /// <summary>
        /// Can be regular or takedown move
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        private void MovePawn(BrownSquare start, BrownSquare end)
        {
            Pawn pawn = start.Pawn;
            start.Pawn = null;
            end.Pawn = pawn;
            //Switch after correct move (no double [and more] take implemented yet)
            IsBlackTurn = !IsBlackTurn;
            Takedown(pawn.takedown);
            pawn.takedown = new List<Pawn>();
            return;
        }

        public void Takedown(List<Pawn> pawns)
        {
            foreach(Square[] square_row in squares)
            {
                foreach(Square square in square_row)
                {
                    if (square is BrownSquare)
                    {
                        if (pawns.Contains((square as BrownSquare).Pawn))
                        {
                            (square as BrownSquare).Pawn = null;
                        }
                    }
                }
            }

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
