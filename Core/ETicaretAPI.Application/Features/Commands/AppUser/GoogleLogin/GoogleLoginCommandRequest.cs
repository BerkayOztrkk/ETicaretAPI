using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Commands.AppUser.GoogleLogin
{
    public class GoogleLoginCommandRequest:IRequest<GoogleLoginCommandResponse>
    {
        public string email { get; set; }
        public string firstname { get; set; }
        public string Id { get; set; }
        public string Idtoken { get; set; }
        public string lastname { get; set; }
        public string Name { get; set; }
        public string photourl { get; set; }
        public string provider { get; set; }
    }
}
