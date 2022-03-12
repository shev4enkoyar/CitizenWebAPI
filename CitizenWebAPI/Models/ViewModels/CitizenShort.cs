using System;

namespace CitizenWebAPI.Models.ViewModels
{
    public class CitizenShort : IEquatable<CitizenShort>
    {
        public CitizenShort(string id, string name, string sex)
        {
            Id = id;
            Name = name;
            Sex = sex;
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public string Sex { get; set; }

        public bool Equals(CitizenShort other)
        {
            if (other is null)
                return false;

            return Id == other.Id && Name == other.Name && Sex == other.Sex;
        }

        public override bool Equals(object obj) => Equals(obj as CitizenShort);
    }
}
