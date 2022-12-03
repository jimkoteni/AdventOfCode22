using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode22.Day_03
{
	public class Day03 : DayBase
	{
		protected override string RunFirstIssue()
		{
			return File.ReadAllLines(fileName)
				.Select(item =>
				{
					var count = item.Length;
					var firstHalf = item[.. (count / 2)];
					var secondHalf = item[(count / 2) .. count];

					var set = new HashSet<char>();
					foreach (var i in firstHalf)
					{
						set.Add(i);
					}

					foreach (var i in secondHalf)
					{
						if (set.Contains(i))
						{
							return GetCharWorth(i);	
						}
					}

					return 0;
				})
				.Sum()
				.ToString();
		}

		protected override string RunSecondIssue()
		{
			var rucksacks = File.ReadAllLines(fileName);

			var results = new List<int>();

			for (var i = 0; i < rucksacks.Length; i += 3)
			{
				var set1 = new HashSet<char>();
				foreach (var ch in rucksacks[i])
				{
					set1.Add(ch);
				}
				
				var set2 = new HashSet<char>();
				foreach (var ch in rucksacks[i + 1])
				{
					set2.Add(ch);
				}

				foreach (var ch in rucksacks[i + 2])
				{
					if (set1.Contains(ch) && set2.Contains(ch))
					{
						results.Add(GetCharWorth(ch));
						break;
					}
				}
			}
			
			return results.Sum().ToString();
		}
		
		private static int GetCharWorth(char? duplicate)
		{
			if (!duplicate.HasValue)
				throw new ArgumentNullException();

			var code = Encoding.ASCII.GetBytes(new[] { duplicate.Value })[0];

			return code > 96 ? code - 96 : code - 64 + 26;
		}
	}
}