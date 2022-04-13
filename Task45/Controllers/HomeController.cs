using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Task45.Models;
using Task45.ViewModels;

namespace Task45.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private readonly UserManager<User> _userManager;

    private readonly SignInManager<User> _signInManager;

    private ApplicationContext _db;

    public HomeController(UserManager<User> userManager, SignInManager<User> signInManager, ILogger<HomeController> logger, ApplicationContext db)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _logger = logger;
        _db = db;
    }

    [Authorize]
    public IActionResult Index()
    {
        User? user = _userManager.Users.FirstOrDefault(u => u.Email == User.Identity.Name);
        if (user == null || user.Status != "ok")
            return RedirectToAction("Logout", "Account");
        
        return View();
    }

    [Authorize]
    [HttpGet]
    public IActionResult Message(string recipient)
    {
        MessageViewModel messageViewModel = new MessageViewModel { Recipient = recipient };
        return View(messageViewModel);
    }

    [Authorize]
    [HttpPost]
    public IActionResult SendMessage(MessageViewModel message)
    {

        if (ModelState.IsValid)
        {

            if (message.Recipient == User.Identity.Name)
                return View("Message", message);

            Message newMessage = new Message() { Recipient = message.Recipient, Theme = message.Theme, Text = message.Text, Sender = User.Identity.Name, Date = DateTime.Now };
            _db.Add(newMessage);
            _db.SaveChangesAsync();
        }
        return RedirectToAction("Index", "Home");

    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [Authorize]
    public List<Message> GetUserMessages()
    {
        List<Message> messages = _db.Messages.Where(m => m.Recipient == User.Identity.Name).ToList();
        messages.Reverse();
        return messages;
    }

    [Authorize]
    public int GetUserMessagesCount()
    {
        return _db.Messages.Count(m => m.Recipient == User.Identity.Name);
    }

}

