using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreRDLC.Contracts
{
    public interface IReportService
    {
        byte[] GenerateReportAsync(string reportName);
    }
}
