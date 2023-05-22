using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Usuario
    {
        public static ML.Result Add(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using(DL.UestebanMarzamContext context = new DL.UestebanMarzamContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"UsuarioAdd '{usuario.Nombre}'," +
                                                                          $"'{usuario.ApellidoPaterno}'," +
                                                                          $"'{usuario.ApellidoMaterno}'," +
                                                                          $"'{usuario.UserName}'," +
                                                                          $"'{usuario.Contraseña}'");

                    if(query >= 1)
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

        public static ML.Result GetById(string userName)
        {
            ML.Result result = new ML.Result();

            try
            {
                using(DL.UestebanMarzamContext context = new DL.UestebanMarzamContext())
                {
                    var query = context.Usuarios.FromSqlRaw($"UsuarioGetByIdUserName '{userName}'").AsEnumerable().FirstOrDefault();

                    if(query != null)
                    {
                        ML.Usuario usuario = new ML.Usuario();

                        usuario.IdUsuario = query.IdUsuario;
                        usuario.Nombre = query.Nombre;
                        usuario.ApellidoPaterno = query.ApellidoPaterno;
                        usuario.ApellidoMaterno = query.ApellidoMaterno;
                        usuario.UserName = query.UserName;
                        usuario.Contraseña = query.Contraseña;

                        result.Object = usuario;
                        result.Correct = true;
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
