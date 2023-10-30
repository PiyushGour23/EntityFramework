using Billboard.Models;

namespace Billboard.Service
{
    public interface IBlogpostRepository
    {
        Task<Blogpost> AddAsync(Blogpost blogpost);
        Task<IEnumerable<Blogpost>> GetAsync();
    }
}
