namespace IoC.Decorator.Example
{
    using System;
    using Microsoft.Extensions.DependencyInjection;

    public sealed class IocProvider
    {
        private static IServiceProvider _serviceProvider = new ServiceCollection()
            .AddTransient<IFoo>(CreateService<IFoo>)
            .BuildServiceProvider();

        private static TService CreateService<TService>(IServiceProvider sp) {
            if (typeof(TService) == typeof(IFoo)) {
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