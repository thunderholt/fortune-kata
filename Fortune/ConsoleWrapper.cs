using System;

namespace Fortune
{
	public class ConsoleWrapper : IConsole
	{
		public void Write(string value)
		{
			Console.Write(value);
		}

		public void WriteLine(string value, params string[] args)
		{
			Console.WriteLine(value, args);
		}

		public string ReadLine()
		{
			return Console.ReadLine();
		}

		public string Prompt(string message)
		{
			Console.Write(message);
			Console.Write(" ");
			return Console.ReadLine();
		}
	}
}
