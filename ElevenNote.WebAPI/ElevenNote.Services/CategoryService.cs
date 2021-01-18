using ElevenNote.Data;
using ElevenNote.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Services
{
    public class CategoryService
    {
        private readonly Guid _categoryID;

        public CategoryService(Guid catID)
        {
            _categoryID = catID;
        }

        public bool CreateCategory(CategoryCreate model)
        {
            var entity = new Category
            {
                SessionID = _categoryID,
                Name = model.Name,
                Notes = model.Notes
            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Categories.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }
    }
}
