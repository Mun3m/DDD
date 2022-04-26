using VistaClaim.Domain.Entities._Base;
using VistaClaim.Domain.Entities._Base.Exceptions;
using VistaClaim.Domain.Entities.ClientEntity.Events;
using VistaClaim.Domain.Entities.ClientEntity.Properties;
using VistaClaim.Entities._Base;

namespace VistaClaim.Domain.Entities.ClientEntity
{
    public class Client : AggregateRoot
    {
        public Client(EntityId id, EntityId companyId, ClientName name, ClientEmail email)
        {
            Apply(new ClientCreatedEvent
            {
                Id = id,
                CompanyId = companyId,
                Name = name,
                Email = email
            });
        }

        public EntityId CompanyId { get; internal set; }

        public ClientName Name { get; internal set; }

        public ClientEmail Email { get; internal set; }

        public void Edit(ClientName name, ClientEmail email)
        {
            Apply(new ClientEditEvent
            {
                Name = name,
                Email = email
            });
        }

        protected override void EnsureValidState()
        {
            var valid = Id != null && CompanyId != null && Email != null && Name != null;

            if (!valid)
                throw new InvalidEntityStateException(this, "Validation checks failed.");
        }

        protected override void When(object @event)
        {
            switch (@event)
            {
                case ClientCreatedEvent e:
                    Id = new EntityId(e.Id);
                    CompanyId = new EntityId(e.CompanyId);
                    Name = new ClientName(e.Name);
                    Email = new ClientEmail(e.Email);
                    break;
                case ClientEditEvent e:
                    {
                        Name = new ClientName(e.Name);
                        Email = new ClientEmail(e.Email);
                        break;
                    }
            }
        }
    }
}
