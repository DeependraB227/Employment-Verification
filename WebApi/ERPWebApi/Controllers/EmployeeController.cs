using ERPModel.CommonMaster;
using ERPModel.Employee;
using ERPService.IRepository;
using ERPService.Repository;
using ERPUtility.Core;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace ERPWebApi.Controllers
{
    [Route]
    [RoutePrefix("api/Employee")]

    public class EmployeeController : ApiController
    {
        //ERPService.Repository.Employee EmployeeObj = new ERPService.Repository.Employee();
        CommonMethods common = new CommonMethods();

        private IEmployee EmployeeRepository;

        // Create constructor using Repository pattern
        public EmployeeController()
        {
            this.EmployeeRepository = new ERPService.Repository.Employee();
        }
        public EmployeeController(IEmployee EmployeeRepository)
        {
            this.EmployeeRepository = EmployeeRepository;
        }


        #region GetEmployeeNameDesignation
        [Route("GetEmployeeNameDesignation")]
        [AllowAnonymous]
        [HttpGet]
        //public async Task<string> getEmploymentVerificationResult(int? EmployeeID, string CompanyName, string VerificationCode, string DbName, string DbPath, string DbUId, string DbPass)
        public async Task<string> getEmploymentVerificationResult(string jsonData)
        {
            //Convert json Deserialize
            var json = JsonConvert.DeserializeObject<EmployeeSearchData>(jsonData);
            // Convert to a tuple
            Tuple<int?, string, string> tplData = new Tuple<int?, string, string>(json.EmployeeId, json.CompanyName, json.ValidationCode);
            //string json=JsonConvert.SerializeObject(tplData);
            string Result = string.Empty;
            Param Parameter = new Param();
            Parameter.DbName = "testingDB";
            Parameter.DbPath = "localhost";
            Parameter.DbUId = "backendUser";
            Parameter.DbPass ="dF9mC7sZ";
            try
            {
                //if (EmployeeID.HasValue)
                {
                    Result = EmployeeRepository.EmploymentVerification(tplData, Parameter).Result;
                }
                await Task.Delay(0);
            }
            catch (Exception exception)
            {
                common.SaveException("GetEmployeeNameDesignation", "Simple Service Error on: {0} " + exception.Message + exception.StackTrace, "API", "", "/ERPWebApi/Controllers/HRController.cs", EncryptAndDecrypt.Decrypt(DbName), EncryptAndDecrypt.Decrypt(DbPath), EncryptAndDecrypt.Decrypt(DbUId), EncryptAndDecrypt.Decrypt(DbPass));
            }
            return Result;
        }
        #endregion
    }
}
