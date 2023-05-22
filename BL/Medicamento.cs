using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace BL
{
    public class Medicamento
    {
        public static ML.Result Add(ML.Medicamento medicamento)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.UestebanMarzamContext context = new DL.UestebanMarzamContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"MedicamentoAdd '{medicamento.Nombre}','{medicamento.Precio}','{medicamento.Imagen}'");

                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;   
            }
            return result;
        }

        public static ML.Result Update(ML.Medicamento medicamento)
        {
            ML.Result result = new ML.Result();

            try
            {
                using(DL.UestebanMarzamContext context = new DL.UestebanMarzamContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"MedicamentoUpdate '{medicamento.IdMedicamento}','{medicamento.Nombre}','{medicamento.Precio}','{medicamento.Imagen}'");

                    if(query>= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result Delete(int idMedicamento)
        {
            ML.Result result = new ML.Result();

            try
            {
                using(DL.UestebanMarzamContext context = new DL.UestebanMarzamContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"MedicamentoDelte '{idMedicamento}'");

                    if(query > 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch(Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using(DL.UestebanMarzamContext context = new DL.UestebanMarzamContext())
                {
                    var query = context.Medicamentos.FromSqlRaw("MedicamentoGetAll").ToList();

                    result.Objects = new List<object>();

                    if( query != null)
                    {
                        foreach(var item in query)
                        {
                            ML.Medicamento medicamento = new ML.Medicamento();

                            medicamento.IdMedicamento = item.IdMedicamento;
                            medicamento.Nombre = item.Nombre;
                            medicamento.Precio = item.Precio.Value;
                            medicamento.Imagen = item.Imagen;

                            result.Objects.Add(medicamento);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch(Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result GetById(int idMedicamento)
        {
            ML.Result result = new ML.Result();

            try
            {
                using(DL.UestebanMarzamContext context = new DL.UestebanMarzamContext())
                {
                    var query = context.Medicamentos.FromSqlRaw($"MedicamentoGetById '{idMedicamento}'").AsEnumerable().FirstOrDefault();

                    if(query != null)
                    {
                        ML.Medicamento medicamento = new ML.Medicamento();

                        medicamento.IdMedicamento = query.IdMedicamento;
                        medicamento.Nombre = query.Nombre;
                        medicamento.Precio = query.Precio.Value;
                        medicamento.Imagen = query.Imagen;

                        result.Object = medicamento;
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch(Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

    }
}