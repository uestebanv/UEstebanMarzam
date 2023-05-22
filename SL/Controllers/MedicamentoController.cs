using Microsoft.AspNetCore.Mvc;

namespace SL.Controllers
{
    public class MedicamentoController : Controller
    {
        [HttpPost]
        [Route("api/Medicamento/Add")]
        public ActionResult Add([FromBody] ML.Medicamento medicamento)
        {
            ML.Result result = BL.Medicamento.Add(medicamento);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpPost]
        [Route("api/Medicamento/Update")]
        public ActionResult Update([FromBody] ML.Medicamento medicamento)
        {
            ML.Result result = BL.Medicamento.Update(medicamento);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpPost]
        [Route("api/Medicamento/Delete/{idMedicamento}")]
        public ActionResult Delete([FromBody] int idMedicamento)
        {
            ML.Result result = BL.Medicamento.Delete(idMedicamento);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpGet]
        [Route("api/Medicamento/GetAll")]
        public ActionResult GetAll()
        {
            ML.Result result = BL.Medicamento.GetAll();

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }

        [HttpPost]
        [Route("api/Medicamento/GetById/{idMedicamento}")]
        public ActionResult GetById([FromBody] int idMedicamento)
        {
            ML.Result result = BL.Medicamento.GetById(idMedicamento);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }
    }
}
