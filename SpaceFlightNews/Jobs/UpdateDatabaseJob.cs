using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Quartz;
using SpaceFlightNews.Interfaces.Repository;
using SpaceFlightNews.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace SpaceFlightNews.Jobs
{
    [DisallowConcurrentExecution]
    public class UpdateDatabaseJob : IJob
    {
        private readonly IConfiguration _configuration;
        private readonly IServiceProvider _provider;
        private readonly ILogger<UpdateDatabaseJob> _logger;

        public UpdateDatabaseJob(IConfiguration configuration, IServiceProvider provider, ILogger<UpdateDatabaseJob> logger)
        {
            this._configuration = configuration;
            this._provider = provider;
            this._logger = logger;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            _logger.Log(LogLevel.Information, "Start UpdateDatabaseJob Execute");
            using (var scope = _provider.CreateScope())
            {
                var articleService = scope.ServiceProvider.GetService<IArticleRepository>();
                
                try
                {               
                    HttpClient client = new HttpClient { BaseAddress = new Uri("https://api.spaceflightnewsapi.net/") };
                    var response = await client.GetAsync("v3/articles?_limit=100");

                    var content = await response.Content.ReadAsStringAsync();

                    var articles = JsonConvert.DeserializeObject<List<Article>>(content);
                    if (response.IsSuccessStatusCode)
                        await articleService.AddNew(articles);

                }
                catch (Exception ex)
                {
                    _logger.Log(LogLevel.Error, "Error UpdateDatabaseJob Execute");
                    await articleService.AlertError<object>(_configuration["Telegram:Token"], _configuration["Telegram:Channel"], $"Error UpdateDatabaseJob Execute:{ex.Message}");
                }
                finally
                {
                    _logger.Log(LogLevel.Information, "Finish UpdateDatabaseJob Execute");
                }
            }
        }
    }
}