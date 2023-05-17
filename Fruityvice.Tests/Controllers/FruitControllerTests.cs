using Fruityvice.Controllers;
using Fruityvice.Models;
using Fruityvice.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Fruityvice.Tests.Controllers
{
    [TestFixture]
    public class FruitControllerTests
    {
        private Mock<IFruitService> _mockFruitService;
        private Mock<ILogger<FruitController>> _mockLogger;
        private FruitController _fruitController;

        [SetUp]
        public void Setup()
        {
            _mockFruitService = new Mock<IFruitService>();
            _mockLogger = new Mock<ILogger<FruitController>>();
            _fruitController = new FruitController(_mockFruitService.Object, _mockLogger.Object);
        }

        [Test]
        public async Task GetAllFruits_ReturnsOkResult_WithListOfFruits()
        {
            Fruit[] expectedFruits = new[] { new Fruit { Name = "Apple" }, new Fruit { Name = "Banana" } };
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonSerializer.Serialize(expectedFruits))
            };
            _mockFruitService.Setup(service => service.GetAllFruits()).ReturnsAsync(expectedFruits);

            IActionResult result = await _fruitController.GetAllFruits();

            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            OkObjectResult okResult = (OkObjectResult)result;
            Assert.That(okResult.Value, Is.EqualTo(expectedFruits));
        }

        [Test]
        public async Task GetAllFruits_Returns503ServiceUnavailable_WhenHttpRequestFails()
        {
            _mockFruitService.Setup(service => service.GetAllFruits()).ThrowsAsync(new HttpRequestException());

            IActionResult result = await _fruitController.GetAllFruits();

            Assert.That(result, Is.InstanceOf<ObjectResult>());
            ObjectResult statusCodeResult = (ObjectResult)result;
            Assert.That(statusCodeResult.StatusCode, Is.EqualTo(StatusCodes.Status503ServiceUnavailable));
        }

        [Test]
        public async Task GetAllFruits_Returns500InternalServerError_WhenJsonDeserializationFails()
        {
            _mockFruitService.Setup(service => service.GetAllFruits()).ThrowsAsync(new JsonException());

            IActionResult result = await _fruitController.GetAllFruits();

            Assert.That(result, Is.InstanceOf<ObjectResult>());
            ObjectResult statusCodeResult = (ObjectResult)result;
            Assert.That(statusCodeResult.StatusCode, Is.EqualTo(StatusCodes.Status500InternalServerError));
        }

        [Test]
        public async Task GetAllFruits_Returns500InternalServerError_WhenUnexpectedErrorOccurs()
        {
            _mockFruitService.Setup(service => service.GetAllFruits()).ThrowsAsync(new Exception());

            IActionResult result = await _fruitController.GetAllFruits();

            Assert.That(result, Is.InstanceOf<ObjectResult>());
            ObjectResult statusCodeResult = (ObjectResult)result;
            Assert.That(statusCodeResult.StatusCode, Is.EqualTo(StatusCodes.Status500InternalServerError));
        }

        [Test]
        public async Task GetFruitsByFamily_ReturnsOkResult_WithListOfFruits()
        {
            Fruit[] expectedFruits = new[] { new Fruit { Name = "Apple" }, new Fruit { Name = "Banana" } };
            string fruitFamily = "Citrus";
            FruitFamilyRequest request = new FruitFamilyRequest { FruitFamily = fruitFamily };
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonSerializer.Serialize(expectedFruits))
            };
            _mockFruitService.Setup(service => service.GetFruitsByFamily(fruitFamily)).ReturnsAsync(expectedFruits);

            IActionResult result = await _fruitController.GetFruitsByFamily(request);

            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            OkObjectResult okResult = (OkObjectResult)result;
            Assert.That(okResult.Value, Is.EqualTo(expectedFruits));
        }

        [Test]
        public async Task GetFruitsByFamily_ReturnsOkResult_WhenInvalidFruitFamily()
        {
            Fruit[] expectedFruits = new Fruit[0];
            string fruitFamily = "Invalid-Family";
            FruitFamilyRequest request = new FruitFamilyRequest { FruitFamily = fruitFamily };
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.NotFound)
            {
                Content = new StringContent(JsonSerializer.Serialize(expectedFruits))
            };
            _mockFruitService.Setup(service => service.GetFruitsByFamily(fruitFamily)).ReturnsAsync(expectedFruits);

            IActionResult result = await _fruitController.GetFruitsByFamily(request);

            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            OkObjectResult okResult = (OkObjectResult)result;
            Assert.That(okResult.Value, Is.EqualTo(expectedFruits));
        }

        [Test]
        public async Task GetFruitsByFamily_ReturnsBadRequest_WhenModelStateIsInvalid()
        {
            _fruitController.ModelState.AddModelError("FruitFamily", "Fruit family is required");
            FruitFamilyRequest request = new FruitFamilyRequest { FruitFamily = null };

            IActionResult result = await _fruitController.GetFruitsByFamily(request);

            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
        }

        [Test]
        public async Task GetFruitsByFamily_Returns503ServiceUnavailable_WhenHttpRequestFails()
        {
            string fruitFamily = "Citrus";
            FruitFamilyRequest request = new FruitFamilyRequest { FruitFamily = fruitFamily };
            _mockFruitService.Setup(service => service.GetFruitsByFamily(fruitFamily)).ThrowsAsync(new HttpRequestException());

            IActionResult result = await _fruitController.GetFruitsByFamily(request);

            Assert.That(result, Is.InstanceOf<ObjectResult>());
            ObjectResult statusCodeResult = (ObjectResult)result;
            Assert.That(statusCodeResult.StatusCode, Is.EqualTo(StatusCodes.Status503ServiceUnavailable));
        }

        [Test]
        public async Task GetFruitsByFamily_Returns500InternalServerError_WhenJsonDeserializationFails()
        {
            string fruitFamily = "Citrus";
            FruitFamilyRequest request = new FruitFamilyRequest { FruitFamily = fruitFamily };
            _mockFruitService.Setup(service => service.GetFruitsByFamily(fruitFamily)).ThrowsAsync(new JsonException());

            IActionResult result = await _fruitController.GetFruitsByFamily(request);

            Assert.That(result, Is.InstanceOf<ObjectResult>());
            ObjectResult statusCodeResult = (ObjectResult)result;
            Assert.That(statusCodeResult.StatusCode, Is.EqualTo(StatusCodes.Status500InternalServerError));
        }

        [Test]
        public async Task GetFruitsByFamily_Returns500InternalServerError_WhenUnexpectedErrorOccurs()
        {
            string fruitFamily = "Citrus";
            FruitFamilyRequest request = new FruitFamilyRequest { FruitFamily = fruitFamily };
            _mockFruitService.Setup(service => service.GetFruitsByFamily(fruitFamily)).ThrowsAsync(new Exception());

            IActionResult result = await _fruitController.GetFruitsByFamily(request);

            Assert.That(result, Is.InstanceOf<ObjectResult>());
            ObjectResult statusCodeResult = (ObjectResult)result;
            Assert.That(statusCodeResult.StatusCode, Is.EqualTo(StatusCodes.Status500InternalServerError));
        }
    }
}