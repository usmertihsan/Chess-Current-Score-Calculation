using System;
using System.Net;
using System.Net.Sockets;
using Chess.Game.Engine;

namespace Chess.Game.UI.Consolee
{
    class Program
    {
        static void Main(string[] args)
        {
            string path;
            Console.WriteLine("Enter path for score calculation, Example path: C:/Users/merti/Desktop/board3.txt");
            Console.WriteLine("Enter path:");
            path = Console.ReadLine();


            LoadGame chessTable = new LoadGame(path);

            chessTable.LoadChessMatrix();
            chessTable.createUnderThreatUniqueArray();
            chessTable.DrawChessTable();
            chessTable.SearchChessStone();
            chessTable.PrintAllStonesInTable();
            chessTable.SearchChessStoneAndEnemys();
            chessTable.PrintUnderThreat();
            chessTable.DrawUniqueThreatChessTable();
            chessTable.DistinctThreatAndNotThreat();
            chessTable.DrawDistinct();
            chessTable.CalculateScoreCurrentChessTable();
            Console.Read();



        }
    }
}
