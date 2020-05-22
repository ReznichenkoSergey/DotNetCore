using FortuneConsole.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FortuneConsole.Models.Classes
{
    public class FortuneGetter : IFortuneGetter
    {
        IFortuneLoader FortuneLoader;

        public FortuneGetter(IFortuneLoader _loader)
        {
            FortuneLoader = _loader;
        }
        public string GetFortune()
        {
            return FortuneLoader.LoadFortune();
        }
    }
}
