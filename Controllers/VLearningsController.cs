using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LearningOrganizerApi.Models;
using LearningOrganizerApi.Services;

namespace LearningOrganizerApi.Controllers;

public class VLearningsController : Controller
{
    private readonly ILogger<VLearningsController> _logger;
    private readonly LearningsService _learningsService;
    private readonly UsersService _usersService;

    public VLearningsController(ILogger<VLearningsController> logger, LearningsService learningsService, UsersService usersService)
    {
        _logger = logger;
        _learningsService = learningsService;
        _usersService = usersService;
    }

    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> Learnings()
    {
        List<Learning> lol = await _learningsService.GetAsync();
        if (lol == null)
        {
            return NotFound();
        }
        ViewData["Learnings"] = lol;
        return View(lol);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
