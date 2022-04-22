using GkShp.Core.DomainTypes;

namespace GkShp.Catalog.Domain
{
    public class Category : Entity
    {
        public string Name { get; private set; }
        public int Code { get; private set; }

        public Category(string name, int code)
        {
            Name = name;
            Code = code;
        }

        public override string ToString()
        {
            return $"{Name} - {Code}";
        }

        public void Validate()
        {
            Validations.ValidateIfEmpty(Name, "The field Name can't be empty");
            Validations.ValidateIfEqual(Code, 0, "The field Code can't be 0");
        }
    }
}
