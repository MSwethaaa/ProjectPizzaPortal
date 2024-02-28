using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ProjectPizzaPortal.Controllers;
using ProjectPizzaPortal.Models;
using System.Text;
using System.Threading.Tasks;

namespace PizzaLib
{
    [TestFixture]
    public class PizzaControllerTest
    {

        private PizzaController pizzaController;

        [SetUp]
        public void Setup()
        {
            // Assuming you have any necessary setup logic here
            pizzaController = new PizzaController();
        }

        [Test]
        public void PizzaSelection_ReturnsView()
        {
            // Arrange & Act
            var result = pizzaController.PizzaSelection();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ViewResult>(result);
        }

        [Test]
        public void OrderCheckout_WithValidPizzaType_ReturnsView()
        {
            // Arrange & Act
            var pizzaType = "Margherita";
            var result = pizzaController.OrderCheckout(pizzaType);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ViewResult>(result);

            // Additional assertion for the model passed to the view
            var model = (result as ViewResult)?.Model as Order;
            Assert.IsNotNull(model);
            Assert.AreEqual(pizzaType, model.Pizza);
        }

        [Test]
        public void OrderCheckout_WithInvalidPizzaType_RedirectsToPizzaSelection()
        {
            // Arrange & Act
            var invalidPizzaType = "InvalidPizzaType";
            var result = pizzaController.OrderCheckout(invalidPizzaType) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("PizzaSelection", result.ActionName);
        }

        [Test]
        public void OrderConfirmation_WithValidOrder_ReturnsView()
        {
            // Arrange & Act
            var pizzaType = "Margherita";
            var quantity = 2;
            var result = pizzaController.OrderConfirmation(pizzaType, quantity);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ViewResult>(result);

            // Additional assertion for the model passed to the view
            var model = (result as ViewResult)?.Model as ConfirmOrder;
            Assert.IsNotNull(model);
            Assert.AreEqual(pizzaType, model.Pizza);
            Assert.AreEqual(quantity, model.Quantity);
        }

        [Test]
        public void OrderConfirmation_WithInvalidPizzaType_RedirectsToPizzaSelection()
        {
            // Arrange & Act
            var invalidPizzaType = "InvalidPizzaType";
            var quantity = 3;
            var result = pizzaController.OrderConfirmation(invalidPizzaType, quantity) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("PizzaSelection", result.ActionName);
        }

        // Add more tests as needed for your specific actions in PizzaController
    }
}

