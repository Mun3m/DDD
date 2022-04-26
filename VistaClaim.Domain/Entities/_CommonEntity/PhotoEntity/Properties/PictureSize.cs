using System;
using System.Collections.Generic;
using System.Text;
using VistaClaim.Entities._Base;

namespace VistaClaim.Domain.Entities._CommonEntity.PhotoEntity.Properties
{
    public class PictureSize : ValueObject
    {
        public int Width { get; internal set; }
        public int Height { get; internal set; }

        public PictureSize(int width, int height)
        {
            CheckValidity(width, height);

            Width = width;
            Height = height;
        }

        internal PictureSize()
        {
        }

        private static void CheckValidity(int width, int height)
        {
            if (width <= 0)
                throw new ArgumentException($"Picture width must be positive number", nameof(width));

            if (height <= 0)
                throw new ArgumentException($"Picture height must be positive number", nameof(width));
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Width;
            yield return Height;
        }
    }
}
