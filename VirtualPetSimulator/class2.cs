//harsh
using System;


namespace VirtualPetSimulator.Shop
{
    interface IAccessory
    {
        void ApplyEffect(Player player);
    }

    interface IPetAccessory
    {
        void ApplyEffect(Pet pet);
    }

    // Single responsibility principle applied.
    class StatAccessory : IAccessory, IPetAccessory
    {
        private readonly string _name;
        private readonly int _healthBonus;
        private readonly int _damageBonus;

        public StatAccessory(string name, int healthBonus, int damageBonus)
        {
            _name = name;
            _healthBonus = healthBonus;
            _damageBonus = damageBonus;
        }

        public void ApplyEffect(Player player)
        {
            player.Health += _healthBonus;
            player.Damage += _damageBonus;
            Console.WriteLine($"Equipped {_name} on player. Health +{_healthBonus}, Damage +{_damageBonus}");
        }

        public void ApplyEffect(Pet pet)
        {
            pet.Health += _healthBonus;
            Console.WriteLine($"Equipped {_name} on pet. Health +{_healthBonus}");
        }
    }

    class Player
    {
        public int Health { get; set; }
        public int Damage { get; set; }

        public Player(int health, int damage)
        {
            Health = health;
            Damage = damage;
        }

        public void ShowStatus()
        {
            Console.WriteLine($"Player Health: {Health}, Damage: {Damage}");
        }
    }

    class Pet
    {
        public int Health { get; set; }

        public Pet(int health)
        {
            Health = health;
        }

        public void ShowStatus()
        {
            Console.WriteLine($"Pet Health: {Health}");
        }
    }

    // Open/closed principle applied.
    class Shop : IStore
    {
        public void BuyAccessory(IAccessory accessory, Player player)
        {
            accessory.ApplyEffect(player);
        }

        public void BuyPetAccessory(IPetAccessory accessory, Pet pet)
        {
            accessory.ApplyEffect(pet);
        }
    }

    interface IStore
    {
        void BuyAccessory(IAccessory accessory, Player player);
        void BuyPetAccessory(IPetAccessory accessory, Pet pet);
    }

    /*class Program
    {
        static void Main(string[] args)
        {
            Player player = new Player(100, 10);
            Pet pet = new Pet(50);
            Shop shop = new Shop();

            // Buying accessories
            IAccessory minorStatBuff = new StatAccessory("Minor Stat Buff", 5, 1);
            shop.BuyAccessory(minorStatBuff, player);

            // Buying upgrades (Scaling with purchases)
            for (int i = 0; i < 3; i++)
            {
                IAccessory statUpgrade = new StatAccessory($"Stat Upgrade {i + 1}", 10 * (i + 1), 2 * (i + 1));
                shop.BuyAccessory(statUpgrade, player);
            }

            // Buying pet accessories
            IPetAccessory petStatBuff = new StatAccessory("Pet Stat Buff", 3, 0);
            shop.BuyPetAccessory(petStatBuff, pet);

            player.ShowStatus();
            pet.ShowStatus();
        }
    }*/
}