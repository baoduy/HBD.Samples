using MediatR;
using SlimBusSample.Handlers;
using SlimBusSample.Helpers;
using SlimBusSample.Models;
using SlimMessageBus.Host.AspNetCore;
using SlimMessageBus.Host.AzureServiceBus;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Service Bus
builder.Services.AddMediatR(typeof(Program).Assembly);

builder.Services.AddHttpContextAccessor()
    .AddSlimMessageBus((mbb, svp) =>
        {
            mbb.Produce<BusMessage>(x => x.DefaultTopic("topic-1").WithBusMessageModifier())
                .Consume<BusMessage>(c=>c.Topic("topic-1")
                    .SubscriptionName("sub-1")
                    .PrefetchCount(10)
                    .Instances(1)
                    .WithConsumer<BusMessageHandler>()
                )
                .Consume<BusMessage>(c=>c.Topic("topic-1")
                    .SubscriptionName("sub-2")
                    .SubscriptionSqlFilter($"{nameof(BusMessage.FilterProperty)}='Steven'")
                    .PrefetchCount(10)
                    .Instances(1)
                    .WithConsumer<BusMessageHandler>()
                )
                .WithProviderServiceBus(new ServiceBusMessageBusSettings(builder.Configuration.GetConnectionString("AzureBus"))
                {
                    TopologyProvisioning = new ServiceBusTopologySettings
                    {
                        Enabled = true,
                        // CanConsumerCreateQueue = false,
                        // CanConsumerCreateTopic = false,
                        // CanProducerCreateTopic = false,
                        // CanProducerCreateQueue = false,
                        CanConsumerCreateSubscription = true,
                    }
                })
                // Add other bus transports, if needed
                //.AddChildBus("Bus2", (builder) => {})
                .WithSerializer(new JsonMessageSerializer())
                ;
        }, addConsumersFromAssembly: new[] { typeof(Program).Assembly }
    );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

await app.RunWithBusAsync();