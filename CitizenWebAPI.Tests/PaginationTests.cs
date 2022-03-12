using CitizenWebAPI.Models;
using CitizenWebAPI.Models.ViewModels;
using CitizenWebAPI.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CitizenWebAPI.Tests
{
    public class PaginationTests
    {
        private readonly IEnumerable<Citizen> _citizens;
        private readonly IEnumerable<CitizenShort> _citizenShorts;

        public PaginationTests()
        {
            Citizen citizen1 = new() { Id = "1", Name = "Jhon", Sex = "male", Age = 25 };
            Citizen citizen2 = new() { Id = "2", Name = "Georg", Sex = "male", Age = 30 };
            Citizen citizen3 = new() { Id = "3", Name = "Anna", Sex = "female", Age = 31 };
            Citizen citizen4 = new() { Id = "4", Name = "Kirill", Sex = "male", Age = 10 };
            Citizen citizen5 = new() { Id = "5", Name = "Lena", Sex = "female", Age = 50 };
            _citizens = new List<Citizen>() { citizen1, citizen2, citizen3, citizen4, citizen5 };
            _citizenShorts = _citizens.Select(x => new CitizenShort(x.Id, x.Name, x.Sex)).ToList();
        }

        [Fact]
        public void Paginate_InputCitizens_ShouldReturnPaginateCollection()
        {
            int validate = 0;
            for (int pageSize = 1; pageSize < 6; pageSize++)
            {
                double maxPage = Math.Ceiling(_citizens.Count() / Convert.ToDouble(pageSize));
                for (int page = 1; page <= maxPage; page++)
                {
                    IEnumerable<CitizenShort> actualCitizens = Pagination.Paginate(_citizens, page, pageSize);
                    var p = _citizenShorts.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                    if (p.SequenceEqual(actualCitizens))
                    {
                        validate++;
                    }
                }
            }
            Assert.Equal(13, validate);
        }
    }
}
