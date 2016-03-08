using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;


namespace XmJoseUnit.Json
{
	[TestFixture ()]
	public class BaseObjectTest
	{
		public class Profile : XmJose.Json.BaseObject
		{
			public string name {get;set; }
			public int age {get;set; }
		}

		[Test ()]
		public void TestCaseDict()
		{
			var d = new Dictionary<string, object> {
				{ "name", "alice"}, {"age", 35}};

			var p = Profile.FromDict<Profile>(d);
			Assert.AreEqual(p.name, d["name"]);
			Assert.AreEqual(p.age, d["age"]);
		}

		[Test ()]
		public void TestCaseAdditionals(){
			var d = new Dictionary<string, object> {
				{ "name", "alice"}, {"age", 35}, 
				{"phone#mobile", "090-9999-9999"}};
			var p = Profile.FromDict<Profile>(d);
			Assert.AreEqual (p ["phone#mobile"].ToString(), d ["phone#mobile"]);

			var p2 = Profile.FromJson<Profile> (p.ToJson ());
			Assert.AreEqual (
				p ["phone#mobile"].ToString(), 
				p2["phone#mobile"].ToString());


			var d2 = p2.ToDict ();
			Assert.AreEqual (d ["phone#mobile"], d2 ["phone#mobile"].ToString ());
		}
	}
}

