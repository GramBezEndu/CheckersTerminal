using System;
using System.Collections.Generic;

namespace CheckersLogic
{
    public class RandomComputerAgent
    {
        //maksymalna punktacja ruchu
        private int maxScore;
        //najlepszy pionek
        private List<(Pawn, int,  List<BrownSquare>)> bestOption;
        private (Pawn, int, List<BrownSquare>) option;
        //lista pionków komputera
        public List<Pawn> pawns = new List<Pawn>();
        public Square[][] currentSituation;
        private Square[][] tempSquares = new Square[8][];

        public RandomComputerAgent(Board currentBoard)
        {
            maxScore = 0;
            bestOption = new List<(Pawn, int, List<BrownSquare>)>();
            option = (null, 0, new List<BrownSquare>());
            currentSituation = currentBoard.squares;
            // Iterujemy po całej planszy
            foreach (Square[] line in currentSituation)
            {
                foreach(Square square in line)
                {
                    if(square is BrownSquare)
                    {
                        // Zakładam że gracz gra białymi
                        if(square != null && ((BrownSquare)square).Pawn != null && !((BrownSquare)square).Pawn.IsWhite)
                        {
                            // Pionek jest dodawany do listy pionków komputera
                            pawns.Add(((BrownSquare)square).Pawn);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Wylicza jak dobra jest dana ścieżka, przechowuję informacje o początkowym pionku, kolejnym jego ruchu, oraz skąd ten pionek\
        /// przyszedł -1 - początek 0 - NE, 1 - NW, 2 - SW, 3 - SE
        /// </summary>
        public void Evaluate(Pawn pawn, BrownSquare source, int directionAxis, Boolean rewrite)
        {
            List<(BrownSquare, int)> possibleTakeDownPaths = new List<(BrownSquare, int)>();
            List<(BrownSquare, int)> possibleMovePaths = new List<(BrownSquare, int)>();
            int x = source.xIndex;
            int y = source.yIndex;


            // Sprawdza typ pionka
            if (pawn is ManPawn)
            {

                // Możliwe ruchy
                if (x + 1 < 8 && y + 1 < 8)
                {
                    possibleMovePaths.Add(((BrownSquare)currentSituation[x + 1][y + 1], 3));
                }
                if (x - 1 >= 0 && y + 1 < 8)
                {
                    possibleMovePaths.Add(((BrownSquare)currentSituation[x - 1][y + 1], 2));
                }



                // Możliwe bicie

                if (x + 2 < 8 && y + 2 < 8)
                {
                    possibleTakeDownPaths.Add(((BrownSquare)currentSituation[x + 2][y + 2], 3));
                }
                if (x - 2 >= 0 && y + 2 < 8)
                {
                    possibleTakeDownPaths.Add(((BrownSquare)currentSituation[x - 2][y + 2], 2));
                }
                if (x - 2 >= 0 && y - 2 >= 0)
                {
                    possibleTakeDownPaths.Add(((BrownSquare)currentSituation[x - 2][y - 2], 1));
                }
                if (x + 2 < 8 && y - 2 >= 0)
                {
                    possibleTakeDownPaths.Add(((BrownSquare)currentSituation[x + 2][y - 2], 0));
                }

            }
            else if(pawn is Dame)
            {
                // Możliwe bicia
                // oś NE
                for (int i = 2; x + i <8 && y + i <8; i++)
                {
                    possibleTakeDownPaths.Add(((BrownSquare)currentSituation[x + i][y + i], 3));
                }
                // oś NW
                for (int i = 2; x - i >= 0 && y + i < 8; i++)
                {
                    possibleTakeDownPaths.Add(((BrownSquare)currentSituation[x - i][y + i], 2));
                }
                // oś SW
                for (int i = 2; x - i >= 0 && y - i >= 0; i++)
                {
                    possibleTakeDownPaths.Add(((BrownSquare)currentSituation[x - i][y - i], 1));
                }
                // oś SE
                for (int i = 2; x + i < 8 && y - i >= 0; i++)
                {
                    possibleTakeDownPaths.Add(((BrownSquare)currentSituation[x + i][y - i], 0));
                }

                // Możliwe ruchy = Możliwe bicia + 4 ruchy
                possibleMovePaths = possibleTakeDownPaths;
                if (x+1<8 && y+1<8)
                {
                    possibleMovePaths.Add(((BrownSquare)currentSituation[x + 1][y + 1], 3));
                }
                if (x - 1 >= 0 && y + 1 < 8)
                {
                    possibleMovePaths.Add(((BrownSquare)currentSituation[x - 1][y + 1], 2));
                }
                if (x - 1 >= 0 && y - 1 >= 0)
                {
                    possibleMovePaths.Add(((BrownSquare)currentSituation[x - 1][y - 1], 1));
                }
                if (x + 1 < 8 && y - 1 >= 0)
                {
                    possibleMovePaths.Add(((BrownSquare)currentSituation[x + 1][y - 1], 0));
                }

            }
            if (rewrite)
            {
                for (int a = 0; a < 8; a++)
                {
                    tempSquares[a] = new Square[8];
                    for (int b = 0; b < 8; b++)
                    {
                        //if this row is odd number (starting from top)
                        if (a % 2 == 0)
                        {
                            //brown first
                            if (b % 2 == 0)
                                tempSquares[a][b] = new BrownSquare(a, b);
                            //then white
                            else
                                tempSquares[a][b] = new WhiteSquare(a, b);
                        }
                        //this row is even number
                        else
                        {
                            //white first
                            if (b % 2 == 0)
                                tempSquares[a][b] = new WhiteSquare(a, b);
                            //then brown
                            else
                                tempSquares[a][b] = new BrownSquare(a, b);
                        }
                    }
                }
                for (int a = 0; a < 8; a++)
                {
                    for (int b = 0; b < 8; b++)
                    {
                        if (currentSituation[a][b] is BrownSquare)
                        {
                            if ((currentSituation[a][b] as BrownSquare).Pawn != null)
                            {
                                if ((currentSituation[a][b] as BrownSquare).Pawn.IsWhite)
                                {
                                    (tempSquares[a][b] as BrownSquare).Pawn = new ManPawn(true);
                                }
                                else
                                {
                                    (tempSquares[a][b] as BrownSquare).Pawn = new ManPawn(false);
                                }
                            }
                        }
                    }
                }
            }
            // Rekurencyjnie sprawdzana możliwość bić
            for (int i=0; i<possibleTakeDownPaths.Count;i++)
            {
                if(pawn.IsTakedownMove(possibleTakeDownPaths[i].Item1, tempSquares, false, option.Item3[option.Item3.Count - 1]))
                {
                    option.Item3.Add(possibleTakeDownPaths[i].Item1);
                    option.Item2 += 2;
                    (Pawn, int, List<BrownSquare>) currentOption = option;
                    currentOption.Item3 = new List<BrownSquare>();
                    currentOption.Item3.AddRange(option.Item3);
                    currentOption.Item3.RemoveAt(0);
                    int axis = possibleTakeDownPaths[i].Item2;

                    // by uniknąć zapętlenia nie sprawdzamy ścieżki, z której przybył pionek
                    if ((axis + 2) % 4 != directionAxis)
                    {
                        Evaluate(pawn, possibleTakeDownPaths[i].Item1, axis, false);
                        if (bestOption.Count == 0)
                        {
                            bestOption.Add(currentOption);
                        }
                        else if (currentOption.Item2 > bestOption[0].Item2)
                        {
                            bestOption = new List<(Pawn, int, List<BrownSquare>)>();
                            bestOption.Add(currentOption);
                        }
                        else if (currentOption.Item2 == bestOption[0].Item2)
                        {
                            bestOption.Add(currentOption);
                        }
                        option.Item2 -= 2;
                        option.Item3.RemoveAt(option.Item3.Count - 1);
                    }
                }
            }
            if (rewrite)
            {
                foreach ((BrownSquare, int) move in possibleMovePaths)
                {
                    if (pawn.IsRegularMove(move.Item1, currentSituation))
                    {
                        option.Item3.Add(move.Item1);
                        option.Item2 += 1;
                        (Pawn, int, List<BrownSquare>) currentOption = option;
                        currentOption.Item3 = new List<BrownSquare>();
                        currentOption.Item3.AddRange(option.Item3);
                        currentOption.Item3.RemoveAt(0);
                        //jeżeli nie ma jeszcze żadnej najlepszej opcji to opcja jest najlepszą
                        if (bestOption.Count == 0)
                        {
                            bestOption.Add(currentOption);
                        }
                        //obecna opcja jest lepsza niż najlepsza opcja
                        else if (currentOption.Item2 > bestOption[0].Item2)
                        {
                            bestOption = new List<(Pawn, int, List<BrownSquare>)>();
                            bestOption.Add(currentOption);
                        }
                        //obecna opcja jest tak samo dobra jak najlepsza opcja, dodawana jest do puli
                        else if (currentOption.Item2 == bestOption[0].Item2)
                        {
                            bestOption.Add(currentOption);
                        }
                        option.Item2 -= 1;
                        option.Item3.RemoveAt(option.Item3.Count - 1);
                    }
                }
            }
        }

        public (Pawn, List<BrownSquare>) SearchForBestMove()
        {
            foreach (Pawn pawn in pawns)
            {
                option.Item1 = pawn;
                option.Item3.Add((BrownSquare)pawn.Position);
                Evaluate(pawn, (BrownSquare)pawn.Position, -1, true);
                option = (null, 0, new List<BrownSquare>());
            }
            if (bestOption.Count == 0)
            {
                throw new Exception();
            }
            if(bestOption.Count == 1)
            {
                return (bestOption[0].Item1, bestOption[0].Item3);
            }
            else
            {
                Random r = new Random();
                int rInt = r.Next(0, bestOption.Count);
                return (bestOption[rInt].Item1, bestOption[rInt].Item3);
            }
        }
    }

}

