using Microsoft.AspNetCore.Mvc;
using MVC.Models;

namespace MVC.Controllers;

public class TestController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public TestController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View(new TestModel());
    }
    [HttpPost]
    public IActionResult Create(TestModel testModel){
        if(!ModelState.IsValid){
            return View("Index", testModel);
        }
        return View("Index", testModel);
    }
}
