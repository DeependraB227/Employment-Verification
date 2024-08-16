using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using Utility.APIConnect;
using Utility.Common_Methods;

namespace ERPUI.Areas.HR.Controllers
{
    [HandleError]
    [RouteArea("Employee", AreaPrefix = "")]
    [RoutePrefix("Employee")]
    [CustomExceptionFilter]

    public class EmployeeController : Controller
    {
       WebAPIConnect.APIConnect.EmployeeConnect apiConnect = new WebAPIConnect.APIConnect.EmployeeConnect();

        // GET: Employee
        public ActionResult Index()
        {
            if (WebConfigKeys.SessionStateResult == WebConfigKeys.ValidateSession())
            {
                return View();
            }
            else
            {
                return Logout();
            }
        }

        #region Logout
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("Logout")]
        public ActionResult Logout()
        {
            return RedirectToRoute("LogOut");
        }
        #endregion

        #region Employment Verification
        [Route("EmploymentVerification")]
        public async Task<ActionResult> EmploymentVerification()
        {
            //Check Session
            if (WebConfigKeys.SessionStateResult == WebConfigKeys.ValidateSession())
            {
                await Task.Delay(0);
                return View();
            }
            else
            {
                return null;
            }
        }

        public async Task<JsonResult> getEmploymentVerificationResult(int EmployeeID, string CompanyName, string VerificationCode)
        {
            //Check Session
            if (WebConfigKeys.SessionStateResult == WebConfigKeys.ValidateSession())
            {
                Tuple<int, string, string > tplData = new Tuple<int, string, string>(EmployeeID, CompanyName, VerificationCode);
                string Result =  apiConnect.getEmploymentVerificationResult(tplData).Result;
                await Task.Delay(0);
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                await Task.Delay(0);
                return null;
            }
        }

        #endregion
    }
}