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

        public virtual List<Human> Humans { get; set; }

        public int? WorldPartId { get; set; }
        public virtual WorldPart  WorldPart { get; set; }
    }
}
