using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using TestApi.Classes;

namespace TestApi
{
    public partial class MainWindow : Window
    {
        private readonly string url1 = "https://fapi.binance.com/fapi/v1/ticker/price";
        private readonly string url2 = "https://fapi.binance.com/fapi/v1/ticker/24hr";
        private readonly string url3 = "https://fapi.binance.com/fapi/v1/ticker/24hr?symbol=BTCUSDT";
        private readonly string url4 = "https://api.binance.com/api/v3/ticker/24hr";
        private readonly string url5 = "https://api.binance.com/api/v3/avgPrice?symbol=BTCUSDT";

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            textBox1.Text = await Task.Run(() => OutputWebResponse(url2));
        }

        private static async Task<string> GetWebResponse(string url)
        {
            // create request..
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);

            // use GET method
            webRequest.Method = "GET";

            // POST!
            HttpWebResponse webResponse = await webRequest.GetResponseAsync() as HttpWebResponse;

            // read response into StreamReader
            Stream responseStream = webResponse.GetResponseStream();
            StreamReader _responseStream = new StreamReader(responseStream);

            // get raw result
            return _responseStream.ReadToEnd();
        }

        private static async Task<string> OutputWebResponse(string url)
        {
            try
            {
                string byc = await GetWebResponse(url);

                int pos1 = byc.IndexOf("[", 0);
                int pos2 = byc.IndexOf("]", pos1);

                string[] str1 = byc.Substring(pos1 + 1, pos2 - 1).Replace("{", "").Replace("\"", "").Split("},");

                List<CollectionCoins> collectionCoins = new List<CollectionCoins>();
                string[] s;

                for (int i = 0; i < str1.Length; i++)
                {
                    s = str1[i].Split(",");
                    collectionCoins.Add(new CollectionCoins(s[0].Split(':')[1], Convert.ToDouble(s[4].Split(':')[1].Replace(".", ",")), 
                        Convert.ToDouble(s[10].Split(':')[1].Replace(".", ","))));
                }

                collectionCoins.Sort();

                string str2 = "";

                foreach (CollectionCoins a in collectionCoins)
                {
                    str2 += a.ToString() + "\n";
                }

                return str2;
            }
            catch (Exception ex)
            {
                return ex.Source + " - " + ex.Message;
            }
        }
    }
}