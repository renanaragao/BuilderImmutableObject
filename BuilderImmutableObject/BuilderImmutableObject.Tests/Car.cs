using System;

namespace BuilderImmutableObject.Tests
{
    public class Car
    {
        [Obsolete]
        public Car()
        {

        }

        public Car(string color, int size, DateTime? date)
        {
            Color = color;
            Size = size;
            Date = date;
        }

        public string Color { get; private set; }
        public int Size { get; private set; }
        public DateTime? Date { get; private set; }
        public string DateString => Date.ToString();
    }
}