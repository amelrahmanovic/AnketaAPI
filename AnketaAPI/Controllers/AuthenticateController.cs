using AnketaAPI.Models;
using AnketaAPI.Models.Identity;
using AnketaAPI.ViewModels;
using AnketaAPI.ViewModels.IdentitiVM;
using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace AnketaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private MapperConfiguration config;
        private Mapper mapper;

        public AuthenticateController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;

            config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            mapper = new Mapper(config);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModelVM model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            
            if (user == null)
                user = await _userManager.FindByEmailAsync(model.Username);

            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                authClaims.Add(new Claim("FullName", user.FirstName + " " + user.LastName));

                var token = CreateToken(authClaims);
                var refreshToken = GenerateRefreshToken();

                user.RefreshToken = refreshToken;
                if(model.RememberMe)
                {
                    _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);
                    user.RefreshTokenExpiryTime = DateTime.Now.AddDays(refreshTokenValidityInDays);
                }
                else
                {
                    _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInMonthsRememberMe"], out int refreshTokenValidityInMonths);
                    user.RefreshTokenExpiryTime = DateTime.Now.AddDays(refreshTokenValidityInMonths);
                }
                

                await _userManager.UpdateAsync(user);

                return Ok(new
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    RefreshToken = refreshToken,
                    Expiration = token.ValidTo
                });
            }
            return Unauthorized();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("getusers")]
        public async Task<List<ApplicationUserVM>> GetUsersAsync()
        {
            List<ApplicationUser> users = _userManager.Users.ToList();
            List<ApplicationUserVM> usersVM = mapper.Map<List<ApplicationUserVM>>(users);
            for (int i = 0; i < usersVM.Count; i++)
            {
                var userRoles = await _userManager.GetRolesAsync(users[i]);
                usersVM[i].UserRoles = (List<string>?)userRoles;
            }
            return usersVM;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("getUserByUserId")]
        public async Task<ApplicationUserVM> GetUserByUserIdAsync([FromQuery] string userId)
        {
            ApplicationUser? user = await _userManager.FindByIdAsync(userId);
            ApplicationUserVM usersVM = mapper.Map<ApplicationUserVM>(user);
            if (user!=null)
                usersVM.UserRoles = (List<string>?)await _userManager.GetRolesAsync(user);
            return usersVM;
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("deleteuser/{userId}")]
        public async Task<IActionResult> DeleteAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                    return Ok(result);
                return BadRequest(result);
            }
            return NotFound();
        }

        //[Authorize]
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModelVM model)
        {
            if(_userManager.Users.Count()==0)
            {
                var userExists = await _userManager.FindByNameAsync(model.Username);
                var userExist2 = await _userManager.FindByEmailAsync(model.Email);
                if (userExists != null || userExist2 != null)
                    return StatusCode(StatusCodes.Status500InternalServerError, value: "User already exists!");

                ApplicationUser user = new()
                {
                    Email = model.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = model.Username,
                    FirstName = model.FirstName == null ? "" : model.FirstName,
                    LastName = model.LastName == null ? "" : model.LastName
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (!result.Succeeded)
                    return StatusCode(StatusCodes.Status500InternalServerError, value: "User creation failed! Please check user details and try again.");

                if (model.UserRoles != null)
                    foreach (var role in model.UserRoles)
                        if (await _roleManager.RoleExistsAsync(role))
                            await _userManager.AddToRoleAsync(user, role);

                return Created();
            }
            else
            {
                if (HttpContext.User.Identity.IsAuthenticated)
                {
                    if (model.Id == null)//Insert user and role
                    {
                        var userExists = await _userManager.FindByNameAsync(model.Username);
                        var userExist2 = await _userManager.FindByEmailAsync(model.Email);
                        if (userExists != null || userExist2 != null)
                            return StatusCode(StatusCodes.Status500InternalServerError, value: "User already exists!");

                        ApplicationUser user = new()
                        {
                            Email = model.Email,
                            SecurityStamp = Guid.NewGuid().ToString(),
                            UserName = model.Username,
                            FirstName = model.FirstName == null ? "" : model.FirstName,
                            LastName = model.LastName == null ? "" : model.LastName
                        };
                        var result = await _userManager.CreateAsync(user, model.Password);
                        if (!result.Succeeded)
                            return StatusCode(StatusCodes.Status500InternalServerError, value: "User creation failed! Please check user details and try again.");

                        if (model.UserRoles != null)
                            foreach (var role in model.UserRoles)
                                if (await _roleManager.RoleExistsAsync(role))
                                    await _userManager.AddToRoleAsync(user, role);

                        return Created();
                    }
                    else//edit role not insert user
                    {
                        var userFromDb = await _userManager.FindByIdAsync(model.Id);
                        if (userFromDb != null)
                        {
                            //after create update user???
                            //userFromDb.FirstName = model.FirstName==null? "":model.FirstName;
                            //userFromDb.LastName = model.LastName == null ? "" : model.LastName;
                            if (model.UserRoles == null)
                            {
                                var rolesFromDb = await _userManager.GetRolesAsync(userFromDb);
                                foreach (var role in rolesFromDb)
                                {
                                    var result = await _userManager.RemoveFromRoleAsync(userFromDb, role);
                                }
                            }
                            else
                            {
                                var rolesFromDb = await _userManager.GetRolesAsync(userFromDb);
                                foreach (var roleFromRequest in model.UserRoles)//Add new role
                                {
                                    if (rolesFromDb.SingleOrDefault(x => x == roleFromRequest) == null)
                                    {
                                        var result = await _userManager.AddToRoleAsync(userFromDb, roleFromRequest);
                                    }
                                }
                                foreach (var roleDb in rolesFromDb)
                                {
                                    if (model.UserRoles.SingleOrDefault(x => x == roleDb) == null)
                                    {
                                        var result = await _userManager.RemoveFromRoleAsync(userFromDb, roleDb);
                                    }
                                }
                            }
                        }
                        return Created();
                    }
                    
                }
                else
                {
                    return StatusCode(401);
                }
                
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("addrole")]
        public async Task<IActionResult> AddRole([FromBody] RoleVM role)
        {
            if (!await _roleManager.RoleExistsAsync(role.name))
            {
                await _roleManager.CreateAsync(new IdentityRole(role.name));
                return Created();
            }
            return NotFound();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("getroles")]
        public List<RoleVM> GetRoles()
        {
            List<IdentityRole> roles = _roleManager.Roles.AsNoTracking().ToList();
            List<RoleVM> roleVM = mapper.Map<List<RoleVM>>(roles);
            return roleVM;
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("deleterole")]
        public async Task<IActionResult> DeleteRoleAsync([FromQuery] string role)
        {
            var roleFind = await _roleManager.FindByNameAsync(role);
            if (roleFind != null)
            {
                var x = await _roleManager.DeleteAsync(roleFind);
                return Ok();
            }
            else
                return NotFound();
        }

        //[Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("deleteroleuser")]
        public async Task<IActionResult> DeleteRoleUserAsync([FromQuery] string userId, [FromQuery] string role)
        {
            var roleFind = await _roleManager.FindByNameAsync(role);
            if (roleFind != null)
            {
                var userFind = await _userManager.FindByIdAsync(userId);
                if (userFind != null)
                {
                    var result = await _userManager.RemoveFromRoleAsync(userFind, role);
                    if (result.Succeeded)
                        return Ok();
                }
            }
            return NotFound();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModelVM model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, value: "User already exists!");

            ApplicationUser user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username,
                FirstName = model.FirstName,
                LastName = model.LastName
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, value: "User creation failed! Please check user details and try again." );

            if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            if (!await _roleManager.RoleExistsAsync(UserRoles.User))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));

            if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.Admin);
            }
            if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.User);
            }
            return Ok("User created successfully!");
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("refresh-token")]
        public async Task<IActionResult> RefreshToken(TokenModelVM tokenModel)
        {
            if (tokenModel is null)
                return BadRequest("Invalid client request");

            string? accessToken = tokenModel.AccessToken;
            string? refreshToken = tokenModel.RefreshToken;

            var principal = GetPrincipalFromExpiredToken(accessToken);
            if (principal == null)
                return BadRequest("Invalid access token or refresh token");

#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            string username = principal.Identity.Name;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

            var user = await _userManager.FindByNameAsync(username);

            if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
                return BadRequest("Invalid access token or refresh token");

            var newAccessToken = CreateToken(principal.Claims.ToList());
            var newRefreshToken = GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            await _userManager.UpdateAsync(user);

            return new ObjectResult(new
            {
                accessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                refreshToken = newRefreshToken
            });
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("EnableNew")]
        public async Task<IActionResult> EnableNew()
        {
            var countUsers = _userManager.Users.Count();

            return countUsers == 0 ? new ObjectResult(new { Enable = true }): new ObjectResult(new { Enable = false });
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("revoke/{username}")]
        public async Task<IActionResult> Revoke(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null) return BadRequest("Invalid user name");

            user.RefreshToken = null;
            await _userManager.UpdateAsync(user);

            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("revoke-all")]
        public async Task<IActionResult> RevokeAll()
        {
            var users = _userManager.Users.ToList();
            foreach (var user in users)
            {
                user.RefreshToken = null;
                await _userManager.UpdateAsync(user);
            }

            return NoContent();
        }

        private JwtSecurityToken CreateToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            _ = int.TryParse(_configuration["JWT:TokenValidityInMinutes"], out int tokenValidityInMinutes);

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddMinutes(tokenValidityInMinutes),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }

        private static string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        private ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"])),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;

        }
    }
}
