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
public class NotesController : ControllerBase
{
    private readonly NotesService _NotesService;
    private readonly UsersService _usersService;

    public NotesController(NotesService NotesService, UsersService usersService)
    {
        _NotesService = NotesService;
        _usersService = usersService;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<List<Note>> Get() =>
        await _NotesService.GetAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Note>> Get(string id) =>
        (await _NotesService.GetAsync(id)) ?? (ActionResult<Note>)NotFound();

    //Devuelve Notes del usuario
    //acá no se si poner un [HttpGet("{id:length(24)}/user")] o un [HttpGet("{id:length(24)}/user/{userId}")]
    [HttpGet("user/{userId:length(24)}")]
    public async Task<List<Note>> GetByUser(string userId) =>
        await _NotesService.GetByUserAsync(userId);

    [HttpPost]
    public async Task<IActionResult> Post(Note newNote)
    {
        await _NotesService.CreateAsync(newNote);

        return CreatedAtAction(nameof(Get), new { id = newNote.Id }, newNote);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Note updatedNote)
    {
        var Note = await _NotesService.GetAsync(id);

        if (Note is null)
        {
            return NotFound();
        }

        updatedNote.Id = Note.Id;

        await _NotesService.UpdateAsync(id, updatedNote);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var Note = await _NotesService.GetAsync(id);

        if (Note is null)
        {
            return NotFound();
        }

        await _NotesService.RemoveAsync(id);

        return NoContent();
    }
}