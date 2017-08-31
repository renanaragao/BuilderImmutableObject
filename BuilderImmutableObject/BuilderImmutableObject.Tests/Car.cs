using System;

namespace BuilderImmutableObject.Tests
{
    public class Car
    {
        [Obsolete]
        public Car()
        {

        }

        public Car(string color, int size)
        {
            Color = color;
            Size = size;
        }

        public string Color { get; private set; }
        public int Size { get; private set; }
    }
}