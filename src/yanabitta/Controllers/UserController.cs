using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using src.Data.IRepositories;
using src.Domain.Configurations;
using src.Domain.Entities;
using src.Services.DTOs;
using src.Services.Exceptions;
using src.Services.IServices;
using System.Threading.Tasks;

namespace src.yanabitta.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService service;
        private readonly IUnitOfWork _unit;

        public UserController(IUserService service, IUnitOfWork unit)
        {
            this.service = service;
            this._unit = unit;
        }

        /// <summary>
        /// Select user by entiring id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(long id)
            => Ok(await service.GetAsync(p => p.Id == id));

        /// <summary>
        /// Create user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<User>> Post([FromForm] UserForCreation user,IFormFile file = null)
            => Ok(await service.CreateAsync(user, file?.OpenReadStream(),file?.Name));

        /// <summary>
        /// Select all users
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<User>> GetAll(PaginationParams @params = null)
            => Ok(await service.GetAllAsync(@params));

        /// <summary>
        /// Remove user by entiring id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ActionResult> Delete(long id)
           => Ok(await service.DeleteAsync(u => u.Id == id));

        /// <summary>
        /// Update user by entiring id to entiring dto
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult<User>> Put(long id, [FromForm]UserForCreation user)
           => Ok(await service.UpdateAsync(u => u.Id == id, user));
    }
}
