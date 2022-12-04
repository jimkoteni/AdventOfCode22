using System.IO;
using System.Linq;

namespace AdventOfCode22.Day_04
{
	public class Day04 : DayBase
	{
		protected override string RunFirstIssue()
		{
			return File.ReadAllLines(fileName)
				.Select(line =>
				{
					var (first, second) = ParseData(line);

					if (first[0] <= second[0] && first[1] >= second[1] ||
					    first[0] >= second[0] && first[1] <= second[1])
						return 1;
					else
						return 0;
				}).Sum().ToString();
		}

		protected override string RunSecondIssue()
		{
			return File.ReadAllLines(fileName)
				.Select(line =>
				{
					var (first, second) = ParseData(line);

					if (first[1] < second[0] || first[0] > second[1])
						return 0;
					else
						return 1;
				}).Sum().ToString();
		}
		
		private (int[], int[]) ParseData(string line)
		{
			var elves = line.Split(',');
			return (
				elves[0].Split('-').Select(int.Parse).ToArray(),
				elves[1].Split('-').Select(int.Parse).ToArray());
		}
	}
}