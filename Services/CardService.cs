using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Blazor;
using System.Net.Http;
using Domain;
namespace Services
{
    public class CardService 
    {
        HttpClient http;
        int offset;
        public CardService(HttpClient http){
            this.http = http;
        }
        public async Task<List<Card>> GetCards(string keyword)
        {
            var limit = 100;
            var url = string.IsNullOrEmpty(keyword) 
                ? $"http://api.giphy.com/v1/gifs/trending?limit={limit}&api_key=GDFJUI7emQX0Sxy9KDREeurBI77Symzr&offset={offset}"
                : $"http://api.giphy.com/v1/gifs/search?q={keyword}&limit={limit}&api_key=GDFJUI7emQX0Sxy9KDREeurBI77Symzr&offset={offset}";
            var searchResponse = await http.GetJsonAsync<SearchResponse>(url);
            var cards = new List<Card>();
            foreach(var gif in searchResponse.data)
            {
                cards.Add(new Card(gif.Image));
            }
            return cards;
        }
    }

    public class SearchResponse
    {
        public Gif[] data { get; set; } = new Gif[]{};
    }

    public class Gif 
    {
        public string Url {get;set;}
        public Dictionary<string, Image> Images { get; set; } = new Dictionary<string, Image>();
        public string Image => Images["fixed_height"]?.Url ?? "";
    }

    public class Image {
        public string Url {get;set;}
    }
}