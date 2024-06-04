using ETicaretAPI.Application.Abstractions.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Commands.Product.UpdateStockQRCodetoStock
{
    public class UpdateStockQRCodetoStockCommandHandler : IRequestHandler<UpdateStockQRCodetoStockCommandRequest, UpdateStockQRCodetoStockCommandResponse>
    {
        readonly IProductService _productService;

        public UpdateStockQRCodetoStockCommandHandler(IProductService productService)
        {
            _productService=productService;
        }

        public async Task<UpdateStockQRCodetoStockCommandResponse> Handle(UpdateStockQRCodetoStockCommandRequest request, CancellationToken cancellationToken)
        {
            await _productService.StockUpdateToProductAsync(request.ProductId, request.Stock);
            return new();
        }
    }
}
