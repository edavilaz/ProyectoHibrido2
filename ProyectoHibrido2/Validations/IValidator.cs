using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoHibrido2.Validations
{
    public interface IValidator
    {
        string NombreError { get; set; }
        string EmailError { get; set; }
        string PasswordError { get; set; }

        Task<bool> Validar(string nombre, string email, string password);

    }
}
