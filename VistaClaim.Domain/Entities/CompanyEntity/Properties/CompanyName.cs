using System;
using System.Collections.Generic;
using System.Text;
using VistaClaim.Entities._Base;

namespace VistaClaim.Domain.Entities.CompanyEntity.Properties
{
    public class CompanyName : ValueObject
    {
        public static CompanyName FromString(string value)
        {
            CheckValidity(value);

            return new CompanyName(value);
        }
        public string Value { get; }

        internal CompanyName(string value) => Value = value;

        protected CompanyName() { }

        public static implicit operator string(CompanyName self) => self.Value;

        private static void CheckValidity(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(nameof(value), $"{nameof(CompanyName)} cannot be empty");

            if (value.Length > 100)
                throw new ArgumentOutOfRangeException(nameof(value), $"{nameof(CompanyName)} cannot be longer than 100 characters");
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}