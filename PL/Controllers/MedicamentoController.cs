using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;



namespace PL.Controllers
{
    public class MedicamentoController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment;

        public MedicamentoController(IConfiguration configuration, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            this.hostingEnvironment = hostingEnvironment;
        }

        public ActionResult GetAll()
        {
            ML.Medicamento medicamento = new ML.Medicamento();
            
            ML.Result result = BL.Medicamento.GetAll();
            
            try
            {
                using (var client = new HttpClient())
                {
                    string urlApi = _configuration["urlApi"];
                    client.BaseAddress = new Uri(urlApi);

                    var responseTask = client.GetAsync("Medicamento/GetAll");
                    responseTask.Wait();

                    var resultServicio = responseTask.Result;

                    if (resultServicio.IsSuccessStatusCode)
                    {
                        var readTask = resultServicio.Content.ReadFromJsonAsync<ML.Result>();
                        readTask.Wait();

                        foreach (var resultItem in readTask.Result.Objects)
                        {
                            ML.Medicamento resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Medicamento>(resultItem.ToString());
                            result.Objects.Add(resultItemList);
                        }
                    }
                    medicamento.Medicamentos = result.Objects;
                }

            }
            catch (Exception ex)
            {
            }
            return View(medicamento);
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
                using (var client = new HttpClient())
                {
                    string urlApi = _configuration["urlApi"];
                    client.BaseAddress = new Uri(urlApi);

                    var responseTask = client.PostAsJsonAsync<int>("Medicamento/GetById/{idMedicamento}", idMedicamento.Value);
                    responseTask.Wait();

                    var result = responseTask.Result;

                    if (result.IsSuccessStatusCode)
                    {
                        ML.Result result1 = new ML.Result();
                        var registro = result.Content.ReadFromJsonAsync<ML.Result>();
                        ML.Medicamento resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Medicamento>(registro.Result.Object.ToString());
                        result1.Object = resultItemList;
                        medicamento = (ML.Medicamento)result1.Object;

                        return PartialView(medicamento);
                    }
                    else
                    {
                        ViewBag.Mensaje = "No se ha Podido consultar la Informacion del Usuario";
                        return PartialView("Modal");
                    }
                }
            }

        }

        [HttpPost]
        public ActionResult Form(ML.Medicamento medicamento)
        {
            IFormFile file = Request.Form.Files["fuImage"];
            if(file != null)
            {
                byte[] imagen = ConverToBytes(file);

                medicamento.Imagen = Convert.ToBase64String(imagen); 
            }
            
            if (medicamento.IdMedicamento == 0)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_configuration["urlApi"]);
                    var postTask = client.PostAsJsonAsync<ML.Medicamento>("Medicamento/Add", medicamento);
                    postTask.Wait();

                    var result = postTask.Result;

                    if (result.IsSuccessStatusCode)
                    {
                        ViewBag.Message = "Se inserto el registro";
                        return View("Modal");
                    }
                    else
                    {
                        ViewBag.Message = "Ocurrio un error al insertar el registro";
                        return View("Modal");
                    }
                }

            }
            else
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_configuration["urlApi"]);
                    var postTask = client.PostAsJsonAsync<ML.Medicamento>("Medicamento/Update", medicamento);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        ViewBag.Message = "Se actualizo la infromacion del Usuario";
                        return View("Modal");
                    }
                    else
                    {
                        ViewBag.Message = "Error al actualizar la informacio";
                        return View("Modal");
                    }
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
