using System;
using System.Collections.Generic;

namespace BuilderImmutableObject.Test
{
    public class Car
    {
        [Obsolete]
        public Car()
        {
        }

        public Car(string color, int size, IEnumerable<int> wheels)
        {
            Color = color;
            Size = size;
            Wheels = wheels;
        }

        public string Color { get; private set; }
        public int Size { get; private set; }
        public IEnumerable<int> Wheels { get; private set; }
    }
}