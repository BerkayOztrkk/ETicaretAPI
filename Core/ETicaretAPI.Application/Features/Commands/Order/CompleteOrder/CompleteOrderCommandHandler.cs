using ETicaretAPI.Application.Abstractions.Services;
using ETicaretAPI.Application.DTOs.Order;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Commands.Order.CompleteOrder
{
    public class CompleteOrderCommandHandler : IRequestHandler<CompleteOrderCommandRequest, CompleteOrderCommandResponse>
    {
        readonly IOrderService _orderService;
        readonly IMailService _mailService;

        public CompleteOrderCommandHandler(IMailService mailService)
        {
            _mailService=mailService;
        }

        public CompleteOrderCommandHandler(IOrderService orderService)
        {
            _orderService=orderService;
        }

        public async Task<CompleteOrderCommandResponse> Handle(CompleteOrderCommandRequest request, CancellationToken cancellationToken)
        {
           (bool succeeded,CompletedOrder dto) result= await _orderService.CompleteOrderAsync(request.Id);
            if (result.succeeded)
               await _mailService.SendCompletedOrderMailAsync(result.dto.Email,result.dto.OrderCode,result.dto.OrderDate,result.dto.Username);
                return new();
            
           
        }
    }
}
