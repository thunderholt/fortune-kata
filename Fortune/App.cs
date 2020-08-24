using System;

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

			_console.Prompt("When were you born (dd/mm/yyyy)?");

			var person = new Person {Name = name};

			_console.WriteLine(
				"Hi {0}!\nYour fortune for today is: {1}",
				person.Name,
				_fortuneCookie.GetTodaysFortune());
		}
	}
}
