using ElevenNote.Services;
using ElevenNote.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;

namespace ElevenNote.WebAPI.Controllers
{
    public class CategoryController : ApiController
    {
        [HttpPost]
        private CategoryService CreateCategoryService()
        {
            var ownerID = Guid.Parse(User.Identity.GetUserId());
            var categoryService = new CategoryService(ownerID);
            return categoryService;
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            var categoryService = CreateCategoryService();
            var categories = categoryService.GetCategories();
            return Ok(categories);
        }

        [HttpGet]
        public IHttpActionResult Get([FromUri] int catID)
        {
            var categoryService = CreateCategoryService();
            var category = categoryService.GetCategoryByID(catID);
            return Ok(category);
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody] CategoryCreate category)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateCategoryService();

            if (!service.CreateCategory(category))
                return InternalServerError();

            return Ok();
        }

        [HttpPut]
        public IHttpActionResult Put([FromBody] CategoryEdit category)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateCategoryService();

            if (!service.UpdateCategory(category))
                return InternalServerError();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult Delete([FromUri] int id)
        {
            var service = CreateCategoryService();

            if (!service.DeleteCategory(id))
                return InternalServerError();

            return Ok();
        }
    }
}
