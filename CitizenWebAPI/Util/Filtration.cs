using CitizenWebAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace CitizenWebAPI.Util
{
    public static class Filtration
    {
        public static IEnumerable<Citizen> GetFiltration(IEnumerable<Citizen> citizens, string sex, int? ageX, int? ageY)
        {
            citizens = SexFiltration(citizens, sex);
            citizens = AgeFiltration(citizens, ageX, ageY);
            return citizens;
        }

        public static IEnumerable<Citizen> SexFiltration(IEnumerable<Citizen> citizens, string sex)
        {
            if (sex != null)
            {
                switch (sex)
                {
                    case "male":
                    case "female":
                        return citizens.Where(x => x.Sex.Equals(sex));
                }
            }
            return citizens;
        }

        public static IEnumerable<Citizen> AgeFiltration(IEnumerable<Citizen> citizens, int? ageX, int? ageY)
        {
            if (ageX != null && ageY != null)
                citizens = citizens.Where(x => x.Age >= ageX && x.Age <= ageY);
            return citizens;
        }
    }
}
