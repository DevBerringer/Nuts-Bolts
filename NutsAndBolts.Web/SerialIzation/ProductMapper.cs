using NutsAndBolts.web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NutsAndBolts.web.SerialIzation
{
    public static class ProductMapper
    {
        /// <summary>
        ///     Maps a Product data model to a Product model view Model
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public static ProductModel SerializeProductModel(Data.Models.Product product)
        {
            return new ProductModel
            {
                Id = product.Id,
                CreateOn = product.CreateOn,
                UpdateOn = product.UpdateOn,
                Price = product.Price,
                Name = product.Name,
                Description = product.Description,
                IsArchived = product.IsArchived,
                IsTaxable = product.IsTaxable
            };
        }
        /// <summary>
        ///     Maps a Product view model to a Product data Model
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public static Data.Models.Product SerializeProductModel(ProductModel product)
        {
            return new Data.Models.Product
            {
                Id = product.Id,
                CreateOn = product.CreateOn,
                UpdateOn = product.UpdateOn,
                Price = product.Price,
                Name = product.Name,
                Description = product.Description,
                IsArchived = product.IsArchived,
                IsTaxable = product.IsTaxable
            };
        }
    }
}
