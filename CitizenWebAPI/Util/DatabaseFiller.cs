using CitizenWebAPI.Data;
using CitizenWebAPI.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace CitizenWebAPI.Util
{
    public class DatabaseFiller
    {
        private readonly ApplicationDbContext _dbContext;

        public DatabaseFiller(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void FillCitizensDb()
        {
            if (Startup.IsFirstStart)
            {
                if (!CheckRelevance())
                {
                    _dbContext.Citizens.RemoveRange(_dbContext.Citizens);
                    FillCitizensDbLogic();
                }
                Startup.IsFirstStart = false;
            }
        }

        private bool CheckRelevance()
        {
            if (_dbContext.Citizens.ToList().SequenceEqual(GetCitizens().OrderBy(x => x.Id).ToList()))
                return true;
            else
                return false;
        }

        private void FillCitizensDbLogic()
        {
            var citizen = GetCitizens();
            _dbContext.AddRange(citizen);
            _dbContext.SaveChanges();
        }

        public static IEnumerable<Citizen> GetCitizens()
        {
            IEnumerable<Citizen> citizens;
            using (WebClient webClient = new())
            {
                var json = webClient.DownloadString(@"http://testlodtask20172.azurewebsites.net/task");
                citizens = JsonConvert.DeserializeObject<IEnumerable<Citizen>>(json);
            }
            foreach (var item in citizens)
                item.Age = GetCitizenAge(item.Id);
            return citizens;
        }

        public static int GetCitizenAge(string id)
        {
            using WebClient webClient = new();
            var json = webClient.DownloadString($@"http://testlodtask20172.azurewebsites.net/task/{id}");
            dynamic deserialize_post = JsonConvert.DeserializeObject(json);
            return deserialize_post.age;
        }
    }
}
