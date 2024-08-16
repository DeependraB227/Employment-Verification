using ERPModel.CommonMaster;
using System;
using System.Threading.Tasks;

namespace ERPService.IRepository
{
    public interface IEmployee  : IDisposable
    {
        void Dispose();

        #region Employee Verification
        Task<string> EmploymentVerification(Tuple<int?, string, string> tplData, Param Parameter);
        #endregion
    }
}
