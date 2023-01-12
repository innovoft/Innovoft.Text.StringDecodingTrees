using System;
using System.Collections.Generic;
using System.Text;

using Innovoft.Text;

using BenchmarkDotNet.Attributes;

namespace Benchmarks
{
	[MemoryDiagnoser]
	public class StringCreationBenchmarks
	{
		#region Constants
		private const string text = "Decoding";
		#endregion //Constants

		#region Class Fields
		private static readonly byte[] encoded = Encoding.UTF8.GetBytes(text);
		private static readonly StringDecodingTree tree = new StringDecodingTree();
		#endregion //Class Fields

		#region Class Constructor
		static StringCreationBenchmarks()
		{
			tree.Add(encoded, 0, encoded.Length, text);
		}
		#endregion //Class Constructor

		[Benchmark]
		public string EncodingGeString()
		{
			return Encoding.UTF8.GetString(encoded, 0, encoded.Length);
		}

		[Benchmark]
		public string StringDecodingTreeGet()
		{
			return tree.GetValue(encoded, 0, encoded.Length);
		}
	}
}
