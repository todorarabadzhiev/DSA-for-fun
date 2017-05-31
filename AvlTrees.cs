using System;

class Node<T>
where T : IComparable<T>
{
	private Node<T> left;
	private Node<T> right;

	private T value;

	private int height;
	private int count;

	public Node(T value)
	{
		this.value = value;
		this.height = 1;
		this.count = 1;
	}

	public static int GetHeight(Node<T> node)
	{
		return node == null ? 0 : node.height;
	}

	public static int GetCount(Node<T> node)
	{
		return node == null ? 0 : node.count;
	}

	private int Balance()
	{
		return GetHeight(this.left) - GetHeight(this.right);

		//if(this.left == null && this.right == null)
		//{
		//	return 0;
		//}
		//if(this.left == null)
		//{
		//	return -this.right.height;
		//}
		//if(this.right == null)
		//{
		//	return this.left.height;
		//}
		//return this.left.height - this.right.height;
	}

	private void Update()
	{
		this.height = Math.Max(GetHeight(this.left), GetHeight(this.right)) + 1;
		this.count = GetCount(this.left) + GetCount(this.right) + 1;
	}

	public static bool Contains(Node<T> node, T value)
	{
		while(node != null)
		{
			int cmp = value.CompareTo(node.value);
			if(cmp == 0)
			{
				return true;
			}
			if(cmp < 0)
			{
				node = node.left;
			}
			else
			{
				node = node.right;
			}
		}

		return false;
	}

	private static Node<T> RotateRight(Node<T> root)
	{
		var newRoot = root.left;
		var lr = newRoot.right;
		newRoot.right = root;
		root.left = lr;

		root.Update();
		newRoot.Update();
		return newRoot;
	}

	private static Node<T> RotateLeft(Node<T> root)
	{
		var newRoot = root.right;
		var rl = newRoot.left;
		newRoot.left = root;
		root.right = rl;

		root.Update();
		newRoot.Update();
		return newRoot;
	}

	public static Node<T> Add(Node<T> node, T value)
	{
		if(node == null)
		{
			return new Node<T>(value);
		}

		if(value.CompareTo(node.value) < 0)
		{
			node.left = Add(node.left, value);
			if(node.Balance() > 1)
			{
				if(node.left.Balance() < 0)
				{
					node.left = RotateLeft(node.left);
				}
				node = RotateRight(node);
			}
		}
		else
		{
			node.right = Add(node.right, value);
			if(node.Balance() < -1)
			{
				if(node.right.Balance() > 0)
				{
					node.right = RotateRight(node.right);
				}
				node = RotateLeft(node);
			}
		}

		node.Update();

		return node;
	}

	public static Node<T> Remove(Node<T> root, T value)
	{
		if(root == null)
		{
			return null;
		}

		int cmp = value.CompareTo(root.value);
		if(cmp == 0)
		{
			if(root.left == null && root.right == null)
			{
				return null;
			}

			if(root.left == null)
			{
				return root.right;
			}

			if(root.right == null)
			{
				return root.left;
			}

			Node<T> nextInOrder;
			root.right = RemoveLeftmost(root.right, out nextInOrder);

			nextInOrder.left = root.left;
			nextInOrder.right = root.right;

			nextInOrder.Update();
			return nextInOrder;
		}

		if(cmp < 0)
		{
			root.left = Remove(root.left, value);
			if(root.Balance() < -1)
			{
				if(root.right.Balance() > 0)
				{
					root.right = RotateRight(root.right);
				}
				root = RotateLeft(root);
			}
		}
		else
		{
			root.right = Remove(root.right, value);
			if(root.Balance() > 1)
			{
				if(root.left.Balance() < 0)
				{
					root.left = RotateLeft(root.left);
				}
				root = RotateRight(root);
			}
		}

		root.Update();
		return root;
	}

	private static Node<T> RemoveLeftmost(Node<T> root, out Node<T> nextInOrder)
	{
		if(root.left == null)
		{
			nextInOrder = root;
			return root.right;
		}

		root.left = RemoveLeftmost(root.left, out nextInOrder);
		if(root.Balance() < -1)
		{
			if(root.right.Balance() > 0)
			{
				root.right = RotateRight(root.right);
			}
			root = RotateLeft(root);
		}

		root.Update();
		return root;
	}

	public static T AtIndex(Node<T> node, int index)
	{
		int currentIndex = GetCount(node.left);
		while(currentIndex != index)
		{
			if(currentIndex < index)
			{
				node = node.left;
			}
			else
			{
				node = node.right;
				index -= currentIndex + 1;
			}
			currentIndex = GetCount(node.left);
		}

		return node.value;
	}

	public static int IndexOf(Node<T> node, T value)
	{
		int index = 0;

		int cmp = value.CompareTo(node.value);
		while(cmp != 0)
		{
			if(cmp < 0)
			{
				node = node.left;
			}
			else
			{
				index += GetCount(node.left) + 1;
				node = node.right;
			}

			if(node == null)
			{
				return -1;
			}

			cmp = value.CompareTo(node.value);
		}

		return index;
	}
}

class AvlTree<T>
where T : IComparable<T>
{
	private Node<T> root;

	public bool Contains(T value)
	{
		return Node<T>.Contains(root, value);
	}

	public void Add(T value)
	{
		root = Node<T>.Add(root, value);
	}

	public void Remove(T value)
	{
		root = Node<T>.Remove(root, value);
	}

	public int Count()
	{
		return Node<T>.GetCount(root);
	}
	public int Height()
	{
		return Node<T>.GetHeight(root);
	}

	public T AtIndex(int index)
	{
		return Node<T>.AtIndex(root, index);
	}

	public int IndexOf(T value)
	{
		return Node<T>.IndexOf(root, value);
	}
}

class Program
{
	static void Main()
	{
		var tree = new AvlTree<int>();

		Console.WriteLine("Count: " + tree.Count());
		Console.WriteLine("Height: " + tree.Height());

		for(int i = 0; i < 10000; ++i)
		{
			tree.Add(i);
		}

		Console.WriteLine("Count: " + tree.Count());
		Console.WriteLine("Height: " + tree.Height());

		for(int i = 0; i < 10000; ++i)
		{
			if(!tree.Contains(i))
			{
				throw new Exception("Doesn't work");
			}
		}
		
		for(int i = 0; i < 10000; ++i)
		{
			tree.Remove(i);
		}

		Console.WriteLine("Count: " + tree.Count());
		Console.WriteLine("Height: " + tree.Height());

		for(int i = 0; i < 10000; ++i)
		{
			if(tree.Contains(i))
			{
				throw new Exception("Doesn't work");
			}
		}

		Console.WriteLine("Works");
	}
}
