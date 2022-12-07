using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode22.Day_05
{
	public class Day05 : DayBase
	{
		const string pattern = @"move (?<count>[0-9]+) from (?<from>[0-9]) to (?<to>[0-9])";

		protected override string RunFirstIssue()
		{
			var stacks = GetStacks();

			File.ReadAllLines(fileName)
				.Skip(10)
				.ToList()
				.ForEach(operation =>
				{
					var (count, from, to) = ParseOperation(operation);

					while (count-- > 0)
						stacks[to].Push(stacks[from].Pop());
				});

			return GetResult(stacks);
		}

		protected override string RunSecondIssue()
		{
			var stacks = GetStacks();

			File.ReadAllLines(fileName)
				.Skip(10)
				.ToList()
				.ForEach(operation =>
				{
					var (count, from, to) = ParseOperation(operation);

					var temp = new Stack<char>();

					while (count-- > 0)
						temp.Push(stacks[from].Pop());
					
					while(temp.Count > 0)
						stacks[to].Push(temp.Pop());
				});

			return GetResult(stacks);
		}

		private string GetResult(Stack<char>[] stacks)
			=> new (stacks.Select(stack => stack.Pop()).ToArray());
		
		private (int, int, int) ParseOperation(string operation)
		{
			var matches = Regex.Matches(operation, pattern)[0];
			var count = int.Parse(matches.Groups["count"].Value);
			var from = int.Parse(matches.Groups["from"].Value);
			var to = int.Parse(matches.Groups["to"].Value);

			return (count, from - 1, to - 1);
		}

		private Stack<char>[] GetStacks()
		{
			var lists = Enumerable.Range(0, 9)
				.Select(_ => new List<char>())
				.ToArray();

			File.ReadAllLines(fileName)
				.Take(8)
				.ToList()
				.ForEach(line =>
				{
					int CharIndex(int i) => 4 * i + 1;

					if (line[CharIndex(0)] != ' ') lists[0].Add(line[CharIndex(0)]);
					if (line[CharIndex(1)] != ' ') lists[1].Add(line[CharIndex(1)]);
					if (line[CharIndex(2)] != ' ') lists[2].Add(line[CharIndex(2)]);
					if (line[CharIndex(3)] != ' ') lists[3].Add(line[CharIndex(3)]);
					if (line[CharIndex(4)] != ' ') lists[4].Add(line[CharIndex(4)]);
					if (line[CharIndex(5)] != ' ') lists[5].Add(line[CharIndex(5)]);
					if (line[CharIndex(6)] != ' ') lists[6].Add(line[CharIndex(6)]);
					if (line[CharIndex(7)] != ' ') lists[7].Add(line[CharIndex(7)]);
					if (line[CharIndex(8)] != ' ') lists[8].Add(line[CharIndex(8)]);
				});
				
			return Enumerable.Range(0, 9)
				.Select(_ => new Stack<char>())
				.Select((stack, index) =>
				{
					for (var i = lists[index].Count - 1; i >= 0; i--)
						stack.Push(lists[index][i]);

					return stack;
				})
				.ToArray();
		}
	}
}