using System;
using System.Net.Http;
using System.Windows;

namespace TestApi
{
    public partial class MainWindow : Window
    {
        private readonly string url1 = "https://fapi.binance.com/fapi/v1/ticker/price";
        private readonly string url2 = "https://fapi.binance.com/fapi/v1/ticker/24hr";
        private readonly string url3 = "https://fapi.binance.com/fapi/v1/ticker/24hr?symbol=BTCUSDT";
        private readonly string url4 = "https://api.binance.com/api/v3/ticker/24hr";
        private readonly string url5 = "https://api.binance.com/api/v3/avgPrice?symbol=BTCUSDT";

        private readonly ApiService _apiService;

        public MainWindow()
        {
            Title = "TestApi v.07.02.2024";
            InitializeComponent();
            _apiService = new ApiService(new HttpClient());
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                textBox1.Text = await _apiService.OutputWebResponse(url2);
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