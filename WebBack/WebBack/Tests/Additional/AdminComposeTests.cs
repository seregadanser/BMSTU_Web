using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBack.Tests.Additional
{
    public class AdminComposeBuilder
    {
        private readonly AdminCompose _adminCompose;

        public AdminComposeBuilder()
        {
            _adminCompose = new AdminCompose();
        }

        public AdminComposeBuilder WithProductId(int? productId)
        {
            _adminCompose.ProductId = productId;
            return this;
        }

        public AdminComposeBuilder WithName(string? name)
        {
            _adminCompose.Name = name;
            return this;
        }

        public AdminComposeBuilder WithDateCome(DateTime? dateCome)
        {
            _adminCompose.DateCome = dateCome;
            return this;
        }

        public AdminComposeBuilder WithDateProduction(DateTime? dateProduction)
        {
            _adminCompose.DateProduction = dateProduction;
            return this;
        }

        public AdminComposeBuilder WithInventoryNumber(int? inventoryNumber)
        {
            _adminCompose.InventoryNumber = inventoryNumber;
            return this;
        }

        public AdminComposeBuilder WithPlaceId(string? placeId)
        {
            _adminCompose.PlaceId = placeId;
            return this;
        }

        public AdminComposeBuilder WithValue(int? value)
        {
            _adminCompose.value = value;
            return this;
        }

        public AdminComposeBuilder WithPlaceOfObjectlId(string? placeOfObjectlId)
        {
            _adminCompose.PlaceOfObjectlId = placeOfObjectlId;
            return this;
        }

        public AdminCompose Build()
        {
            return _adminCompose;
        }
    }

    public static class AdminComposeMother
    {
        public static AdminCompose CreateValidAdminCompose()
        {
            return new AdminComposeBuilder()
                .WithProductId(1)
                .WithName("Product A")
                .WithDateCome(DateTime.Now)
                .WithDateProduction(DateTime.Now.AddMonths(-1))
                .WithInventoryNumber(12345)
                .WithPlaceId("Place123")
                .WithValue(100)
                .WithPlaceOfObjectlId("ObjectPlace123")
                .Build();
        }
    }

}
