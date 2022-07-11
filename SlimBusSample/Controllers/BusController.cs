using Microsoft.AspNetCore.Mvc;
using SlimBusSample.Models;
using SlimMessageBus;

namespace SlimBusSample.Controllers;

[ApiController]
[Route("[controller]")]
public class BusController:ControllerBase
{
    [HttpPost]
    public Task Post(BusMessage message,[FromServices]IMessageBus bus) => bus.Publish(message);
}