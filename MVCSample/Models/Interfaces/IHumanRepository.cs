using MVCSample.Models.Infestation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCSample.Models.Interfaces
{
    public interface IHumanRepository
    {
        IQueryable<Human> GetAllHumans();
        Human GetHuman(int id);
        void CreateHuman(Human obj);
        void ModifyHuman(Human obj);
        void DeleteHuman(int id);
    }
}
