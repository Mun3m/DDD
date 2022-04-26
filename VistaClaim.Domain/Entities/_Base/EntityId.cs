using System;
using System.Collections.Generic;
using System.Text;

namespace VistaClaim.Entities._Base
{
    public class EntityId : ValueObject
    {
        internal EntityId(Guid value)
        {
            if (value == default)
                throw new ArgumentException("Identity must be specified", nameof(value));

            Value = value;
        }

        public static implicit operator Guid(EntityId self) => self.Value;

        public static EntityId FromGuid(Guid value) => new EntityId(value);

        public Guid Value { get; }

        public override string ToString()
        {
            return Value.ToString();
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
