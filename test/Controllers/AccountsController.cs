using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using test.Entities.DTO;
using test.Entities.Models;
using test.JwtFeatures;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace test.Controllers;

[Route("api/[controller]")]
public class AccountsController : ControllerBase
{
    private readonly JwtHandler _jwtHandler;
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;


    public AccountsController(UserManager<User> userManager, IMapper mapper, JwtHandler jwtHandler)
    {
        _userManager = userManager;
        _mapper = mapper;
        _jwtHandler = jwtHandler;
    }


    [HttpPost("Registration")]
    public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistration)
    {
        if (userForRegistration == null || !ModelState.IsValid)
            return BadRequest();

        var user = _mapper.Map<User>(userForRegistration);
        user.ScheduleID = 1;//default schedule
        var result = await _userManager.CreateAsync(user, userForRegistration.Password!);
        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(e => e.Description);

            return BadRequest(new RegistrationResponseDto { Errors = errors });
        }

        return StatusCode(201);
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] UserForAuthenticationDto userForAuthentication)
    {
        var user = await _userManager.FindByNameAsync(userForAuthentication.Email);
        if (user == null || !await _userManager.CheckPasswordAsync(user, userForAuthentication.Password))
            return Unauthorized(new AuthResponseDto { ErrorMessage = "Invalid Authentication" });
        var signingCredentials = _jwtHandler.GetSigningCredentials();
        var claims = _jwtHandler.GetClaims(user);
        var tokenOptions = _jwtHandler.GenerateTokenOptions(signingCredentials, claims);
        var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        return Ok(new AuthResponseDto { IsAuthSuccessful = true, Token = token });
    }
}

