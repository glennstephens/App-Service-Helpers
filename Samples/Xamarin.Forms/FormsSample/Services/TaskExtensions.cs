using System;
using System.Threading.Tasks;

namespace FormsSample
{
	public static class TaskExtensions
	{
		public static void IgnoreResult(this Task t)
		{
		}
	}
}
