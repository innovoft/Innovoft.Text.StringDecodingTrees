using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

namespace Innovoft.Text
{
	[DebuggerDisplay("{Value}")]
	public sealed class StringDecodingTree
	{
		#region Fields
		private string value;
		private readonly StringDecodingTree[] keys = new StringDecodingTree[256];
		#endregion //Fields

		#region Constructors
		public StringDecodingTree()
		{
		}

		public StringDecodingTree(string value)
		{
			this.value = value;
		}
		#endregion //Constructors

		#region Properties
		public string Value { get => this.value; set => this.value = value; }

		public int Count
		{
			get
			{
				var count = 0;
				foreach (var key in keys)
				{
					if (key != null)
					{
						++count;
					}
				}
				return count;
			}
		}
		#endregion //Properties

		#region Methods
		public void ClearKeys()
		{
			Array.Clear(keys, 0, this.keys.Length);
		}

		public void ClearKey(byte key)
		{
			keys[key] = null;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Add(byte[] encoded, int offset, int length, string value)
		{
			Add(this, encoded, offset, length, value);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Add(byte[] encoded, string value)
		{
			Add(this, encoded, 0, encoded.Length, value);
		}

		public void SetValue(byte key, string value)
		{
			var node = keys[key];
			if (node == null)
			{
				node = new StringDecodingTree(value);
				keys[key] = node;
			}
			else
			{
				node.Value = value;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string GetValue(byte[] encoded, int offset, int length)
		{
			return GetValue(this, encoded, offset, length);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public StringDecodingTree GetTree(byte[] encoded, int offset, int length)
		{
			return GetTree(this, encoded, offset, length);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public StringDecodingTree GetTree(byte encoded)
		{
			return keys[encoded];
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public StringDecodingTree GetTreeNULL(byte[] encoded, int offset, int length)
		{
			return GetTreeNULL(this, encoded, offset, length);
		}

		public static void Add(StringDecodingTree tree, byte[] encoded, int offset, int length, string value)
		{
			while (true)
			{
				if (length <= 0)
				{
					tree.value = value;
					return;
				}
				var key = encoded[offset];
				var keys = tree.keys;
				var next = keys[key];
				if (next == null)
				{
					next = new StringDecodingTree();
					keys[key] = next;
				}
				tree = next;
				++offset;
				--length;
			}
		}

		public static string GetValue(StringDecodingTree tree, byte[] encoded, int offset, int length)
		{
			if (length <= 0)
			{
				return tree.Value;
			}
			while (true)
			{
				tree = tree.keys[encoded[offset]];
				--length;
				if (length <= 0)
				{
					return tree.value;
				}
				++offset;
			}
		}

		public static StringDecodingTree GetTree(StringDecodingTree tree, byte[] encoded, int offset, int length)
		{
			if (length <= 0)
			{
				return tree;
			}
			while (true)
			{
				tree = tree.keys[encoded[offset]];
				--length;
				if (length <= 0)
				{
					return tree;
				}
				++offset;
			}
		}

		public static StringDecodingTree GetTreeNULL(StringDecodingTree tree, byte[] encoded, int offset, int length)
		{
			if (length <= 0 || tree == null)
			{
				return tree;
			}
			while (true)
			{
				tree = tree.keys[encoded[offset]];
				--length;
				if (length <= 0 || tree == null)
				{
					return tree;
				}
				++offset;
			}
		}
		#endregion //Methods
	}
}
