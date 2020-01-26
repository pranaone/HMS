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


namespace HospitalManagementSystem.Controllers.Pharmacy
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class InventoryController : ApiController
    {
        [HttpPost, Route("api/Pharmacy/AddInventory")]
        public async Task<IHttpActionResult> AddInventory(InventoryModel inventory)
        {
            if (inventory == null)
            {
                return BadRequest("Please provide valid inputs!");
            }

            CommonResponse validatedResponse = await AuthService.ValidateUserAndToken();
            if (!validatedResponse.IsError)
            {
                if (await ProductService.ProductExist(inventory.ProductID))
                {
                    InventoryModel inventoryToAdd = new InventoryModel()
                    {
                        ProductID = inventory.ProductID,
                        Quantity = inventory.Quantity,
                        Description = inventory.Description
                    };

                    if (await InventoryService.AddInventory(inventoryToAdd))
                    {
                        return Ok("Inventory record Added Successfully!");
                    }
                    else
                    {
                        return BadRequest("Inventory record Adding Failed!");
                    }
                }
                else
                {
                    return BadRequest("No SUch Item Exists");
                }
            }
            else
            {
                return Unauthorized();
            }
        }//end of add

        [HttpGet, Route("api/Pharmacy/GetInventory")]
        public async Task<IHttpActionResult> GetInventory(InventoryModel inventory)
        {
            CommonResponse validatedResponse = await AuthService.ValidateUserAndToken();
            if (!validatedResponse.IsError)
            {
                var Inventory = await InventoryService.GetInventory(inventory);
                if (Inventory.Count > 0)
                {
                    return Ok(Inventory);
                }
                else
                {
                    return BadRequest("No Inventory record Exists!");
                }
            }
            else
            {
                return Unauthorized();
            }
        }//end of get


        [HttpPost, Route("api/Pharmacy/GetSingleInventory")]
        public async Task<IHttpActionResult> GetSingleInventory(InventoryModel inventory)
        {
            CommonResponse validatedResponse = await AuthService.ValidateUserAndToken();
            if (!validatedResponse.IsError)
            {
                var InventoryItem = await InventoryService.GetSingleInventory(inventory);
                if (InventoryItem.Count > 0)
                {
                    return Ok(InventoryItem);
                }
                else
                {
                    return BadRequest("No Inventory record Exists!");
                }
            }
            else
            {
                return Unauthorized();
            }
        }//end of get


        [HttpPost, Route("api/Pharmacy/UpdateInventory")]
        public async Task<IHttpActionResult> UpdateInventory(InventoryModel inventory)
        {
            if (inventory == null)
            {
                return BadRequest("Please provide valid inputs!");
            }

            CommonResponse validatedResponse = await AuthService.ValidateUserAndToken();
            if (!validatedResponse.IsError)
            {
                if (await InventoryService.InventoryExist(inventory))
                {
                    if (await InventoryService.UpdateInventory(inventory))
                    {
                        return Ok("Inventory Record Updated Successfully!");
                    }
                    else
                    {
                        return BadRequest("Failed to Update Inventory Record!");
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
        }//end of update


        [HttpPost, Route("api/Pharmacy/DeleteInventory")]
        public async Task<IHttpActionResult> DeleteInventory(InventoryModel inventory)
        {
            if (inventory == null)
            {
                return BadRequest("Please provide valid inputs!");
            }

            CommonResponse validatedResponse = await AuthService.ValidateUserAndToken();
            if (!validatedResponse.IsError)
            {
                if (await InventoryService.InventoryExist(inventory))
                {
                    if (await InventoryService.DeleteInventory(inventory))
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

    }
}
