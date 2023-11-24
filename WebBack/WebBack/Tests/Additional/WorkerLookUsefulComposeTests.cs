using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBack.Tests.Additional
{
    public class WorkerLookUsefulComposeBuilder
    {
        private readonly WorkerLookUsefulCompose _workerLookUsefulCompose;

        public WorkerLookUsefulComposeBuilder()
        {
            _workerLookUsefulCompose = new WorkerLookUsefulCompose();
        }

        public WorkerLookUsefulComposeBuilder WithInventoryNumber(int inventoryNumber)
        {
            _workerLookUsefulCompose.Inventory_number = inventoryNumber;
            return this;
        }

        public WorkerLookUsefulComposeBuilder WithName(string? name)
        {
            _workerLookUsefulCompose.Name = name;
            return this;
        }

        public WorkerLookUsefulComposeBuilder WithDateCome(DateTime? dateCome)
        {
            _workerLookUsefulCompose.DateCome = dateCome;
            return this;
        }

        public WorkerLookUsefulComposeBuilder WithDateProduction(DateTime? dateProduction)
        {
            _workerLookUsefulCompose.DateProduction = dateProduction;
            return this;
        }

        public WorkerLookUsefulComposeBuilder WithDateOfStart(DateTime? dateOfStart)
        {
            _workerLookUsefulCompose.DateOfStart = dateOfStart;
            return this;
        }

        public WorkerLookUsefulCompose Build()
        {
            return _workerLookUsefulCompose;
        }
    }

    public static class WorkerLookUsefulComposeMother
    {
        public static WorkerLookUsefulCompose CreateValidWorkerLookUsefulCompose()
        {
            return new WorkerLookUsefulComposeBuilder()
                .WithInventoryNumber(1)
                .WithName("Product A")
                .WithDateCome(new DateTime(2022, 1, 1))
                .WithDateProduction(new DateTime(2021, 1, 1))
                .WithDateOfStart(new DateTime(2020, 1, 1))
                .Build();
        }
    }

}
