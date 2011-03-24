using System;
using NUnit.Framework;
using Expedia;
using Rhino.Mocks;

namespace ExpediaTest
{
	[TestFixture()]
	public class CarTest
	{	
		private Car targetCar;
		private MockRepository mocks;
		
		[SetUp()]
		public void SetUp()
		{
			targetCar = new Car(5);
			mocks = new MockRepository();
		}
		
		[Test()]
		public void TestThatCarInitializes()
		{
			Assert.IsNotNull(targetCar);
		}	
		
		[Test()]
		public void TestThatCarHasCorrectBasePriceForFiveDays()
		{
			Assert.AreEqual(50, targetCar.getBasePrice()	);
		}
		
		[Test()]
		public void TestThatCarHasCorrectBasePriceForTenDays()
		{
            var target = new Car(10);
			Assert.AreEqual(80, target.getBasePrice());	
		}
		
		[Test()]
		public void TestThatCarHasCorrectBasePriceForSevenDays()
		{
			var target = new Car(7);
			Assert.AreEqual(10*7*.8, target.getBasePrice());
		}
		
		[Test()]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestThatCarThrowsOnBadLength()
		{
			new Car(-5);
		}

        [Test()]
        public void TestThatCarLocationIsCorrectInDatabase()
        {
            IDatabase mockDatabase mocks.Stub<IDatabase>();

            String myCarLoc = "Japan";
            String otherCarLoc = "Ireland";

            using (mocks.Record())
            {
                mockDatabase.getCarLocation(12);
                LastCall.Return(myCarLoc);

                mockDatabase.getCarLocation(42);
                LastCall.Return(otherCarLoc);
            }

            var target = new Car(365);

            target.Database = mockDatabase;

            String result;

            result = target.getCarLocation(12);
            Assert.AreEqual(result, myCarLoc);

            result = target.getCarLocation(42);
            Assert.AreEqual(result, otherCarLoc);
        }

        [Test()]
        public void TestThatHotelDoesGetMileageFromDatabase()
        {
            IDatabase mockDatabase = mocks.Stub<IDatabase>();
            int miles = 1234;

            mockDatabase.Miles = miles;

            var target = new Car(365);

            target.Database = mockDatabase;

            int testMiles = target.Mileage;
            Assert.AreEqual(testMiles, miles);
        }

        [Test()]
        public void TestThatCarRentalPricesAreCorrectWithObjectMother()
        {
            Car BMW = ObjectMother.BMW();
            Car Saab = ObjectMother.Saab();

            Assert.AreEqual(80, BMW.getBasePrice());
            Assert.AreEqual(10 * 7 * .8, Saab.getBasePrice());
        }
	}
}
