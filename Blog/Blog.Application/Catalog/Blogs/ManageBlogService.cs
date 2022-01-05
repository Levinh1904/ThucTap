using Blog.Application.Common;
using Blog.Data.EF;
using Blog.Data.Entities;
using Blog.Utilities.Exceptions;
using Blog.ViewModels.BlogImages;
using Blog.ViewModels.Blogs;
using Blog.ViewModels.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Catalog.Blogs
{
    public class ManageBlogService : IManageBlogService
    {
        private readonly BlogDbContext _context;
        private readonly IStorageService _storageService;

        public ManageBlogService(BlogDbContext context, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }

        public async Task<int> AddImage(int blogId, BlogImageCreateRequest request)
        {
            var blogImage = new BlogImage()
            {
                Caption = request.Caption,
                DateCreated = DateTime.Now,
                IsDefault = request.IsDefault,
                BlogId = blogId,
                SortOrder = request.SortOrder
            };

            if (request.ImageFile != null)
            {
                blogImage.ImagePath = await this.SaveFile(request.ImageFile);
                blogImage.FileSize = request.ImageFile.Length;
            }
            _context.BlogImages.Add(blogImage);
            await _context.SaveChangesAsync();
            return blogImage.Id;
        }

        public async Task AddViewcount(int blogId)
        {
            var blog = await _context.Blogs.FindAsync(blogId);
            blog.ViewCount += 1;
            await _context.SaveChangesAsync();
        }

        public async Task<int> Create(BlogCreateRequest request)
        {
            var blog = new BLBlog()
            {
                Stock = request.Stock,
                ViewCount = 0,
                DateCreated = DateTime.Now,
                Translations = new List<BlogTranslation>()
                {
                    new BlogTranslation()
                    {
                        Name =  request.Name,
                        Description = request.Description,
                        Details = request.Details,
                        SeoDescription = request.SeoDescription,
                        SeoAlias = request.SeoAlias,
                        SeoTitle = request.SeoTitle,
                        LanguageId = request.LanguageId
                    }
                }
            };
            //Save image
            if (request.ThumbnailImage != null)
            {
                blog.BlogImages = new List<BlogImage>()
                {
                    new BlogImage()
                    {
                        Caption = "Thumbnail image",
                        DateCreated = DateTime.Now,
                        FileSize = request.ThumbnailImage.Length,
                        ImagePath = await this.SaveFile(request.ThumbnailImage),
                        IsDefault = true,
                        SortOrder = 1
                    }
                };
            }
            _context.Blogs.Add(blog);
            await _context.SaveChangesAsync();
            return blog.Id;
        }

        public async Task<int> Delete(int blogId)
        {
            var blog = await _context.Blogs.FindAsync(blogId);
            if (blog == null) throw new BlogException($"Cannot find a blog: {blogId}");

            var images = _context.BlogImages.Where(i => i.BlogId == blogId);
            foreach (var image in images)
            {
                await _storageService.DeleteFileAsync(image.ImagePath);
            }

            _context.Blogs.Remove(blog);

            return await _context.SaveChangesAsync();
        }

        public async Task<PagedResult<BlogViewModel>> GetAllPaging(GetManageBlogPagingRequest request)
        {
            //1. Select join
            var query = from p in _context.Blogs
                        join pt in _context.BlogTranslations on p.Id equals pt.BlogId
                        join pic in _context.BlogInCategories on p.Id equals pic.BlogId
                        join c in _context.Categories on pic.CategoryId equals c.Id
                        select new { p, pt, pic };
            //2. filter
            if (!string.IsNullOrEmpty(request.Keyword))
                query = query.Where(x => x.pt.Name.Contains(request.Keyword));

            if (request.CategoryIds.Count > 0)
            {
                query = query.Where(p => request.CategoryIds.Contains(p.pic.CategoryId));
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

        public async Task<BlogViewModel> GetById(int blogId, string languageId)
        {
            var blog = await _context.Blogs.FindAsync(blogId);
            var blogTranslation = await _context.BlogTranslations.FirstOrDefaultAsync(x => x.BlogId == blogId
            && x.LanguageId == languageId);

            var blogViewModel = new BlogViewModel()
            {
                Id = blog.Id,
                DateCreated = blog.DateCreated,
                Description = blogTranslation != null ? blogTranslation.Description : null,
                LanguageId = blogTranslation.LanguageId,
                Details = blogTranslation != null ? blogTranslation.Details : null,
                Name = blogTranslation != null ? blogTranslation.Name : null,
                SeoAlias = blogTranslation != null ? blogTranslation.SeoAlias : null,
                SeoDescription = blogTranslation != null ? blogTranslation.SeoDescription : null,
                SeoTitle = blogTranslation != null ? blogTranslation.SeoTitle : null,
                Stock = blog.Stock,
                ViewCount = blog.ViewCount
            };
            return blogViewModel;
        }

        public async Task<BlogImageViewModel> GetImageById(int imageId)
        {
            var image = await _context.BlogImages.FindAsync(imageId);
            if (image == null)
                throw new BlogException($"Cannot find an image with id {imageId}");

            var viewModel = new BlogImageViewModel()
            {
                Caption = image.Caption,
                DateCreated = image.DateCreated,
                FileSize = image.FileSize,
                Id = image.Id,
                ImagePath = image.ImagePath,
                IsDefault = image.IsDefault,
                BlogId = image.BlogId,
                SortOrder = image.SortOrder
            };
            return viewModel;
        }

        public async Task<List<BlogImageViewModel>> GetListImages(int blogId)
        {
            return await _context.BlogImages.Where(x => x.BlogId == blogId)
                .Select(i => new BlogImageViewModel()
                {
                    Caption = i.Caption,
                    DateCreated = i.DateCreated,
                    FileSize = i.FileSize,
                    Id = i.Id,
                    ImagePath = i.ImagePath,
                    IsDefault = i.IsDefault,
                    BlogId = i.BlogId,
                    SortOrder = i.SortOrder
                }).ToListAsync();
        }

        public async Task<int> RemoveImage(int imageId)
        {
            var blogImage = await _context.BlogImages.FindAsync(imageId);
            if (blogImage == null)
                throw new BlogException($"Cannot find an image with id {imageId}");
            _context.BlogImages.Remove(blogImage);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Update(BlogUpdateRequest request)
        {
            var blog = await _context.Blogs.FindAsync(request.Id);
            var blogTranslations = await _context.BlogTranslations.FirstOrDefaultAsync(x => x.BlogId == request.Id
            && x.LanguageId == request.LanguageId);

            if (blog == null || blogTranslations == null) throw new BlogException($"Cannot find a blog with id: {request.Id}");

            blogTranslations.Name = request.Name;
            blogTranslations.SeoAlias = request.SeoAlias;
            blogTranslations.SeoDescription = request.SeoDescription;
            blogTranslations.SeoTitle = request.SeoTitle;
            blogTranslations.Description = request.Description;
            blogTranslations.Details = request.Details;

            //Save image
            if (request.ThumbnailImage != null)
            {
                var thumbnailImage = await _context.BlogImages.FirstOrDefaultAsync(i => i.IsDefault == true && i.BlogId == request.Id);
                if (thumbnailImage != null)
                {
                    thumbnailImage.FileSize = request.ThumbnailImage.Length;
                    thumbnailImage.ImagePath = await this.SaveFile(request.ThumbnailImage);
                    _context.BlogImages.Update(thumbnailImage);
                }
            }

            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateImage(int imageId, BlogImageUpdateRequest request)
        {
            var blogImage = await _context.BlogImages.FindAsync(imageId);
            if (blogImage == null)
                throw new BlogException($"Cannot find an image with id {imageId}");

            if (request.ImageFile != null)
            {
                blogImage.ImagePath = await this.SaveFile(request.ImageFile);
                blogImage.FileSize = request.ImageFile.Length;
            }
            _context.BlogImages.Update(blogImage);
            return await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdatePrice(int blogId, decimal newPrice)
        {
            var blog = await _context.Blogs.FindAsync(blogId);
            if (blog == null) throw new BlogException($"Cannot find a blog with id: {blogId}");
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateStock(int blogId, int addedQuantity)
        {
            var blog = await _context.Blogs.FindAsync(blogId);
            if (blog == null) throw new BlogException($"Cannot find a blog with id: {blogId}");
            blog.Stock += addedQuantity;
            return await _context.SaveChangesAsync() > 0;
        }

        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return fileName;
        }
    }
}