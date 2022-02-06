
using SpaceFlightNews.DTO;
using SpaceFlightNews.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpaceFlightNews.Interfaces.Repository
{
    public interface IArticleRepository
    {   
        Task AddNew(IEnumerable<Article> articles);

        Task<ResponseHttp<T>> AlertError<T>(string apiToken, string chatId, string text);

    }
}