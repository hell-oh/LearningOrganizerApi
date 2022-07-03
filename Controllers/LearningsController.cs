using LearningOrganizerApi.Models;
using LearningOrganizerApi.Services;
using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace LearningOrganizerApi.Controllers;

// [DisableCors]
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class LearningsController : ControllerBase
{
    private readonly LearningsService _learningsService;
    private readonly UsersService _usersService;

    public LearningsController(LearningsService learningsService, UsersService usersService)
    {
        _learningsService = learningsService;
        _usersService = usersService;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<List<Learning>> Get() =>
        await _learningsService.GetAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Learning>> Get(string id) =>
        (await _learningsService.GetAsync(id)) ?? (ActionResult<Learning>)NotFound();

    //Devuelve learnings del usuario
    //acá no se si poner un [HttpGet("{id:length(24)}/user")] o un [HttpGet("{id:length(24)}/user/{userId}")]
    [HttpGet("user/{userId:length(24)}")]
    public async Task<List<Learning>> GetByUser(string userId) =>
        await _learningsService.GetByUserAsync(userId);

    [HttpPost]
    public async Task<IActionResult> Post(Learning newLearning)
    {
        await _learningsService.CreateAsync(newLearning);

        return CreatedAtAction(nameof(Get), new { id = newLearning.Id }, newLearning);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Learning updatedLearning)
    {
        var learning = await _learningsService.GetAsync(id);

        if (learning is null)
        {
            return NotFound();
        }

        updatedLearning.Id = learning.Id;

        await _learningsService.UpdateAsync(id, updatedLearning);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var learning = await _learningsService.GetAsync(id);

        if (learning is null)
        {
            return NotFound();
        }

        await _learningsService.RemoveAsync(id);

        return NoContent();
    }
}