using System;

namespace BuilderImmutableObject.Tests
{
    public class Car
    {
        [Obsolete]
        public Car()
        {

        }

        public Car(string color, int size, DateTime? data)
        {
            Color = color;
            Size = size;
            Data = data;
        }

        public string Color { get; private set; }
        public int Size { get; private set; }
        public DateTime? Data { get; private set; }
    }
}