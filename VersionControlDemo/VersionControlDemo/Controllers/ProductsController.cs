using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VersionControlDemo.Models;

namespace VersionControlDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0", Deprecated = true)] //Accept 1.0
    [ApiVersion("2.0")] //Accept 2.0
    public class ProductsController : ControllerBase
    {
        public ProductsController()
        {

        }

        [HttpGet]
        [MapToApiVersion("1.0")] //Only Route Get with version = 1.0
        public ActionResult<ProductsV1> GetProductsV1()
        {
            ProductsV1 myProd = new ProductsV1()
            {
                Name = "Amazing Super Man Toy",
                Price = 200
            };
            return Ok(myProd);
        }

        [HttpGet]
        [MapToApiVersion("2.0")] //Only Route Get with version = 2.0
        public ActionResult<ProductsV1> GetProductsV2()
        {
            ProductsV2 myProd = new ProductsV2()
            {
                Name = "Amazing Super Man Toy",
                Price = 200,
                StockRemain = 5
            };
            return Ok(myProd);
        }
    }
}
