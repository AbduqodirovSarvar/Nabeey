using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nabeey.Domain.Configurations;
using Nabeey.Service.DTOs.UserBalls;
using Nabeey.Service.DTOs.UserBookStatus;
using Nabeey.Service.DTOs.Users;
using Nabeey.Service.Interfaces;
using Nabeey.Web.Models;

namespace Nabeey.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserBallController : ControllerBase
    {
        private readonly IUserBallService ballService;
        public UserBallController(IUserBallService ballService)
        {
            this.ballService = ballService;
        }

        [HttpPost("create")]
        public async ValueTask<IActionResult> PostAsync(UserBallCreationDto dto)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.ballService.AddAsync(dto)
        });

        [HttpPut("update")]
        public async ValueTask<IActionResult> UpdateAsync(UserBallUpdateDto dto)
            => Ok(new Response
            {
                StatusCode = 200,
                Message = "Success",
                Data = await this.ballService.ModifyAsync(dto)
            });

        [HttpDelete("delete/{id:long}")]
        public async ValueTask<IActionResult> DeleteAsync(long id)
            => Ok(new Response
            {
                StatusCode = 200,
                Message = "Success",
                Data = await this.ballService.DeleteAsync(id)
            });

        [AllowAnonymous]
        [HttpGet("get/{id:long}")]
        public async ValueTask<IActionResult> GetAsync(long id)
            => Ok(new Response
            {
                StatusCode = 200,
                Message = "Success",
                Data = await this.ballService.RetrieveByIdAsync(id)
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
                Data = await this.ballService.RetrieveAllAsync(@params, filter, userId, bookId)
            });
    }
}
