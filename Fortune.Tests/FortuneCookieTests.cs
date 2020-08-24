using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using FakeItEasy;

namespace Fortune.Tests
{
	public class FortuneCookieTests
	{
		private IDateTimeOffset _dateTimeOffset;
		private FortuneCookie _fortuneCookie;

		[SetUp]
		public void Setup()
		{
			_dateTimeOffset = A.Fake<IDateTimeOffset>();
			_fortuneCookie = new FortuneCookie(_dateTimeOffset);
		}

		[Test]
		public void GetTodaysFortune_ItReturnsTheCorrectFortuneForTheCurrentDate()
		{
			// Arrange
			A.CallTo(() => _dateTimeOffset.Now).Returns(new DateTimeOffset(2035, 4, 13, 10, 30, 15, TimeSpan.Zero));

			// Act
			string fortune = _fortuneCookie.GetTodaysFortune();

			// Assert
			Assert.AreEqual("Newlyweds should be avoided!", fortune);
		}

		[TestCase("24/08/2020", "Bad luck falls on Mondays!")] // Monday
		[TestCase("12/01/2021", "Beware of figs!")] // Tuesday
		[TestCase("19/06/2019", "You will meet a tall, dark untimely end!")] // Wednesday
		[TestCase("23/11/1950", "Avocadoes are lucky!")] // Thursday
		[TestCase("13/04/2035", "Newlyweds should be avoided!")] // Friday
		[TestCase("25/09/2021", "Everything's coming up Milhouse!")] // Saturday
		[TestCase("07/01/2120", "Fortune cookies are always untrue!")] // Sunday
		public void GetFortuneForDate_ItReturnsTheCorrectFortuneForEachDayOfTheWeek(string dateString, string expectedFortune)
		{
			// Arrange
			var date = DateTimeOffset.ParseExact(dateString, "dd/MM/yyyy", null);

			// Act
			string fortune = _fortuneCookie.GetFortuneForDate(date);

			// Assert
			Assert.AreEqual(expectedFortune, fortune);
		}
	}
}
