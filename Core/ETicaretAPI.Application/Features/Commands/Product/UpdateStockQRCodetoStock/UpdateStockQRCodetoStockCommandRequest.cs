using MediatR;

namespace ETicaretAPI.Application.Features.Commands.Product.UpdateStockQRCodetoStock
{
    public class UpdateStockQRCodetoStockCommandRequest:IRequest<UpdateStockQRCodetoStockCommandResponse>
    {
        public string ProductId { get; set; }
        public int Stock { get; set; }
    }
}