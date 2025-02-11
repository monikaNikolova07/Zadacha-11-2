using Coffee;
using System.Formats.Asn1;
using System.Reflection;

namespace TestProject1CoffeeMashine
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void TestConstruction()
        {
            VendingCoffeeMachine vending = new VendingCoffeeMachine(3.0, 2.0);

            Assert.That(vending.CoffeePrice == 1.50);
            Assert.That(vending.CoffeeWithMilkPrice == 2.00);
            Assert.That(vending.CoffeeAmount == 3); 
            Assert.That(vending.MilkAmount == 2);
        }

        [Test]
        public void TestCoffeePrice()
        {
            VendingCoffeeMachine vending = new VendingCoffeeMachine(3.0, 2.0);
            Assert.That(vending.CoffeePrice == 1.50);
        }

        [Test]
        public void TestCoffeeWithMilkPrice()
        {
            VendingCoffeeMachine vending = new VendingCoffeeMachine(3.0, 2.0);
            Assert.That(vending.CoffeeWithMilkPrice == 2.00);
        }

        [Test]
        public void TestCoffeeAmount()
        {
            Assert.That(() => new VendingCoffeeMachine(-4, 2.0), Throws.ArgumentException);
        }

        [Test]
        public void TestMilkAmount()
        {
            Assert.That(() => new VendingCoffeeMachine(3.0, -2.0), Throws.ArgumentException);
        }

        // Valid cases
        [TestCase(1, 2.0, 0.5)]
        [TestCase(2, 3.0, 1)]
        public void OrderCoffee_ShouldOrderCorrectly(int choise, double insertedAmmount, double change)
        {
            VendingCoffeeMachine vending = new VendingCoffeeMachine(12.0, 100.0);
            Assert.That(vending.OrderCoffee(choise, insertedAmmount) == $"Напитката е приготвена. Вашето ресто: {change:F2} лв.");
        }

        // Invalid choice
        [TestCase(0, 2.0)]
        [TestCase(3, 3.0)]
        public void OrderCoffee_ShouldThrow_whenInvlaidChoise(int choise, double insertedAmmount)
        {
            VendingCoffeeMachine vending = new VendingCoffeeMachine(15.0, 101.0);
            Assert.That(() => vending.OrderCoffee(choise, insertedAmmount), Throws.ArgumentException);
        }

        // Invalid cases coffee amount
        [TestCase(1, 2.0, 0.5, 1)]
        [TestCase(2, 3.0, 1, 1)]
        public void OrderCoffee_ShouldDecreaseCoffeeAmountCorrectly(int choise, double insertedAmmount, double change,double asseptedAmmount)
        {
            VendingCoffeeMachine vending = new VendingCoffeeMachine(11.0, 101.0);
            vending.OrderCoffee(choise, insertedAmmount);
            Assert.That(() => vending.CoffeeAmount == asseptedAmmount);
        }

        // Invalid choice
        [TestCase(1, 2.0)]
        [TestCase(2, 3.0)]
        public void OrderCoffee_ShouldThrow_WhenInsufficientCoffee(int choise, double insertedAmmount)
        {
            VendingCoffeeMachine vending = new VendingCoffeeMachine(3.0, 101.0);
            Assert.That(() => vending.OrderCoffee(choise, insertedAmmount), Throws.InvalidOperationException);
        }

        // Valid cases
        [TestCase(1, 2.0, 0.5)]
        [TestCase(2, 3.0, 1)]
        public void OrderCoffee_ShouldCalculateChangeCorrectly(int choise, double insertedAmmount, double change)
        {
            VendingCoffeeMachine vending = new VendingCoffeeMachine(11.0, 101.0);
            Assert.That(() => vending.OrderCoffee(choise, insertedAmmount) == $"Напитката е приготвена. Вашето ресто: {change:F2} лв.");
        }

        // Valid cases
        [TestCase(2, 2.0, 0.5, 51)]
        [TestCase(2, 3.0, 1, 51)]
        public void OrderCoffee_ShouldDecreaseMilkAmountCorrectly(int choise, double insertedAmmount,
            double change, double asseptedAmmount)
        {
            VendingCoffeeMachine vending = new VendingCoffeeMachine(11.0, 101.0);
            vending.OrderCoffee(choise, insertedAmmount);
            Assert.That(() => vending.MilkAmount == asseptedAmmount);
        }
        
        // Invalid choice
        [TestCase(2, 2.0)]
        [TestCase(2, 3.0)]
        public void OrderCoffee_ShouldThrow_WhenInsufficientMilk(int choise, double insertedAmmount)
        {
            VendingCoffeeMachine vending = new VendingCoffeeMachine(11.0, 1.0);
            Assert.That(() => vending.OrderCoffee(choise, insertedAmmount), Throws.InvalidOperationException);
        }

        // Invalid choice
        [TestCase(1, 0.90, 2)]
        [TestCase(2, 0.05, 0.0)]
        public void OrderCoffee_ShouldThrow_WhenInsufficientPrice(int choise, double insertedAmmount, double change)
        {
            VendingCoffeeMachine vending = new VendingCoffeeMachine(11.0, 101.0);
            Assert.That(() => vending.OrderCoffee(choise, insertedAmmount), Throws.ArgumentException);
        }


    }
}