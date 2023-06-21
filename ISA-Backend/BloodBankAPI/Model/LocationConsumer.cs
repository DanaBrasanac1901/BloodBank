namespace BloodBankAPI.Model
{
    using System;
    using System.Threading.Tasks;
    using MassTransit;
    using Model;

    public class LocationConsumer :IConsumer<Location>{

        private StoreLocation storage;
        public LocationConsumer()
        {
            storage = StoreLocation.Instance;
        }

        public async Task Consume(ConsumeContext<Location> context)
        {
            if(!storage.locs.Contains(context.Message))
                storage.Store(context.Message);
            await context.RespondAsync(context.Message);
            
            
        }
    }
}