using System;
using System.Collections.Generic;
using System.Linq;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Linq;

namespace XmJose.Json
{
	public class BaseObject
	{
		[JsonExtensionData]
		public IDictionary<string, JToken> Addtionals = new Dictionary<string, JToken>();
		public JToken this[string name]
		{
			get { return this.Addtionals[name]; }
			set { this.Addtionals[name] = value; }
		}


		public T ConvertAddtionals<T>() where T: BaseObject
		{
			return FromJson<T>(this.AddtionalsToJson());
		}
			
		public string AddtionalsToJson()
		{
			return JsonConvert.SerializeObject(this.Addtionals);
		}

		public void MergeObject(BaseObject obj)
		{
			this.Addtionals = ((Dictionary<string, JToken>)this.Addtionals).Merge(obj.ToDict());
		}

		public virtual string ToJson()
		{
			return JsonConvert.SerializeObject(this,
				Formatting.None,
				new JsonSerializerSettings
				{
					TypeNameHandling = TypeNameHandling.None,
					DefaultValueHandling = DefaultValueHandling.Ignore,
				});
		}

		public string ToBase64Url()
		{
			return this.ToJson().ToBase64Url();
		}

		public Dictionary<string, JToken> ToDict()
		{
			return JsonConvert.DeserializeObject<Dictionary<string, JToken>>(this.ToJson());
		}


		public bool IsSameObject(BaseObject obj)
		{
			var d1 = this.ToDict();
			var d2 = obj.ToDict();

			// Dictionary<string, JToken> => compare after ToString() 
			return d1.All(x => x.Value.ToString() == d2[x.Key].ToString());
		}

		public static T FromJson<T>(string jsonstr) where T : BaseObject
		{
			var ret = JsonConvert.DeserializeObject<T>(jsonstr,
				new JsonSerializerSettings
				{
					NullValueHandling = NullValueHandling.Ignore
				}
			);

			return ret;
		}

		public static T FromDict<T>(Dictionary<string,string> dict) where T: BaseObject
		{
			return FromJson<T> (
				JsonConvert.SerializeObject(dict));
		}

		public static T FromDict<T>(Dictionary<string,object> dict) where T: BaseObject
		{
			return FromJson<T> (
				JsonConvert.SerializeObject(dict));
		}

		public static T FromBase64Url<T>(string b64url)  where T: BaseObject
		{
			var json = b64url.ToStringFromBase64Url ();
			return FromJson<T> (json);
		}

		public static T FromQueryString<T>(string query) where T: BaseObject
		{
			return FromDict<T> (query.ParseQuery ());
		}

		[JsonIgnore]
		public DateTime LastUpdated{ get; set; }

		public static T Cascade<T>(params BaseObject[] joseobjects) where T:BaseObject
		{
			JObject result = new JObject();
			foreach (BaseObject obj in joseobjects)
			{
				if (obj == null)
					continue;

				JObject parsed = JObject.Parse( obj.ToJson() );
				foreach (var property in parsed)
					result[property.Key] = property.Value;
			}
			return  FromJson<T>(result.ToString());
		}

		public virtual string ToQueryString()
		{   
			// PCL doens't support HttpUtility.UrlEncode
			var vars = ToDict().Select(
				value =>
				String.Format("{0}={1}",
					Uri.EscapeUriString(value.Key),
					Uri.EscapeUriString(value.Value.ToString())
				)   
			).ToArray<string>();
			return string.Join("&",vars);
		}   

	}
}

