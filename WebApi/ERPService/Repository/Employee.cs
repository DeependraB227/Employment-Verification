using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ERPDal;
using ERPModel.CommonMaster;
using ERPService.IRepository;
using ERPUtility.Core;

namespace ERPService.Repository
{
    public class Employee : IEmployee , IDisposable
    {
        private bool disposed = false;
        CommonMethods common = new CommonMethods();
        private int MaxCommandTimeOut = System.Web.Configuration.WebConfigurationManager.AppSettings["MaxCommandTimeOut"].ToInt();

        #region Dispose
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    common.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

        #region Employment Varification
        public async Task<string> EmploymentVerification(Tuple<int?, string, string> tplData, Param Parameter) {
            try
            {
                string Result = string.Empty;
                List<Employee> Data = null;

                long EmpId = Convert.ToInt64(tplData.Item1);
                using (ApplicationEntities db = ApplicationEntities.CreateEntitiesSpecificDatabaseName(Parameter.DbName, Parameter.DbPath, Parameter.DbUId, Parameter.DbPass))
                {
                    Data = (from e in db.EmployeeMaster
                            where e.EMPLOYEE_ID == tplData.Item1 && e.COMPANY_NAME == tplData.Item2 && e.VARIFICATION_CODE == tplData.Item3
                            select EmployeeId).FirstOrDefault();
                }

                if(Data ==null || Data.Count == 0)
                {
                    Result = "Not Verified"; 
                }
                else
                {
                    Result = "Verified";
                }

                await Task.Delay(0);
                return Result;
            }
            catch (Exception exception)
            {
                common.SaveException("EmployeeNameDesignation", "Simple Service Error on: {0} " + exception.Message + exception.StackTrace, "API", "/ERPService/Repository/Audit.cs", "", Parameter.DbName, Parameter.DbPath, Parameter.DbUId, Parameter.DbPass);
            }
            return null;
        }

        public async Task<string> EmploymentVerificationSP(Tuple<int?, string, string> tplData, Param Parameter)
        {
            try
            {
                string Result = string.Empty;
                long EmpId = Convert.ToInt64(tplData.Item1);
                SqlConnection con = CustomDatabaseConnectionString.StringClass(Parameter.DbName, Parameter.DbPath, Parameter.DbUId, Parameter.DbPass);
                SqlCommand command = new SqlCommand("UDP_get_EmploymentVarification", con);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@EmployeeID", tplData.Item1);
                command.Parameters.AddWithValue("@CompanyName", tplData.Item2);
                command.Parameters.AddWithValue("@VarificationCode", tplData.Item3);
                con.Open();
                command.CommandTimeout = MaxCommandTimeOut;
                SqlDataReader objDr = command.ExecuteReader();
                while (objDr.Read())
                {
                    Result = Convert.ToString(objDr["RESULT"]);
                }
                await Task.Delay(0);
                return Result;
            }
            catch (Exception exception)
            {
                common.SaveException("EmployeeNameDesignation", "Simple Service Error on: {0} " + exception.Message + exception.StackTrace, "API", "/ERPService/Repository/Audit.cs", "", Parameter.DbName, Parameter.DbPath, Parameter.DbUId, Parameter.DbPass);
            }
            return null;
        }

        #endregion
    }
}