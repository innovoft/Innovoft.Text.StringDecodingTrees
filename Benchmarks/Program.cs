using System;
using System.Collections.Generic;
using System.Text;

using BenchmarkDotNet.Running;

namespace Benchmarks
{
	internal static class Program
	{
		private static void Main(string[] args)
		{
			BenchmarkRunner.Run(typeof(Program).Assembly);
		}
	}
}