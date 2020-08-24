using System;

namespace Fortune
{
	public interface IFortuneCookie
	{
		string GetTodaysFortune();
		string GetFortuneForDate(DateTimeOffset date);
	}
}
