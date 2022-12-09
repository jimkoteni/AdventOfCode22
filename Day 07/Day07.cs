using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode22.Day_07
{
	public class Day07 : DayBase
	{
		protected override string RunFirstIssue()
		{
			var head = CreateTree();

			return GetFolders(head, new())
				.Where(f => f.Size < 100000)
				.Sum(f => f.Size)
				.ToString();
		}

		protected override string RunSecondIssue()
		{
			var head = CreateTree();

			var needSpace = 70000000 - 30000000;
			var needDelete = head.Size - needSpace;
			
			return GetFolders(head, new())
				.Where(f => f.Size > needDelete)
				.OrderBy(n => n.Size)
				.First()
				.Size
				.ToString();
		}
		
		private Node CreateTree()
		{
			Node head = new("/");

			foreach (var line in File.ReadAllLines(fileName).Skip(1))
			{
				if (line[0] == '$')
				{
					var items = line.Split(' ');
					if (items[1] == "cd")
					{
						head = items[2] == ".." ?
							head.Parent :
							head.Children.Single(c => c.Name == items[2]);
					}
				}
				else
				{
					var items = line.Split(' ');
					if (items[0] == "dir")
						head.Children.Add(new(items[1], head));
					else
						head.Children.Add(new(items[1], int.Parse(items[0]), head));
				}
			}

			while (head.Parent != null)
				head = head.Parent;

			head.Size = GetSize(head);

			return head;
		}
		
		private List<Node> GetFolders(Node head, List<Node> list)
		{
			if (head.Type == Node.ItemType.Folder)
				list.Add(head);

			if (head.Children.Any())
			{
				foreach (var child in head.Children)
					GetFolders(child, list);
			}
			
			return list;
		}

		private int GetSize(Node head)
		{
			var summary = 0;
			
			foreach (var child in head.Children)
			{
				if (child.Size == 0)
				{
					child.Size =  GetSize(child);
				}

				summary += child.Size;
			}

			return summary;
		}
	}
}