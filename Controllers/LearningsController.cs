using LearningOrganizerApi.Models;
using LearningOrganizerApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace LearningOrganizerApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LearningsController : ControllerBase
{
    private readonly LearningsService _learningsService;

    public LearningsController(LearningsService learningsService) =>
        _learningsService = learningsService;

    [HttpGet]
    public async Task<List<Learning>> Get() =>
        await _learningsService.GetAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Learning>> Get(string id)
    {
        var learning = await _learningsService.GetAsync(id);

        if (learning is null)
        {
            return NotFound();
        }

        return learning;
    }

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