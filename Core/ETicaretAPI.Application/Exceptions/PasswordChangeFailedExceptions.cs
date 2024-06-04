using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Exceptions
{
    public class PasswordChangeFailedExceptions : Exception
    {
        public PasswordChangeFailedExceptions():base("Şifre güncellenirken beklenmeyen bir hata oluştu")
        {
        }

        public PasswordChangeFailedExceptions(string? message) : base(message)
        {
        }

        public PasswordChangeFailedExceptions(string? message, Exception? innerException) : base(message, innerException)
        {
        }

       
    }
}
