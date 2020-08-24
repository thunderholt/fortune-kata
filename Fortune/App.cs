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
			_console.Write("What's your name? ");
			var name = _console.ReadLine();

			_console.Write("When were you born (dd/mm/yyyy)? ");
			_console.ReadLine();

			var person = new Person {Name = name};

			_console.WriteLine(
				"Hi {0}!\nYour fortune for today is: {1}",
				person.Name,
				_fortuneCookie.GetTodaysFortune());
		}
	}
}
