using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KD.WPF.Client.APIClient
{
    public interface IDataLoggingContextProvider
    {
        Guid? GetCurrentUserId();
        string GetCurrentUserName();
        Guid? GetCurrentAreaId();
        string GetCurrentAreaName();
        string GetCurrentModuleName();
    }
}
