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
        private readonly Guid _ownerID;

        public CategoryService(Guid ownerID)
        {
            _ownerID = ownerID;
        }

        public bool CreateCategory(CategoryCreate model)
        {
            var entity = new Category
            {
                //CategoryID = model.CategoryID,
                OwnerID = _ownerID,
                Name = model.Name,
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
                        .Where(e => e.OwnerID == _ownerID)
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

        public CategoryDetail GetCategoryByID(int catID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Categories
                        .Single(e => e.CategoryID == catID && e.OwnerID == _ownerID);

                return
                    new CategoryDetail
                    {
                        CategoryID = entity.CategoryID,
                        Name = entity.Name,
                        CreatedUTC = entity.CreatedUTC,
                        ModifiedUTC = entity.ModifiedUTC
                    };
            }
        }

        public bool UpdateCategory(CategoryEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Categories
                        .Single(e => e.CategoryID == model.CategoryID && e.OwnerID == _ownerID);

                entity.Name = model.Name;
                entity.ModifiedUTC = model.ModifiedUTC;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteCategory(int categoryID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Categories
                        .Single(e => e.CategoryID == categoryID && e.OwnerID == _ownerID);

                ctx.Categories.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
