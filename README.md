# BuilderImmutableObject #
Immutable Object Builder for C# objects.

### How to install with Nuget ?
```
Install-Package BuilderImmutableObject
```

### What is this repository for? ###
Library to work with C# immutable objects.

### How do I use it? ###

```.cs
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
 ```
 
 ```.cs
 public class BuilderTest
    {
        private Car _car;

        [SetUp]
        public void SetUp()
        {
            _car = new Car(color: "red", size: 78);
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
            var newCar = _car
                .Set(x => x.Color, "black")
                .Set(x => x.Size, 678)
                .Build();

            Assert.AreNotSame(_car, newCar);

            Assert.AreEqual(78, _car.Size);
            Assert.AreEqual("red", _car.Color);

            Assert.AreEqual(678, newCar.Size);
            Assert.AreEqual("black", newCar.Color);
        }
    }
    
