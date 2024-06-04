using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Commands.ProductImageFile.ChangeShowCase
{
    public class ChangeShowCaseCommandRequest:IRequest<ChangeShowCaseCommandResponse>
    {
        public string ImageId { get; set; }
        public string ProductId { get; set; }
    }
}
