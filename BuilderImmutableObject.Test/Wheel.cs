namespace BuilderImmutableObject.Test
{
    public class Wheel
    {

        private Wheel()
        {
            
        }

        public Wheel(int rim, string brand)
        {
            Rim = rim;
            Brand = brand;
        }

        public int Rim { get; private set; }
        public string Brand { get; private set; }
        public string Description => $"{Brand}-{Rim}";
    }
}