using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ProjetoApi.Configuration;
using ProjetoApi.Data;
using ProjetoApi.Models.DTOs.Requests;
using ProjetoApi.Models.DTOs.Responses;

namespace ProjetoApi.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class AuthManagementController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _database;
        private readonly JwtConfig _jwtConfig;

        public AuthManagementController(UserManager<IdentityUser> userManager, IOptionsMonitor<JwtConfig> jwtconfig, ApplicationDbContext database)
        {
            _userManager = userManager;
            _jwtConfig = jwtconfig.CurrentValue;
            _database = database;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationDto user)
        {
            if (ModelState.IsValid)
            {
                var existingEmail = await _userManager.FindByEmailAsync(user.Email);

                if (existingEmail != null)
                {
                    return BadRequest(new RegistrationResponse()
                    {
                        Errors = new List<string>()
                        {
                            "Email já cadastrado."
                        },
                        Success = false
                    });
                }

                var existingUser = await _userManager.FindByNameAsync(user.Username);
                if (existingUser != null)
                {
                    return BadRequest(new RegistrationResponse()
                    {
                        Errors = new List<string>()
                        {
                            "Nome de usuário já está em uso."
                        },
                        Success = false
                    });
                }

                var newUser = new IdentityUser() { Email = user.Email, UserName = user.Username };
                var isCreated = await _userManager.CreateAsync(newUser, user.Password);

                if (isCreated.Succeeded)
                {
                    await _userManager.AddClaimAsync(newUser, new Claim("NomeCompleto", user.Name));
                    _userManager.AddToRoleAsync(newUser, "user").Wait();
                    return Ok("Usuário criado com sucesso!");
                }
                else
                {
                    return BadRequest(new RegistrationResponse()
                    {
                        Errors = isCreated.Errors.Select(x => x.Description).ToList(),
                        Success = false
                    });
                }
            }

            return BadRequest(new RegistrationResponse()
            {
                Errors = new List<string>()
                {
                    "Dados inválidos."
                },
                Success = false
            });
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest user)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _userManager.FindByEmailAsync(user.Email);

                if (existingUser == null)
                {
                    return BadRequest(new RegistrationResponse()
                    {
                        Errors = new List<string>()
                        {
                            "Email não encontrado."
                        },
                        Success = false
                    });
                }

                var passwordIsValid = await _userManager.CheckPasswordAsync(existingUser, user.Password);
                if (!passwordIsValid)
                {
                    return BadRequest(new RegistrationResponse()
                    {
                        Errors = new List<string>()
                        {
                            "Senha Inválida."
                        },
                        Success = false
                    });
                }

                var jwtToken = GenerateJwtTokenAsync(existingUser);
                return Ok(new RegistrationResponse()
                {
                    Success = true,
                    Token = await jwtToken
                });
            }

            return BadRequest(new RegistrationResponse()
            {
                Errors = new List<string>()
                    {
                        "Dados inválidos."
                    },
                Success = false
            });

        }
        private async Task<string> GenerateJwtTokenAsync(IdentityUser user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            string userId = user.Id;
            var userRole = await _userManager.GetRolesAsync(user);
            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("id", user.Id),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(ClaimTypes.Role, userRole[0]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(6),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);

            return jwtToken;
        }
    }
}