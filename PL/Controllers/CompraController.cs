using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class CompraController : Controller
    {
        public ActionResult Medicamentos()
        {
            ML.Medicamento medicamento = new ML.Medicamento();

            ML.Result result = BL.Medicamento.GetAll();

            if (result.Correct)
            {
                medicamento.Medicamentos = result.Objects;
                return View(medicamento);
            }
            else
            {
                return View(medicamento);
            }
        }
    }
}
