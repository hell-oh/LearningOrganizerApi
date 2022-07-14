using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace LearningOrganizerApi.Controllers
{
    public class HelloWorldController : Controller
    {
        // 
        // GET: /HelloWorld/

        public IActionResult Index()
        {
            return View();
        }

        // 
        // GET: /HelloWorld/Welcome/ 

        public string Welcome()
        {
            return "This is the Welcome action method...";
        }

    //HelloWorld/Welcome?name=Rick&numtimes=4
//         // GET: /HelloWorld/Welcome/ 
// // Requires using System.Text.Encodings.Web;
// public string Welcome(string name, int numTimes = 1)
// {
//     return HtmlEncoder.Default.Encode($"Hello {name}, NumTimes is: {numTimes}");
// }
    }
}