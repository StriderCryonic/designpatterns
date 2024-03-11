//vignesh
using System;
using System.Globalization;

namespace VirtualPetSimulator.VirtualPet
{
    public interface iPetComponent {
        void updateHealth(int change);
        void updateDamage(int change);
        void incrementLevel();
        string describe();
        int getLevel();
        int getDamage();
        int getHealth();
        string getHealthStatus();
        int gethealthPurchaseCount();
        int getdamagePurchaseCount();
        void updatehealthPurchaseCount();
        void updatedamagePurchaseCount();
        void updatemaxHealth();
    }
    //abstract component class for further abstraction

    public abstract class Pet : iPetComponent
    {
        protected int maxHealth = 1;
        protected int health = 1;   
        protected int damage = 1;
        protected int level = 1;

        public int healthPurchaseCount = 0;
        public int damagePurchaseCount = 0;
        public double difficultyScale = 0.1;
        public double costScale = 10;

        public void incrementLevel()
        {
            this.level += 1;
        }

        public void updateDamage(int change)
        {
            this.damage += change;
        }

        public void updateHealth(int change)
        {
            this.health += change;
        }

        public virtual string describe()
        {
            return "Default Description";
        }

        public int getDamage()
        {
            return this.damage;
        }
        public int getHealth()
        {
            return this.health;
        }
        public string getHealthStatus()
        {
            return $"{this.health}/{this.maxHealth}";
        }

        public int getLevel()
        {
            return this.level;
        }

        public int gethealthPurchaseCount()
        {
            return this.healthPurchaseCount;
        }
        public int getdamagePurchaseCount()
        {
            return this.damagePurchaseCount;
        }

        public void updatemaxHealth()
        {
            this.maxHealth += 5;
        }

        public void updatehealthPurchaseCount()
        {
            this.healthPurchaseCount++;
        }

        public void updatedamagePurchaseCount()
        {
            this.damagePurchaseCount++;
        }
    }

    //concrete pet classes
    class Dog : Pet
    {
        public Dog()
        {
            this.health = 10;
            this.maxHealth = this.health;
            this.damage = 1;
        }

        public override string describe()
        {
            return "Pet Type: Dog(Common)\n";
        }
    }
    class Dingo : Pet
    {
        public Dingo()
        {
            this.health = 8;
            this.maxHealth = this.health;
            this.damage = 3;
        }
        public override string describe()
        {
            return "Pet Type: Dingo(Uncommon)\n";
        }
    }
    class Dragon : Pet
    {
        public Dragon()
        {
            this.health = 20;
            this.maxHealth = this.health;
            this.damage = 10;
        }
        public override string describe()
        {
            return "Pet Type: Dragon(Epic)\n";
        }
    }
    class Dodo : Pet
    {
        public Dodo()
        {
            this.health = 5;
            this.maxHealth = this.health;
            this.damage = 100;
        }
        public override string describe()
        {
            return "Pet Type: Dodo(Legendary)\n";
        }
    }
    class Default : Pet
    {

    }

    //abstract breed decorator
    public abstract class PetBreedDecorator : iPetComponent
    {
        private iPetComponent _pet;
        public PetBreedDecorator(iPetComponent pet)
        {
            this._pet = pet;
        }

        public void incrementLevel()
        {
            this._pet.incrementLevel();
        }

        public void updateDamage(int change)
        {
            this._pet.updateDamage(change);
        }

        public void updateHealth(int change)
        {
            this._pet.updateHealth(change);
        }

        public virtual string describe()
        {
            return this._pet.describe();
        }

        public int getLevel()
        {
            return _pet.getLevel();
        }

        public int getDamage()
        {
            return _pet.getDamage();
        }

        public int getHealth()
        {
            return _pet.getHealth();
        }

        public string getHealthStatus()
        {
            return _pet.getHealthStatus();
        }
        public int gethealthPurchasecount()
        {
            return _pet.gethealthPurchaseCount();
        }
        public int getdamagePurchasecount()
        {
            return _pet.getdamagePurchaseCount();
        }

        public int gethealthPurchaseCount()
        {
            return _pet.gethealthPurchaseCount();
        }

        public int getdamagePurchaseCount()
        {
            return _pet.getdamagePurchaseCount();
        }

        public void updatemaxHealth()
        {
            _pet.updatemaxHealth();
        }

        public void updatehealthPurchaseCount()
        {
            _pet.updatehealthPurchaseCount();
        }

        public void updatedamagePurchaseCount()
        {
            _pet.updatedamagePurchaseCount();
        }
    }

    //concrete breed decorators
    public class highBreed : PetBreedDecorator
    {
        public highBreed(Pet pet) : base(pet) { }

        public override string describe()
        {
            return base.describe() + $"Breed: High (Stats++)\nHealth: {base.getHealthStatus()}\nDamage: {base.getDamage() * 3} ({base.getDamage()})\n";
        }
    }

    public class mediumBreed : PetBreedDecorator
    {
        public mediumBreed(Pet pet) : base(pet) { }

        public override string describe()
        {
            return base.describe() + $"Breed: Medium (Stats+)\nHealth: {base.getHealthStatus()}\nDamage: {base.getDamage() * 2} ({base.getDamage()})\n";
        }
    }

    public class lowBreed : PetBreedDecorator
    {
        public lowBreed(Pet pet) : base(pet) { }

        public override string describe()
        {
            return base.describe() + $"Breed: Low (No change in stats)\nHealth: {base.getHealthStatus()}\nDamage: {base.getDamage()}\n";
        }
    }


    //gacha

    public class Gacha
    {
        PetBreedDecorator gachaPet;
        Pet objPet;

        public PetBreedDecorator getGacha()
        {
            Random rnd = new Random();
            //there are 4 pets, and 3 breed types
            int pet = rnd.Next(0, 4);
            int breed = rnd.Next(0, 3);


            switch (pet)
            {
                case 0:
                    objPet = new Dog();
                    break;
                case 1:
                    objPet = new Dingo();
                    break;
                case 2:
                    objPet = new Dragon();
                    break;
                case 3:
                    objPet = new Dodo();
                    break;
                default://will never run
                    objPet = new Default();
                    break;
            }

            switch (breed)
            {
                case 0:
                    gachaPet = new highBreed(objPet);
                    break;
                case 1:
                    gachaPet = new mediumBreed(objPet);
                    break;
                case 2:
                    gachaPet = new lowBreed(objPet);
                    break;
                default: //will never run
                    gachaPet = new lowBreed(objPet);
                    break;
            }


            return gachaPet;
        }
    }
}
