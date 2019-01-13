namespace IoC.Decorator.Example
{
    using System;

    public class StartTimeAttribute : Attribute, IFoo
    {
        private readonly IFoo _foo;
        public StartTimeAttribute(IFoo foo)
        {
            _foo = foo;
        }

        public void PrintHelloWorld()
        {
            Console.WriteLine(DateTime.Now.ToString());
            _foo.PrintHelloWorld();
        }
    }
}