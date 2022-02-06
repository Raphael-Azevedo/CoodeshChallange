using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpaceFlightNews.Data;
using SpaceFlightNews.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceFlightNews.Controllers
{
    [Route("")]
    [ApiController]
    public class ArticleController : Controller
    {
        private readonly DataContext _context;

        public ArticleController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<Article>> Index()
        {
            return Ok("Back-end Challenge 2021 🏅 - Space Flight News");
        }

        [HttpGet]
        [Route("articles/{skip}/{take}")]
        public async Task<ActionResult<List<Article>>> GetAll([FromRoute]int skip = 0, [FromRoute]int take = 5)
        {
            var total = await _context.Articles.CountAsync();
            var articles = await _context.Articles.AsNoTracking()
                .Skip(skip)
                .Take(take)
                .ToListAsync();
            return Ok(new
            {
                total,
                articles
            });
        }

        [HttpGet]
        [Route("articles/{id:int}")]
        public async Task<ActionResult<Article>> GetById(int id)
        {
            var articles = await _context.Articles.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return Ok(articles);
        }

        [HttpPost]
        [Route("articles/")]
        public async Task<ActionResult<List<Article>>> AddArticle(
            [FromBody]Article model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                _context.Articles.Add(model);
                await _context.SaveChangesAsync();
                return Ok(new { message = "Artigo adicionado com sucesso." });
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Não foi possível inserir artigo" });
            }
        }

        [HttpPut]
        [Route("articles/{id:int}")]
        public async Task<ActionResult<List<Article>>> UpdateById(int id,
            [FromBody]Article model)
        {
            if (id != model.Id)
                return NotFound(new {message = "Artigo não encotrado"});

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                _context.Entry<Article>(model).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok(model);
            }
            catch(DbUpdateConcurrencyException)
            {
                return BadRequest(new { message = "Este Artigo já foi atualizado" });
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Não foi possível atualizar o artigo" });
            }
        }

        [HttpDelete]
        [Route("articles/{id:int}")]
        public async Task<ActionResult<List<Article>>> DeleteById(int id)
        {
            var article = await _context.Articles.FirstOrDefaultAsync(x => x.Id == id);
            if (article == null)
                return NotFound(new { message = "Artigo não encontrado" });

            try
            {
                _context.Articles.Remove(article);
                await _context.SaveChangesAsync();
                return Ok(new { message = "Artigo excluido com sucesso." });
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Não foi possível remover o artigo" });
            }
        }
    }
}
