﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ToDoTodayAPI.Models;
using ToDoTodayAPI.Models.Identity;

namespace ToDoTodayAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<ToDoUser> userManager;
        private readonly IConfiguration configuration;

        public UsersController(UserManager<ToDoUser> userManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.configuration = configuration;
        }

        [HttpGet("self")]
        public IActionResult Self()
        {
            var user = HttpContext.User;
            if (!user.Identity.IsAuthenticated)
            {
                return Unauthorized();
            }

                return Ok();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginData login)
        {
            var user = await userManager.FindByNameAsync(login.Username);
            if (user != null)
            {
                var result = await userManager.CheckPasswordAsync(user, login.Password);
                if (result)
                {
                    return Ok(new
                    {
                        UserId = user.Id,
                        Token = CreateToken(user),
                    });
                }

                await userManager.AccessFailedAsync(user);
            }

            return Unauthorized();
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterData register)
        {
            var user = new ToDoUser
            {
                Email = register.Email,
                UserName = register.Email,
                FirstName = register.FirstName,
                LastName = register.LastName,
            };

            var result = await userManager.CreateAsync(user, register.Password);

            if (!result.Succeeded)
            {
                return BadRequest(new
                {
                    message = "Registration Failed",
                    errors = result.Errors,

                });
            }
            return Ok(new
            {
                UserId = user.Id,
                Token = CreateToken(user),
            });
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUser(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound();

            return Ok(new
            {
                UserId = user.Id,
                user.Email,
                user.FirstName,
                user.LastName,
                user.EyeColor,
                user.FavoriteFood,
                user.Birthday,
            });
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUser(string userId, UpdateUserData data)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound();

            user.FirstName = data.FirstName;
            user.LastName = data.LastName;
            user.EyeColor = data.EyeColor;
            user.FavoriteFood = data.FavoriteFood;
            user.Birthday = data.Birthday;

            await userManager.UpdateAsync(user);

            return Ok(new
            {
                UserId = user.Id,
                user.Email,
                user.FirstName,
                user.LastName,
                user.EyeColor,
                user.FavoriteFood,
                user.Birthday,
            });
        }

        private string CreateToken(ToDoUser user)
        {
            var secret = configuration["JWT:Secret"];
            var secretBytes = Encoding.UTF8.GetBytes(secret);
            var signingKey = new SymmetricSecurityKey(secretBytes);

            var tokenClaims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim("UserId", user.Id),
                new Claim("FullName", $"{user.FirstName} {user.LastName}"),
            };

            var token = new JwtSecurityToken(
                expires: DateTime.UtcNow.AddSeconds(10),
                claims: tokenClaims,
                signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
                );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenString;
        }
    }
}
