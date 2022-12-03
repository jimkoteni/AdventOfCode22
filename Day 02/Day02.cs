using System;
using System.IO;
using System.Linq;

namespace AdventOfCode22.Day_02
{
	public class Day02 : DayBase
	{
		protected override string RunFirstIssue()
		{
			return File.ReadAllLines(fileName)
				.Select(item =>
				{
					var equipments = item.Split(" ");
					return GetWinnerPoints(equipments[0], equipments[1]) + GetEquipmentPoints(equipments[1]);
				})
				.Sum()
				.ToString();
		}

		protected override string RunSecondIssue()
		{
			return File.ReadAllLines(fileName)
				.Select(item =>
				{
					var equipments = item.Split(" ");
					var newEquipment = GetNewEquipment(equipments[0], equipments[1]);
					return GetWinnerPoints(equipments[0], newEquipment) + GetEquipmentPoints(newEquipment);
				})
				.Sum()
				.ToString();
		}

		private static string GetNewEquipment(string opponent, string result)
			=> (opponent, result) switch
			{
				("A", "X") => "Z",
				("A", "Y") => "X",
				("A", "Z") => "Y",
				("B", "X") => "X",
				("B", "Y") => "Y",
				("B", "Z") => "Z",
				("C", "X") => "Y",
				("C", "Y") => "Z",
				("C", "Z") => "X",
				_ => throw new ArgumentOutOfRangeException()
			};

		private static int GetWinnerPoints(string x, string y)
			=> (x, y) switch
			{
				("A", "X") => 3,
				("A", "Y") => 6,
				("A", "Z") => 0,
				("B", "X") => 0,
				("B", "Y") => 3,
				("B", "Z") => 6,
				("C", "X") => 6,
				("C", "Y") => 0,
				("C", "Z") => 3,
				_ => throw new ArgumentOutOfRangeException()
			};

		private static int GetEquipmentPoints(string equipment)
			=> equipment switch
			{
				"X" => 1,
				"Y" => 2,
				"Z" => 3,
				_ => throw new ArgumentOutOfRangeException(nameof(equipment), equipment, null)
			};
	}
}