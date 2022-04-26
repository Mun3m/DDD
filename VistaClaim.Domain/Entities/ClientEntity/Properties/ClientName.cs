using System;
using System.Collections.Generic;
using VistaClaim.Entities._Base;

namespace VistaClaim.Domain.Entities.ClientEntity.Properties
{
    public class ClientName : ValueObject
    {
        public static ClientName FromString(string value)
        {
            CheckValidity(value);

            return new ClientName(value);
        }
        public string Value { get; }

        internal ClientName(string value) => Value = value;

        public static implicit operator string(ClientName self) => self.Value;

        private static void CheckValidity(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(nameof(value), $"{nameof(ClientName)} cannot be empty");

            if (value.Length > 100)
                throw new ArgumentOutOfRangeException(nameof(value), $"{nameof(ClientName)} cannot be longer than 100 characters");
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
