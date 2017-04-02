using System.Net;
using System.Text;

namespace AIPDBAPI
{
    public class AIPDB
    {
        /// <summary>
        /// Much nicer categorizing of the things you can report for!
        /// </summary>
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

        /// <summary>
        /// Returns true if IP has beeen reported before or false if it hasen't!
        /// </summary>
        /// <param name="API_KEY">API Key From AIPDB.com</param>
        /// <param name="IP">IP To Check</param>
        /// <returns></returns>
        public bool CheckIP(string API_KEY, string IP)
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

        /// <summary>
        /// Reports IP for selected reasons.
        /// </summary>
        /// <param name="API_KEY">API Key From AIPDB.com</param>
        /// <param name="IP">IP To Report</param>
        /// <param name="COMMENT">Any other commments for why it's being reported.</param>
        /// <param name="Categories">Categories to report for!</param>
        /// <returns></returns>
        public static bool ReportIP(string API_KEY, string IP, string COMMENT, params Categories[] Categories)
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
