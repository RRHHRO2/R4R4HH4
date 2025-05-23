using Proyecto.Context;
using Proyecto.Models;
using Proyecto.Security;
using Proyecto.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();
        private readonly IPasswordEncripter _passordEncripter = new PasswordEncripter();

        public AuthResults Auth(string user, string password, out Usuario usuario)
        {
            usuario = db.Usuarios.FirstOrDefault(x => x.Correo == user);

            if (usuario == null)
                return AuthResults.NotExists;

            password = _passordEncripter.Encript(password, new List<byte[]>()
                 .AddHash(usuario.HashKey)
                 .AddHash(usuario.HashIV));

            if (password != usuario.Contrasena)
                return AuthResults.PasswordNotMatch;

            return AuthResults.Success;
        }
    }
}