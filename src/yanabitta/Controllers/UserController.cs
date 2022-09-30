using Data.IRepositories;
using Domain.Configurations;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.DTOs;
using Services.Exceptions;
using Services.IServices;
using System.Threading.Tasks;

namespace yanabitta.Controllers
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

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get([FromForm] long id)
        {
            var res = await service.GetAsync(p => p.Id == id);

            return res is not null 
                ? Ok(res) 
                : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<User>> Post([FromForm] UserForCreation user)
        {
            var res = await service.CreateAsync(user);

            if (res != null)
                return new ObjectResult(res);

            return BadRequest();
        }

        [HttpGet,Authorize]
        public async Task<ActionResult<User>> GetAll()
        {
            var p = new PaginationParams()
            {
                PageSize = 1,
                PageIndex = 2
            };

            var res = await service.GetAllAsync(p);

            return Ok(res);
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(long id)
        {
            var check = await service.DeleteAsync(u => u.Id == id);

            if (check == false)
                throw new UserException(404, "Not found");

            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult<User>> Put(long id, [FromForm]UserForCreation user)
        {
            var res = await service.UpdateAsync(u => u.Id == id, user);

            if (res == null)
                throw new UserException(404,"Not found");

            return Ok(res);
        }
    }
}
