using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Exceptions
{

    public class NotFoundUserException : Exception
    {
        public NotFoundUserException(): base("Kullanıcı adı veya şifre hatalı")
        {
        }



        public NotFoundUserException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected NotFoundUserException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
