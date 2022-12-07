using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode22.Day_06
{
	public class Day06 : DayBase
	{
		protected override string RunFirstIssue()
		{
			return GetDistinctSequenceIndex(4);
		}

		protected override string RunSecondIssue()
		{
			return GetDistinctSequenceIndex(14);
		}
		
		private string GetDistinctSequenceIndex(int sequenceLength)
		{
			var source = File.ReadAllLines(fileName).Single();

			for (var i = 0; i < source.Length - sequenceLength; i++)
			{
				var slice = new HashSet<char>(source[i .. (i + sequenceLength)]);

				if (slice.Count == sequenceLength)
					return (i + sequenceLength).ToString();
			}

			return 0.ToString();
		}
	}
}