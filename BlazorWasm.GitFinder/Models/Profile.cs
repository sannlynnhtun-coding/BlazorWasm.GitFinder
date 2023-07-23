namespace BlazorWasm.GitFinder.Models
{
    public class Profile
    {
        public string login { get; set; }
        public int id { get; set; }
        public string node_id { get; set; }
        public string avatar_url { get; set; }
        public string gravatar_id { get; set; }
        public string url { get; set; }
        public string html_url { get; set; }
        public string followers_url { get; set; }
        public string following_url { get; set; }
        public string gists_url { get; set; }
        public string starred_url { get; set; }
        public string subscriptions_url { get; set; }
        public string organizations_url { get; set; }
        public string repos_url { get; set; }
        public string events_url { get; set; }
        public string received_events_url { get; set; }
        public string type { get; set; }
        public bool site_admin { get; set; }
        public string name { get; set; }
        public string company { get; set; }
        public string blog { get; set; }
        public string location { get; set; }
        public string email { get; set; }
        public string hireable { get; set; }
        public string bio { get; set; }
        public string twitter_username { get; set; }
        public int public_repos { get; set; }
        public int public_gists { get; set; }
        public int followers { get; set; }
        public int following { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }

    public static class NumberFormatter
    {
        public static string NumberToKilo(this int number)
        {
            string numStr = number.ToString();
            if (numStr.Length <= 3)
            {
                return numStr;
            }
            else if (numStr.Length >= 4 && numStr.Length <= 5)
            {
                return $"{numStr.Substring(0, numStr.Length - 3)}.{numStr.Substring(numStr.Length - 3, 1)}k";
            }
            else if (numStr.Length == 6)
            {
                return $"{numStr.Substring(0, numStr.Length - 3)}k";
            }
            else
            {
                return $"{numStr.Substring(0, numStr.Length - 6)}M";
            }
        }
    }
}
