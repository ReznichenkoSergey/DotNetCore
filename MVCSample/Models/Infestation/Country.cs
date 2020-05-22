using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCSample.Models.Infestation
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Population { get; set; }
        public int SickCount { get; set; }
        public int DeadCount { get; set; }
        public int RecoveredCount { get; set; }
        public bool Vaccine { get; set; }

        public List<Human> Humans { get; set; }
    }
}
