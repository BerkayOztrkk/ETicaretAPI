﻿using ETicaretAPI.Application.Abstractions;
using ETicaretAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence.Concretes
{
    public class ProductService : IProductService
    {
        public List<Product> GetProducts()

           => new()
           {
               new(){Id=Guid.NewGuid(),Name="Product 1",Price=100,Stock=10},
               new(){Id=Guid.NewGuid(),Name="Product 2",Price=120,Stock=50},
               new(){Id=Guid.NewGuid(),Name="Product 3",Price=140,Stock=40},
               new(){Id=Guid.NewGuid(),Name="Product 4",Price=150,Stock=30},
           };
        
    }
}
