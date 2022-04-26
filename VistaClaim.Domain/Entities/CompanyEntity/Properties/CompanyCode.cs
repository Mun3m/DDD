using System;
using System.Collections.Generic;
using VistaClaim.Entities._Base;

namespace VistaClaim.Domain.Entities.CompanyEntity.Properties
{
    public class CompanyCode : ValueObject
    {
        public static CompanyCode FromString(string value)
        {
            CheckValidity(value);

            return new CompanyCode(value);
        }
        public string Value { get; }

        internal CompanyCode(string value) => Value = value;
        protected CompanyCode() { }

        public static implicit operator string(CompanyCode self) => self.Value;

        private static void CheckValidity(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(nameof(value), $"{nameof(CompanyCode)} cannot be empty");

            if (value.Length > 100)
                throw new ArgumentOutOfRangeException(nameof(value), $"{nameof(CompanyCode)} cannot be longer than 100 characters");
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
