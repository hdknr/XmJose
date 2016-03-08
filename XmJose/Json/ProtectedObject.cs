using System;

using Newtonsoft.Json;

namespace XmJose.Json
{
	public class ProtectedObject : BaseObject
	{
					
		/// <summary>
		/// Base64url Cache
		/// </summary>
		/// <value>The b64u.</value>
		[JsonIgnore]
		public string b64u {
			get;
			set;
		}
	}
}

