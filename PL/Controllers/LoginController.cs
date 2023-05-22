using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string contraseña)
        {
            ML.Result result = BL.Usuario.GetById(username);
            
            if (result.Correct)
            {
                ML.Usuario usuario = (ML.Usuario)result.Object;

                if (contraseña == usuario.Contraseña)
                {
                    return RedirectToAction("index", "home");

                }
                else
                {
                    ViewBag.Message = "la contraseña no coincide";
                    return PartialView ("modal");
                }
            }
            else
            {
                ViewBag.message = "el usuario no existe";
                return PartialView ("modal");
            }
        }

        [HttpPost]
        public ActionResult Form(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();


            if (usuario.IdUsuario == 0)
            {
                result = BL.Usuario.Add(usuario);
                if (result.Correct)
                {
                    ViewBag.Message = "Se agrego el registro";
                }
                else
                {
                    ViewBag.Message = "Ocurrio un error al agregar el registro";
                }
            }
            return View("Modal");
        }
        
    }
}
