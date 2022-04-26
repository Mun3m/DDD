using System;
using System.Collections.Generic;
using System.Text;
using VistaClaim.Entities._Base;

namespace VistaClaim.Domain.Entities.ClientEntity.Properties
{
    public class ClientEmail : ValueObject
    {
        public static ClientEmail FromString(string value)
        {
            CheckValidity(value);

            return new ClientEmail(value);
        }
        public string Value { get; }

        internal ClientEmail(string value) => Value = value;

        public static implicit operator string(ClientEmail self) => self.Value;

        private static void CheckValidity(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(nameof(value), $"{nameof(ClientEmail)} cannot be empty");

            if (value.Length > 100)
                throw new ArgumentOutOfRangeException(nameof(value), $"{nameof(ClientEmail)} cannot be longer than 100 characters");
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
