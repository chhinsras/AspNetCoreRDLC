using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AspNetCoreRDLC.Contracts;

namespace AspNetCoreRDLC.Controllers
{
    [Route("api/[controller]")]
    public class ReportController : Controller
    {
        private IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet]
        public ActionResult GetT(string reportName)
        {
            return Ok("30");
        }

        [HttpGet("{reportName}/{fileType}")]
        public ActionResult GetPDF(string reportName, string fileType)
        {
            var returnString = _reportService.GenerateReportAsync(reportName, fileType);
            switch (fileType.ToLower())
            {
                default:
                case "pdf":
                    return File(returnString, System.Net.Mime.MediaTypeNames.Application.Octet, reportName + ".pdf");
                case "word":
                    return File(returnString, System.Net.Mime.MediaTypeNames.Application.Octet, reportName + ".doc");
                case "excel":
                    return File(returnString, System.Net.Mime.MediaTypeNames.Application.Octet, reportName + ".xls");
            }
        }
    }
}