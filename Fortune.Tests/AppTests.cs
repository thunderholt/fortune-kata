using FakeItEasy;
using NUnit.Framework;

namespace Fortune.Tests
{
    public class AppTests
    {
		private IFortuneCookie _fortuneCookie;
		private IConsole _console;
		private App _app;

		[SetUp]
		public void Setup()
		{
			_fortuneCookie = A.Fake<IFortuneCookie>();
			_console = A.Fake<IConsole>();
			_app = new App(_fortuneCookie, _console);
		}

		[Test]
		public void Run_ItPromptsTheUserToEnterTheirName()
		{
			// Act
			_app.Run();

			// Assert
			A.CallTo(() => _console.Write("What's your name? ")).MustHaveHappenedOnceExactly().Then(
				A.CallTo(() => _console.ReadLine()).MustHaveHappenedOnceExactly());
		}

		[Test]
		public void Run_GivenTheUserHasEnteredTheirName_ItGreetsTheUserAndTellsThemTheirFortune()
		{
			// Arrange
			A.CallTo(() => _console.ReadLine()).Returns("Jane Doe");
			A.CallTo(() => _fortuneCookie.GetTodaysFortune()).Returns("Certain bears do not harm humans. Today you will meet no such bears.");

			// Act
			_app.Run();

			// Assert
			A.CallTo(() => _console.WriteLine(
				"Hi {0}!\nYour fortune for today is: {1}",
				"Jane Doe",
				"Certain bears do not harm humans. Today you will meet no such bears."))
				.MustHaveHappenedOnceExactly();
		}
	}
}
