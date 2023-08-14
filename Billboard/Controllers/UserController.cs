using Billboard.Data;
using Billboard.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Billboard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserDbContext _context;

        public UserController(UserDbContext context)  // this way we will get object of DbContext at runtime
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<User>> GetUsers()
        {
            try
            {
                //if(_context.DbSet == null)
                //{
                //    return (IEnumerable<User>)NotFound();
                //}
                return await _context.User.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetUserById(int UserId)
        {
            try
            {
                var user = await _context.User.FindAsync(UserId);
                return user == null ? NotFound() : Ok(user);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(User user)
        {
            try
            {
                await _context.User.AddAsync(user);
                await _context.SaveChangesAsync();
                return Ok(user);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPut("{UserId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int UserId, User user)
        {
            try
            {
                if (UserId != user.UserId) return BadRequest();
                _context.Entry(user).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpDelete("{UserId}")]
        public async Task<IActionResult> Delete(int UserId)
        {
            var userdelete = await _context.User.FindAsync(UserId);
            if (userdelete == null)
            {
                return NoContent();
            }               
            _context.User.Remove(userdelete);
            await _context.SaveChangesAsync();
            {
                return NoContent();
            }

        }
    }
}
