using FortuneConsole.Models;
using FortuneConsole.Models.Classes;
using FortuneConsole.Models.Interfaces;
using System;
using Unity;
using Unity.Injection;

namespace FortuneConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            IUnityContainer container = new UnityContainer();
            container.RegisterType<IFortuneLoader, FortuneLoader>();
            container.RegisterType<IFortuneTeller, FortuneTeller>();
            container.RegisterType<IFortuneGetter, FortuneGetter>();
            container.RegisterType<IFortuneFacade, FortuneFacade>();

            var value = container.Resolve<IFortuneFacade>();
            
            //GetFortune
            Console.WriteLine("Method GetFortune:");
            Console.WriteLine(value.GetFortune());
            
            //ShowFortune
            Console.WriteLine("Method ShowFortune:");
            value.ShowFortune();
            
            Console.ReadKey();
        }
    }
}
