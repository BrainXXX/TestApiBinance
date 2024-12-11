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
    private bool _disposed = false;

    public ApiService(HttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    public async Task<List<CoinModel>> GetCoinsAsync(string apiUrl)
    {
        if (string.IsNullOrWhiteSpace(apiUrl))
        {
            throw new ArgumentException("API URL cannot be null or whitespace.", nameof(apiUrl));
        }

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
                throw new HttpRequestException($"Error response from API: {response.StatusCode}");
            }
        }
    }

    public async Task<string> OutputWebResponse(string url)
    {
        if (string.IsNullOrWhiteSpace(url))
        {
            throw new ArgumentException("URL cannot be null or whitespace.", nameof(url));
        }

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

    public static string FavoriteCoins(string listCoins, List<string> listFavoriteCoins)
    {
        // Проверяем, что входная строка и список избранных монет не пустые
        if (string.IsNullOrWhiteSpace(listCoins) || listFavoriteCoins.Count == 0)
        {
            return string.Empty;
        }

        // Фильтруем строки, оставляя только те, которые начинаются с подстрок из списка
        string filtered = string.Join("\n", listCoins.Split('\n')
            .Where(line => listFavoriteCoins.Any(substring => line.StartsWith(substring, StringComparison.OrdinalIgnoreCase))));

        return filtered;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed)
        {
            return;
        }

        if (disposing)
        {
            _httpClient?.Dispose();
        }

        _disposed = true;
    }
}