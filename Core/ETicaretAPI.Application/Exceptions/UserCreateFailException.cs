using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Exceptions
{
    public class UserCreateFailException : Exception
    {
        public UserCreateFailException():base("Kullanıcı oluşturulurken beklenmeyen bir hata ile karşılaşıldı!")
        {
        }

        public UserCreateFailException(string? message) : base(message)
        {
        }

        public UserCreateFailException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
