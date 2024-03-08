//C# Program to implement a Virtual Pet Simulator
using System;
using VirtualPetSimulator;
using VirtualPetSimulator.user;

namespace PetSim
{
    class MainLoop
    {
        public static void Main(string[] args)
        {
            User user = new User("Ash Ketchup", 12);

            Console.Write($"{user.Name} owns pet:\n{user.currentPet.describe()}");
        }
    }
}