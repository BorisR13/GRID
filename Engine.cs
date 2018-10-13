using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GRID
{
    class Engine
    {

        private RaceTower raceTower;
        public bool Isrunning = false;

        int totalLaps;
        int trackLength;
        int currentLap;

        public Engine()
        {
            raceTower = new RaceTower();
        }

        public void Run()
        {
            Isrunning = true;

            StringBuilder sb = new StringBuilder();

            totalLaps = int.Parse(Console.ReadLine());
            trackLength = int.Parse(Console.ReadLine());
            raceTower.SetTrackInfo(totalLaps, trackLength);

            currentLap = 0;

            string output;
            while (Isrunning)
            {
                if (currentLap <= totalLaps)
                {
                    if (raceTower.CurrentLap == totalLaps)
                    {
                        Isrunning = false;
                        sb.AppendLine(raceTower.Winner().Trim());
                        Console.WriteLine(sb.ToString().Trim());
                    }

                    try
                    {
                        string line = Console.ReadLine();
                        output = ProcessCommand(line);
                    }
                    catch (InvalidOperationException e)
                    {
                        output = $"Error: {e.Message}";
                    }

                    sb.AppendLine(output);
                }
            }
        }

        private string ProcessCommand(string line)
        {
            string output = string.Empty;

            List<string> args = line.Split().ToList();
            string command = args[0];

            switch (command)
            {
                case "RegisterDriver":
                    raceTower.RegisterDriver(args);
                    break;
                case "CompleteLaps":
                    output = raceTower.CompleteLaps(args);
                    break;
                case "Box":
                    raceTower.DriverBoxes(args);
                    break;
                case "Leaderboard":
                    output = raceTower.GetLeaderboard();
                    break;
            }

            currentLap = raceTower.CurrentLap;


            return output;
        }
    }
}
