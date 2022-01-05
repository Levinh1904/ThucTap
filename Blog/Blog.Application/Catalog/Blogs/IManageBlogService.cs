
using Blog.ViewModels.BlogImages;
using Blog.ViewModels.Blogs;
using Blog.ViewModels.Common;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.Application.Catalog.Blogs
{
    public interface IManageBlogService
    {
        Task<int> Create(BlogCreateRequest request);

        Task<int> Update(BlogUpdateRequest request);

        Task<int> Delete(int blogId);

        Task<BlogViewModel> GetById(int blogId, string languageId);

        Task<bool> UpdatePrice(int blogId, decimal newPrice);

        Task<bool> UpdateStock(int blogId, int addedQuantity);

        Task AddViewcount(int blogId);

        Task<PagedResult<BlogViewModel>> GetAllPaging(GetManageBlogPagingRequest request);

        Task<int> AddImage(int blogId, BlogImageCreateRequest request);

        Task<int> RemoveImage(int imageId);

        Task<int> UpdateImage(int imageId, BlogImageUpdateRequest request);

        Task<BlogImageViewModel> GetImageById(int imageId);

        Task<List<BlogImageViewModel>> GetListImages(int blogId);
    }
}