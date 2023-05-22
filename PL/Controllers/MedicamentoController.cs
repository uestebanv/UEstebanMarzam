using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace PL.Controllers
{
    public class MedicamentoController : Controller
    {
        public ActionResult GetAll()
        {
            ML.Medicamento medicamento = new ML.Medicamento();
            
            ML.Result result = BL.Medicamento.GetAll();
            
            if(result.Correct)
            {
                medicamento.Medicamentos = result.Objects;
                return View(medicamento);
            }
            else
            {
                return View(medicamento);
            }
        }

        [HttpGet]
        public ActionResult Form(int? idMedicamento)
        {
            
            ML.Medicamento medicamento = new ML.Medicamento();

            if (idMedicamento == null)
            {
                //ML.Result result = BL.Medicamento.Add(medicamento);
                return View(medicamento);
                
            }
            else
            {
                ML.Result result = BL.Medicamento.GetById(idMedicamento.Value);
                if (result.Correct)
                {
                    medicamento = (ML.Medicamento)result.Object;
                    return View(medicamento);
                }
                else
                {
                    ViewBag.Message = "Error al consultar la infromacion";
                    return View("Modal");
                }
            }
        }

        [HttpPost]
        public ActionResult Form(ML.Medicamento medicamento)
        {
            ML.Result result = new ML.Result();

            IFormFile file = Request.Form.Files["fuImage"];
            if(file != null)
            {
                byte[] imagen = ConverToBytes(file);

                medicamento.Imagen = Convert.ToBase64String(imagen); 
            }
            
            if (medicamento.IdMedicamento == 0)
            {
                result = BL.Medicamento.Add(medicamento);
                if(result.Correct)
                {
                    ViewBag.Message = "Se agrego el registro";
                }
                else
                {
                    ViewBag.Message = "Ocurrio un error al agregar el registro";
                }
                return View("Modal");

            }
            else
            {
                result = BL.Medicamento.Update(medicamento);
                if(result.Correct)
                {
                    ViewBag.Message = "Se actualizo el registro";
                    return View("Modal");
                }
                else
                {
                    ViewBag.Message = "Error en la Actualizacion de datos";
                    return View("Modal");
                }
            }
        }

        public byte[] ConverToBytes(IFormFile imagen)
        {
            using var fileStream = imagen.OpenReadStream();

            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, (int)fileStream.Length);
            return bytes;
        }
    }
}
