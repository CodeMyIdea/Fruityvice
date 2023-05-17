using Fruityvice.Models;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Fruityvice.Services
{
    public interface IFruitService
    {
        Task<Fruit[]> GetAllFruits();
        Task<Fruit[]> GetFruitsByFamily(string fruitFamily);
    }

    public class FruityviceService : IFruitService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://www.fruityvice.com/api/fruit";
        private readonly ILogger<FruityviceService> _logger;
        public FruityviceService(HttpClient httpClient, ILogger<FruityviceService> logger)

        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<Fruit[]> GetAllFruits()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"{_baseUrl}/all");
                if (response.IsSuccessStatusCode)
                {
                    Stream contentStream = await response.Content.ReadAsStreamAsync();
                    Fruit[] fruits = await JsonSerializer.DeserializeAsync<Fruit[]>(contentStream);
                    return fruits;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all fruits");
            }

            return new Fruit[0];
        }

        public async Task<Fruit[]> GetFruitsByFamily(string fruitFamily)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"{_baseUrl}/family/{fruitFamily}");
                if (response.IsSuccessStatusCode)
                {
                    Stream contentStream = await response.Content.ReadAsStreamAsync();
                    Fruit[] fruits = await JsonSerializer.DeserializeAsync<Fruit[]>(contentStream);
                    return fruits;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving fruits by family: {fruitFamily}");
            }

            return new Fruit[0];
        }
    }
}