using Blog.ViewModels.Blogs;
using Blog.ViewModels.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.Application.Catalog.Blogs
{
    public interface IPublicBlogService
    {
        Task<PagedResult<BlogViewModel>> GetAllByCategoryId(string languageId, GetPublicBlogPagingRequest request);
    }
}