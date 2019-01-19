namespace IoC.Decorator.Example
{
    using System.Net.Http;
    using System.Net;
    using System;
    using Infrastructure;
    using Microsoft.Extensions.DependencyInjection;

    public sealed class IocProvider
    {
        private static IServiceProvider CreateServiceProvider()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddTransient<IFoo>(CreateService<IFoo>);
            serviceCollection.AddHttpClient<MovieHttpClient>()
                .ConfigurePrimaryHttpMessageHandler(handler => new HttpClientHandler
                {
                    AutomaticDecompression = DecompressionMethods.GZip
                });

            return serviceCollection.BuildServiceProvider();
        }

        private static IServiceProvider _serviceProvider = CreateServiceProvider();

        private static TService CreateService<TService>(IServiceProvider sp)
        {
            if (typeof(TService) == typeof(IFoo))
            {
                var foo = new Foo();
                dynamic service = new StartTimeAttribute(foo);
                return service;
            }

            return default(TService);
        }
        public static IServiceProvider ServiceProvider
        {
            get
            {
                return _serviceProvider;
            }
        }

    }
}