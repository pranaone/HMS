using HospitalManagementSystem.Models;
using HospitalManagementSystem.Models.Common;
using HospitalManagementSystem.Models.Report;
using HospitalManagementSystem.Services.Auth;
using HospitalManagementSystem.Services.ReportService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace HospitalManagementSystem.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ReportController : ApiController
    {
        [HttpPost, Route("api/report/AddReport")]
        public async Task<IHttpActionResult> AddReport(ReportModel report)
        {
            if (report == null)
            {
                return BadRequest("Please provide valid inputs!");
            }

            CommonResponse validatedResponse = await AuthService.ValidateUserAndToken();
            if (!validatedResponse.IsError)
            {
                if (await ReportService.ReportExists(report))
                {
                    return BadRequest("Report Already Exists");
                }
                else
                {
                    if (await ReportService.AddReport(report))
                    {
                        return Ok("Report Added Successfully!");
                    }
                    else
                    {
                        return BadRequest("Report Adding Failed!");
                    }
                }
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpGet, Route("api/report/GetReports")]
        public async Task<IHttpActionResult> GetReports(ReportModel report)
        {
            CommonResponse validatedResponse = await AuthService.ValidateUserAndToken();
            if (!validatedResponse.IsError)
            {
                var reports = await ReportService.GetReports(report);
                if (reports.Count > 0)
                {
                    return Ok(reports);
                }
                else
                {
                    return BadRequest("No Reports Exists!");
                }
            }
            else
            {
                return Unauthorized();
            }
        }


        [HttpPost, Route("api/report/GetPatientReport")]
        public async Task<IHttpActionResult> GetPatientReport(ReportModel report)
        {
            CommonResponse validatedResponse = await AuthService.ValidateUserAndToken();
            if (!validatedResponse.IsError)
            {
                var reports = await ReportService.GetPatientReport(report);
                if (reports.Count > 0)
                {
                    return Ok(reports);
                }
                else
                {
                    return BadRequest("No Reports Exists!");
                }
            }
            else
            {
                return Unauthorized();
            }
        }



        [HttpPost, Route("api/report/AddNewReportType")]
        public async Task<IHttpActionResult> AddNewReportType(ReportTypeModel reportType)
        {
            CommonResponse validatedResponse = await AuthService.ValidateUserAndToken();
            if (!validatedResponse.IsError)
            {
                if (await ReportService.ReportTypeExists(reportType))
                {
                    return BadRequest("Report Type Already Exists");
                }
                else
                {
                    var reportTypeTocreate = new ReportTypeModel
                    {
                        Name = reportType.Name
                    };

                    CommonResponse roleResponse = await ReportService.AddNewReportType(reportTypeTocreate);

                    if (roleResponse.IsError)
                    {
                        return BadRequest("Error In Adding The New Report Type!");
                    }
                    else
                    {
                        return Ok("Successfully Added A New Report Type!");
                    }
                }
            }
            else
            {
                return Unauthorized();
            }
        }


        [HttpGet, Route("api/report/GetReportTypes")]
        public async Task<IHttpActionResult> GetUserRoles()
        {
            CommonResponse validatedResponse = await AuthService.ValidateUserAndToken();
            if (!validatedResponse.IsError)
            {
                var reportTypeResponse = await ReportService.GetReportTypes();

                if (reportTypeResponse == null)
                {
                    return BadRequest("Error In Getting User Roles!");
                }
                else
                {
                    return Ok(reportTypeResponse);
                }
            }
            else
            {
                return Unauthorized();
            }
        }


        [HttpPost, Route("api/report/UpdateReportType")]
        public async Task<IHttpActionResult> UpdateReportType(ReportTypeModel reportType)
        {
            CommonResponse validatedResponse = await AuthService.ValidateUserAndToken();
            if (!validatedResponse.IsError)
            {
                if (await ReportService.ReportTypeExistsForUpdateAndDelete(reportType.ID))
                {
                    var reportTypeToUpdate = new ReportTypeModel
                    {
                        ID = reportType.ID,
                        Name = reportType.Name
                    };

                    CommonResponse roleResponse = await ReportService.UpdateReportType(reportTypeToUpdate);

                    if (roleResponse.IsError)
                    {
                        return BadRequest("Error In Updating The Report Type!");
                    }
                    else
                    {
                        return Ok("Report Type Updated Successfully!");
                    }
                }
                else
                {
                    return BadRequest("Report Type Does Not Exist!");
                }
            }
            else
            {
                return Unauthorized();
            }
        }


        [HttpPost, Route("api/report/DeleteReportType")]
        public async Task<IHttpActionResult> DeleteReportType(ReportTypeModel reportType)
        {

            CommonResponse validatedResponse = await AuthService.ValidateUserAndToken();
            if (!validatedResponse.IsError)
            {
                if (await ReportService.ReportTypeExistsForUpdateAndDelete(reportType.ID))
                {
                    var reportTypeToDelete = new ReportTypeModel
                    {
                        ID = reportType.ID,
                        Name = reportType.Name
                    };

                    CommonResponse roleResponse = await ReportService.DeleteReportType(reportTypeToDelete);

                    if (roleResponse.IsError)
                    {
                        return BadRequest("Error In Deleting The User Role!");
                    }
                    else
                    {
                        return Ok("User Role Deleted Successfully!");
                    }
                }
                else
                {
                    return BadRequest("User Role Does Not Exist!");
                }
            }
            else
            {
                return Unauthorized();
            }
        }




        //[HttpPost, Route("api/report/UpdateReport")]
        //public async Task<IHttpActionResult> UpdateReport(ReportModel report)
        //{
        //    if (report == null)
        //    {
        //        return BadRequest("Please provide valid inputs!");
        //    }

        //    CommonResponse validatedResponse = await AuthService.ValidateUserAndToken();
        //    if (!validatedResponse.IsError)
        //    {
        //        if (await ReportService.ReportExists(report))
        //        {
        //            if (await ReportService.UpdateReport(report))
        //            {
        //                return Ok("Report Updated Successfully!");
        //            }
        //            else
        //            {
        //                return BadRequest("Failed to Update Report!");
        //            }
        //        }
        //        else
        //        {
        //            return BadRequest("No Such Report Exisits!");
        //        }
        //    }
        //    else
        //    {
        //        return Unauthorized();
        //    }
        //}


        //[HttpPost, Route("api/report/DeleteReport")]
        //public async Task<IHttpActionResult> DeleteReport(ReportModel report)
        //{
        //    if (report == null)
        //    {
        //        return BadRequest("Please provide valid inputs!");
        //    }

        //    CommonResponse validatedResponse = await AuthService.ValidateUserAndToken();
        //    if (!validatedResponse.IsError)
        //    {
        //        if (await ReportService.ReportExists(report))
        //        {
        //            if (await ReportService.DeleteReport(report))
        //            {
        //                return Ok("Report Deleted Successfully!");
        //            }
        //            else
        //            {
        //                return BadRequest("Failed to Delete Report!");
        //            }
        //        }
        //        else
        //        {
        //            return BadRequest("No Such Report Exisits!");
        //        }
        //    }
        //    else
        //    {
        //        return Unauthorized();
        //    }
        //}

    }
}
