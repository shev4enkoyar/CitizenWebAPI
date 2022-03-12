using CitizenWebAPI.Models;
using CitizenWebAPI.Util;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Xunit;

namespace CitizenWebAPI.Tests
{
    public class DatabaseFillerTests
    {
        public IEnumerable<Citizen> _citizensWithAge;
        public IEnumerable<Citizen> _citizensWithOutAge;
        public DatabaseFillerTests()
        {
            using WebClient webClient = new();
            var json = webClient.DownloadString(@"http://testlodtask20172.azurewebsites.net/task");
            _citizensWithAge = JsonConvert.DeserializeObject<IEnumerable<Citizen>>(json);
            _citizensWithOutAge = JsonConvert.DeserializeObject<IEnumerable<Citizen>>(json);
            foreach (var item in _citizensWithAge)
                item.Age = DatabaseFiller.GetCitizenAge(item.Id);
        }

        [Fact]
        public void GetCitizenAge_WithTele2Api_ShouldReturnValidAge()
        {
            foreach (var item in _citizensWithOutAge)
            {
                using WebClient webClient = new();
                var jsonString = webClient.DownloadString($@"http://testlodtask20172.azurewebsites.net/task/{item.Id}");
                Citizen citizen = JsonConvert.DeserializeObject<Citizen>(jsonString);
                Assert.Equal(citizen.Age, DatabaseFiller.GetCitizenAge(item.Id));
            }
        }

        [Fact]
        public void GetCitizens_CitizenCount_ShouldReturnValidCount()
        {
            Assert.Equal(_citizensWithAge.Count(), DatabaseFiller.GetCitizens().Count());
        }

        [Fact]
        public void GetCitizens_CitizensId_ShouldReturnTrueIfValid()
        {
            var citizens = DatabaseFiller.GetCitizens();
            int valid = 0;
            for (; valid < _citizensWithAge.Count(); valid++)
            {

                Citizen expectedCitizen = _citizensWithAge.Skip(valid).Take(1).First();
                Citizen citizen = citizens.Skip(valid).Take(1).First();

                if (expectedCitizen.Id != citizen.Id && expectedCitizen.Name != citizen.Name &&
                    expectedCitizen.Sex != citizen.Sex && expectedCitizen.Age != citizen.Age)
                {
                    break;
                }
            }
            Assert.Equal(_citizensWithAge.Count(), valid);
        }
    }
}
