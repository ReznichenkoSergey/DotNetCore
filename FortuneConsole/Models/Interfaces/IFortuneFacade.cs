using System;
using System.Collections.Generic;
using System.Text;
using Unity;

namespace FortuneConsole.Models.Interfaces
{
    public interface IFortuneFacade
    {
        void ShowFortune();

        string GetFortune();
        /*
        //IFortuneLoader FortuneLoader { get; set; }
        [InjectionMethod]
        void InjectFortuneLoader(IFortuneLoader _loader);

        [InjectionMethod]
        void InjectTellFortune(IFortuneTeller fortuneTeller);
        */
        //void InjectFortuneGetter(IFortuneGetter _getter);

        //IFortuneGetter FortuneGetter { get; set; }
        //string GetFortune(IFortuneGetter _getter);

        //void TellFortune(IFortuneTeller _teller);
    }
}
