using MicroBill.Data;
using MicroBill.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace MicroBill.Controllers
{
    public class LoginController : Controller
    {
        private readonly AppDbContext _context;


        public LoginController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            string hashedInputPassword = getHash(password);

            var user = _context.Users.FirstOrDefault(u => u.Username == email && u.Password == hashedInputPassword);
            if (user != null)
            {
                HttpContext.Session.SetString("UserId", user.Id.ToString());
                HttpContext.Session.SetString("Username", user.Username);
                return Json(true);
            }
            return Json(false);
        }

        [HttpPost]
        public IActionResult Register(string username, string email, string password)
        {
            //var existingUser = _context.Users.FirstOrDefault(u => u.Email == email || u.Username == username );
            var emailExists = _context.Users.Any(u => u.Email == email);
            var usernameExists = _context.Users.Any(u => u.Username == username);

            if (emailExists && usernameExists)
            {
                return Json(new { success = false, value = "Email and Username already exist" });
            }
            else if (emailExists)
            {
                return Json(new { success = false, value = "Email already exists" });
            }
            else if (usernameExists)
            {
                return Json(new { success = false, value = "Username already exists" });
            }

            //if (existingUser != null)
            //{
            //    var message = "Email already exists";
            //    return Json( new { success= false, value = message });
            //}
            password = getHash(password);

            var user = new Users { Username = username, Email = email, Password = password, CreatedAt = DateTime.Now };
            _context.Users.Add(user);
            _context.SaveChanges();

            return Json(new { success = true});
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        public IActionResult Login() => View();
        public IActionResult Register() => View();


        public IActionResult Index() => View();

        private string getHash(string text)
        {
            // SHA512 is disposable by inheritance.  
            using (var sha256 = SHA256.Create())
            {
                // Send a sample text to hash.  
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(text));

                // Get the hashed string.  
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }
    }
}
