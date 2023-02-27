using AutoMapper;
using Contracts;
using Entities.Configurations;
using Entities.DataTranferObjects;
using Entities.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApi.Controllers
{
    [ApiVersion("1.0", Deprecated = true)]
    [Route("api/authentication")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    public class AuthenticationController : ControllerBase
    {
        private UserManager<User> _usermanger;
        private IAuthenticationManager _authonticationManger;
        private IMapper _mapper;
        private ILoggerManager _logger;
        public AuthenticationController(UserManager<User> usermanger, ILoggerManager logger, IMapper mapper, IAuthenticationManager authonticationManger)
        {
            _usermanger = usermanger;
            _authonticationManger = authonticationManger;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegisterationDto userRegisterationDto)
        {
            userRegisterationDto.Role = "User";
            userRegisterationDto.UserName = userRegisterationDto.Email;


            var user = _mapper.Map<User>(userRegisterationDto);

            if (userRegisterationDto == null)
            {
                return Unauthorized(new { message = "Object Is Null" });

            }
            else if (ModelState.IsValid == false)
            {
                return Unauthorized(ModelState);
            }

            var result = await _usermanger.CreateAsync(user, userRegisterationDto.Password);

            if (result.Succeeded == false)
            {
                var err = "";

                foreach (var error in result.Errors)
                {
                    err = error.Description;
                    ModelState.TryAddModelError(error.Code, error.Description);
                }

                return Unauthorized(new { message = $"{err}" });

            }
            else
            {
                await _usermanger.AddToRoleAsync(user, userRegisterationDto.Role);

                return Created("Created", new { message = "success" });
            }

        }



        [HttpPost("login")]
        public async Task<IActionResult> Authenticate([FromBody] UserLoginDto user)
        {

            try
            {
                var res = await _authonticationManger.ValidateUser(user);

                if (res == "NotFound")
                {
                    return Unauthorized(new { message = "Your Email Is Wrong" });

                }
                else if (res == "wrongPass")
                {
                    return Unauthorized(new { message = "Wrong Password" });

                }
                else if (res == "AllTrue")
                {
                    _logger.LogError($"User Login");
                    var token = await _authonticationManger.CreateToken();
                    return Ok(new { message = "success", Token = token });

                }
                else
                {
                    return Unauthorized(new { message = "SomethingError" });
                }

            }
            catch (System.Exception ex)
            {
                _logger.LogWarn($"{nameof(Authenticate)} : Authenticate Failed . wrong Email or Password");
                _logger.LogError($"somthing went wrong : {ex.InnerException}");
                _logger.LogError($"somthing went wrong : {ex}");
                return Unauthorized(new { message = ex.Message });
            }
        }

    }
}
