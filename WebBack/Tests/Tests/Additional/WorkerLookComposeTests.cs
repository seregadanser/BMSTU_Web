using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBack.Tests.Additional
{
    public class WorkerLookComposeBuilder
    {
        private readonly WorkerLookCompose _workerLookCompose;

        public WorkerLookComposeBuilder()
        {
            _workerLookCompose = new WorkerLookCompose();
        }

        public WorkerLookComposeBuilder WithInventoryNumber(int inventoryNumber)
        {
            _workerLookCompose.Inventory_number = inventoryNumber;
            return this;
        }

        public WorkerLookComposeBuilder WithName(string? name)
        {
            _workerLookCompose.Name = name;
            return this;
        }

        public WorkerLookComposeBuilder WithDateCome(DateTime? dateCome)
        {
            _workerLookCompose.DateCome = dateCome;
            return this;
        }

        public WorkerLookComposeBuilder WithDateProduction(DateTime? dateProduction)
        {
            _workerLookCompose.DateProduction = dateProduction;
            return this;
        }

        public WorkerLookCompose Build()
        {
            return _workerLookCompose;
        }
    }

    public static class WorkerLookComposeMother
    {
        public static WorkerLookCompose CreateValidWorkerLookCompose()
        {
            return new WorkerLookComposeBuilder()
                .WithInventoryNumber(1)
                .WithName("Product A")
                .WithDateCome(new DateTime(2022, 1, 1))
                .WithDateProduction(new DateTime(2021, 1, 1))
                .Build();
        }
    }

}
