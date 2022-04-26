using VistaClaim.Domain.Entities._Base;
using VistaClaim.Domain.Entities._Base.Exceptions;
using VistaClaim.Domain.Entities.CompanyEntity.Events;
using VistaClaim.Domain.Entities.CompanyEntity.Properties;
using VistaClaim.Entities._Base;

namespace VistaClaim.Domain.Entities.CompanyEntity
{
    public class Company : AggregateRoot
    {
        // TO DO, automapper needs public ctor!
        public Company() { }

        public Company(EntityId id, CompanyName name, CompanyCode code)
        {
            Apply(new CompanyCreatedEvent
            {
                Id = id,
                Name = name,
                Code = code
            });
        }

        public CompanyName Name { get; internal set; }
        public CompanyCode Code { get; internal set; }
        public bool Active { get; set; }

        protected override void EnsureValidState()
        {
            var valid = Id != null && Name != null && Code != null;

            if (!valid)
                throw new InvalidEntityStateException(this, "Validation checks failed.");
        }

        protected override void When(object @event)
        {
            switch (@event)
            {
                case CompanyCreatedEvent e:
                    Id = new EntityId(e.Id);
                    Name = new CompanyName(e.Name);
                    Code = new CompanyCode(e.Code);
                    Active = true;
                    break;
            }
        }
    }
}
