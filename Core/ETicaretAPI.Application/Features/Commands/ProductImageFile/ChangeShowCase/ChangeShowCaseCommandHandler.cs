using ETicaretAPI.Application.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Commands.ProductImageFile.ChangeShowCase
{
    public class ChangeShowCaseCommandHandler : IRequestHandler<ChangeShowCaseCommandRequest, ChangeShowCaseCommandResponse>
    {
        private IProductImageWriteRepository _productImageWriteRepository;

        public ChangeShowCaseCommandHandler(IProductImageWriteRepository productImageWriteRepository)
        {
            _productImageWriteRepository=productImageWriteRepository;
        }

        public async Task<ChangeShowCaseCommandResponse> Handle(ChangeShowCaseCommandRequest request, CancellationToken cancellationToken)
        {
            var query = _productImageWriteRepository.Table
                  .Include(p => p.Products)
                  .SelectMany(p => p.Products, (pif, p) => new
                  {
                      pif,
                      p
                  });
            var data = await query.FirstOrDefaultAsync(p => p.p.Id==Guid.Parse(request.ProductId)&&p.pif.ShowCase);
            if (data!=null) 
            data.pif.ShowCase=false;

            var image = await query.FirstOrDefaultAsync(p => p.pif.Id==Guid.Parse(request.ImageId));
            if (image!=null)
            image.pif.ShowCase=true;

            await _productImageWriteRepository.SaveAsync();
            return new();
        }
    }

       
    }

