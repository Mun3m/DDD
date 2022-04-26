using System;
using System.Collections.Generic;
using System.Text;
using VistaClaim.Entities._Base;

namespace VistaClaim.Domain.Entities.AssignmentEntity.Properties
{
    public class AssignmentClaimNumber : ValueObject
    {
        public static AssignmentClaimNumber FromString(string value)
        {
            CheckValidity(value);

            return new AssignmentClaimNumber(value);
        }
        public string Value { get; }

        internal AssignmentClaimNumber(string value) => Value = value;

        public static implicit operator string(AssignmentClaimNumber self) => self.Value;

        private static void CheckValidity(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(nameof(value), $"{nameof(AssignmentClaimNumber)} cannot be empty");

            if (value.Length > 100)
                throw new ArgumentOutOfRangeException(nameof(value), $"{nameof(AssignmentClaimNumber)} cannot be longer than 100 characters");
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}

