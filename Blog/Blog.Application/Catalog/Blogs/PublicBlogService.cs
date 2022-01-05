using Blog.Data.EF;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Blog.ViewModels.Common;
using Blog.ViewModels.Blogs;

namespace Blog.Application.Catalog.Blogs
{
    public class PublicBlogService : IPublicBlogService
    {
        private readonly BlogDbContext _context;

        public PublicBlogService(BlogDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<BlogViewModel>> GetAllByCategoryId(string languageId, GetPublicBlogPagingRequest request)
        {
            //1. Select join
            var query = from p in _context.Blogs
                        join pt in _context.BlogTranslations on p.Id equals pt.BlogId
                        join pic in _context.BlogInCategories on p.Id equals pic.BlogId
                        join c in _context.Categories on pic.CategoryId equals c.Id
                        where pt.LanguageId == languageId
                        select new { p, pt, pic };
            //2. filter
            if (request.CategoryId.HasValue && request.CategoryId.Value > 0)
            {
                query = query.Where(p => p.pic.CategoryId == request.CategoryId);
            }
            //3. Paging
            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new BlogViewModel()
                {
                    Id = x.p.Id,
                    Name = x.pt.Name,
                    DateCreated = x.p.DateCreated,
                    Description = x.pt.Description,
                    Details = x.pt.Details,
                    LanguageId = x.pt.LanguageId,
                    SeoAlias = x.pt.SeoAlias,
                    SeoDescription = x.pt.SeoDescription,
                    SeoTitle = x.pt.SeoTitle,
                    Stock = x.p.Stock,
                    ViewCount = x.p.ViewCount
                }).ToListAsync();

            //4. Select and projection
            var pagedResult = new PagedResult<BlogViewModel>()
            {
                Items = data
            };
            return pagedResult;
        }
    }
}