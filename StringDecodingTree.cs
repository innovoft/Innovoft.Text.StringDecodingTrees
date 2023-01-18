using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Innovoft.Text
{
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
		#endregion //Properties

		#region Methods
		public void ClearKeys()
		{
			Array.Clear(keys, 0, this.keys.Length);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Add(byte[] encoded, int offset, int length, string value)
		{
			Add(this, encoded, offset, length, value);
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
		#endregion //Methods
	}
}
