using System;
using Newtonsoft.Json;

namespace TwitterApi
{
	public record Tweet
	{
        [JsonProperty("id")]
		public long Id { get; set; }

        [JsonProperty("text")]
		public string Text { get; set; }

        [JsonProperty("user")]
		public User User { get; set; }
	}

	public record User
    {
        [JsonProperty("name")]
		public string Name { get; set; }
    }
}

