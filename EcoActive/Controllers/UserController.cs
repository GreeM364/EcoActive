using AutoMapper;
using EcoActive.API.Models;
using EcoActive.BLL.Exceptions;
using EcoActive.BLL.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace EcoActive.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        protected APIResponse _response;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
            _response = new APIResponse();
        }

        [HttpGet("profile")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProfile()
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (string.IsNullOrWhiteSpace(userId))
                    return Unauthorized();

                var result = await _userService.GetProfileAsync(userId);

                _response.Result = _mapper.Map<ProfileViewModel>(result);
                _response.StatusCode = HttpStatusCode.OK;

                return Ok(_response);

            }
            catch (NotFoundException ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.ErrorMessages = new List<string> { ex.Message };

                return NotFound(_response);
            }
        }
    }
}
