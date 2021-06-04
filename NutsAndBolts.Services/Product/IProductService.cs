﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutsAndBolts.Services.Product
{
    public interface IProductService
    {
        List<Data.Models.Product> GetAllProducts();
        Data.Models.Product GetProductById(int id);
        ServiceResponse<Data.Models.Product> CreateProduct(Data.Models.Product product);
        ServiceResponse<Data.Models.Product> ArchiveProduct(int id);
    }
}
