using Clebra.Loopus.Core;
using Clebra.Loopus.Core.Service;
using Clebra.Loopus.Model;
using System;
using System.Collections.Generic;

namespace Clebra.Loopus.Service
{
    public interface IProductDataService : IDataService<Product>
    {
        List<Product> GetAllByCategoryId(string categoryId);
    }
}