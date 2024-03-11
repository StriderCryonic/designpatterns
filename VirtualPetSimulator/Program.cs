//C# Program to implement a Virtual Pet Simulator
using System;
using System.ComponentModel;
using VirtualPetSimulator;
using VirtualPetSimulator.user;
using VirtualPetSimulator.VirtualPet;
using VirtualPetSimulator.work;

namespace PetSim
{
    class Game
    {
        public void getUserConfirmation()
        {
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }
        public PetBreedDecorator rollGacha(User user)
        {
            Console.Write("Rolling Gacha.");
            for (int i = 0; i < 5; i++)
            {
                Console.Write(".");
                Thread.Sleep(200);
            }
            Console.WriteLine();    
            return user.gachapon.getGacha();
        }
        public User intro()
        {
            Console.Write("Enter your name:");
            string name = Console.ReadLine();
            if (name == null)
            {
                name = "defaultName";
            }
            Console.Write("Enter your age:");
            int age = Convert.ToInt32(Console.ReadLine());
            User user = new User(name, age);

            Console.Write($"Welcome, {user.Name}.\nRolling Gacha for First Pet.\n");
            user.currentPet = rollGacha(user);
            Console.Write($"\nYour first pet is:\n{user.currentPet.describe()}\nCongratulations!\n");

            getUserConfirmation();
            Console.Clear();
            return user;

        }

        public void shop(User user)
        {
            string temp;
            int choice = -1;
            Console.Clear();
            Console.WriteLine("Welcome to the shop!\nPlease select your option.");
            while(choice != 4)
            {
                Console.Write($"{user.Name}'s Balance: {user.Balance}.\n1. View current pet stats\n2. Upgrade Pet Health(Cost={50 + (user.currentPet.gethealthPurchasecount() * 10)})\n3. Upgrade Pet Damage(Cost={50 + (user.currentPet.getdamagePurchasecount() * 10)})\n4. Leave shop:");
                choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        Console.WriteLine(user.currentPet.describe() + "\n\n");
                        getUserConfirmation();
                        Console.Clear();
                        break;
                    case 2:
                        if (user.Balance >= (50 + (user.currentPet.gethealthPurchasecount() * 10)))
                        {
                            temp = user.currentPet.getHealthStatus();
                            //purchase
                            user.currentPet.updatemaxHealth();
                            user.currentPet.updateHealth(5);
                            user.Balance -= 50 + user.currentPet.gethealthPurchasecount() * 10;
                            user.currentPet.updatehealthPurchaseCount();
                            Console.WriteLine($"Purchased health upgrade!\nStat Change: {temp} --> {user.currentPet.getHealthStatus()}");
                        }
                        else
                        {
                            Console.WriteLine("Failure!(Insufficient Balance.)");
                        }
                        getUserConfirmation();
                        Console.Clear();
                        break;
                    case 3:
                        if (user.Balance >= (50 + (user.currentPet.getdamagePurchasecount() * 10)))
                        {
                            temp = user.currentPet.getDamage().ToString();
                            //purchase
                            user.currentPet.updateDamage(5);
                            user.Balance -= 50 + (user.currentPet.getdamagePurchasecount() * 10);
                            user.currentPet.updatedamagePurchaseCount();
                            Console.WriteLine($"Purchased damage upgrade!\nStat Change: {temp} --> {user.currentPet.getDamage()}");
                        }
                        else
                        {
                            Console.WriteLine("Failure!(Insufficient Balance.)");
                        }
                        getUserConfirmation();
                        Console.Clear();
                        break;
                    case 4:
                        break;
                    default:
                        Console.WriteLine("Invalid Option, Try again.");
                        getUserConfirmation();
                        Console.Clear();
                        break;
                }
            }
        }

        public bool gameLoop(User user)
        {
            Object temp;
            Console.Write($"[Day {user.dayCount}]\n\nSelect what you want to do:\n\n1.View pet stats\n2.Go to Dungeons\n3.Go to work\n4.Visit Shop\n5.Exit:");
            int choice = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();
            switch (choice)
            {
                case 1:
                    Console.WriteLine(user.currentPet.describe() + "\n");
                    getUserConfirmation();
                    user.dayCount--;
                    break;
                case 2:
                    Console.WriteLine("Dungeon Visited.");
                    getUserConfirmation();
                    Console.Clear();
                    break;
                case 3:
                    if (user.currentJob == null)
                    {
                        user.currentJob = new DeliveryJob();
                    }
                    temp = user.currentJob;
                    user.Balance += user.currentJob.GetIncome();
                    Console.WriteLine($"You have spent all day at work.\n{user.currentJob.GetIncome()} Units have been credited to your account.\nYour new balance is {user.Balance}");
                    user.checkjobUpgrade();
                    if (user.currentJob != temp)
                    {
                        Console.WriteLine($"Your Job has been upgraded! Congratulations. You are now making {user.currentJob.GetIncome()} Units.");
                    }
                    getUserConfirmation();
                    break;
                case 4:
                    shop(user);
                    Console.Clear();
                    user.dayCount--;
                    break;
                case 5:
                    Console.Clear();
                    Console.WriteLine("Thanks for playing! See you next time!");
                    return true;
            }
            return false;
        }

        public void runGame() {
            User user = intro();
            while (!gameLoop(user))
            {
                Console.Clear();
                user.dayCount += 1;
            }
        }
    }
    class MainLoop
    {
        

        public static void Main(string[] args)
        {
            Game game = new Game();
            game.runGame();
        }
    }
}