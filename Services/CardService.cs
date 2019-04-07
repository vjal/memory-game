using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Blazor;
using System.Net.Http;
using Domain;
namespace Services
{
    public class CardService : ICardService
    {
        HttpClient http;
        int offset;
        public CardService(HttpClient http)
        {
            this.http = http;
        }
        public async Task<List<Card>> GetCards(string keyword)
        {
            var limit = 32;
            if (string.IsNullOrEmpty(keyword))
                keyword = "funny";
            var url = $"http://api.giphy.com/v1/gifs/search?q={keyword}&limit={limit}&api_key=GDFJUI7emQX0Sxy9KDREeurBI77Symzr&offset={offset}";
            var searchResponse = await http.GetJsonAsync<SearchResponse>(url);
            var cards = new List<Card>();
            foreach (var gif in searchResponse.data)
            {
                cards.Add(new Card(gif.Image));
            }
            offset += limit;
            return cards;
        }
    }

    public class SearchResponse
    {
        public Gif[] data { get; set; } = new Gif[] { };
    }

    public class Gif
    {
        public string Url { get; set; }
        public Dictionary<string, Image> Images { get; set; } = new Dictionary<string, Image>();
        public string Image => Images["fixed_height"]?.Url ?? "";
    }

    public class Image
    {
        public string Url { get; set; }
    }
}