using Xunit;

namespace BuilderImmutableObject.Test
{
    public class BuilderTest
    {
        private readonly Car _car;
        private readonly Wheel _wheel;

        public BuilderTest()
        {
            _car = new Car(color: "red", size: 78, wheels: new[] {1, 2});
            _wheel = new Wheel(rim: 34, brand: "Continental");
        }

        [Fact]
        public void Must_Change_Color_Property()
        {
            var newCar = _car.Set(x => x.Color, "black").Build();

            Assert.NotSame(_car, newCar);

            Assert.Equal(78, _car.Size);
            Assert.Equal("red", _car.Color);

            Assert.Equal(78, newCar.Size);
            Assert.Equal("black", newCar.Color);
        }

        [Fact]
        public void Must_Change_All_Properties()
        {
            var newCar = _car
                .Set(x => x.Color, "black")
                .Set(x => x.Size, 678)
                .Build();

            Assert.NotSame(_car, newCar);

            Assert.Equal(78, _car.Size);
            Assert.Equal("red", _car.Color);

            Assert.Equal(678, newCar.Size);
            Assert.Equal("black", newCar.Color);
        }

        [Fact]
        public void Must_Add_An_Item_To_The_List()
        {
            var newCar = _car
                .Set(x => x.Wheels, new[] { 3 })
                .Build();

            Assert.Equal(new[] { 1, 2 }, _car.Wheels);
            Assert.Equal(new[] { 3 }, newCar.Wheels);
        }

        [Fact]
        public void Should_Copy_Wheels()
        {
            var newWheel = _wheel.Set(x => x.Brand, "Pirelli").Build();

            Assert.NotSame(_wheel, newWheel);

            Assert.Equal(34, _wheel.Rim);
            Assert.Equal("Continental", _wheel.Brand);

            Assert.Equal(34, newWheel.Rim);
            Assert.Equal("Pirelli", newWheel.Brand);
        }
    }
}
