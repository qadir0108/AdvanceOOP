using DesignPatterns.Facade;
using DesignPatterns.Observer;
using DesignPatterns.Singleton;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //ExampleSingleton exampleSingleton = new ExampleSingleton();
            //exampleSingleton.Run();

            //ExampleSingletonRealworld exampleSingletonRealworld = new ExampleSingletonRealworld();
            //exampleSingletonRealworld.Run();

            //ExampleFacade exampleFacade = new ExampleFacade(); 
            //exampleFacade.Run();

            //ExampleFacadeRealworld exampleFacadeRealworld = new ExampleFacadeRealworld();
            //exampleFacadeRealworld.Run();

            //ExampleObserver exampleObserver = new ExampleObserver();
            //exampleObserver.Run();

            ExampleObserverRealworld exampleObserverRealworld = new ExampleObserverRealworld();
            exampleObserverRealworld.Run();

        }
    }
}
