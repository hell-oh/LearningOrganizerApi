using LearningOrganizerApi.Models;
using LearningOrganizerApi.Services;
using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace LearningOrganizerApi.Controllers;

// [DisableCors]
// [Authorize]
[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly CategoriesService _categoriesService;
    private readonly UsersService _usersService;
    private readonly LearningsService _learningsService;

    public CategoriesController(CategoriesService categoriesService, UsersService usersService, LearningsService learningsService)

    {
        _categoriesService = categoriesService;
        _usersService = usersService;
        _learningsService = learningsService;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<List<Category>> Get() =>
        await _categoriesService.GetAsync();

    [AllowAnonymous]
    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Category>> Get(string id) =>
        (await _categoriesService.GetAsync(id)) ?? (ActionResult<Category>)NotFound();

    [HttpGet("publiclearnings/{categoryName:length(24)}")]
    public async Task<List<Learning>> GetLearningsByCategory(string categoryName) =>
        await _categoriesService.GetLearningsByCategoryAsync(categoryName);

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Post(Category newCategory)
    {
        await _categoriesService.CreateAsync(newCategory);

        return CreatedAtAction(nameof(Get), new { id = newCategory.Id }, newCategory);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Category updatedCategory)
    {
        var category = await _categoriesService.GetAsync(id);

        if (category is null)
        {
            return NotFound();
        }

        updatedCategory.Id = category.Id;

        await _categoriesService.UpdateAsync(id, updatedCategory);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var category = await _categoriesService.GetAsync(id);

        if (category is null)
        {
            return NotFound();
        }

        await _categoriesService.RemoveAsync(id);

        return NoContent();
    }
}