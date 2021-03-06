﻿using System;
using FakeItEasy;
using NUnit.Framework;

namespace Fortune.Tests
{
	public class AppTests
	{
		private IDateTimeOffset _dateTimeOffset;
		private IFortuneCookie _fortuneCookie;
		private IConsole _console;
		private App _app;

		[SetUp]
		public void Setup()
		{
			_dateTimeOffset = A.Fake<IDateTimeOffset>();
			_fortuneCookie = A.Fake<IFortuneCookie>();
			_console = A.Fake<IConsole>();
			_app = new App(_dateTimeOffset, _fortuneCookie, _console);
		}

		[Test]
		public void Run_ItPromptsTheUserToEnterTheirNameAndDateOfBirth()
		{
			// Act
			_app.Run();

			// Assert
			A.CallTo(() => _console.Prompt("What's your name?")).MustHaveHappenedOnceExactly()
				.Then(A.CallTo(() => _console.Prompt("When were you born (dd/mm/yyyy)?")).MustHaveHappenedOnceExactly());
		}

		[TestCase("24/08/2020", "Hi {0}!")]
		[TestCase("14/06/2020", "Happy birthday, {0}!")]
		public void Run_GivenTheUserHasEnteredTheirName_ItGreetsTheUserAndTellsThemTheFortuneForTodayAndTheirBirthday(string nowDateString, string expectedGreeting)
		{
			// Arrange
			A.CallTo(() => _dateTimeOffset.Now).Returns(DateTimeOffset.ParseExact(nowDateString, "dd/MM/yyyy", null));
			A.CallTo(() => _console.Prompt("What's your name?")).Returns("Jane Doe");
			A.CallTo(() => _console.Prompt("When were you born (dd/mm/yyyy)?")).Returns("14/06/1982");
			A.CallTo(() => _fortuneCookie.GetTodaysFortune()).Returns("Certain bears do not harm humans. Today you will meet no such bears.");
			A.CallTo(() => _fortuneCookie.GetFortuneForDate(A<DateTimeOffset>.That.Matches(d => d.Year == 1982 && d.Month == 6 && d.Day == 14))).Returns("Today you will experience an average 1G of gravity.");

			// Act
			_app.Run();

			// Assert
			A.CallTo(() => _console.WriteLine(
				expectedGreeting + "\nYour fortune for today is: {1}",
				"Jane Doe",
				"Certain bears do not harm humans. Today you will meet no such bears."))
				.MustHaveHappenedOnceExactly().Then(

				A.CallTo(() => _console.WriteLine(
					"On the day you were born your fortune was: {0}",
					"Today you will experience an average 1G of gravity."))
					.MustHaveHappenedOnceExactly()
			);
		}
	}
}
