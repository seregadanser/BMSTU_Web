using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBack.Tests.Additional
{
    public class ProductBuilder
    {
        private readonly Product _product;

        public ProductBuilder()
        {
            _product = new Product();
        }

        public ProductBuilder WithId(int id)
        {
            _product.Id = id;
            return this;
        }

        public ProductBuilder WithName(string? name)
        {
            _product.Name = name;
            return this;
        }

        public ProductBuilder WithValue(int? value)
        {
            _product.Value = value;
            return this;
        }

        public ProductBuilder WithDateCome(DateTime? dateCome)
        {
            _product.DateCome = dateCome;
            return this;
        }

        public ProductBuilder WithDateProduction(DateTime? dateProduction)
        {
            _product.DateProduction = dateProduction;
            return this;
        }

        public Product Build()
        {
            return _product;
        }
    }

    public static class ProductMother
    {
        public static Product CreateValidProduct()
        {
            return new ProductBuilder()
                .WithId(1)
                .WithName("Product A")
                .WithValue(100)
                .WithDateCome(new DateTime(2000, 1, 1))
                .WithDateProduction(new DateTime(1990, 1, 1))
                .Build();
        }
    }

}
