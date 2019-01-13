namespace IoC.Decorator.Example
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore;
    using Microsoft.Extensions.DependencyInjection;
  using Newtonsoft.Json;



   
    class Program
    {
        const string url = "http://localhost:3000";
        static void Main(string[] args)
        {
            var foo = IocProvider.ServiceProvider.GetService<IFoo>();
            foo.PrintHelloWorld();
            FakeServer.FakeServer.StartServer(url);
            Console.Read();
        }

        

    }
}