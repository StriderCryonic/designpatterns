//yaseen and mervyn
using System;
using VirtualPetSimulator.VirtualPet;
using VirtualPetSimulator.user;

namespace VirtualPetSimulator.dungeon
{
    class Dungeon
    {
        private Random random = new Random();
        private int randomNumber;
        private Enemy currentEnemy;

        public Dungeon()
        {
            randomNumber = random.Next(1, 101);

            if (randomNumber <= 25)
                currentEnemy = new Slime();
            else if (randomNumber <= 50)
                currentEnemy = new Hilichurl();
            else if (randomNumber <= 75)
                currentEnemy = new Megaslime();
            else
                currentEnemy = new Childe();

            Console.WriteLine($"A wild {currentEnemy.GetType().Name} appears in the dungeon!");
        }

        public void StartCombat(Pet pet)
        {
            Console.WriteLine("Combat begins!");

            while (currentEnemy.IsAlive() && pet.getHealth() > 0)
            {
                int petDamage = pet.getDamage();
                currentEnemy.TakeDamage(petDamage);

                Console.WriteLine($"Pet attacks {currentEnemy.GetType().Name} for {petDamage} damage.");

                if (!currentEnemy.IsAlive())
                {
                    Console.WriteLine($"{currentEnemy.GetType().Name} is defeated! You win!");
                    break;
                }

                int enemyDamage = currentEnemy.Attack();
                pet.updateHealth(-enemyDamage);

                Console.WriteLine($"{currentEnemy.GetType().Name} attacks Pet for {enemyDamage} damage.");

                if (pet.getHealth() <= 0)
                {
                    if (random.Next(1, 11) == 1)
                    {
                        Console.WriteLine($"Pet has died! {currentEnemy.GetType().Name} wins!");
                    }
                    else
                    {
                        Console.WriteLine($"Pet is defeated, but survives this round.");
                    }
                    break;
                }
            }
        }
    }

    abstract class Enemy
    {
        protected int hp;
        protected int attackPower;

        public bool IsAlive()
        {
            return hp > 0;
        }

        public void TakeDamage(int damage)
        {
            hp -= damage;
            if (hp < 0)
                hp = 0;
        }

        public int Attack()
        {
            return attackPower;
        }
    }

    class Slime : Enemy
    {
        public Slime()
        {
            hp = 20;
            attackPower = 5;
        }
    }

    class Hilichurl : Enemy
    {
        public Hilichurl()
        {
            hp = 30;
            attackPower = 8;
        }
    }

    class Megaslime : Enemy
    {
        public Megaslime()
        {
            hp = 40;
            attackPower = 5;
        }
    }

    class Childe : Enemy
    {
        public Childe()
        {
            hp = 50;
            attackPower = 10;
        }
    }


    /*
        class Program
        {
            static void Main()
            {
                Console.WriteLine("Welcome to the Dungeon Adventure!");

                Console.Write("Enter your name: ");
                string userName = Console.ReadLine();

                Console.Write("Enter your age: ");
                int userAge;
                while (!int.TryParse(Console.ReadLine(), out userAge))
                {
                    Console.Write("Invalid age. Please enter a valid number: ");
                }

                User user = new User(userName, userAge);
                DLC decoratedUser = new DLC(user);

                Console.WriteLine($"\nUser Details:\nName: {user.Name}\nAge: {user.Age}\nBalance: {user.Balance}\n");

                Dungeon dungeon = new Dungeon();
                Pet pet = new Pet();

                dungeon.StartCombat(pet);

                decoratedUser.ApplyDLC();

                Console.WriteLine($"\nUpdated Balance after DLC: {user.Balance}");

                Console.WriteLine("\nThanks for playing the Dungeon Adventure!");
            }
        }
    */
    class DLC
    {
        private User user;
        public DLC(User user)
        {
            this.user = user;
        }

        public void ApplyDLC()
        {
            user.Balance += 200;
        }
    }


}
namespace VirtualPetSimulator.user
{
    class User
    {
        public string Name { get; }
        public int Age { get; }
        private int balance;
        public PetBreedDecorator currentPet;


        public User(string name, int age)
        {
            Name = name;
            Age = age;
            balance = 200;

            Gacha gachapon = new Gacha();
            currentPet = gachapon.getGacha();
        }

        public int Balance
        {
            get { return balance; }
            set { balance = value; }
        }
    }
}
