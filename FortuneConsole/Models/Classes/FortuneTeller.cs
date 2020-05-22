using FortuneConsole.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FortuneConsole.Models.Classes
{
    class FortuneTeller : IFortuneTeller
    {
        IFortuneLoader FortuneLoader;

        public FortuneTeller(IFortuneLoader _loader)
        {
            FortuneLoader = _loader;
        }
        public void TellFortune()
        {
            Console.WriteLine(FortuneLoader.LoadFortune());
        }
    }
}
