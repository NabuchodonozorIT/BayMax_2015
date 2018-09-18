namespace iTwitter
{
    public class TweetsUser
    {
        public int ID { get; set; }
        public string Twitt { get; set; }
        public string TimeCreateTwitt { get; set; }
    }

    public class Tweets
    {
        public string id { get; set; }
        public string id_str { get; set; }
        public string text { get; set; }
        public string Twitt { get; set; }
        public string source { get; set; }
        public string created_at { get; set; }
    }
}
