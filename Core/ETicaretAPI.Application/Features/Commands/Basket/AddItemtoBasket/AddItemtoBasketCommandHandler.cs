using ETicaretAPI.Application.Abstractions.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Commands.Basket.AddItemtoBasket
{
    public class AddItemtoBasketCommandHandler : IRequestHandler<AddItemtoBasketCommandRequest, AddItemtoBasketCommandResponse>
    {
        readonly IBasketService _basketService;

        public AddItemtoBasketCommandHandler(IBasketService basketService)
        {
            _basketService=basketService;
        }

        public async Task<AddItemtoBasketCommandResponse> Handle(AddItemtoBasketCommandRequest request, CancellationToken cancellationToken)
        {
            await _basketService.AddItemtoBasketAsync(new()
            {
                ProductId=request.ProductId,
                    Quantity=request.Quantity,
            });
            return new();
        }
    }
}
