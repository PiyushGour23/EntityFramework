using Billboard.Data;
using Billboard.Models;
using Billboard.Models.DTO;
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

        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser(AddUserDto addUserDto)
        {
            try
            {
                //Map DTO to Domain Model
                var userdto = new User
                {
                    Name = addUserDto.Name,
                    Email = addUserDto.Email,
                    Password = addUserDto.Password,
                    PhoneNumber = addUserDto.PhoneNumber,
                    CreatedDate = addUserDto.CreatedDate,
                };
                await _context.User.AddAsync(userdto);
                await _context.SaveChangesAsync();


                //Map DTO to Domain Model for the angular application
                var response = new UserAngularDto
                {
                    UserId = userdto.UserId,
                    Name = userdto.Name,
                    Email = userdto.Email,
                    Password = userdto.Password,
                    PhoneNumber = userdto.PhoneNumber,
                    CreatedDate = userdto.CreatedDate,
                };


                return Ok(response);
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
