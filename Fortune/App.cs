using System;
using System.Globalization;

namespace Fortune
{
	public class App
	{
		private readonly IDateTimeOffset _dateTimeOffset;
		private readonly IFortuneCookie _fortuneCookie;
		private readonly IConsole _console;

		public App(IDateTimeOffset dateTimeOffset, IFortuneCookie fortuneCookie, IConsole console)
		{
			_dateTimeOffset = dateTimeOffset;
			_fortuneCookie = fortuneCookie;
			_console = console;
		}

		public void Run()
		{
			var name = _console.Prompt("What's your name?");

			var dobString = _console.Prompt("When were you born (dd/mm/yyyy)?");
			DateTimeOffset.TryParseExact(dobString, "dd/MM/yyyy", null, DateTimeStyles.None, out var dob);

			var person = new Person
			{
				Name = name,
				DateOfBirth = dob
			};

			var now = _dateTimeOffset.Now;
			bool todayIsBirthday = now.Day == person.DateOfBirth.Day && now.Month == person.DateOfBirth.Month;

			string greeting = todayIsBirthday ? "Happy birthday, {0}!" : "Hi {0}!";

			_console.WriteLine(
				greeting + "\nYour fortune for today is: {1}",
				person.Name,
				_fortuneCookie.GetTodaysFortune());

			_console.WriteLine(
				"On the day you were born your fortune was: {0}",
				_fortuneCookie.GetFortuneForDate(person.DateOfBirth));
		}
	}
}
