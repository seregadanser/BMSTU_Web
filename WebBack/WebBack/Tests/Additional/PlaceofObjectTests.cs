using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBack.Tests.Additional
{
    public class PlaceofObjectBuilder
    {
        private readonly PlaceofObject _placeofObject;

        public PlaceofObjectBuilder()
        {
            _placeofObject = new PlaceofObject();
        }

        public PlaceofObjectBuilder WithId(int id)
        {
            _placeofObject.Id = id;
            return this;
        }

        public PlaceofObjectBuilder WithPlaceId(int? placeId)
        {
            _placeofObject.PlaceId = placeId;
            return this;
        }

        public PlaceofObjectBuilder WithInventoryId(int? inventoryId)
        {
            _placeofObject.InventoryId = inventoryId;
            return this;
        }

        public PlaceofObject Build()
        {
            return _placeofObject;
        }
    }

    public static class PlaceofObjectMother
    {
        public static PlaceofObject CreateValidPlaceofObject()
        {
            return new PlaceofObjectBuilder()
                .WithId(1)
                .WithPlaceId(123)
                .WithInventoryId(456)
                .Build();
        }
    }

}
