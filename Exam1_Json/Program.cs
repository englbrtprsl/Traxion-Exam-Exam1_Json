using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http.Headers;

namespace Exam1_Json
{
    public class Program
    {
        public static void Main(string[] args)
        {
            getdata().Wait();
        }

        static async Task getdata()
        {
            using (var client = new HttpClient())
            {
                // Update port # in the following line.
                client.BaseAddress = new Uri("https://api.openweathermap.org/data/2.5/weather");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string apiData = "Philippines";
                string apiKey = "c5d1f3535b24f58da88543ff29a5d5e2";

                HttpResponseMessage respon = await client.GetAsync("?q=" + apiData + "&appid=" + apiKey);

                if (respon.StatusCode == HttpStatusCode.OK)
                {
                    dynamic responseData = await respon.Content.ReadAsStringAsync();
                    Rootobject obj = JsonConvert.DeserializeObject<Rootobject>(responseData);

                    Console.WriteLine("City ID: " + obj.id);
                    Console.WriteLine("City Name: " + obj.name);
                    Console.WriteLine("City Timezone: " + obj.timezone);
                    Console.WriteLine("City Code: " + obj.cod);

                    Console.WriteLine("City Coordinate: Longitute - " + obj.coord.lon);
                    Console.WriteLine("City Coordinate: Latitude - " + obj.coord.lat);

                }

                Console.ReadKey();
            }

        }
    }

    public class Rootobject
    {
        public Coord coord { get; set; }
        public Weather[] weather { get; set; }
        public string _base { get; set; }
        public Main main { get; set; }
        public int visibility { get; set; }
        public Wind wind { get; set; }
        public Clouds clouds { get; set; }
        public int dt { get; set; }
        public Sys sys { get; set; }
        public int timezone { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public int cod { get; set; }
    }

    public class Coord
    {
        public float lon { get; set; }
        public float lat { get; set; }
    }

    public class Main
    {
        public float temp { get; set; }
        public float feels_like { get; set; }
        public float temp_min { get; set; }
        public float temp_max { get; set; }
        public int pressure { get; set; }
        public int humidity { get; set; }
    }

    public class Wind
    {
        public float speed { get; set; }
        public int deg { get; set; }
    }

    public class Clouds
    {
        public int all { get; set; }
    }

    public class Sys
    {
        public int type { get; set; }
        public int id { get; set; }
        public string country { get; set; }
        public int sunrise { get; set; }
        public int sunset { get; set; }
    }

    public class Weather
    {
        public int id { get; set; }
        public string main { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
    }

}
