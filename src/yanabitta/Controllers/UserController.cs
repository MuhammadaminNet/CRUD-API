using Microsoft.AspNetCore.Authorization;
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

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get([FromForm] long id)
        {
            var res = await service.GetAsync(p => p.Id == id);

            return res is not null ? Ok(res) : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<User>> Post([FromForm] UserForCreation user)
        {
            var res = await service.CreateAsync(user);

            return res != null ? new ObjectResult(res) : BadRequest();
        }

        [HttpGet]
        public async Task<ActionResult<User>> GetAll()
        {
            var paginationItems = new PaginationParams()
            {
                PageSize = 1,
                PageIndex = 2
            };

            return Ok(await service.GetAllAsync(paginationItems));
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(long id)
        {
            var check = await service.DeleteAsync(u => u.Id == id);

            return check == false ? throw new UserException(400,"Not Found") : Ok();
        }

        [HttpPut]
        public async Task<ActionResult<User>> Put(long id, [FromForm]UserForCreation user)
        {
            var res = await service.UpdateAsync(u => u.Id == id, user);

            return res == null ? throw new UserException(400, "Not found") : Ok(res);
        }
    }
}
