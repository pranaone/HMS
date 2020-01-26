using HospitalManagementSystem.Models.Common;
using HospitalManagementSystem.Models.Pharmacy;
using HospitalManagementSystem.Services.Auth;
using HospitalManagementSystem.Services.Pharmacy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace HospitalManagementSystem.Controllers.Pharmacy
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CartController : ApiController
    {
        [HttpPost, Route("api/Pharmacy/AddToCart")]
        public async Task<IHttpActionResult> AddToCart(CartDetailsModel cartDetails)
        {
            if (cartDetails == null)
            {
                return BadRequest("Please provide valid inputs!");
            }

            CommonResponse validatedResponse = await AuthService.ValidateUserAndToken();
            if (!validatedResponse.IsError)
            {
                if (await CartService.AddToCart(cartDetails))
                {
                    var cartDetailsList = await CartService.GetCartDetails(cartDetails);
                    if (cartDetailsList.Count > 0)
                    {
                        return Ok(cartDetailsList);
                    }
                    else
                    {
                        return BadRequest("No Inventory record Exists!");
                    }
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


        [HttpPost, Route("api/Pharmacy/GetCartDetails")]
        public async Task<IHttpActionResult> GetCartDetails(CartDetailsModel cartDetails)
        {
            CommonResponse validatedResponse = await AuthService.ValidateUserAndToken();
            if (!validatedResponse.IsError)
            {
                var cartDetailsList = await CartService.GetCartDetails(cartDetails);
                if (cartDetailsList.Count > 0)
                {
                    return Ok(cartDetailsList);
                }
                else
                {
                    return BadRequest("No Cart Detail record Exists!");
                }
            }
            else
            {
                return Unauthorized();
            }
        }//end of get


        [HttpPost, Route("api/Pharmacy/DeleteCartDetail")]
        public async Task<IHttpActionResult> DeleteCartDetail(CartDetailsModel cartDetails)
        {
            if (cartDetails == null)
            {
                return BadRequest("Please provide valid inputs!");
            }

            CommonResponse validatedResponse = await AuthService.ValidateUserAndToken();
            if (!validatedResponse.IsError)
            {
                if (await CartService.CartDetailExists(cartDetails))
                {
                    if (await CartService.DeleteCartDetail(cartDetails))
                    {
                        return Ok("Inventory Record Deleted Successfully!");
                    }
                    else
                    {
                        return BadRequest("Failed to Delete Inventory Record !");
                    }
                }
                else
                {
                    return BadRequest("No Such Inventory Record Exisits!");
                }
            }
            else
            {
                return Unauthorized();
            }
        }//end of Delete

        [HttpPost, Route("api/Pharmacy/VoidBill")]
        public async Task<IHttpActionResult> VoidBill(CartDetailsModel cartDetails)
        {
            if (cartDetails == null)
            {
                return BadRequest("Please provide valid inputs!");
            }

            CommonResponse validatedResponse = await AuthService.ValidateUserAndToken();
            if (!validatedResponse.IsError)
            {
                if (await CartService.CartDetailExistToVoid(cartDetails))
                {
                    if (await CartService.VoidBill(cartDetails))
                    {
                        return Ok("Cart Records Deleted Successfully!");
                    }
                    else
                    {
                        return BadRequest("Failed to Delete Inventory Record !");
                    }
                }
                else
                {
                    return Ok("Bill Voided!");
                }
            }
            else
            {
                return Unauthorized();
            }
        }//end of Delete
    }
}