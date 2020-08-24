using System;
using System.Globalization;

namespace Fortune
{
	public class App
	{
		private readonly IFortuneCookie _fortuneCookie;
		private readonly IConsole _console;

		public App(IFortuneCookie fortuneCookie, IConsole console)
		{
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

			_console.WriteLine(
				"Hi {0}!\nYour fortune for today is: {1}",
				person.Name,
				_fortuneCookie.GetTodaysFortune());

			_console.WriteLine(
				"On the day you were born your fortune was: {0}",
				_fortuneCookie.GetFortuneForDate(person.DateOfBirth));
		}
	}
}
