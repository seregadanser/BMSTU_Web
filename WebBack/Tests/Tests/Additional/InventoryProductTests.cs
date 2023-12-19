using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBack.Tests.Additional
{
    public class InventoryProductBuilder
    {
        private readonly InventoryProduct _inventoryProduct;

        public InventoryProductBuilder()
        {
            _inventoryProduct = new InventoryProduct();
        }

        public InventoryProductBuilder WithInventoryNumber(int inventoryNumber)
        {
            _inventoryProduct.InventoryNumber = inventoryNumber;
            return this;
        }

        public InventoryProductBuilder WithProductId(int? productId)
        {
            _inventoryProduct.ProductId = productId;
            return this;
        }

        public InventoryProduct Build()
        {
            return _inventoryProduct;
        }
    }

    public static class InventoryProductMother
    {
        public static InventoryProduct CreateValidInventoryProduct()
        {
            return new InventoryProductBuilder()
                .WithInventoryNumber(12345)
                .WithProductId(1)
                .Build();
        }
    }

}
