using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nabeey.Domain.Configurations;
using Nabeey.Service.DTOs.Quizzes;
using Nabeey.Service.DTOs.UserBookStatus;
using Nabeey.Service.Interfaces;
using Nabeey.Web.Models;

namespace Nabeey.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserBookStatusController : ControllerBase
    {
        private readonly IUserBookStatusService statusService;
        public UserBookStatusController(IUserBookStatusService statusService)
        {
            this.statusService = statusService;
        }

        [HttpPost("create")]
        public async ValueTask<IActionResult> PostAsync(UserBookStatusCreationDto dto)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.statusService.AddAsync(dto)
        });

        [HttpPut("update")]
        public async ValueTask<IActionResult> UpdateAsync(UserBookStatusUpdateDto dto)
            => Ok(new Response
            {
                StatusCode = 200,
                Message = "Success",
                Data = await this.statusService.ModifyAsync(dto)
            });

        [HttpDelete("delete/{id:long}")]
        public async ValueTask<IActionResult> DeleteAsync(long id)
            => Ok(new Response
            {
                StatusCode = 200,
                Message = "Success",
                Data = await this.statusService.DeleteAsync(id)
            });

        [AllowAnonymous]
        [HttpGet("get/{id:long}")]
        public async ValueTask<IActionResult> GetAsync(long id)
            => Ok(new Response
            {
                StatusCode = 200,
                Message = "Success",
                Data = await this.statusService.RetrieveByIdAsync(id)
            });

        [AllowAnonymous]
        [HttpGet("get-all")]
        public async ValueTask<IActionResult> GetAllAsync(
        [FromQuery] PaginationParams @params,
        [FromQuery] Filter filter,
        [FromQuery] long? userId,
        [FromQuery] long? bookId
        )
            => Ok(new Response
            {
                StatusCode = 200,
                Message = "Success",
                Data = await this.statusService.RetrieveAllAsync(@params, filter, userId, bookId)
            });
    }
}
