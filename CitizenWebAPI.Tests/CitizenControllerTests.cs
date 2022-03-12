using CitizenWebAPI.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Xunit;

namespace CitizenWebAPI.Tests
{
    public class CitizenControllerTests
    {
        public IEnumerable<Citizen> _citizens;

        public CitizenControllerTests()
        {
            using WebClient webClient = new();
            var json = webClient.DownloadString(@"http://testlodtask20172.azurewebsites.net/task");
            _citizens = JsonConvert.DeserializeObject<IEnumerable<Citizen>>(json);
        }

        [Fact]
        public void GetCitizen_GetCitizenFromApis_ShouldReturnCorrectValidateNumber()
        {
            int validate = 0;
            foreach (var citizen in _citizens)
            {
                using WebClient webClient = new();
                if (webClient.DownloadString($@"http://testlodtask20172.azurewebsites.net/task/{citizen.Id}") ==
                    webClient.DownloadString($@"https://coffemint.space/api/citizen/get/{citizen.Id}"))
                {
                    validate++;
                }
                else
                    break;
            }
            Assert.Equal(_citizens.Count(), validate);
        }
    }
}
