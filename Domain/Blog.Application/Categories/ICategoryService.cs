using Blog.ViewModels.Blog;
using Blog.ViewModels.Categories;
using Blog.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Categories
{
    public interface ICategoryService
    {
        Task<int> Create(CategoryCreateRequest request);

        Task<int> Update(CategoryUpdateRequest request);

        Task<int> Delete(int categoryId);

        Task<PagedResult<CategoryViewModel>> GetAllPaging(GetManageBlogPagingRequest request);

        Task<CategoryViewModel> GetById(int id);

        Task<List<CategoryViewModel>> GetAll();
    }
}
