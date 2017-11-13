using System;
using NUnit.Framework;

namespace BuilderImmutableObject.Tests
{
    public class BuilderTest
    {
        private Car _car;

        [SetUp]
        public void SetUp()
        {
            _car = new Car(color: "red", size: 78, date: null);
        }

        [Test]
        public void Must_Change_Color_Property()
        {
            var newCar = _car.Set(x => x.Color, "black").Build();

            Assert.AreNotSame(_car, newCar);

            Assert.AreEqual(78, _car.Size);
            Assert.AreEqual("red", _car.Color);

            Assert.AreEqual(78, newCar.Size);
            Assert.AreEqual("black", newCar.Color);
        }

        [Test]
        public void Must_Change_All_Properties()
        {
            var today = DateTime.Now;

            var newCar = _car
                .Set(x => x.Color, "black")
                .Set(x => x.Size, 678)
                .Set(x => x.Date, today)
                .Set(x => x.DateString, "newString")
                .Build();

            Assert.AreNotSame(_car, newCar);

            Assert.AreEqual(78, _car.Size);
            Assert.AreEqual("red", _car.Color);
            Assert.IsNull(_car.Date);

            Assert.AreEqual(678, newCar.Size);
            Assert.AreEqual("black", newCar.Color);
            Assert.AreEqual(today, newCar.Date);
            Assert.AreEqual(today.ToString(), newCar.DateString);
        }

        [Test]
        public void Must_Be_Possible_To_Instantiate_Class_With_Private_Constructor()
        {
            var inventory = Inventory.Start("foo");

            Assert.AreEqual("foo", inventory.Description);

            inventory = inventory.Set(x => x.Description, "bar").Build();

            Assert.AreEqual("bar", inventory.Description);
        }
    }
}
