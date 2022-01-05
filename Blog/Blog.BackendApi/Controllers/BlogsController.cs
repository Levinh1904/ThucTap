
using Blog.Application.Catalog.Blogs;
using Blog.ViewModels.BlogImages;
using Blog.ViewModels.Blogs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Blog.BackendApi.Controllers
{
    //api/blogs
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BlogsController : ControllerBase
    {
        private readonly IPublicBlogService _publicBlogService;
        private readonly IManageBlogService _manageBlogService;

        public BlogsController(IPublicBlogService publicBlogService,
            IManageBlogService manageBlogService)
        {
            _publicBlogService = publicBlogService;
            _manageBlogService = manageBlogService;
        }

        //http://localhost:port/blogs?pageIndex=1&pageSize=10&CategoryId=
        [HttpGet("{languageId}")]
        public async Task<IActionResult> GetAllPaging(string languageId, [FromQuery]GetPublicBlogPagingRequest request)
        {
            var blogs = await _publicBlogService.GetAllByCategoryId(languageId, request);
            return Ok(blogs);
        }

        //http://localhost:port/blog/1
        [HttpGet("{blogId}/{languageId}")]
        public async Task<IActionResult> GetById(int blogId, string languageId)
        {
            var blog = await _manageBlogService.GetById(blogId, languageId);
            if (blog == null)
                return BadRequest("Cannot find blog");
            return Ok(blog);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm]BlogCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var blogId = await _manageBlogService.Create(request);
            if (blogId == 0)
                return BadRequest();

            var blog = await _manageBlogService.GetById(blogId, request.LanguageId);

            return CreatedAtAction(nameof(GetById), new { id = blogId }, blog);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromForm]BlogUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var affectedResult = await _manageBlogService.Update(request);
            if (affectedResult == 0)
                return BadRequest();
            return Ok();
        }

        [HttpDelete("{blogId}")]
        public async Task<IActionResult> Delete(int blogId)
        {
            var affectedResult = await _manageBlogService.Delete(blogId);
            if (affectedResult == 0)
                return BadRequest();
            return Ok();
        }

     

        //Images
        [HttpPost("{blogId}/images")]
        public async Task<IActionResult> CreateImage(int blogId, [FromForm]BlogImageCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var imageId = await _manageBlogService.AddImage(blogId, request);
            if (imageId == 0)
                return BadRequest();

            var image = await _manageBlogService.GetImageById(imageId);

            return CreatedAtAction(nameof(GetImageById), new { id = imageId }, image);
        }

        [HttpPut("{blogId}/images/{imageId}")]
        public async Task<IActionResult> UpdateImage(int imageId, [FromForm]BlogImageUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _manageBlogService.UpdateImage(imageId, request);
            if (result == 0)
                return BadRequest();

            return Ok();
        }

        [HttpDelete("{blogId}/images/{imageId}")]
        public async Task<IActionResult> RemoveImage(int imageId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _manageBlogService.RemoveImage(imageId);
            if (result == 0)
                return BadRequest();

            return Ok();
        }

        [HttpGet("{blogId}/images/{imageId}")]
        public async Task<IActionResult> GetImageById(int blogId, int imageId)
        {
            var image = await _manageBlogService.GetImageById(imageId);
            if (image == null)
                return BadRequest("Cannot find blog");
            return Ok(image);
        }
    }
}