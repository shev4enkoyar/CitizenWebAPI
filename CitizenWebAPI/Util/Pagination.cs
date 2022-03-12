using CitizenWebAPI.Models;
using CitizenWebAPI.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace CitizenWebAPI.Util
{
    public static class Pagination
    {
        public static IEnumerable<CitizenShort> Paginate(IEnumerable<Citizen> citizens, int page, int pageSize)
        {
            return citizens.Select(x => new CitizenShort(x.Id, x.Name, x.Sex)).Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }
    }
}
