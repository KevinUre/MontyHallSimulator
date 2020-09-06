using System;
using System.Collections.Generic;
using System.Linq;

namespace MontyHallSimulation
{
    class Program
    {
        public static readonly int NUMBER_OF_TRIALS = 10000000;

        static void Main(string[] args)
        {
            Console.WriteLine($"Number of Trials per player: {String.Format("{0:n0}", NUMBER_OF_TRIALS)}");
            bool[] keeperResults = new bool[NUMBER_OF_TRIALS];
            bool[] swapperResults = new bool[NUMBER_OF_TRIALS];
            for (int i = 0; i < NUMBER_OF_TRIALS; i++)
            {
                List<Door> doors = InitDoors();
                doors.RandomElement().Picked = true;
                doors.FindAll(d => !d.Prize && !d.Picked).RandomElement().Opened = true;
                keeperResults[i] = doors.Find(d => d.Picked).Prize;
                checkProgress(i, "Keeps");
            }
            for (int i = 0; i < NUMBER_OF_TRIALS; i++)
            {
                List<Door> doors = InitDoors();
                doors.RandomElement().Picked = true;
                doors.FindAll(d => !d.Prize && !d.Picked).RandomElement().Opened = true; 
                Door old = doors.Find(d => d.Picked);
                Door @new = doors.Find(d => !d.Picked && !d.Opened);
                old.Picked = false;
                @new.Picked = true;
                swapperResults[i] = doors.Find(d => d.Picked).Prize;
                checkProgress(i, "Swaps");
            }
            var temp = keeperResults.ToList();
            var temp2 = temp.FindAll(e => e);
            double keeperWinPercentage = (double)keeperResults.ToList().FindAll(e => e).Count / NUMBER_OF_TRIALS * 100;
            double swapperWinPercentage = (double)swapperResults.ToList().FindAll(e => e).Count / NUMBER_OF_TRIALS * 100;
            Console.Clear();
            Console.WriteLine($"Number of Trials per player: {String.Format("{0:n0}", NUMBER_OF_TRIALS)}");
            Console.WriteLine($"Keeper Win Percentage: {Math.Round(keeperWinPercentage,2)}%");
            Console.WriteLine($"Swapper Win Percentage: {Math.Round(swapperWinPercentage, 2)}%");
            Console.ReadLine();
        }

        static void checkProgress(int i, string player)
        {
            if (i % (NUMBER_OF_TRIALS / 100) == 0)
            {
                Console.SetCursorPosition(0, 1);
                Console.WriteLine($"{player}: {i / (NUMBER_OF_TRIALS / 100)}% Complete");
            }
        }

        static List<Door> InitDoors()
        {
            List<Door> doors = new List<Door>(3);
            for (int i = 0; i < 3; i++)
                doors.Add(new Door() { Opened = false, Picked = false, Prize = false });
            doors.RandomElement().Prize = true;
            return doors;
        }
    }

    class Door
    {
        public bool Picked;
        public bool Opened;
        public bool Prize;
    }

    public static class ListExtensions
    {
        public static Random randomNumberGenerator = new Random();

        public static T RandomElement<T>(this List<T> list)
        {
            return list[randomNumberGenerator.Next(list.Count)];
        }
    }
}
