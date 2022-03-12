using CitizenWebAPI.Models;
using CitizenWebAPI.Util;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CitizenWebAPI.Tests
{
    public class FiltrationTests
    {
        private readonly IEnumerable<Citizen> _citizens;
        public FiltrationTests()
        {
            Citizen citizen1 = new() { Id = "1", Name = "Jhon", Sex = "male", Age = 25 };
            Citizen citizen2 = new() { Id = "2", Name = "Georg", Sex = "male", Age = 30 };
            Citizen citizen3 = new() { Id = "3", Name = "Anna", Sex = "female", Age = 31 };
            Citizen citizen4 = new() { Id = "4", Name = "Kirill", Sex = "male", Age = 10 };
            Citizen citizen5 = new() { Id = "5", Name = "Lena", Sex = "female", Age = 50 };
            _citizens = new List<Citizen>() { citizen1, citizen2, citizen3, citizen4, citizen5 };
        }

        [Fact]
        public void SexFiltration_InputCitizens_ShouldReturnOnly3Male()
        {
            int validate = 0;
            IEnumerable<Citizen> actualCitizens = Filtration.SexFiltration(_citizens, "male");
            foreach (var citizen in actualCitizens)
            {
                if (citizen.Sex.Equals("male"))
                    validate++;
            }
            Assert.Equal(actualCitizens.Count(), validate);
        }

        [Fact]
        public void AgeFiltration_InputCitizensAgeX20AgeY35_ShouldReturn3Citizens()
        {
            int validate = 0;
            IEnumerable<Citizen> actualCitizens = Filtration.AgeFiltration(_citizens, 20, 35);
            foreach (var citizen in actualCitizens)
            {
                if (citizen.Age >= 20 && citizen.Age <= 35)
                    validate++;
            }
            Assert.Equal(actualCitizens.Count(), validate);
        }

        [Fact]
        public void GetFiltration_InputCitizensSexFemaleAgeX15AgeY40_ShouldReturn1Citizens()
        {
            int validate = 0;
            IEnumerable<Citizen> actualCitizens = Filtration.GetFiltration(_citizens, "female", 15, 40);
            foreach (var citizen in actualCitizens)
            {
                if (citizen.Age >= 15 && citizen.Age <= 40 && citizen.Sex.Equals("female"))
                    validate++;
            }
            Assert.Equal(actualCitizens.Count(), validate);
        }
    }
}
