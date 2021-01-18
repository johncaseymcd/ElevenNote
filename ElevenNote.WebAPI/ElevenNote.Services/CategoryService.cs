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
        private readonly Guid _sessionID;

        public CategoryService(Guid catID)
        {
            _sessionID = catID;
        }

        public bool CreateCategory(CategoryCreate model)
        {
            var entity = new Category
            {
                OwnerID = _sessionID,
                Name = model.Name,
                Notes = model.Notes,
                CreatedUTC = DateTimeOffset.Now
            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Categories.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<CategoryListItem> GetCategories()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Categories
                        .Where(e => e.OwnerID == _sessionID)
                        .Select(
                            e => 
                            new CategoryListItem
                            {
                                CategoryID = e.CategoryID,
                                Name = e.Name,
                                CreatedUTC = e.CreatedUTC
                            }
                        );

                return query.ToArray();
            }
        }

        public bool UpdateCategory(CategoryEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Categories
                        .Single(e => e.CategoryID == model.CategoryID && e.OwnerID == _sessionID);

                entity.Name = model.Name;
                entity.Notes = model.Notes;
                entity.ModifiedUTC = model.ModifiedUTC;

                return ctx.SaveChanges() == 1;
            }
        }


    }
}
