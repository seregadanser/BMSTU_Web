using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBack.Tests.Additional
{
    public class UsefulBuilder
    {
        private readonly Useful _useful;

        public UsefulBuilder()
        {
            _useful = new Useful();
        }

        public UsefulBuilder WithInventoryId(int inventoryId)
        {
            _useful.InventoryId = inventoryId;
            return this;
        }

        public UsefulBuilder WithPersonId(string? personId)
        {
            _useful.PersonId = personId;
            return this;
        }

        public UsefulBuilder WithDateOfStart(DateTime? dateOfStart)
        {
            _useful.DateOfStart = dateOfStart;
            return this;
        }

        public Useful Build()
        {
            return _useful;
        }
    }

    public static class UsefulMother
    {
        public static Useful CreateValidUseful()
        {
            return new UsefulBuilder()
                .WithInventoryId(1)
                .WithPersonId("john.doe")
                .WithDateOfStart(new DateTime(1980, 1, 1))
                .Build();
        }
    }

}
