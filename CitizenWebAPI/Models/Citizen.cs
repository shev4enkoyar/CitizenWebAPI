using System;

namespace CitizenWebAPI.Models
{
    public class Citizen : IEquatable<Citizen>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Sex { get; set; }

        public int Age { get; set; }

        public bool Equals(Citizen other)
        {
            if (other is null)
                return false;

            return Id == other.Id && Name == other.Name && Sex == other.Sex && Age == other.Age;
        }
    }
}
