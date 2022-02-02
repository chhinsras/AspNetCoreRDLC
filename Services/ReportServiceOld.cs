using AspNetCore.Reporting;
using AspNetCoreRDLC.Contracts;
using AspNetCoreRDLC.Dtos;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

// Using AspNetCore.Reporting Library
namespace AspNetCoreRDLC.Services
{
    public class ReportServiceOld : IReportService
    {
        public byte[] GenerateReportAsync(string reportName, string fileType)
        {
            string fileDirPath = Assembly.GetExecutingAssembly().Location.Replace("AspNetCoreRDLC.dll", string.Empty);
            string rdlcFilePath = string.Format("{0}ReportFiles/{1}.rdlc", fileDirPath, reportName);

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Encoding.GetEncoding("utf-8");
            AspNetCore.Reporting.LocalReport report = new AspNetCore.Reporting.LocalReport(rdlcFilePath);

            ReportResult result;

            if (reportName == "UserDetails")
            {
                List<UserDto> userList = new List<UserDto>();
                userList.Add(new UserDto
                {
                    FirstName = "Alex ​សិស្សសាលា",
                    LastName = "Smith",
                    Email = "alex.smith@gmail.com",
                    Phone = "2345334432"
                });

                userList.Add(new UserDto
                {
                    FirstName = "John​ កុមារា",
                    LastName = "Legend",
                    Email = "john.legend@gmail.com",
                    Phone = "5633435334"
                });

                userList.Add(new UserDto
                {
                    FirstName = "Stuart",
                    LastName = "Jones",
                    Email = "stuart.jones@gmail.com",
                    Phone = "3575328535"
                });

                report.AddDataSource("dsUsers", userList);

            } else if (reportName == "UserProfile")
            {
                // UserDto userProfile = new UserDto();
                // userProfile.FirstName = "ឈិន";
                // userProfile.LastName = "ស្រស់";
                // userProfile.Email = "john.legend@gmail.com";
                // userProfile.Phone = "5633435334";

                List<UserDto> userList = new List<UserDto>();
                userList.Add(new UserDto
                {
                    FirstName = "ស្រស់ ​សិស្សសាលា",
                    LastName = "Chhin",
                    Email = "chhinsras@gmail.com",
                    Phone = "017999740"
                }); 

                userList.Add(new UserDto
                {
                    FirstName = "Alex ​សិស្សសាលា",
                    LastName = "Smith",
                    Email = "alex.smith@gmail.com",
                    Phone = "2345334432"
                });

                report.AddDataSource("dsUser", userList);

            }


            if (fileType == "pdf")
            {
                result = report.Execute(GetRenderType("pdf"), 1, parameters);
            }
            else if (fileType == "excel")
            {
                result = report.Execute(GetRenderType("excel"), 1, parameters);
            }
            else if (fileType == "word")
            {
                result = report.Execute(GetRenderType("word"), 1, parameters);
            }
            else
            {
                result = report.Execute(GetRenderType("pdf"), 1, parameters);
            }

            return result.MainStream;

        }

        private RenderType GetRenderType(string reportType)
        {
            var renderType = RenderType.Pdf;
            switch (reportType.ToLower())
            {
                default:
                case "pdf":
                    renderType = RenderType.Pdf;
                    break;
                case "word":
                    renderType = RenderType.Word;
                    break;
                case "excel":
                    renderType = RenderType.Excel;
                    break;
            }

            return renderType;
        }
    }
}
