using System;
using System.Collections.Generic;

namespace YourNamespace
{
    // Interface for job types
    public interface IJob
    {
        int GetIncome();
    }

    // Delivery job type
    public class DeliveryJob : IJob
    {
        public int GetIncome()
        {
            return 50;
        }
    }

    // Retail job type
    public class RetailJob : IJob
    {
        public int GetIncome()
        {
            return 75;
        }
    }

    // Office job type
    public class OfficeJob : IJob
    {
        public int GetIncome()
        {
            return 100;
        }
    }

    // Class responsible for managing job types and income generation
    public class Work
    {
        // Dictionary to store earnings from each job type
        private static Dictionary<Type, int> earnings = new Dictionary<Type, int>();

        // Perform work and earn income based on job type
        public static int DoJob(IJob job)
        {
            int income = job.GetIncome();
            Type jobType = job.GetType();

            if (earnings.ContainsKey(jobType))
            {
                earnings[jobType] += income;
            }
            else
            {
                earnings.Add(jobType, income);
            }

            return income;
        }

        // Get total earnings from all job types
        public static int GetTotalEarnings()
        {
            int totalEarnings = 0;
            foreach (var income in earnings.Values)
            {
                totalEarnings += income;
            }
            return totalEarnings;
        }
    }
}
/*
    public class Program
    {
        public static void Main(string[] args)
        {
            int userInput = 0;
            int totalSessionEarnings = 0;

            while (userInput != 5)
            {
                Console.WriteLine("Select a job type:");
                Console.WriteLine("1. Delivery");
                Console.WriteLine("2. Retail");
                Console.WriteLine("3. Office");
                Console.WriteLine("4. Exit and Display Salary");
                Console.WriteLine("5. Exit");

                // Read user input
                userInput = int.Parse(Console.ReadLine());

                // Perform job based on user input
                switch (userInput)
                {
                    case 1:
                        IJob deliveryJob = new DeliveryJob();
                        int deliveryIncome = Work.DoJob(deliveryJob);
                        Console.WriteLine($"1 hour of Delivery work completed. Earned: {deliveryIncome}");
                        totalSessionEarnings += deliveryIncome;
                        break;
                    case 2:
                        IJob retailJob = new RetailJob();
                        int retailIncome = Work.DoJob(retailJob);
                        Console.WriteLine($"1 hour of Retail work completed. Earned: {retailIncome}");
                        totalSessionEarnings += retailIncome;
                        break;
                    case 3:
                        IJob officeJob = new OfficeJob();
                        int officeIncome = Work.DoJob(officeJob);
                        Console.WriteLine($"1 hour of Office work completed. Earned: {officeIncome}");
                        totalSessionEarnings += officeIncome;
                        break;
                    case 4:
                        Console.WriteLine($"Total earnings from the beginning of the session: {totalSessionEarnings}");
                        break;
                    case 5:
                        Console.WriteLine("Exiting...");
                        break;
                    default:
                        Console.WriteLine("Invalid input. Please enter a number between 1 and 5.");
                        break;
                }
            }

            Console.WriteLine($"Total Earnings: {Work.GetTotalEarnings()}");
        }
    }
}
*/