using AutoMapper;
using EcoActive.API.Models;
using EcoActive.BLL.DataTransferObjects;
using EcoActive.BLL.Exceptions;
using EcoActive.BLL.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EcoActive.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FactoryAdministratorController : ControllerBase
    {
        private readonly IFactoryAdministratorService _factoryAdministratorService;
        protected APIResponse _response;
        private readonly IMapper _mapper;

        public FactoryAdministratorController(IFactoryAdministratorService factoryAdministratorService, IMapper mapper)
        {
            _factoryAdministratorService = factoryAdministratorService;
            _mapper = mapper;
            _response = new APIResponse();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetFactoryAdministrators()
        {
            try
            {
                var factoryAdministrator = await _factoryAdministratorService.GetAsync();

                _response.Result = _mapper.Map<List<FactoryAdministratorViewModel>>(factoryAdministrator);
                _response.StatusCode = HttpStatusCode.OK;

                return Ok(_response);

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };

                return _response;
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetFactoryAdministratorById(string id)
        {
            try
            {
                var doctor = await _factoryAdministratorService.GetByIdAsync(id);

                _response.Result = _mapper.Map<FactoryAdministratorViewModel>(doctor);
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

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> Post([FromBody] FactoryAdministratorCreateViewModel request)
        {
            try
            {
                var factoryAdministratorDTO = _mapper.Map<FactoryAdministratorCreateDTO>(request);
                var factoryAdministrator = await _factoryAdministratorService.CreateAsync(factoryAdministratorDTO);

                _response.Result = _mapper.Map<FactoryAdministratorViewModel>(factoryAdministrator);
                _response.StatusCode = HttpStatusCode.Created;

                return StatusCode(StatusCodes.Status201Created, _response);

            }
            catch (BadRequestException ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages = new List<string> { ex.ToString() };

                return BadRequest(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };

                return _response;
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> Put(string id, [FromBody] FactoryAdministratorUpdateViewModel request)
        {
            try
            {
                var factoryAdministratorDTO = _mapper.Map<FactoryAdministratorUpdateDTO>(request);
                var factoryAdministrator = await _factoryAdministratorService.UpdateAsync(id, factoryAdministratorDTO);

                _response.Result = _mapper.Map<FactoryAdministratorViewModel>(factoryAdministrator);
                _response.StatusCode = HttpStatusCode.OK;

                return Ok(_response);

            }
            catch (BadRequestException ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages = new List<string> { ex.ToString() };

                return BadRequest(_response);
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

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> Delete(string id)
        {
            try
            {
                await _factoryAdministratorService.DeleteAsync(id);

                _response.StatusCode = HttpStatusCode.NoContent;
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
