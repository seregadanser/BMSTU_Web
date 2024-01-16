using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBack.Tests.Additional
{
    public class PlaceBuilder
    {
        private readonly Place _place;

        public PlaceBuilder()
        {
            _place = new Place();
        }

        public PlaceBuilder WithId(int id)
        {
            _place.Id = id;
            return this;
        }

        public PlaceBuilder WithNumberStay(int? numberStay)
        {
            _place.NumberStay = numberStay;
            return this;
        }

        public PlaceBuilder WithNumberLayer(int? numberLayer)
        {
            _place.NumberLayer = numberLayer;
            return this;
        }

        public PlaceBuilder WithSize(int? size)
        {
            _place.Size = size;
            return this;
        }

        public Place Build()
        {
            return _place;
        }
    }

    public static class PlaceMother
    {
        public static Place CreateValidPlace1()
        {
            return new PlaceBuilder()
                .WithId(1)
                .WithNumberStay(10)
                .WithNumberLayer(2)
                .WithSize(100)
                .Build();
        }
        public static Place CreateValidPlace2()
        {
            return new PlaceBuilder()
                .WithId(2)
                .WithNumberStay(10)
                .WithNumberLayer(2)
                .WithSize(100)
                .Build();
        }
        public static Place CreateValidPlace3()
        {
            return new PlaceBuilder()
                .WithId(3)
                .WithNumberStay(10)
                .WithNumberLayer(2)
                .WithSize(100)
                .Build();
        }
    }

}
