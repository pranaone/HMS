using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using HospitalManagementSystem.Models.Common;
using HospitalManagementSystem.Models.Pharmacy;
using HospitalManagementSystem.Services.Auth;
using HospitalManagementSystem.Services.Pharmacy;
using HospitalManagementSystem.Models.ViewReports;

namespace HospitalManagementSystem.Controllers.Pharmacy
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class SalesController : ApiController
    {
        [HttpPost, Route("api/Pharmacy/AddSales")]
        public async Task<IHttpActionResult> AddSales(SalesModel sales)
        {
            if (sales == null)
            {
                return BadRequest("Please provide valid inputs!");
            }

            CommonResponse validatedResponse = await AuthService.ValidateUserAndToken();
            if (!validatedResponse.IsError)
            {
                    if (await SalesService.AddSales(sales))
                    {
                        return Ok("Transaction Successfully Entered!");
                    }
                    else
                    {
                        return BadRequest("Transaction Cannot be Processed!");
                    }
            }
            else
            {
                return Unauthorized();
            }
        }//end of add

        [HttpGet, Route("api/Pharmacy/GetSales")]
        public async Task<IHttpActionResult> GetSales(SalesModel sales)
        {
            CommonResponse validatedResponse = await AuthService.ValidateUserAndToken();
            if (!validatedResponse.IsError)
            {
                var Sales = await SalesService.GetSales(sales);
                if (Sales.Count > 0)
                {
                    return Ok(Sales);
                }
                else
                {
                    return BadRequest("No Sales Exists!");
                }
            }
            else
            {
                return Unauthorized();
            }
        }//end of get

        [HttpPost, Route("api/Pharmacy/GetSalesReports")]
        public async Task<IHttpActionResult> GetSalesReport(SalesReportModel sales)
        {
            CommonResponse validatedResponse = await AuthService.ValidateUserAndToken();
            if (!validatedResponse.IsError)
            {
                var Sales = await SalesService.GetSalesReport(sales);
                if (Sales.Count > 0)
                {
                    return Ok(Sales);
                }
                else
                {
                    return BadRequest("No Sales Exists!");
                }
            }
            else
            {
                return Unauthorized();
            }
        }//end of get


        [HttpPost, Route("api/Pharmacy/UpdateSales")]
        public async Task<IHttpActionResult> UpdateSales(SalesModel sales)
        {
            if (sales == null)
            {
                return BadRequest("Please provide valid inputs!");
            }

            CommonResponse validatedResponse = await AuthService.ValidateUserAndToken();
            if (!validatedResponse.IsError)
            {
                if (await SalesService.SalesExist(sales))
                {
                    if (await SalesService.UpdateSales(sales))
                    {
                        return Ok("Transaction Updated Successfully!");
                    }
                    else
                    {
                        return BadRequest("Failed to Update the Transaction!");
                    }
                }
                else
                {
                    return BadRequest("No Such Transaction Exisits!");
                }
            }
            else
            {
                return Unauthorized();
            }
        }//end of update


        [HttpPost, Route("api/Pharmacy/DeleteSales")]
        public async Task<IHttpActionResult> DeleteSales(SalesModel sales)
        {
            if (sales == null)
            {
                return BadRequest("Please provide valid inputs!");
            }

            CommonResponse validatedResponse = await AuthService.ValidateUserAndToken();
            if (!validatedResponse.IsError)
            {
                if (await SalesService.SalesExist(sales))
                {
                    if (await SalesService.DeleteSales(sales))
                    {
                        return Ok("Sales Deleted Successfully!");
                    }
                    else
                    {
                        return BadRequest("Failed to Delete Sales!");
                    }
                }
                else
                {
                    return BadRequest("No Such Sale Exisits!");
                }
            }
            else
            {
                return Unauthorized();
            }
        }//end of Delete


        [HttpPost, Route("api/Pharmacy/OpenNewBill")]
        public async Task<IHttpActionResult> OpenNewBill(CartHeaderModel cartHeader)
        {
            CommonResponse validatedResponse = await AuthService.ValidateUserAndToken();
            if (!validatedResponse.IsError)
            {
                var headerID = await SalesService.GetNewCartHeader(cartHeader);
                if (headerID > 0)
                {
                    return Ok(headerID);
                }
                else
                {
                    return BadRequest("No Sales Exists!");
                }
            }
            else
            {
                return Unauthorized();
            }
        }//end of get
    }
}
