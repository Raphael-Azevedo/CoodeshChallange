using Microsoft.EntityFrameworkCore;
using SpaceFlightNews.Data;
using SpaceFlightNews.DTO;
using SpaceFlightNews.Interfaces.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace SpaceFlightNews.Repository.Repository
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly DataContext _context;
        private readonly HttpClient _client;


        public ArticleRepository(DataContext context)
        {
            _context = context;
            this._client = new HttpClient();
        }


        public async Task AddNew(IEnumerable<Models.Article> articles)
        {

            foreach (var article in articles)
            {
                var result = await _context
                                   .Articles
                                   .Where(x => x.Id == article.Id).AnyAsync();
                if (!result)
                {
                    _context.Articles.Add(article);
                    await _context.SaveChangesAsync();
                }
            }

        
        }

        public async Task<ResponseHttp<T>> AlertError<T>(string apiToken, string chatId, string text)
        {
            string requestUri = $"https://api.telegram.org/bot{apiToken}/sendMessage?chat_id={chatId}&text={text}";
            var responseMessage = await _client.GetAsync(requestUri);
            var result = await responseMessage.Content.ReadAsStringAsync();

            return new ResponseHttp<T>() { IsSuccessStatusCode = responseMessage.IsSuccessStatusCode, Item = JsonSerializer.Deserialize<T>(result) };
        }
    }
}