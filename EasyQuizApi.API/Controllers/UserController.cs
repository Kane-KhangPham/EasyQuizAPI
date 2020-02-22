using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyQuizApi.Data.RepositoryBase;
using EasyQuizApi.Share.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EasyQuizApi.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login( UserLogin userLogin )
        {
            Console.Write("-----" + userLogin.Username + "+++" + userLogin.Password);
            try
            {
                var user = _userRepository.Authenticate(userLogin.Username, userLogin.Password);
                if (user == null)
                {
                    return NotFound();
                }
                return Ok(user);
            } catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}