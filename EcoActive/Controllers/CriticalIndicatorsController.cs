using AutoMapper;
using EcoActive.API.Models;
using EcoActive.BLL.Exceptions;
using EcoActive.BLL.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EcoActive.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CriticalIndicatorsController : ControllerBase
    {
        private readonly ICriticalIndicatorsService _criticalIndicatorsService;
        protected APIResponse _response;
        private readonly IMapper _mapper;

        public CriticalIndicatorsController(ICriticalIndicatorsService criticalIndicatorsService, IMapper mapper)
        {
            _criticalIndicatorsService = criticalIndicatorsService;
            _mapper = mapper;
            _response = new APIResponse();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetCriticalIndicators()
        {
            try
            {
                var criticalIndicators = await _criticalIndicatorsService.GetAsync();

                _response.Result = _mapper.Map<List<CriticalIndicatorsViewModel>>(criticalIndicators);
                _response.StatusCode = HttpStatusCode.OK;

                return Ok(_response);

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };

                return _response;
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetCriticalIndicatorsById(string id)
        {
            try
            {
                var criticalIndicators = await _criticalIndicatorsService.GetByIdAsync(id);

                _response.Result = _mapper.Map<CriticalIndicatorsViewModel>(criticalIndicators);
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
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };

                return _response;
            }
        }

        [HttpGet("{id}/factory")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetCriticalIndicatorsByFactory(string id)
        {
            try
            {
                var criticalIndicators = await _criticalIndicatorsService.GetCriticalIndicatorsByFactoryAsync(id);

                _response.Result = _mapper.Map<List<CriticalIndicatorsViewModel>>(criticalIndicators);
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
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };

                return _response;
            }
        }
    }
}
