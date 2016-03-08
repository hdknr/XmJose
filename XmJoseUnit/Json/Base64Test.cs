using NUnit.Framework;
using System;
using XmJose.Json;

namespace XmJoseUnit.Json
{
	[TestFixture ()]
	public class Base64Test
	{
		/// <summary>
		/// JWS
		/// Appendix C.  Notes on Implementing base64url Encoding without Padding
		/// 
		/// https://tools.ietf.org/html/rfc7515#appendix-C
		/// </summary>
		[Test ()]
		public void TestCaseBase64Url ()
		{
			var src = new byte[]{3,236,255,224,193 };

			Assert.AreEqual (
				src.ToBase64Url (),		// Extension Methods
				"A-z_4ME");
		}
	}
}

