using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee
{
    public class VendingCoffeeMachine
    {
        private double coffeePrice;
        private double coffeeWithMilkPrice;
        private double coffeeAmount;
        private double milkAmount;
        public double CoffeePrice
        {
            get { return coffeePrice; }
            private set { coffeePrice = value; }
        }

        public double CoffeeWithMilkPrice
        {
            get { return coffeeWithMilkPrice; }
            private set { coffeeWithMilkPrice = value; }
        }

        public double CoffeeAmount
        {
            get { return coffeeAmount; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Количество кафе не може да бъде отрицателно.");
                }
                coffeeAmount = value;
            }
        }

        public double MilkAmount
        {
            get { return milkAmount; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Количество мляко не може да бъде отрицателно.");
                }
                milkAmount = value;
            }
        }
        public VendingCoffeeMachine(double initialCoffeeAmount, double initialMilkAmount)
        {
            CoffeePrice = 1.50;
            CoffeeWithMilkPrice = 2.00;
            CoffeeAmount = initialCoffeeAmount;
            MilkAmount = initialMilkAmount;
        }

        public string OrderCoffee(int choice, double insertedAmount)
        {
            double price;
            if (choice == 1)
            {
                price = coffeePrice;
                if (CoffeeAmount < 10)
                {
                    throw new InvalidOperationException("Недостатъчно кафе за приготвяне на напитката.");
                }
            }
            else if (choice == 2)
            {
                price = CoffeeWithMilkPrice;
                if (coffeeAmount < 10)
                {
                    throw new InvalidOperationException("Недостатъчно кафе за приготвяне на напитката.");
                }
                if (MilkAmount < 50)
                {
                    throw new InvalidOperationException("Недостатъчно мляко за приготвяне на напитката.");
                }
            }
            else
            {
                throw new ArgumentException("Невалиден избор. Моля, изберете 1 за кафе или 2 за кафе с мляко.");
            }


            if (insertedAmount < price)
            {
                double deficit = price - insertedAmount;
                throw new ArgumentException($"Недостатъчно средства. Напитката струва {price:F2} лв, а вие сте въвели {insertedAmount:F2} лв. Трябват още {deficit:F2} лв.");
            }


            CoffeeAmount -= 10;
            if (choice == 2)
            {
                MilkAmount -= 50;
            }

            double change = insertedAmount - price;
            return $"Напитката е приготвена. Вашето ресто: {change:F2} лв.";
        }

        public double GetRemainingCoffee()
        {
            return CoffeeAmount;
        }

        public double GetRemainingMilk()
        {
            return MilkAmount;
        }
    
    }
}
