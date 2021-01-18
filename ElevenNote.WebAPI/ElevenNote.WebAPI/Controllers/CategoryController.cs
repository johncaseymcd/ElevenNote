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
        private CategoryService CreateCategoryService()
        {
            var ownerID = Guid.Parse(User.Identity.GetUserId());
            var categoryService = new CategoryService(ownerID);
            return categoryService;
        }

        public IHttpActionResult Get()
        {
            var categoryService = CreateCategoryService();
            var categories = categoryService.GetCategories();
            return Ok(categories);
        }

        public IHttpActionResult Get(Int16 catID)
        {
            var categoryService = CreateCategoryService();
            var category = categoryService.GetCategoryByID(catID);
            return Ok(category);
        }

        public IHttpActionResult Post(CategoryCreate category)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateCategoryService();

            if (!service.CreateCategory(category))
                return InternalServerError();

            return Ok();
        }

        public IHttpActionResult Put(CategoryEdit category)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateCategoryService();

            if (!service.UpdateCategory(category))
                return InternalServerError();

            return Ok();
        }

        public IHttpActionResult Delete(Int16 catID)
        {
            var service = CreateCategoryService();

            if (!service.DeleteCategory(catID))
                return InternalServerError();

            return Ok();
        }
    }
}
