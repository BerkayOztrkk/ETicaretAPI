using ETicaretAPI.Application.Abstractions.Storage;
using ETicaretAPI.Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Core;


namespace ETicaretAPI.Application.Features.Commands.ProductImageFile.UploadProductImage
{
    public class UploadProductImageCommandHandler : IRequestHandler<UploadProductImageCommandRequest, UploadProductImageCommandResponse>
    {
        IStorageService _storageService;
        IProductReadRepository _productReadRepository;
        IProductImageWriteRepository _productImageWriteRepository;

        public UploadProductImageCommandHandler(IProductImageWriteRepository productImageWriteRepository, IStorageService storageService, IProductReadRepository productReadRepository)
        {

            productImageWriteRepository=_productImageWriteRepository;
            _storageService=storageService;
            _productReadRepository=productReadRepository;
        }

        public async Task<UploadProductImageCommandResponse> Handle(UploadProductImageCommandRequest request, CancellationToken cancellationToken)
        {
            List<(string fileName, string pathOrContainerName)> result = await _storageService.UploadAsync("photo-images", request.Files);
            Domain.Entities.Product product = await _productReadRepository.GetByIdAsync(request.Id);

            await _productImageWriteRepository.AddRangeAsync(result.Select(r => new Domain.Entities.ProductImageFile
            {
                FileName=r.fileName,
                Path=r.pathOrContainerName,
                Storage=_storageService.StorageName,

            }).ToList());
            await _productImageWriteRepository.SaveAsync();
            return new();
        }
    }
}
