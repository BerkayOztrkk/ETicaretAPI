using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence.Repositories
{
    public class ProductImageWriteRepository : WriteRepository<Domain.Entities.ProductImageFile>, IProductImageWriteRepository
    {
        public ProductImageWriteRepository(ETicaretAPIDbContext context) : base(context)
        {
        }
    }
}
