using Billboard.Data;
using Billboard.Models;
using Billboard.Service;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using Microsoft.EntityFrameworkCore;

namespace Billboard.Container
{
    public class BlogpostRepository : IBlogpostRepository
    {
        private readonly UserDbContext _userDbContext;
        public BlogpostRepository(UserDbContext userDbContext) 
        {
            _userDbContext = userDbContext;
        }

        public async Task<Blogpost> AddAsync(Blogpost blogpost)
        {
            await _userDbContext.blogpost.AddAsync(blogpost);
            await _userDbContext.SaveChangesAsync();
            return blogpost;
        }

        public async Task<IEnumerable<Blogpost>> GetAsync()
        {
            return await _userDbContext.blogpost.ToListAsync();
        }
    }
}
