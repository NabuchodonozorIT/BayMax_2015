using OpenFile;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows;

namespace iTwitter
{
    public class Tweeter
    {
        public int CounterTweets = 0;
        ReadConfig readConfig = new ReadConfig();

        public string SourceConfig { get; set; }

        public List<TweetsUser> GetTwitts(string userName, int count, bool choiceTypSearch)
        {
            string accessToken = GetAccessToken().Result;
            HttpClient httpClient = new HttpClient();
            List<TweetsUser> twittsUserList = new List<TweetsUser>();

            HttpRequestMessage requestUserTimeline;

            switch (choiceTypSearch)
            {
                case false:
                    requestUserTimeline = new HttpRequestMessage(HttpMethod.Get, string.Format("https://api.twitter.com/1.1/statuses/user_timeline.json?count={0}&screen_name={1}&trim_user=1&exclude_replies=1", count, userName));
                    break;
                default:
                    requestUserTimeline = new HttpRequestMessage(HttpMethod.Get, string.Format("https://api.twitter.com/1.1/search/tweets.json?count={0}&q=%23{1}", count, userName));
                    break;
            } 

            requestUserTimeline.Headers.Add("Authorization", "Bearer " + accessToken);

            try
            {
            var responseUserTimeLine = httpClient.SendAsync(requestUserTimeline).Result;           

            JavaScriptSerializer serializer = new JavaScriptSerializer();

            dynamic twitts = serializer.Deserialize<List<Tweets>>(responseUserTimeLine.Content.ReadAsStringAsync().Result);
               
            
                foreach (Tweets token in twitts)
                {
                    TweetsUser twittsUser = new TweetsUser();
                    twittsUser.TimeCreateTwitt = token.created_at;
                    twittsUser.Twitt = token.text;
                    twittsUser.ID = CounterTweets;
                    twittsUserList.Add(twittsUser);
                    CounterTweets++;
                }
            }
            catch (AggregateException)
            { 
                MessageBox.Show("error conection, chack your web connection"); 
            }
            catch (Exception) 
            {
                MessageBox.Show("error conection, chack your token"); 
            }
            CounterTweets = 0;
            return twittsUserList;
        }
        public async Task<string> GetAccessToken()
        {
            try
            {
                readConfig.xmlUrl = SourceConfig;
                AccessTokenTwiitXml AccessTokenTwiit = readConfig.GetAccessTokenTwiitXml();
                string OAuthConsumerKey = AccessTokenTwiit.OAuthConsumerKey;
                string OAuthConsumerSecret = AccessTokenTwiit.OAuthConsumerSecret;

                HttpClient httpClient = new HttpClient();
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "https://api.twitter.com/oauth2/token");
                var customerInfo = Convert.ToBase64String(new UTF8Encoding().GetBytes(OAuthConsumerKey + ":" + OAuthConsumerSecret));
                request.Headers.Add("Authorization", "Basic " + customerInfo);
                request.Content = new StringContent("grant_type=client_credentials", Encoding.UTF8, "application/x-www-form-urlencoded");

                var response = httpClient.SendAsync(request).Result;

                string json = response.Content.ReadAsStringAsync().Result;
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                dynamic item = serializer.Deserialize<object>(json);
                return item["access_token"];
            }              
            catch (Exception)
            {
                MessageBox.Show(string.Format("chack the paramiters create token API Twiit"));
            }
            return null;
        }
    }
}