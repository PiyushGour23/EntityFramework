using Billboard.Data;
using Billboard.Models;
using Billboard.Models.DTO;
using Billboard.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace Billboard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogpostController : ControllerBase
    {
        private readonly IBlogpostRepository _blogpostRepository;
        private readonly UserDbContext _context;

        public BlogpostController(IBlogpostRepository blogpostRepository, UserDbContext context)
        {
            _blogpostRepository = blogpostRepository;
            _context = context;
        }

        [HttpPost("AddBlogpost")]
        public async Task<IActionResult> AddBlogpost(AddBlogpostDto addBlogpostDto)
        {
            try
            {
                //Map DTO to Model
                var blogpostdto = new Blogpost
                {
                    Title = addBlogpostDto.Title,
                    ShortDescription = addBlogpostDto.ShortDescription,
                    Content = addBlogpostDto.Content,
                    FeaturedImageUrl = addBlogpostDto.FeaturedImageUrl,
                    UrlHandle = addBlogpostDto.UrlHandle,
                    PublishedDate = addBlogpostDto.PublishedDate,
                    Author = addBlogpostDto.Author,
                    IsVisible = addBlogpostDto.IsVisible,
                };
                var data = await _blogpostRepository.AddAsync(blogpostdto);


                //Convert Domain Model to DTO for angular
                var response = new AddBlogpostAngularDto
                {
                    Id = blogpostdto.Id,
                    Title = blogpostdto.Title,
                    ShortDescription = blogpostdto.ShortDescription,
                    Content = blogpostdto.Content,
                    FeaturedImageUrl = blogpostdto.FeaturedImageUrl,
                    UrlHandle = blogpostdto.UrlHandle,
                    PublishedDate = blogpostdto.PublishedDate,
                    Author = blogpostdto.Author,
                    IsVisible = blogpostdto.IsVisible,
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetBlogpost")]
        public async Task<IActionResult> GetBlogpost()
        {
            var data = await _blogpostRepository.GetAsync();
            if(data == null)
            {
                return BadRequest();
            }
            //Map DTO to Domain Model
            var resposne = new List<AddBlogpostAngularDto>();
            foreach (var blog in data)
            {
                resposne.Add(new AddBlogpostAngularDto
                {
                    Id = blog.Id,
                    Title = blog.Title,
                    ShortDescription = blog.ShortDescription,
                    Content = blog.Content,
                    FeaturedImageUrl = blog.FeaturedImageUrl,
                    UrlHandle = blog.UrlHandle,
                    PublishedDate = blog.PublishedDate,
                    Author = blog.Author,
                    IsVisible = blog.IsVisible,
                });
            }
            return Ok(resposne);
        }
    }
}
