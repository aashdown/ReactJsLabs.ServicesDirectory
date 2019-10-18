using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GreetingService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GreetingController : ControllerBase
    {
        private readonly ILogger<GreetingController> _log;

        private string[] salutations = new[]
        {
            "Greetings to you, [name].",
            "Hello [name].",
            "Hi [name]!.",
            "Howdy [name]!!!",
            "Whatsup, [name]?!",
            "Yo,[name], whatsup?",
            "How ya doin', [name] ?",
            "Hey there, [name] ;)"
        };

        public GreetingController(
            ILogger<GreetingController> logger
        )
        {
            _log = logger;
        }

        public string Get(string name)
        {
            var randomIndex = new Random().Next(0, salutations.Length -1);
            var randomGreeting = salutations[randomIndex].Replace("[name]", name);

            _log.LogInformation($"[{name}] received. Responding with [{randomGreeting}]");

            Response.Headers["Access-Control-Allow-Origin"] = "*";
            return randomGreeting;
        }
    }
}