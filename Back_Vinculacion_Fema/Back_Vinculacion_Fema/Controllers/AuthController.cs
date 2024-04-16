﻿using System.Security.Cryptography;
using System.Text;
using Back_Vinculacion_Fema.CRUD;
using Back_Vinculacion_Fema.Models.DbModels;
using Back_Vinculacion_Fema.Models.RequestModels;
using Back_Vinculacion_Fema.Models.Utilidades;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace Back_Vinculacion_Fema.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly vinculacionfemaContext _contexto; //Comentario 

        public AuthController(vinculacionfemaContext contexto)
        {
            _contexto = contexto;
        }

        [HttpPost("login")]
        public IActionResult Authenticate(UserLoginRequest credentials)
        {
            var encryptedPassword = EncryptPassword(credentials.Password); ; //Debe consumir el metodo de cifrado

            User usuarioLogic = new User(_contexto);
            var usuario = usuarioLogic.GetUsuarioLogin(credentials.Nombre, encryptedPassword);

            if (usuario == null)
            {
                return Unauthorized();
            }

            return Ok(Token.GenerarToken(usuario.UserName));
        }

        private string EncryptPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }


    }
}
