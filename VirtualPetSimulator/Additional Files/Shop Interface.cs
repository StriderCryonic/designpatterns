using System;

// Component Interface
public interface IShop
{
    string GetDescription();
    double GetCost();
}

// Concrete Component - ToyShop
public class ToyShop : IShop
{
    public string GetDescription()
    {
        return "Toy Shop";
    }

    public double GetCost()
    {
        return 50.0;
    }
}

// Concrete Component - FoodShop
public class FoodShop : IShop
{
    public string GetDescription()
    {
        return "Food Shop";
    }

    public double GetCost()
    {
        return 30.0;
    }
}

// Decorator Interface (Interface Segregation Principle)
public interface IShopDecorator : IShop
{
    // Additional methods specific to decorators can be added here
}

// Decorator Abstract Class (Open/Closed Principle, Dependency Inversion Principle)
public abstract class ShopDecorator : IShopDecorator
{
    protected IShop decoratedShop;

    public ShopDecorator(IShop shop)
    {
        decoratedShop = shop;
    }

    // Open/Closed Principle: Extension without modification
    public virtual string GetDescription()
    {
        return decoratedShop.GetDescription();
    }

    // Open/Closed Principle: Extension without modification
    public virtual double GetCost()
    {
        return decoratedShop.GetCost();
    }
}

// Concrete Decorator - DiscountDecorator
public class DiscountDecorator : ShopDecorator
{
    public DiscountDecorator(IShop shop) : base(shop)
    {
    }

    // Liskov Substitution Principle: Subclass extends behavior without changing base behavior
    public override string GetDescription()
    {
        // Decorate the description
        return $"{base.GetDescription()} with Discount";
    }

    // Liskov Substitution Principle: Subclass extends behavior without changing base behavior
    public override double GetCost()
    {
        // Decorate the cost with a discount
        return base.GetCost() * 0.9;
    }
}

class Program
{
    static void Main()
    {
        // Creating a ToyShop
        IShop toyShop = new ToyShop();
        DisplayShopDetails(toyShop);

        // Adding a discount to the ToyShop
        IShopDecorator discountedToyShop = new DiscountDecorator(toyShop);
        DisplayShopDetails(discountedToyShop);

        // Creating a FoodShop
        IShop foodShop = new FoodShop();
        DisplayShopDetails(foodShop);

        // Adding a discount to the FoodShop
        IShopDecorator discountedFoodShop = new DiscountDecorator(foodShop);
        DisplayShopDetails(discountedFoodShop);
    }

    // Single Responsibility Principle: A method for displaying shop details
    static void DisplayShopDetails(IShop shop)
    {
        Console.WriteLine($"Description: {shop.GetDescription()}, Cost: ${shop.GetCost()}");
    }
}
