using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;

namespace TestApi
{
    public partial class MainWindow : Window
    {
        private readonly string url1 = "https://fapi.binance.com/fapi/v1/ticker/24hr";
        //private readonly string url2 = "https://fapi.binance.com/fapi/v1/ticker/24hr?symbol=BTCUSDT";
        private readonly string url3 = "https://api.binance.com/api/v3/ticker/24hr";
        //private readonly string url4 = "https://api.binance.com/api/v3/avgPrice?symbol=BTCUSDT";

        private readonly List<string> listFavoriteCoins = ["BTCUSDT ", "ETHUSDT ", "BNBUSDT ", "SOLUSDT ", "TONUSDT ", "CAKEUSDT ", "TRUMPUSDT ", "RAYUSDT "];
        private readonly ApiService _apiService;

        public MainWindow()
        {
            Title = "TestApi v.11.12.2024";
            InitializeComponent();
            _apiService = new ApiService(new HttpClient());
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                textBox1.Clear();
                textBox2.Clear();

                var task1 = _apiService.OutputWebResponse(url1);
                var task2 = _apiService.OutputWebResponse(url3);

                await Task.WhenAll(task1, task2);

                textBox1.Text = task1.Result;
                textBox3.Text = ApiService.FavoriteCoins(task1.Result, listFavoriteCoins);

                textBox2.Text = task2.Result;
                textBox4.Text = ApiService.FavoriteCoins(task2.Result, listFavoriteCoins);
            }
            catch (Exception ex)
            {
                textBox1.Text = $"{ex.Source}: {ex.Message}";
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            _apiService.Dispose();
        }
    }
}