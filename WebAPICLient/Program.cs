using System;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace WebAPICLient
{

    class Zip
    {
        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("country abbreviation")]
        public string CountryAbbreviation { get; set; }

        //[JsonProperty("places[0]['place name']")]
        public List<Places> Places{ get; set; }

    }

    public class Places
    {
        [JsonProperty("place name")]
        public string PlaceName { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("longitude")]
        public string Longitude { get; set; }

        [JsonProperty("latitude")]
        public string Latitude { get; set; }
    }
    class Program
    {
        private static readonly HttpClient client = new HttpClient();

        static async Task Main(string[] args)
        {
            await ProcessRepoitories();
        }


        private static async Task ProcessRepoitories()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Enter a zipcode to learn more about it:");

                    var ZipCode = Console.ReadLine();

                    if (string.IsNullOrEmpty(ZipCode))
                    {
                        continue;
                    }

                    var result = await client.GetAsync("http://api.zippopotam.us/us/" + ZipCode);
                    var resultRead = await result.Content.ReadAsStringAsync();
                  
                    var ZipInfo = JsonConvert.DeserializeObject<Zip>(resultRead);

                    Console.WriteLine("--------------------------");
                    Console.WriteLine("Country: " + ZipInfo.Country);
                    Console.WriteLine("Country Abbreviation: " + ZipInfo.CountryAbbreviation);
                    ZipInfo.Places.ForEach(t => Console.Write("Place Name: " + t.PlaceName + "\n" + "State: " + t.State + "\n" + "Longitude: " + t.Longitude + "\n" + "Latitude: " + t.Latitude + "\n"));
                    Console.WriteLine("--------------------------");
                }
                catch
                {
                    Console.WriteLine("Error, please enter a valid zip code");
                }
            }
        }
    }

}

