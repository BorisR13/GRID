using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GRID
{
    class RaceTower
    {
        private int LapsNumber;
        private int TrackLength;
        public int CurrentLap { get; private set; }

        private Dictionary<string, Driver> drivers;
        private Dictionary<string, Driver> dnfDrivers;
        private List<Driver> leaderboard;

        public RaceTower()
        {
            drivers = new Dictionary<string, Driver>();
            dnfDrivers = new Dictionary<string, Driver>();
            leaderboard = new List<Driver>();
            drivers.OrderBy(d => d.Value.TotalTime);
        }

        public void SetTrackInfo(int lapsNumber, int trackLength)
        {
            this.LapsNumber = lapsNumber;
            this.TrackLength = trackLength;
        } // done

        public void RegisterDriver(List<string> commandArgs)
        {
            string type = commandArgs[1];
            string name = commandArgs[2];
            int hp = int.Parse(commandArgs[3]);
            double fuelAmount = double.Parse(commandArgs[4]);
            string tyreType = commandArgs[5];
            double tyreHardness = double.Parse(commandArgs[6]);
            Tyre tyre;

            if (tyreType == "Ultrasoft")
            {
                double grip = double.Parse(commandArgs[7]);
                tyre = new UltrasoftTyre(tyreHardness, grip);
            }
            else
            {
                tyre = new HardTyre(tyreHardness);
            }

            Car car = new Car(hp, fuelAmount, tyre);
            Driver driver;
            if (type == "Aggressive")
            {
                driver = new AggressiveDriver(name, 0, car);
                drivers.Add(driver.Name, driver);
            }
            else if (type == "Endurance")
            {
                driver = new EnduranceDriver(name, 0, car);
                drivers.Add(driver.Name, driver);
            }
        } // done

        public void DriverBoxes(List<string> commandArgs)// done
        {
            string reasonToBox = commandArgs[1];
            string driversName = commandArgs[2];
            drivers[driversName].TotalTime += 20;

            if (reasonToBox == "Refuel")
            {
                double fuelAmount = double.Parse(commandArgs[3]);

                if (drivers[driversName].Car.FuelAmount + fuelAmount > 160)
                {
                    drivers[driversName].Car.FuelAmount = 160;
                }
                else
                {
                    drivers[driversName].Car.FuelAmount += fuelAmount;
                }
            }
            else if (reasonToBox == "ChangeTyres")
            {
                string tyreType = commandArgs[3];
                double hardness = double.Parse(commandArgs[4]);

                if (tyreType == "Ultrasoft")
                {
                    double grip = double.Parse(commandArgs[5]);
                    Tyre tyre = new UltrasoftTyre(hardness, grip);
                    drivers[driversName].Car.Tyre = tyre;
                }
                else if (tyreType == "Hard")
                {
                    Tyre tyre = new HardTyre(hardness);
                    drivers[driversName].Car.Tyre = tyre;
                }
            }
        }

        public string CompleteLaps(List<string> commandArgs)
        {
            string output;
            try
            {
                output = Action(commandArgs);
            }
            catch (InvalidOperationException e)
            {
                output = e.Message;
                throw new InvalidOperationException(e.Message);
            }

            return output;
        } // done

        private string Action(List<string> commandArgs)
        {
            string output = string.Empty;
            int numberOfLaps = int.Parse(commandArgs[1]);
            int remainingLaps = LapsNumber - CurrentLap;

            if (numberOfLaps > remainingLaps)
            {
                output = $"There is no time!On lap {CurrentLap}.";
                throw new InvalidOperationException(output);
            }

            for (int i = 0; i < numberOfLaps; i++)
            {
                foreach (var driver in drivers)
                {
                    driver.Value.TotalTime += 60 / (TrackLength / driver.Value.Speed);
                    driver.Value.Car.FuelAmount -= TrackLength * driver.Value.FuelConsumptionPerKm;
                    driver.Value.Car.Tyre.Degradate();

                    if (driver.Value.Car.OutOfFuel)
                    {
                        Driver dnfDriver = driver.Value;
                        dnfDriver.ReasonToDnf = "Out of fuel!";
                        dnfDrivers.Add(driver.Value.Name, dnfDriver);
                        drivers.Remove(driver.Value.Name);
                    }
                    else if (driver.Value.Car.Tyre.BlownUp)
                    {
                        Driver dnfDriver = driver.Value;
                        dnfDriver.ReasonToDnf = "Blown tyre!";
                        dnfDrivers.Add(driver.Value.Name, dnfDriver);
                        drivers.Remove(driver.Value.Name);
                    }
                }

                CurrentLap++;
            }

            return output; ;
        }

        public string GetLeaderboard()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{CurrentLap}/{LapsNumber}");

            drivers.OrderBy(driver => driver.Value.TotalTime);
            drivers.Concat(dnfDrivers);

            foreach (var driver in drivers)
            {
                leaderboard.Add(driver.Value);
            }

            leaderboard.OrderBy(d => d.TotalTime);

            foreach (var driver in leaderboard)
            {
                int position = leaderboard.IndexOf(driver) + 1;
                if (driver.ReasonToDnf == null)
                {
                    sb.AppendLine($"{position} {driver.Name} {driver.TotalTime:f3}");
                }
                else
                {
                    sb.AppendLine($"{position} {driver.Name} {driver.ReasonToDnf}");
                }
                position++;
            }

            return sb.ToString().Trim();
        } // done

        public string Winner()
        {
            leaderboard.OrderBy(d => d.TotalTime);
            Driver winner = leaderboard.First();
            string output = $"{winner.Name} wins the race for {winner.TotalTime:f3} seconds";

            return output;
        }

    }
}
