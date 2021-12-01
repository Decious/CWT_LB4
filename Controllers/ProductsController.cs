using Kaberdin_LB4.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Kaberdin_LB4.Controllers
{
    public class ProductsController : Controller
    {
        private TextFileDatabase database = new TextFileDatabase();

        [HttpPost]
        public IActionResult Add([FromBody]ProductModel product)
        {
            database.AddProduct(product);
            return Ok();
        }

        [HttpDelete]
        public IActionResult Remove(int id)
        {
            database.RemoveProduct(id);
            return Ok();
        }

        [HttpPost]
        public IActionResult Modify([FromBody] ModifyRequestModel<ProductModel> modifyRequest)
        {
            database.EditProduct(modifyRequest.Id, modifyRequest.Entity);
            return Ok();
        }

        [HttpGet]
        public IActionResult Get(int id)
        {
            var product = database.GetProductById(id);

            if(product == null)
            {
                return new JsonResult(new ErrorModel("No product found by that id"));
            }

            return new JsonResult(product);
        }
    }
}
