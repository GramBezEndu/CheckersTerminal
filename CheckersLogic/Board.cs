using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public bool IsWhiteTurn = true;
        public string TurnMessage
        {
            get
            {
                if (IsWhiteTurn)
                    return whiteTurnText;
                return blackTurnText;
            }
        }

        private bool gameFinished = false;

        public BrownSquare GetSelectedSquareAsStart()
        {
            return selectedSquareAsStart;
        }

        public void SetSelectedSquareAsStart(BrownSquare value)
        {
            if (value == null || value.Pawn == null)
                selectedSquareAsStart = null;
            else if (!IsWhiteTurn && (!value.Pawn.IsWhite))
                selectedSquareAsStart = value;
            else if (IsWhiteTurn && (value.Pawn.IsWhite))
                selectedSquareAsStart = value;
        }

        private const string blackTurnText = "BLACK TURN";
        private const string whiteTurnText = "WHITE TURN";

        private const string notValidMoveText = "NOT VALID MOVE";
        /// <summary>
        /// Indicates who won game (black or white)
        /// </summary>
        private string whoWonText;

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

        /// <summary>
        /// Method checks and turns pawn into dame
        /// </summary>
        /// <param name="pawn"></param>
        public void ChangePawnToDame(Pawn pawn)
        {
            if(pawn.IsWhite)
            {
                if(pawn.Position.xIndex == squares.Length - 1)
                {
                    if(pawn is ManPawn)
                    {
                        //Turn into dame
                        Square position = pawn.Position;
                        pawn = new Dame(true);
                        (position as BrownSquare).Pawn = pawn;
                    }
                }
            }
            else if(pawn.IsWhite == false)
            {
                if (pawn.Position.xIndex == 0)
                {
                    if (pawn is ManPawn)
                    {
                        //Turn into dame
                        Square position = pawn.Position;
                        pawn = new Dame(false);
                        (position as BrownSquare).Pawn = pawn;
                    }
                }
            }
        }

        public bool AcceptMove()
        {
            bool correctMove = false;
            //Debug.WriteLine(String.Format("Start {0} {1}", GetSelectedSquareAsStart().xIndex, GetSelectedSquareAsStart().yIndex));
            //Debug.WriteLine(String.Format("End[0] {0} {1}", selectedSquaresToEnd[0].xIndex, selectedSquaresToEnd[0].yIndex));
            if (GetSelectedSquareAsStart() != null && selectedSquaresToEnd.Count > 0)
            {
                correctMove = true;
                //First case: accepting only one move
                if (selectedSquaresToEnd.Count == 1)
                {
                    if (CanMovePawn(GetSelectedSquareAsStart(), selectedSquaresToEnd[0]))
                    {
                        MovePawn(GetSelectedSquareAsStart(), selectedSquaresToEnd[0]);
                    }
                    else
                        correctMove = false;
                }
                //More than 1 move in one turn -> every move HAS TO be a takedown
                else
                {
                    //ten przypadek do przemyślenia
                    BrownSquare firstSquare = GetSelectedSquareAsStart();

                    Pawn pawn = selectedSquareAsStart.Pawn;
                    //keep the reference to pawn position
                    //Square pawnPos = pawn.position;
                    BrownSquare toBeRemoved = firstSquare;
                    for(int i = 0;i<selectedSquaresToEnd.Count;i++)
                    {
                        if(pawn.IsTakedownMove(selectedSquaresToEnd[i], squares))
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
                        pawn.TakedownList = new List<Pawn>();
                    }
                    //Trzeba usunąć pionki przez które przeskakiwał (o ile nie będzie tego w metodzie)
                    else
                    {
                        IsWhiteTurn = !IsWhiteTurn;
                        Takedown(pawn.TakedownList);
                        pawn.TakedownList = new List<Pawn>();
                        ChangePawnToDame(pawn);
                    }
                    //Create another method Pawn.MoveIsTakeDown and iterate
                    //+ change every move start and end square
                }
            }
            //Reset selected squares no matter the result
            SetSelectedSquareAsStart(null);
            selectedSquaresToEnd = new List<BrownSquare>();
            return correctMove;
        }

        private void AddWhitePawns()
        {
            if(blackPawnsAtBeginningOnBottom)
            {
                var whitePawns = new List<ManPawn>();
                for (int i = 0; i < 12; i++)
                    whitePawns.Add(new ManPawn(true));

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
            if(!IsWhiteTurn)
            {
                if (start.Pawn.IsWhite)
                    return false;
            }
            else
            {
                if (!start.Pawn.IsWhite)
                    return false;
            }
            if (start.Pawn.Position == end)
                return false;
            return start.Pawn.IsValidMove(end, squares);
        }

        public void OnInteraction(BrownSquare square)
        {
            if (GetSelectedSquareAsStart() == null)
                SetSelectedSquareAsStart(square);
            else
            {
                if (selectedSquaresToEnd.Contains(square) || GetSelectedSquareAsStart() == square)
                    return;
                else
                    selectedSquaresToEnd.Add(square);
            }
        }

        /// <summary>
        /// Can be regular or takedown move
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public void MovePawn(BrownSquare start, BrownSquare end)
        {
            Pawn pawn = start.Pawn;
            start.Pawn = null;
            end.Pawn = pawn;
            //Switch after correct move (no double [and more] take implemented yet)
            IsWhiteTurn = !IsWhiteTurn;
            Takedown(pawn.TakedownList);
            pawn.TakedownList = new List<Pawn>();
            ChangePawnToDame(pawn);
            return;
        }

        private void Takedown(List<Pawn> pawns)
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
                var blackPawns = new List<ManPawn>();
                for (int i = 0; i < 12; i++)
                    blackPawns.Add(new ManPawn(false));

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

        private void CheckForGameEnd()
        {
            //condition 1
            NoPawnsLeft();
            //condition 2 (no valid move possible)
        }

        private void NoPawnsLeft()
        {
            bool foundWhitePawn = false;
            bool foundBlackPawn = false;
            foreach (var s in squares)
            {
                foreach (var square in s)
                {
                    if (square is BrownSquare)
                    {
                        if ((square as BrownSquare).Pawn != null)
                        {
                            if ((square as BrownSquare).Pawn.IsWhite)
                                foundWhitePawn = true;
                            else
                                foundBlackPawn = true;
                        }
                    }
                }
            }
            if (!foundBlackPawn)
            {
                EndGame(true);
            }
            else if (!foundWhitePawn)
            {
                EndGame(false);
            }
        }

        private void EndGame(bool whiteWon)
        {
            gameFinished = true;
            if(whiteWon)
                whoWonText = "WHITE WINS!";
            else
                whoWonText = "BLACK WINS!";
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
