using LearningOrganizerApi.Models;
using LearningOrganizerApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearningOrganizerApi.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly UsersService _usersService;

    public UsersController(UsersService usersService) =>
        _usersService = usersService;


    [AllowAnonymous]
    [Route("autenticate")]
    [HttpPost]
    public ActionResult Login( [FromBody] User user)
    {
        var token = _usersService.Authenticate(user.Email, user.Password);

        if(token is null)
        {
            return Unauthorized();
        }
        return Ok(new {token, user});
    }



    // [AllowAnonymous]
    // [Route("autenticate")]
    // [HttpPost]
    // public async Task<ActionResult<User>> Autenticate( [FromBody] User user)
    // {
    //     var userFromDb = await _usersService.GetAsync(user.Email);
    //     if (userFromDb is null)
    //     {
    //         return NotFound();
    //     }
    //     if (userFromDb.Password != user.Password)
    //     {
    //         return Unauthorized();
    //     }
    //     return userFromDb;
    // }




    [HttpGet]
    public async Task<List<User>> Get() =>
        await _usersService.GetAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<User>> Get(string id)
    {
        var user = await _usersService.GetAsync(id);

        if (user is null)
        {
            return NotFound();
        }

        return user;
    }

    [HttpPost]
    public async Task<IActionResult> Post(User newUser)
    {
        await _usersService.CreateAsync(newUser);

        return CreatedAtAction(nameof(Get), new { id = newUser.Id }, newUser);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, User updatedUser)
    {
        var user = await _usersService.GetAsync(id);

        if (user is null)
        {
            return NotFound();
        }

        updatedUser.Id = user.Id;

        await _usersService.UpdateAsync(id, updatedUser);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var user = await _usersService.GetAsync(id);

        if (user is null)
        {
            return NotFound();
        }

        await _usersService.RemoveAsync(id);

        return NoContent();
    }
}