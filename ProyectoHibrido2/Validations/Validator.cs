using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProyectoHibrido2.Validations
{
    public class Validator : IValidator
    {
        public string NombreError { get; set; } = "";
        public string EmailError { get; set; } = "";
        public string PasswordError { get; set; } = "";

        private const string NombreVacioErrorMsg = "Por favor, informe su nombre";
        private const string NombreInvalidoErrorMsg = "Por favor, informe un nombre válido";
        private const string EmailVacioErrorMsg = "Por favor, informe su email";
        private const string EmailInvalidoErrorMsg = "Por favor, informe un email válido";
        private const string PasswordVacioErrorMsg = "Por favor, informe un password";
        private const string PasswordInvalidoErrorMsg = "La contraseña debe tener por lo menos 8 caracteres incluídos mayúsculas, minúsculas y un símbolo";

        public Task<bool> Validar(string nombre, string email, string password)
        {
            var IsNombreValido = ValidarNombre(nombre);
            var IsEmailValido = ValidarEmail(email);
            var IsPasswordValido = ValidarPassword(password);

            return Task.FromResult(IsNombreValido && IsEmailValido && IsPasswordValido);
        }

        private bool ValidarNombre(string nombre)
        {
            if (string.IsNullOrEmpty(nombre))
            {
                NombreError = NombreVacioErrorMsg;
                return false;
            }

            if (nombre.Length < 3)
            {
                NombreError = NombreInvalidoErrorMsg;
                return false;
            }

            NombreError = "";
            return true;
        }

        private bool ValidarEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                EmailError = EmailVacioErrorMsg;
                return false;
            }
            if (!Regex.IsMatch(email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
            {
                EmailError = EmailInvalidoErrorMsg;
                return false;
            }
            EmailError = "";
            return true;
        }

        private bool ValidarPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                PasswordError = PasswordVacioErrorMsg;
                return false;
            }
            if (password.Length < 8 || !Regex.IsMatch(password, @"[a-zA-Z]") || !Regex.IsMatch(password, @"\d"))
            {
                PasswordError = PasswordInvalidoErrorMsg;
                return false;
            }
            PasswordError = "";
            return true;
        }
    }
}
