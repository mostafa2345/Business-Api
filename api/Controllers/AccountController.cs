using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Register;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> userManager;
        private readonly ITockenService tockenService;
        private readonly SignInManager<AppUser> signInManager;

        public AccountController(UserManager<AppUser> userManager,ITockenService tockenService,SignInManager<AppUser> signInManager)
        {
            this.userManager = userManager;
            this.tockenService = tockenService;
            this.signInManager = signInManager;
        }

[HttpPost("login")]
public async Task<IActionResult> Login(LoginDto  loginDto){
if(!ModelState.IsValid){
   return BadRequest(ModelState); 
}

var user = await userManager.Users.FirstOrDefaultAsync(x=>x.UserName==loginDto.Username.ToLower());
if(user==null){
return Unauthorized("");
}

var  result= await signInManager.CheckPasswordSignInAsync(user, loginDto.Password,false);

if(!result.Succeeded){

    return Unauthorized(" Username or password are invalid");
}
return Ok(
    new NewUserDto{
     Username =user.UserName,
     Email=user.Email,
     Tokem=tockenService.CreateTocken(user),
    }
);
}



        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var appUser = new AppUser
                {
                    UserName = registerDto.Username,
                    Email = registerDto.Email,

                };
                var CreatedUser = await userManager.CreateAsync(appUser, registerDto.Password);
                if (CreatedUser.Succeeded)
                {
                    var roleReasult = await userManager.AddToRoleAsync(appUser, "User");
                    if (roleReasult.Succeeded)
                    {

                        return Ok(new NewUserDto{
                            Username=appUser.UserName,
                            Email=appUser.Email,
                            Tokem=tockenService.CreateTocken(appUser)
                        });
                    }
                    else
                    {
                        return StatusCode(500, roleReasult.Errors);
                    }

                }
                else
                {
                    return StatusCode(500, CreatedUser.Errors);
                }

            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }

        }
    


    
    
    
    
    
    
    }
}