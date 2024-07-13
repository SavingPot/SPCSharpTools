using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SP.Tools
{
	public static class AwaitExtensions
	{
		public static TaskAwaiter GetAwaiter(this TimeSpan timeSpan) => Task.Delay(timeSpan).GetAwaiter();
		public static TaskAwaiter GetAwaiter(this float timeSecond) => Task.Delay(TimeSpan.FromSeconds(timeSecond)).GetAwaiter();
		public static TaskAwaiter GetAwaiter(this int timeSecond) => Task.Delay(TimeSpan.FromSeconds(timeSecond)).GetAwaiter();
		public static TaskAwaiter GetAwaiter(this double timeSecond) => Task.Delay(TimeSpan.FromSeconds(timeSecond)).GetAwaiter();
	}
}
