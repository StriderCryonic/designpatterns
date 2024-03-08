//C# Program to implement a Virtual Pet Simulator
using System;
using VirtualPetSimulator.VirtualPet;

namespace PetSim
{
    class MainLoop
    {
        public static void Main(string[] args)
        {
            Gacha gachabox = new Gacha();
            PetBreedDecorator newPet = gachabox.getGacha();
            Console.WriteLine(newPet.describe());
        }
    }
}