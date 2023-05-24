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

        //[HttpPost]
        public ActionResult Carrito(int idMedicamento)
        {
            ML.Medicamento medicamento = new ML.Medicamento();
            medicamento.Medicamentos = new List<object>();
            decimal suma = 0;

            if (HttpContext.Session.GetString("Productos") == null)
            {
                if (medicamento.IdMedicamento == 0)
                {
                    return View(medicamento);
                }
                else
                {
                    ML.Result result = BL.Medicamento.GetById(idMedicamento);

                    ML.Medicamento medicamento1 = (ML.Medicamento)result.Object;
                    medicamento1.Cantidad = 1;
                    medicamento1.SubTotal = medicamento.Precio * medicamento.Cantidad;
                    medicamento1.Total = medicamento.SubTotal;
                    medicamento1.TotalVendido = medicamento.Cantidad;
                    medicamento1.Medicamentos.Add(medicamento);

                    HttpContext.Session.SetString("Productos", Newtonsoft.Json.JsonConvert.SerializeObject(medicamento1.Medicamentos));

                    return View(medicamento);
                }

            }
            else
            {
                int prod = 0;
                ML.Result result = BL.Medicamento.GetById(idMedicamento);
                ML.Medicamento producto = (ML.Medicamento)result.Object;
                producto.Cantidad = 1;
                producto.SubTotal = producto.Precio * producto.Cantidad;

                decimal sum = producto.SubTotal;

                bool existe = false;
                bool uno = false;

                List<object> medc = Newtonsoft.Json.JsonConvert.DeserializeObject<List<object>>(HttpContext.Session.GetString("Productos"));

                foreach (var obj in medc)
                {
                    ML.Medicamento objProducto = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Medicamento>(obj.ToString());
                    if (producto.IdMedicamento == objProducto.IdMedicamento)
                    {
                        objProducto.Cantidad++;
                        objProducto.SubTotal = objProducto.Precio * objProducto.Cantidad;
                        suma += objProducto.SubTotal;
                        existe = true;
                    }
                    if (uno == false)
                    {
                        if (existe == true)
                        {
                            medicamento.Medicamentos.Add(objProducto);
                            break;
                        }
                        if (existe == false)
                        {
                            medicamento.Medicamentos.Add(producto);
                            uno = true;
                        }
                    }

                    medicamento.Medicamentos.Add(objProducto);
                    prod += objProducto.Cantidad;
                }
                suma = suma + sum;
                medicamento.Total = suma;
                medicamento.TotalVendido = prod + producto.Cantidad;

                HttpContext.Session.SetString("Productos", Newtonsoft.Json.JsonConvert.SerializeObject(medicamento.Medicamentos));
                HttpContext.Session.SetInt32("TotalProductos", medicamento.TotalVendido);
                return View(medicamento);
            }
        }
    }
}
