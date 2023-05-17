using Fruityvice.Models;
using Fruityvice.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Fruityvice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FruitController : ControllerBase
    {
        private readonly IFruitService _fruitService;
        private readonly ILogger<FruitController> _logger;

        public FruitController(IFruitService fruitService, ILogger<FruitController> logger)
        {
            _fruitService = fruitService;
            _logger = logger;
        }

        /// <summary>
        /// Get all fruits details
        /// </summary>
        /// <returns>List of Fruits</returns>
        [HttpGet("all")]
        public async Task<IActionResult> GetAllFruits()
        {
            try
            {
                Fruit[] fruits = await _fruitService.GetAllFruits();
                return Ok(fruits);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error calling external API");
                return StatusCode(StatusCodes.Status503ServiceUnavailable, "Error calling external API");
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "Error deserializing response from external API");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deserializing response from external API");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred");
                return StatusCode(StatusCodes.Status500InternalServerError, "Unexpected error occurred");
            }
        }

        /// <summary>
        /// Get the fruits details by family
        /// </summary>
        /// <param name="request">{fruitFamily = "Rosaceae"}</param>
        /// <returns>List of Fruits</returns>
        [HttpPost("family")]
        public async Task<IActionResult> GetFruitsByFamily([FromBody][Required] FruitFamilyRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Fruit[] fruits = await _fruitService.GetFruitsByFamily(request.FruitFamily);
                return Ok(fruits);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error calling external API");
                return StatusCode(StatusCodes.Status503ServiceUnavailable, "Error calling external API");
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "Error deserializing response from external API");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deserializing response from external API");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred");
                return StatusCode(StatusCodes.Status500InternalServerError, "Unexpected error occurred");
            }
        }
    }
}