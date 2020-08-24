namespace Fortune
{
	public interface IConsole
	{
		void Write(string value);
		void WriteLine(string value, params string[] args);
		string ReadLine();
		string Prompt(string message);
	}
}
