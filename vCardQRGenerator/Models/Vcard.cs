using System;
using System.Text.Json.Serialization;
using CsvHelper.Configuration.Attributes;

namespace vCardQRGenerator.Models
{
	public class Vcard
	{
        [Optional]
        [JsonPropertyName("id")]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Optional]
        [JsonPropertyName("firstname")]
        public string FirstName { get; set; } = "";

        [Optional]
        [JsonPropertyName("lastname")]
        public string LastName { get; set; } = "";

        [Optional]
        [JsonPropertyName("personaltel")]
        public string? PersonalTel { get; set; } = "";

        [Optional]
        [JsonPropertyName("orgtitle")]
        public string? OrgTitle { get; set; } = "";

        [Optional]
        [JsonPropertyName("orgname")]
        public string? OrgName { get; set; } = "";

        [Optional]
        [JsonPropertyName("orgtel")]
        public string? OrgTel { get; set; } = "";

        [Optional]
        [JsonPropertyName("orgemail")]
        public string? OrgEmail { get; set; } = "";

        [Optional]
        [JsonPropertyName("orgurl")]
        public string? OrgUrl { get; set; } = "";

        [Optional]
        [JsonPropertyName("orgstreet")]
        public string? OrgStreet { get; set; } = "";

        [Optional]
        [JsonPropertyName("orgcity")]
        public string? OrgCity { get; set; } = "";

        [Optional]
        [JsonPropertyName("orgregion")]
        public string? OrgRegion { get; set; } = "";

        [Optional]
        [JsonPropertyName("orgpost")]
        public string? OrgPost { get; set; } = "";

        [Optional]
        [JsonPropertyName("orgcountry")]
        public string? OrgCountry { get; set; } = "";

        [Optional]
        [JsonPropertyName("birthday")]
        public string? Birthday { get; set; } = "";

        [Optional]
        [JsonPropertyName("gender")]
        public string? Gender { get; set; } = "";

        [Optional]
        [JsonPropertyName("homestreet")]
        public string? HomeStreet { get; set; } = "";

        [Optional]
        [JsonPropertyName("homecity")]
        public string? HomeCity { get; set; } = "";

        [Optional]
        [JsonPropertyName("homeregion")]
        public string? HomeRegion { get; set; } = "";

        [Optional]
        [JsonPropertyName("homepost")]
        public string? HomePost { get; set; } = "";

        [Optional]
        [JsonPropertyName("homecountry")]
        public string? HomeCountry { get; set; } = "";

        [Optional]
        [JsonPropertyName("hometel")]
        public string? HomeTel { get; set; } = "";

        [Optional]
        [JsonPropertyName("homeemail")]
        public string? HomeEmail { get; set; } = "";

        [Optional]
        [JsonPropertyName("homeurl")]
        public string? HomeUrl { get; set; } = "";


        [Optional]
        [JsonPropertyName("facebook")]
        public string? Facebook { get; set; } = "";

        [Optional]
        [JsonPropertyName("twitter")]
        public string? Twitter { get; set; } = "";

        [Optional]
        [JsonPropertyName("flickr")]
        public string? Flickr { get; set; } = "";

        [Optional]
        [JsonPropertyName("youtube")]
        public string? Youtube { get; set; } = "";

        [Optional]
        [JsonPropertyName("skype")]
        public string? Skype { get; set; } = "";
    }
}

