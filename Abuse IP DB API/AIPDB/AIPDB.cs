using System.Net;
using System.Text;

namespace AIPDBAPI
{
    public class AIPDB
    {

        private string API_KEY = string.Empty;

        // Run On Instance Creation
        public AIPDB(string APIKey) {
            API_KEY = APIKey;
        }

        public enum Categories
        {
            Fraud_Orders = 3,
            DDoS_Attack = 4,
            Open_Proxy = 9,
            Web_Spam = 10,
            Email_Spam = 11,
            Port_Scan = 14,
            Brute_Force = 18,
            Bad_Web_Bot = 19,
            Exploited_Host = 20,
            Web_App_Attack = 21,
            SSH = 22,
            IoT_Targeted = 23
        };

        public bool CheckIP(string IP)
        {
            // Make WEb Request
            HttpWebRequest HWR = (HttpWebRequest)WebRequest.Create(string.Format(
                "https://www.abuseipdb.com/check/{0}/json?key={1}", IP, API_KEY));
            HttpWebResponse HWRR = (HttpWebResponse)HWR.GetResponse();

            // Get The Response Stream And Read 16KB Of Data
            System.IO.Stream S = HWRR.GetResponseStream();
            byte[] data = new byte[8192];
            S.Read(data, 0, 8192);
            S.Close();

            // Parse Data For Final Report
            string Text = Encoding.UTF8.GetString(data).Replace("\0", string.Empty);
            if (Text == "[]")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool ReportIP(string IP, string COMMENT, params Categories[] Categories)
        {
            // Parse input
            string FINAL = string.Empty;
            foreach (Categories CODE in Categories)
            {
                FINAL = FINAL + (int)CODE + ",";
            }

            // Remove Last Comma
            FINAL = FINAL.Remove(FINAL.Length - 1);

            // Make Web Request
            HttpWebRequest HWR = (HttpWebRequest)WebRequest.Create(string.Format(
                "https://www.abuseipdb.com/report/json?key={0}&category={1}&comment={2}&ip={3}", API_KEY, FINAL, COMMENT, IP));
            HttpWebResponse HWRR = (HttpWebResponse)HWR.GetResponse();

            return false;
        }
    }
}
