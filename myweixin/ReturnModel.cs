using Newtonsoft.Json;

namespace GetHtmlPages
{
    class ReturnModel
    {
        public string title { get; set; }
        public string doi { get; set; }
        public string pmid { get; set; }
        public string ads_id { get; set; }
        public string altmetric_jid { get; set; }
        public string journal { get; set; }
        public string type { get; set; }
        public string altmetric_id { get; set; }
        public string schema { get; set; }
        public bool is_oa { get; set; }

        public int cited_by_fbwalls_count { get; set; }
        public int cited_by_feeds_count { get; set; }
        public int cited_by_gplus_count { get; set; }
        public int cited_by_posts_count { get; set; }
        public int cited_by_tweeters_count { get; set; }
        public int cited_by_accounts_count { get; set; }
        public int last_updated { get; set; }
        public float score { get; set; }
        public string url { get; set; }
        public int added_on { get; set; }
        public int published_on { get; set; }
        public int readers_count { get; set; }
        public string details_url { get; set; }
        public string[] tq { get; set; }
        public string[] issns { get; set; }
        public string[] subjects { get; set; }
        public Cohorts cohorts { get; set; }
        public Context context { get; set; }
        public History history { get; set; }
        public Readers readers { get; set; }
        public Images images { get; set; }
    }

    public class Cohorts
    {
        public int sci { get; set; }
        public int pub { get; set; }
        public int com { get; set; }
        public int doc { get; set; }
    }

    public class Context
    {
        public All all { get; set; }
        public All journal { get; set; }
        public All similar_age_3m { get; set; }
        public All similar_age_journal_3m { get; set; }
    }

    public class All
    {
        public int count { get; set; }
        public string mean { get; set; }
        public int rank { get; set; }
        public int pct { get; set; }
        public int higher_than { get; set; }
    }

    public class History
    {
        [JsonProperty(PropertyName = "1y")]
        public float y1 { get; set; }

        [JsonProperty(PropertyName = "6m")]
        public float m6 { get; set; }

        [JsonProperty(PropertyName = "3m")]
        public float m3 { get; set; }

        [JsonProperty(PropertyName = "1m")]
        public float m1 { get; set; }

        [JsonProperty(PropertyName = "1w")]
        public float w1 { get; set; }

        [JsonProperty(PropertyName = "6d")]
        public float d6 { get; set; }

        [JsonProperty(PropertyName = "5d")]
        public float d5 { get; set; }

        [JsonProperty(PropertyName = "4d")]
        public float d4 { get; set; }

        [JsonProperty(PropertyName = "3d")]
        public float d3 { get; set; }

        [JsonProperty(PropertyName = "2d")]
        public float d2 { get; set; }

        [JsonProperty(PropertyName = "1d")]
        public float d1 { get; set; }

        public float at { get; set; }
    }

    public class Readers
    {
        public string citeulike { set; get; }
        public string mendeley { set; get; }
        public string connotea { set; get; }
    }
    public class Images
    {
        public string small { set; get; }
        public string medium { set; get; }
        public string large { set; get; }
    }
}