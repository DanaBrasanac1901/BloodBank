namespace WebApi.Consumers
{
    using System;
    using System.Threading.Tasks;
    using Contracts;
    using MassTransit;
    using Model;

    public class LocationConsumer :
        IConsumer<Location>
    { 

        public LocationConsumer()
        {
        }

        public Task Consume(ConsumeContext<Location> context)
        {
            LocationRepo repo = new LocationRepo();
            repo.Init();
            
            return context.RespondAsync(repo.GetLocation(context.Message.Id));
        }
    }
}