using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode22.Day_01
{
	public static class Day01
	{
		private static string fileName = "Day 01/input.txt";
		public static string Run()
		{
			// return GetTop();
			return GetTopThree();
		}

		private static string GetTop()
		{
			var max = 0;

			using var reader = new StreamReader(fileName);
			string? line;
			var elf = 0;
			while ((line = reader.ReadLine()) != null)
			{
				if (line == string.Empty)
				{
					if (elf > max)
						max = elf;
					elf = 0;
				}
				else
				{
					var calories = int.Parse(line);
					elf += calories;
				}
			}

			return max.ToString();
		}

		private static string GetTopThree()
		{
			var elves = new List<int>();

			File.ReadAllLines(fileName)
				.Aggregate(0, (elf, item) =>
				{
					if (item == string.Empty)
					{
						elves.Add(elf);
						elf = 0;
					}
					else
					{
						elf += int.Parse(item);
					}

					return elf;
				});

			return elves
				.OrderByDescending(x => x)
				.Take(3)
				.Sum()
				.ToString();
		}
		
	}
}