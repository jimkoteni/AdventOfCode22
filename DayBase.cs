using System;

namespace AdventOfCode22
{
	public abstract class DayBase
	{
		protected string fileName => $"{ GetType().Name.Insert(3, " ")}/input.txt";
		
		public void Run()
		{
			Console.WriteLine($"--------- { GetType().Name } ---------");
			Console.WriteLine(RunFirstIssue());
			Console.WriteLine(RunSecondIssue());
		}

		protected abstract string RunFirstIssue();
		
		protected abstract string RunSecondIssue();
	}
}