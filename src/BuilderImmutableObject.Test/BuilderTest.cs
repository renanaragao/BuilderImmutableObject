using Xunit;

namespace BuilderImmutableObject.Test
{
    public class BuilderTest
    {
        private readonly Car car;
        private readonly Wheel wheel;
        private readonly Tire tire;

        public BuilderTest()
        {
            car = new Car(color: "red", size: 78, wheels: new[] { 1, 2 });
            wheel = new Wheel(rim: 34, brand: "Continental");
            tire = new Tire();
        }

        [Fact]
        public void Must_Change_Color_Property()
        {
            var newCar = car.Set(x => x.Color, "black").Build();

            Assert.NotSame(car, newCar);

            Assert.Equal(78, car.Size);
            Assert.Equal("red", car.Color);

            Assert.Equal(78, newCar.Size);
            Assert.Equal("black", newCar.Color);
        }

        [Fact]
        public void Must_Change_All_Properties()
        {
            var newCar = car
                .Set(x => x.Color, "black")
                .Set(x => x.Size, 678)
                .Build();

            Assert.NotSame(car, newCar);

            Assert.Equal(78, car.Size);
            Assert.Equal("red", car.Color);

            Assert.Equal(678, newCar.Size);
            Assert.Equal("black", newCar.Color);
        }

        [Fact]
        public void Must_Add_An_Item_To_The_List()
        {
            var newCar = car
                .Set(x => x.Wheels, new[] { 3 })
                .Build();

            Assert.Equal(new[] { 1, 2 }, car.Wheels);
            Assert.Equal(new[] { 3 }, newCar.Wheels);
        }

        [Fact]
        public void Should_Copy_Wheels()
        {
            var newWheel = wheel.Set(x => x.Brand, "Pirelli").Build();

            Assert.NotSame(wheel, newWheel);

            Assert.Equal(34, wheel.Rim);
            Assert.Equal("Continental", wheel.Brand);

            Assert.Equal(34, newWheel.Rim);
            Assert.Equal("Pirelli", newWheel.Brand);
        }

        [Fact]
        public void Should_Copy_Tire()
        {
            var newTire = tire
                .Set(x => x.Brand, "Pirelli")
                .Build();

            Assert.NotSame(tire, newTire);

            Assert.Equal(78, tire.Calibration);
            Assert.Equal("Pirelli", tire.Brand);

            Assert.Equal(78, newTire.Calibration);
            Assert.Equal("Pirelli", newTire.Brand);
        }
    }
}
