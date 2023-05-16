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
    public class EnvironmentalIndicatorsControler : ControllerBase
    {
        private readonly IEnvironmentalIndicatorsService _environmentalIndicatorsService;
        protected APIResponse _response;
        private readonly IMapper _mapper;

        public EnvironmentalIndicatorsControler(IEnvironmentalIndicatorsService environmentalIndicatorsService, IMapper mapper)
        {
            _environmentalIndicatorsService = environmentalIndicatorsService;
            _mapper = mapper;
            _response = new APIResponse();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetEnvironmentalIndicators()
        {
            try
            {
                var environmentalIndicators = await _environmentalIndicatorsService.GetAsync();

                _response.Result = _mapper.Map<List<EnvironmentalIndicatorsViewModel>>(environmentalIndicators);
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
        public async Task<ActionResult<APIResponse>> GetEnvironmentalIndicatorsById(string id)
        {
            try
            {
                var environmentalIndicators = await _environmentalIndicatorsService.GetByIdAsync(id);

                _response.Result = _mapper.Map<EnvironmentalIndicatorsViewModel>(environmentalIndicators);
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
        public async Task<ActionResult<APIResponse>> GetEnvironmentalIndicatorsByFactoryAsync(string id)
        {
            try
            {
                var environmentalIndicators = await _environmentalIndicatorsService.GetEnvironmentalIndicatorsByFactoryAsync(id);

                _response.Result = _mapper.Map<List<EnvironmentalIndicatorsViewModel>>(environmentalIndicators);
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
