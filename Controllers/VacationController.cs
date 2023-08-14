using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FullStackCSharpExNotes.Models;

namespace FullStackCSharpExNotes.Controllers;

//Added for Include:
using Microsoft.EntityFrameworkCore;

//ADDED for session check
using Microsoft.AspNetCore.Mvc.Filters;


[SessionCheck]
public class VacationController : Controller
{
    private readonly ILogger<VacationController> _logger;

    // Add field - adding context into our class // "db" can eb any name
    private MyContext db;

    public VacationController(ILogger<VacationController> logger, MyContext context)
    {
        _logger = logger;
        db = context;
    }



// ==============(DASHBOARD)===================
    [HttpGet("dashboard")]
    public IActionResult Index()
    {
    List<Vacation> vacations = db.Vacations.Include(v => v.Creator).ToList();        
        //passing vacations down to the view...
        return View("All", vacations);
    }


// ==============(NEW - view)==================

    [HttpGet("vacation/new")]
    public IActionResult New()
    {
        //returns itself if left blank
        return View();
    }

// ========(handle NEW PostMethod - view)=========

    [HttpPost("vacation/create")]
    //bringing in the model
    public IActionResult Create(Vacation newVacay)
    {
        if(!ModelState.IsValid)
        {
            //trigger to see validations
            return View("New");
        }

        newVacay.UserId = (int) HttpContext.Session.GetInt32("UUID");

        //vacations from context
        db.Vacations.Add(newVacay);
        db.SaveChanges();
        //When success, send to index aka home
        return RedirectToAction("Index");
    }







// ===================================


    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}



//!SESSION CHECK ===========================================
// Name this anything you want with the word "Attribute" at the end -- adding filter for session at top*
public class SessionCheckAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        // Find the session, but remember it may be null so we need int?
        int? userId = context.HttpContext.Session.GetInt32("UUID");
        // Check to see if we got back null
        if(userId == null)
        {
            // Redirect to the Index page if there was nothing in session
            // "Home" here is referring to "HomeController", you can use any controller that is appropriate here
            context.Result = new RedirectToActionResult("Index", "User", null);
        }
    }
}