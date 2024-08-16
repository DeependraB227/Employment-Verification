using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Utility.APIConnect;

namespace WebAPIConnect.APIConnect
{
    public class EmployeeConnect
    {
        private LogActionFilter log = new LogActionFilter();
        private ExceptionConnect exconnect = new ExceptionConnect();

        #region getEmployment Verification Connect
        public async Task<string> getEmploymentVerificationResult(Tuple<int, string, string> tup)
        {
            string Result = string.Empty;

            try
            {
                HttpClient client = new HttpClient();
                HttpContext context = HttpContext.Current;
                string baseURL = WebConfigKeys.WebApiUrlHR;
                
                string json = JsonConvert.SerializeObject(tup);

                //var url = "http://172.0.0.1/api/Employee/" + "getEmploymentVerificationResult?EmployeeID=" + tup.Item1 + "&CompanyName=" + tup.Item2 + "&VerificationCode=" + tup.Item3 + "&DbName=" + EncryptAndDecrypt.Encrypt(context.Session["DbName"].ToString()) + "&DbPath=" + EncryptAndDecrypt.Encrypt(context.Session["DbPath"].ToString()) + "&DbUId=" + EncryptAndDecrypt.Encrypt(context.Session["DbUId"].ToString()) + "&DbPass=" + EncryptAndDecrypt.Encrypt(context.Session["DbPass"].ToString());
                var url = "http://172.0.0.1/api/Employee/" + "getEmploymentVerificationResult?jsonData=" + json ;
                client.Timeout = TimeSpan.FromHours(WebConfigKeys.ConnectionTimeOut);
                HttpResponseMessage response = client.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    var responsedata = response.Content.ReadAsStringAsync().Result;
                    Result = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(responsedata);
                }
                await Task.Delay(0);
            }
            catch (Exception exception)
            {
                log.OnActionExecuting("GetLastAttendanceRecord", "/Utility/APIConnect/HRConnect.cs", exception.StackTrace + exception.GetBaseException());
                exconnect.SaveException("GetLastAttendanceRecord", "Simple Service Error on: {0} " + exception.Message + exception.StackTrace, "UI", "/Utility/APIConnect/HRConnect.cs", "");
            }
            return Result;
        }
        #endregion      
    }
}
