using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Game.Engine
{
    public class LoadGame
    {
        private string path;  // getting path directory for load any kind of game
        private string[,] chessMatrix; // creation matrix 2D
        private int chessMatrixSize; // size of chess table 8x8
        private string[,] underThreatUniqueChars; // eliminated unique threated stones
        private List<string> findedStones; // store all stones in table
        private List<string> charList; // all stone names
        private List<string> whiteStone; // white stones names
        private List<string> blackStone; // black stones names
        private List<int> locationEnemys; // store enemy locations
        private List<string> underThreat; // store under threat stones
        private string[,] distinctStone; // eliminate stones

        public LoadGame(string path) // constructor
        {
            this.path = path;
            this.chessMatrixSize = 8;             
            findedStones = new List<string>();
            locationEnemys = new List<int>();
            charList = new List<string> { "ks","as","fs","vs","ss","ps","kb","ab","fb","vb","sb","pb"};
            whiteStone = new List<string> {"pb","ab","fb","vb","sb","kb"};
            blackStone = new List<string> {"ks","as","fs","vs","ss","ps"};       
            underThreat = new List<string>();
            distinctStone = new string[8, 8];
        }

        public void LoadChessMatrix() // reading text file into 2d matrix from given location
        {
            String input = File.ReadAllText(this.path);

            int i = 0, j = 0;
            chessMatrix = new string[8, 8];
            foreach (var row in input.Split('\n'))
            {
                j = 0;
                foreach (var col in row.Trim().Split(' '))
                {
                    chessMatrix[i, j] = (col.Trim());
                    j++;
                }
                i++;
            }
            
        }

        public void createUnderThreatUniqueArray()
        {
            underThreatUniqueChars = new string[chessMatrixSize, chessMatrixSize];
        }

        public void DrawUniqueThreatChessTable()
        {
            Console.WriteLine(" ");
            Console.WriteLine("Unique Threat stones:");
            for (int i = 0; i < chessMatrixSize; i++)
            {
                for (int j = 0; j < chessMatrixSize; j++)
                {
                    string currentValue = underThreatUniqueChars[i, j];

                    if (charList.Contains(currentValue))
                    {
                        Console.Write(currentValue);
                        Console.Write(",");
                    }
                }
            }
        }

        public int StonePointCalculate(string stone)
        {
            int point=0;

            if (stone.StartsWith("p"))
            {
                point = 1;
            }

            else if (stone.StartsWith("a"))
            {
                point = 3;
            }

            else if (stone.StartsWith("f"))
            {
                point = 3;
            }

            else if (stone.StartsWith("k"))
            {
                point = 5;
            }

            else if (stone.StartsWith("v"))
            {
                point = 9;
            }

            else if (stone.StartsWith("s"))
            {
                point = 100;
            }

            return point;
        }

        public void DistinctThreatAndNotThreat()
        {
            for (int i = 0; i < chessMatrixSize; i++)
            {
                for (int j = 0; j < chessMatrixSize; j++)
                {
                    if(chessMatrix[i, j] == underThreatUniqueChars[i, j])
                    {
                        distinctStone[i, j] = "--";
                    }
                    else
                    {
                        distinctStone[i, j] = chessMatrix[i, j];
                    }         
                }  
            }
        }

        public void DrawDistinct() 
        {
            Console.WriteLine(" ");
            Console.WriteLine("Not Under Threat stones:");
            for (int i = 0; i < chessMatrixSize; i++)
            {
                for (int j = 0; j < chessMatrixSize; j++)
                {
                    string currentValue = distinctStone[i, j];

                    if (charList.Contains(currentValue))
                    {
                        Console.Write(currentValue);
                        Console.Write(",");
                    }
                }
            }
        }

        public void CalculateScoreCurrentChessTable()
        {
            Console.WriteLine(" ");
            float blackStonePoint=0;
            float whiteStonePoint=0;
            for (int i = 0; i < chessMatrixSize; i++)
            {
                for (int j = 0; j < chessMatrixSize; j++)
                {
                    string currentValue = underThreatUniqueChars[i, j];

                    if (blackStone.Contains(currentValue))
                    {
                        float takenPoint = StonePointCalculate(currentValue);
                        blackStonePoint +=(takenPoint/2);
                    }

                    else if (whiteStone.Contains(currentValue))
                    {
                        float takenPoint = StonePointCalculate(currentValue);
                        whiteStonePoint += (takenPoint/2);
                    }
                }
            }

            for (int i = 0; i < chessMatrixSize; i++)
            {
                for (int j = 0; j < chessMatrixSize; j++)
                {
                    string currentValue = distinctStone[i, j];

                    if (blackStone.Contains(currentValue))
                    {
                        float takenPoint = StonePointCalculate(currentValue);
                        blackStonePoint += (takenPoint);
                    }

                    else if (whiteStone.Contains(currentValue))
                    {
                        float takenPoint = StonePointCalculate(currentValue);
                        whiteStonePoint += (takenPoint);
                    }
                }
            }

            Console.WriteLine("Black:{0}", blackStonePoint);
            Console.WriteLine("White:{0}", whiteStonePoint);
        }
            public void DrawChessTable()
        {
            for (int i = 0; i < chessMatrixSize; i++)
            {
                for (int j = 0; j < chessMatrixSize; j++)
                {
                    Console.Write(chessMatrix[i, j] + "\t");
                }
                Console.WriteLine();
            }
        }

        private void CharacterCheck(string input) // that function listed all current stones in chess table
        {   
            bool isExist = charList.Contains(input);
            if (isExist)
            {
                findedStones.Add(input);
            }
        }

        public void PrintAllStonesInTable() // print current stones in chess table
        {
            Console.WriteLine("Finded stones:");
            foreach (string i in findedStones)
            {     
                Console.Write(i);
                Console.Write(", ");
            }
            Console.WriteLine(" ");
        }

        public void SearchChessStone() // one by one checks the stones if exist
        {
            for (int i = 0; i < chessMatrixSize; i++)
            {
                for (int j = 0; j < chessMatrixSize; j++)
                {
                    string input = chessMatrix[i, j];
                    CharacterCheck(input);
                }              
            }
        }

        public void PrintUnderThreat()
        {
            Console.WriteLine("Under threat stones:");
            foreach (string i in underThreat)
            {
                Console.Write(i);
                Console.Write(", ");
            }
          
        }

        public void SearchChessStoneAndEnemys() // one by one checks the stones and their enemies
        {
            for (int i = 0; i < chessMatrixSize; i++)
            {
                for (int j = 0; j < chessMatrixSize; j++)
                {
                    string input = chessMatrix[i, j];

                    if (input == "pb" || input == "ps")
                    {
                        PawnEnemyDetect(i, j);
                    }

                    if (input == "ab" || input == "as")
                    {
                        HorseEnemyDetect(i, j);
                    }

                    if (input == "vb" || input == "vs")
                    {
                        QueenEnemyDetect(i, j);
                    }
                }
            }
        }

        private void horseEnemyCheck(string enemyOrNot, int i,int j,int candidateRow, int candidateColumn)
        {
            string currentChar = chessMatrix[i, j];
            if(currentChar == "ab")
            {
                bool isExist = blackStone.Contains(enemyOrNot);
                if (isExist)
                {
                    underThreatUniqueChars[candidateRow, candidateColumn] = enemyOrNot;
                    underThreat.Add(enemyOrNot);
                }
            }
            else if(currentChar=="as")
            {
                bool isExist = whiteStone.Contains(enemyOrNot);
                if (isExist)
                {
                    underThreatUniqueChars[candidateRow, candidateColumn] = enemyOrNot;
                    underThreat.Add(enemyOrNot);
                }
            }
        }

        private bool QueenEnemyCheck(string enemyOrNot, int i, int j,int candidateRow,int candidateColumn)
        {
            string currentChar = chessMatrix[i, j];
            bool isExist = false;
            if (currentChar == "vb")
            {
                if(!whiteStone.Contains(enemyOrNot))
                {
                    isExist = blackStone.Contains(enemyOrNot);
                    if (isExist)
                    {
                        underThreatUniqueChars[candidateRow, candidateColumn] = enemyOrNot;
                        underThreat.Add(enemyOrNot);
                    }
                }
                else
                {
                    isExist = whiteStone.Contains(enemyOrNot);
                    
                }
            }

            else if (currentChar == "vs")
            {
                if (!blackStone.Contains(enemyOrNot))
                {
                    isExist = whiteStone.Contains(enemyOrNot);
                    if (isExist)
                    {
                        underThreatUniqueChars[candidateRow, candidateColumn] = enemyOrNot;
                        underThreat.Add(enemyOrNot);
                    }
                }

                else
                {
                    isExist = blackStone.Contains(enemyOrNot);                  
                }
            }

            return isExist;
        }

        private void QueenEnemyDetect(int i, int j)
        {
            string enemyOrNot;
            // side left
            bool findStutitonSideLeft = false;
            int rowSideLeft= i;
            int columnSideLeft = j;

            while (findStutitonSideLeft == false)
            {
                columnSideLeft--;

                if (rowSideLeft < 0 || columnSideLeft < 0 || rowSideLeft > 7 || columnSideLeft > 7)
                {
                    findStutitonSideLeft = true;
                }

                else

                {
                    findStutitonSideLeft = QueenEnemyCheck(enemyOrNot = chessMatrix[rowSideLeft, columnSideLeft], i, j,i,columnSideLeft);
                }
            }
            //side right

            bool findStutitonSideRight = false;
            int rowSideRight = i;
            int columnSideRight = j;

            while (findStutitonSideRight == false)
            {
                columnSideRight++;

                if (rowSideRight < 0 || columnSideRight < 0 || rowSideRight > 7 || columnSideRight > 7)
                {
                    findStutitonSideRight = true;
                }
                else

                {
                    findStutitonSideRight = QueenEnemyCheck(enemyOrNot = chessMatrix[rowSideRight, columnSideRight], i, j,i,columnSideRight);
                }
            }

            // vertical top
            bool findStutitonVerticalTop = false;
            int rowVerticalTop = i;
            int columnVerticalTop = j;

            while (findStutitonVerticalTop == false)
            {
                rowVerticalTop--;

                if (rowVerticalTop < 0 || columnVerticalTop < 0 || rowVerticalTop > 7 || columnVerticalTop > 7)
                {
                    findStutitonVerticalTop = true;
                }

                else

                {
                    findStutitonVerticalTop = QueenEnemyCheck(enemyOrNot = chessMatrix[rowVerticalTop, columnVerticalTop], i, j,rowVerticalTop,columnVerticalTop);
                }
            }

            // vertical bottom
            bool findStutitonVerticalBottom = false;
            int rowVerticalBottom = i;
            int columnVerticalBottom = j;
            while (findStutitonVerticalBottom == false)
            {
                rowVerticalBottom++;
                if (rowVerticalBottom < 0 || columnVerticalBottom < 0 || rowVerticalBottom > 7 || columnVerticalBottom > 7)
                {
                    findStutitonVerticalBottom = true;
                }
                else
                {
                    findStutitonVerticalBottom = QueenEnemyCheck(enemyOrNot = chessMatrix[rowVerticalBottom, columnVerticalBottom], i, j,rowVerticalBottom,columnVerticalBottom);
                }
            }

            //right cross //calısıyor
            bool findStutitonRightCross = false;
            int rowRightCross = i;
            int columnRightCross = j;
            while (findStutitonRightCross == false)
            {
                rowRightCross++;
                columnRightCross++;
                if (rowRightCross < 0 || columnRightCross < 0 || rowRightCross > 7 || columnRightCross > 7)
                {
                    findStutitonRightCross = true;
                }
                else
                {
                    findStutitonRightCross = QueenEnemyCheck(enemyOrNot = chessMatrix[rowRightCross, columnRightCross], i, j,rowRightCross,columnRightCross);
                }
            }

            //rightcross2 //calısıyor
            bool findStutitonRightCross2 = false;
            int rowRightCross2 = i;
            int columnRightCross2 = j;
            while (findStutitonRightCross2 == false)
            {
                rowRightCross2++;
                columnRightCross2--;
                if (rowRightCross2 < 0 || columnRightCross2 < 0 || rowRightCross2 > 7 || columnRightCross2 > 7)
                {
                    findStutitonRightCross2 = true;
                }
                else
                {
                    findStutitonRightCross2 = QueenEnemyCheck(enemyOrNot = chessMatrix[rowRightCross2, columnRightCross2], i, j,rowRightCross2,columnRightCross2);
                }
            }

            // left cross // look        
            bool findStutitonLeftCross = false;
            int rowLeftCross = i;
            int columnLeftCross = j;
            while (findStutitonLeftCross == false)
            {
                rowLeftCross--;
                columnLeftCross--;
                if (rowLeftCross < 0 || columnLeftCross < 0 || rowLeftCross > 7 || columnLeftCross > 7)
                {
                    findStutitonLeftCross = true;
                }

                else
                {
                    findStutitonLeftCross = QueenEnemyCheck(enemyOrNot = chessMatrix[rowLeftCross, columnLeftCross], i, j,rowLeftCross,columnLeftCross);
                }
            }

            //left cross 2 // look

            bool findStutitonLeftCross2 = false;
            int rowLeftCross2 = i;
            int columnLeftCross2 = j;    
            while (findStutitonLeftCross2 == false)
            {
                rowLeftCross2--;
                columnLeftCross2++;
                if (rowLeftCross2 < 0 || columnLeftCross2 < 0 || rowLeftCross2 > 7 || columnLeftCross2 > 7)
                {
                    findStutitonLeftCross2 = true;
                }
                else
                {
                    findStutitonLeftCross2 = QueenEnemyCheck(enemyOrNot = chessMatrix[rowLeftCross2, columnLeftCross2], i, j,rowLeftCross2,columnLeftCross2);
                }
            }
        }

        private void PawnEnemyCheck(string enemyOrNot, int i, int j,int candidateRow,int candidateColumn)
        {
            string currentChar = chessMatrix[i, j];
            if (currentChar == "pb")
            {
                bool isExist = blackStone.Contains(enemyOrNot);
                if (isExist)
                {
                    underThreatUniqueChars[candidateRow, candidateColumn] = enemyOrNot;
                    underThreat.Add(enemyOrNot);
                }
            }

            else if (currentChar == "ps")
            {
                bool isExist = whiteStone.Contains(enemyOrNot);
                if (isExist)
                {
                    underThreatUniqueChars[candidateRow, candidateColumn] = enemyOrNot;
                    underThreat.Add(enemyOrNot);
                }
            }
        }

        private void KingEnemyCheck(string enemyOrNot, int i, int j,int candidateRow,int candidateColumn)
        {
            string currentChar = chessMatrix[i, j];
            if (currentChar == "sb")
            {
                bool isExist = blackStone.Contains(enemyOrNot);
                if (isExist)
                {
                    underThreatUniqueChars[candidateRow, candidateColumn] = enemyOrNot;
                    underThreat.Add(enemyOrNot);
                }
            }

            else if (currentChar == "ss")
            {
                bool isExist = whiteStone.Contains(enemyOrNot);
                if (isExist)
                {
                    underThreatUniqueChars[candidateRow, candidateColumn] = enemyOrNot;
                    underThreat.Add(enemyOrNot);
                }
            }
        }

        private void HorseEnemyDetect(int i, int j)
        {        
            string enemyOrNot;
            if((i - 1 >= 0 & j - 2 >= 0) & (i - 1 <= 7 & j - 2 <= 7))
            {
                horseEnemyCheck(enemyOrNot = chessMatrix[i - 1, j - 2], i, j,i-1,j-2);
            }

            if ((i + 1 >= 0 & j - 2 >= 0) & (i + 1 <=7 & j - 2 <=7))
            {
                horseEnemyCheck(enemyOrNot = chessMatrix[i + 1, j - 2], i, j,i+1,j-2);
            }

            if ((i - 2 >= 0 & j - 1 >= 0) & (i - 2 <= 7 & j - 1 <= 7))
            {
                horseEnemyCheck(enemyOrNot = chessMatrix[i - 2, j - 1], i, j,i-2,j-1);
            }

            if ((i - 2 >= 0 & j + 1 >= 0) & (i - 2 <= 7 & j + 1 <= 7))
            {
                horseEnemyCheck(enemyOrNot = chessMatrix[i - 2, j + 1], i, j,i-2,j+1);
            }

            if ((i - 1 >= 0 & j + 2 >= 0) & (i - 1 <=7 & j + 2 <=7))
            {
                horseEnemyCheck(enemyOrNot = chessMatrix[i - 1, j + 2], i, j,i-1,j+2);
            }

            if ((i + 1 >= 0 & j + 2 >= 0) & (i + 1 <= 7 & j + 2 <= 7))
            {
                horseEnemyCheck(enemyOrNot = chessMatrix[i + 1, j + 2], i, j,i+1,j+2);
            }

            if ((i + 2 >= 0 & j - 1 >= 0) & (i + 2 <= 7 & j - 1 <= 7))
            {
                horseEnemyCheck(enemyOrNot = chessMatrix[i + 2, j - 1], i, j,i+2,j-1);
            }

            if ((i + 2 >= 0 & j + 1 >= 0) & (i + 2 <= 7 & j + 1 <= 7))
            {
                horseEnemyCheck(enemyOrNot = chessMatrix[i + 2, j + 1], i, j,i+2,j+1);
            } 
        }

        private void PawnEnemyDetect(int i, int j)
        {
            string enemyOrNot;
            string currentChar = chessMatrix[i, j];
            if (currentChar == "ps")
            {
                if ((i + 1 >= 0 & j - 1 >= 0) & (i + 1 <= 7 & j - 1 <= 7))
                {
                    PawnEnemyCheck(enemyOrNot = chessMatrix[i + 1, j - 1], i, j,i+1,j-1);
                }

                if ((i + 1 >= 0 & j + 1 >= 0) & (i + 1 <= 7 & j + 1 <= 7))
                {
                    PawnEnemyCheck(enemyOrNot = chessMatrix[i + 1, j + 1], i, j,i+1,j+1);
                }
            }

            else if (currentChar == "pb")
            {
                if ((i - 1 >= 0 & j - 1 >= 0) & (i - 1 <= 7 & j - 1 <= 7))
                {
                    PawnEnemyCheck(enemyOrNot = chessMatrix[i - 1, j - 1], i, j,i-1,j-1);
                }

                if ((i - 1 >= 0 & j + 1 >= 0) & (i - 1 <= 7 & j + 1 <= 7))
                {
                    PawnEnemyCheck(enemyOrNot = chessMatrix[i - 1, j + 1], i, j,i-1,j+1);
                }
            }   
        }

        private void KingEnemyDetect(int i, int j)
        {
            string enemyOrNot;
            if ((i + 1 >= 0 & j >= 0) & (i + 1 <= 7 & j <= 7))
            {
                KingEnemyCheck(enemyOrNot = chessMatrix[i + 1, j], i, j,i+1,j);
            }

            if ((i - 1 >= 0 & j >= 0) & (i - 1 <= 7 & j <= 7))
            {
                KingEnemyCheck(enemyOrNot = chessMatrix[i - 1, j ], i, j,i-1,j);
            }

            if ((i >= 0 & j + 1 >= 0) & (i <= 7 & j + 1 <= 7))
            {
                KingEnemyCheck(enemyOrNot = chessMatrix[i , j + 1], i, j,i,j+1);
            }

            if ((i >= 0 & j - 1 >= 0) & (i <= 7 & j - 1 <= 7))
            {
                KingEnemyCheck(enemyOrNot = chessMatrix[i , j - 1], i, j,i,j-1);
            }

            if ((i - 1 >= 0 & j + 1 >= 0) & (i - 1 <= 7 & j + 1 <= 7))
            {
                KingEnemyCheck(enemyOrNot = chessMatrix[i - 1, j + 1], i, j,i-1,j+1);
            }

            if ((i + 1 >= 0 & j + 1 >= 0) & (i + 1 <= 7 & j + 1 <= 7))
            {
                KingEnemyCheck(enemyOrNot = chessMatrix[i + 1, j + 1], i, j,i+1,j+1);
            }

            if ((i - 1 >= 0 & j - 1 >= 0) & (i - 1 <= 7 & j - 1 <= 7))
            {
                KingEnemyCheck(enemyOrNot = chessMatrix[i - 1, j - 1], i, j,i-1,j-1);
            }

            if ((i + 1 >= 0 & j - 1 >= 0) & (i + 1 <= 7 & j - 1 <= 7))
            {
                KingEnemyCheck(enemyOrNot = chessMatrix[i + 1, j - 1], i, j,i+1,j-1);
            }
        }
    }
}


