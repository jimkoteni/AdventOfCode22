using System.IO;
using System.Linq;

namespace AdventOfCode22.Day_08
{
	public class Day08 : DayBase
	{
		protected override string RunFirstIssue()
		{
			var trees = GetTrees();
			var visibility = new bool[trees.GetLength(0), trees.GetLength(1)];
			
			// from left to right
			for (var i = 0; i < trees.GetLength(0); i++)
			{
				var max = -1;
				for (var j = 0; j < trees.GetLength(1); j++)
				{
					SetVisibility(trees, visibility, i, j, ref max);
				}
			}
			
			// from right to left
			for (var i = 0; i < trees.GetLength(0); i++)
			{
				var max = -1;
				for (var j = trees.GetLength(1) - 1; j >= 0; j--)
				{
					SetVisibility(trees, visibility, i, j, ref max);
				}
			}
			
			// from top to bottom
			for (var i = 0; i < trees.GetLength(0); i++)
			{
				var max = -1;
				for (var j = 0; j < trees.GetLength(1); j++)
				{
					SetVisibility(trees, visibility, j, i, ref max);
				}
			}

			// from bottom to top
			for (var i = 0; i < trees.GetLength(0); i++)
			{
				var max = -1;
				for (var j = trees.GetLength(1) - 1; j >= 0; j--)
				{
					SetVisibility(trees, visibility, j, i, ref max);
				}
			}

			var result = 0;
			for (var i = 0; i < visibility.GetLength(0); i++)
			{
				for (var j = 0; j < visibility.GetLength(1); j++)
				{
					if (visibility[i, j])
						result += 1;
				}
			}

			return result.ToString();
		}



		protected override string RunSecondIssue()
		{
			var trees = GetTrees();
			var max = 0;
			for (var i = 1; i < trees.GetLength(0) - 1; i++)
			{
				for (var j = 1; j < trees.GetLength(1) - 1; j++)
				{
					var value = GetTreeValue(trees, i, j, trees.GetLength(0), trees.GetLength(1)); 

					if (max < value)
						max = value;
				}
			}

			return max.ToString();
		}

		private int GetTreeValue(byte[,] trees, int i, int j, int iMax, int jMax)
		{
			var tree = trees[i, j];

			// to the left side
			var left = 0;
			for (var index = j - 1; index >= 0; index--)
			{
				if (trees[i, index] >= tree)
				{
					left += 1;
					break;
				}

				left += 1;
			}
			
			// to the right side
			var right = 0;
			for (var index = j + 1; index < jMax; index++)
			{
				if (trees[i, index] >= tree)
				{
					right += 1;
					break;
				}

				right += 1;
			}
			
			// to the up
			var up = 0;
			for (var index = i - 1; index >= 0; index--)
			{
				if (trees[index, j] >= tree)
				{
					up += 1;
					break;
				}

				up += 1;
			}
			
			// to the down
			var down = 0;
			for (var index = i + 1; index < iMax; index++)
			{
				if (trees[index, j] >= tree)
				{
					down += 1;
					break;
				}

				down += 1;
			}

			return left * right * up * down;
		}


		private byte[,] GetTrees()
		{
			var lines = File.ReadAllLines(fileName);

			var breadth = lines.First().Length;
			var depth = lines.Length;

			var array = new byte[breadth, depth];

			for (var i = 0; i < lines.Length; i++)
			{
				for (var j = 0; j < lines[i].Length; j++)
				{
					array[i, j] = (byte)((byte)lines[i][j] - (byte)'0');
				}
			}

			return array;
		}

		private static void SetVisibility(byte[,] trees, bool[,] visibility, int i, int j, ref int max)
		{
			if (trees[i, j] > max)
			{
				max = trees[i, j];
				visibility[i, j] = true;
			}
		}
	}
}