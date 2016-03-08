using System;
using System.Collections.Generic;
using System.Linq;

namespace XmJose.Json
{
	public static class CollectionsExtensions
	{
		public static Dictionary<T1, T2> Merge<T1, T2>(
			this Dictionary<T1, T2> first, 
			Dictionary<T1, T2> second)
		{
			if (first == null) throw new ArgumentNullException("first");
			if (second == null) throw new ArgumentNullException("second");

			return first.Concat(second).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

		}
	}
}

