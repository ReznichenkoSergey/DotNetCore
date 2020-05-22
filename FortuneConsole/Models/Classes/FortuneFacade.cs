using FortuneConsole.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FortuneConsole.Models.Classes
{
    public class FortuneFacade : IFortuneFacade
    {
        IFortuneTeller Teller;
        IFortuneGetter Getter;

        public FortuneFacade(IFortuneTeller _teller, IFortuneGetter _getter)
        {
            Teller = _teller;
            Getter = _getter;
        }

        public string GetFortune()
        {
            return Getter.GetFortune();
        }

        public void ShowFortune()
        {
            Teller.TellFortune();
        }
    }
}
