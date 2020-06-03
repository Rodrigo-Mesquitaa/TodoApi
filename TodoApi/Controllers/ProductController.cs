using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Data;
using TodoApi.Models;

namespace TodoApi.Controllers
{

    [ApiController]
    [Route("v1/products")]
    public class ProductController : ControllerBase
    {
        /// <summary>
        /// Metodo de busca de produto
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task<ActionResult<List<Product>>> Get([FromServices] DataContext context)
        {
            var products = await context.Products.Include(x => x.Category).ToListAsync();
            return products;
        }
        /// <summary>
        /// Metodo de busca por Id de produto
        /// </summary>
        /// <param name="context"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("id:int")]

        public async Task<ActionResult<Product>> GetById([FromServices] DataContext context, int id)
        {
            var product = await context.Products.Include(x => x.Category).FirstOrDefaultAsync(x => x.Id == id);
            return product;
        }
        /// <summary>
        /// Metodo de busca por categoria de produto
        /// </summary>
        /// <param name="context"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("categories/{id:int}")]
        public async Task<ActionResult<List<Product>>> GetByCategory([FromServices] DataContext context, int id)
        {
            var products = await context.Products
                .Include(x => x.Category)
                .Where(x => x.CategoryId == id)
                .ToListAsync();
            return products;
        }
        /// <summary>
        /// Metodo de inclusão de produto
        /// </summary>
        /// <param name="context"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Product>> Post(
            [FromServices] DataContext context,
            [FromBody] Product model)
        {
            if (ModelState.IsValid)
            {
                context.Products.Add(model);
                await context.SaveChangesAsync();
                return model;
            }
            else
            {
                return BadRequest(ModelState);
            }

        }
    }
}






