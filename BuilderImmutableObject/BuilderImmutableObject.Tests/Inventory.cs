using System;

namespace BuilderImmutableObject.Tests
{
    public class Inventory
    {
        private Inventory()
        {
            
        }

        private Inventory(DateTime date, string description)
        {
            Date = date;
            Description = description;
        }

        public DateTime Date { get; private set; }
        public string Description { get; private set; }

        public static Inventory Start(string description) => new Inventory(DateTime.Now, description);
    }
}
