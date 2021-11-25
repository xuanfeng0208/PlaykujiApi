using Microsoft.AspNetCore.Mvc;
using PlayKuji.Domain.Dtos;
using PlayKuji.Domain.Interfaces.Logics;
using PlayKuji.Domain.Models.SearchModel;
using System;
using System.Collections.Generic;

namespace PlayKuji.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        readonly IProductLogic _productLogic;

        public ProductController(
            IProductLogic productLogic)
        {
            _productLogic = productLogic;
        }

        [HttpGet]
        public ActionResult<List<ProductDto>> Get([FromQuery] ProductSearchModel searchModel)
        {
            if (searchModel == null)
                searchModel = new();

            var result = _productLogic.GetList(searchModel);

            return result;
        }

        [HttpGet("{id}")]
        public ActionResult<ProductDto> Get(Guid? id)
        {
            if (id == null)
                return BadRequest();

            var result = _productLogic.GetByID(id.Value);
            if (result == null)
                return NotFound();

            return result;
        }
    }
}