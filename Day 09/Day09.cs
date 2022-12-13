using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode22.Day_09
{
	public class Day09 : DayBase
	{
		protected override string RunFirstIssue()
		{
			Point head = new(0, 0);
			Point tail = new(0, 0);
			List<Point> visited = new() { new(0, 0) };

			foreach (var line in File.ReadAllLines(fileName))
			{
				var items = line.Split(" ");
				var direction = items[0];
				var stepCount = int.Parse(items[1]);

				for (var i = 0; i < stepCount; i++)
				{
					UpdateHead(head, direction);
					UpdateTail(head, tail, visited);
				}
			}

			return visited.Distinct().Count().ToString();
		}
		
		protected override string RunSecondIssue()
		{
			Point[] knots =
			{
				new (0, 0),
				new (0, 0),
				new (0, 0),
				new (0, 0),
				new (0, 0),
				new (0, 0),
				new (0, 0),
				new (0, 0),
				new (0, 0),
				new (0, 0),
			};
			List<Point> visited = new(){ new(0, 0) };
			
			foreach (var line in File.ReadAllLines(fileName))
			{
				var items = line.Split(" ");
				var direction = items[0];
				var stepCount = int.Parse(items[1]);

				for (var i = 0; i < stepCount; i++)
				{
					UpdateHead(knots[0], direction);

					for (var j = 1; j < knots.Length; j++)
					{
						UpdateTail(knots[j - 1], knots[j]);
					}
					
					visited.Add(new Point(knots[^1].X, knots[^1].Y));
				}
			}
			
			return visited.Distinct().Count().ToString();
		}

		private static void UpdateHead(Point head, string direction)
		{
			var (x, y) = HeadShift(direction);
			head.X += x;
			head.Y += y;
		}
		
		private static void UpdateTail(Point head, Point tail, ICollection<Point> visited = null)
		{
			var xDiff = head.X - tail.X;
			var yDiff = head.Y - tail.Y;

			if (Math.Abs(xDiff) < 2 && Math.Abs(yDiff) < 2)
				return;

			if (Math.Abs(xDiff) < Math.Abs(yDiff))
			{
				tail.X += xDiff;

				if (yDiff > 0)
					tail.Y += yDiff - 1;
				else
					tail.Y += yDiff + 1;
			}
			else
			{
				tail.Y += yDiff;

				if (xDiff > 0)
					tail.X += xDiff - 1;
				else
					tail.X += xDiff + 1;
			}

			visited?.Add(new Point(tail.X, tail.Y));
		}

		private static (int x, int y) HeadShift(string direction)
			=> direction switch
			{
				"R" => (1, 0),
				"L" => (-1, 0),
				"U" => (0, 1),
				"D" => (0, -1),
				_ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
			};

		class Point
		{
			public int X { get; set; }
			public int Y { get; set; }

			public Point(int x, int y)
			{
				X = x;
				Y = y;
			}

			public override bool Equals(object obj) => obj is Point p && X == p.X && Y == p.Y;

			public override int GetHashCode() => X.GetHashCode() ^ Y.GetHashCode();
		}
	}
}