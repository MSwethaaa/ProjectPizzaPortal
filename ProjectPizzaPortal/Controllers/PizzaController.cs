using Microsoft.AspNetCore.Mvc;
using ProjectPizzaPortal.Models;

namespace ProjectPizzaPortal.Controllers
{
    public class PizzaController : Controller
    {
       
            // Dummy data for pizza types
            private static readonly List<Pizza> PizzaTypes = new List<Pizza>
    {
        new Pizza { PizzaId = 1, PizzaType = "Margherita", Price = 300.45 },
        new Pizza { PizzaId = 2, PizzaType = "Pepperoni Pizza", Price = 550.50 },
        new Pizza { PizzaId = 3, PizzaType = "Cheese Pizza", Price = 400.50 },
        new Pizza { PizzaId = 4, PizzaType = "Chicken Pizza", Price = 600.50 },
        new Pizza { PizzaId = 5, PizzaType = "Mushroom Pizza", Price = 450.50 },
        new Pizza { PizzaId = 6, PizzaType = "Plain Pizza", Price = 99.50 }
        // Add more pizza types as needed
    };

            [HttpGet]
            public IActionResult PizzaSelection()
            {
                // Pass pizza types to the PizzaSelection view
                return View(PizzaTypes);
            }

            [HttpGet]
            public IActionResult OrderCheckout(string pizzaType)
            {
                // Get the selected pizza based on the pizza type
                var selectedPizza = PizzaTypes.FirstOrDefault(pizza => pizza.PizzaType == pizzaType);

                if (selectedPizza == null)
                {
                    // Handle invalid pizza type
                    return RedirectToAction("PizzaSelection");
                }

                // Pass data to the OrderCheckout view
                var model = new Order
                {
                    Pizza = pizzaType
                };

                return View(model);
            }

            [HttpPost]
            public IActionResult OrderConfirmation(string pizzaType, int quantity)
            {
                // Your logic to process the order (save to database, etc.)

                // Retrieve pizza details based on the selected pizza in the order
                var selectedPizza = PizzaTypes.FirstOrDefault(pizza => pizza.PizzaType == pizzaType);

                if (selectedPizza == null)
                {
                    // Handle invalid pizza type
                    return RedirectToAction("PizzaSelection");
                }

                // Calculate the total order amount
                var orderAmount = CalculateOrderAmount(selectedPizza.Price, quantity);

                // For simplicity, assuming you save the order and retrieve the order details
                var confirmedOrder = new ConfirmOrder
                {
                    OrderId = GenerateOrderId(),
                    Pizza = selectedPizza.PizzaType,
                    Quantity = quantity,
                    Amount = orderAmount
                };

                // Redirect to OrderConfirmation view with the confirmed order details
                return View("OrderConfirmation", confirmedOrder);
            }

            public string GenerateOrderId()
            {
                // Replace this with your actual logic to generate a unique order ID
                // For simplicity, returning a dummy order ID
                return Guid.NewGuid().ToString();
            }

            public double CalculateOrderAmount(double pizzaPrice, int quantity)
            {
                // Replace this with your actual logic to calculate the order amount
                // For simplicity, returning a fixed amount for each pizza type
                return pizzaPrice * quantity;
            }

            public void ConfigureServices(ServiceCollection services)
            {
                throw new NotImplementedException();
            }
        }
    }

   
