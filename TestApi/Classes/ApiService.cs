using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TestApi.Classes;

public class ApiService : IDisposable
{
    private readonly HttpClient _httpClient;

    public ApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<CoinModel>> GetCoinsAsync(string apiUrl)
    {
        // Выполняем GET-запрос к API
        using (HttpResponseMessage response = await _httpClient.GetAsync(apiUrl))
        {
            // Проверяем, что запрос выполнен успешно
            if (response.IsSuccessStatusCode)
            {
                // Читаем содержимое ответа в виде строки
                string jsonString = await response.Content.ReadAsStringAsync();

                // Настройка JsonSerializerOptions для десериализации
                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };

                // Десериализуем JSON в список объектов CoinModel
                List<CoinModel> coins = JsonSerializer.Deserialize<List<CoinModel>>(jsonString, options);

                // Сортируем список по убыванию QuoteVolume
                coins = coins.OrderByDescending(coin => coin.QuoteVolume).ToList();

                return coins;
            }
            else
            {
                return null;
            }
        }
    }

    public async Task<string> OutputWebResponse(string url)
    {
        List<CoinModel> coins = await GetCoinsAsync(url);

        if (coins != null)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var coin in coins)
            {
                sb.AppendLine($"{coin.Symbol,-15}{coin.LastPrice,20:F4}{coin.QuoteVolume,15:F2}");
            }
            return sb.ToString();
        }
        else
        {
            return "Ошибка вывода!";
        }
    }

    public void Dispose()
    {
        _httpClient?.Dispose();
    }
}