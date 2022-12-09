using System.Collections.Generic;

namespace AdventOfCode22.Day_07
{
	public class Node
	{
		public Node Parent { get; init; }
		
		public List<Node> Children { get; init; }

		public string Name { get; init; }

		public int Size { get; set; }

		public ItemType Type { get; init; }

		public Node(Node parent)
		{
			Parent = parent;
			Children = new();
		}
		public Node(string name, Node parent = null) : this(parent)
		{
			Name = name;
			Type = ItemType.Folder;
		}
		
		public Node(string name, int size, Node parent = null) : this(parent)
		{
			Name = name;
			Size = size;
			Type = ItemType.File;
		}

		public enum ItemType
		{
			Folder,
			File,
		}
	}
}