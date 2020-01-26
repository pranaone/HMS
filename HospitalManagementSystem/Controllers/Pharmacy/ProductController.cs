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
        public class ProductController : ApiController
        {
            [HttpPost, Route("api/Pharmacy/AddProducts")]
            public async Task<IHttpActionResult> AddProducts(ProductModel product)
            {
                if (product == null)
                {
                    return BadRequest("Please provide valid inputs!");
                }

                CommonResponse validatedResponse = await AuthService.ValidateUserAndToken();
                if (!validatedResponse.IsError)
                {
                    if (await ProductService.ProductExist(product.ID))
                    {
                        return BadRequest("Product Already Exists");
                    }
                    else
                    {
                        if (await ProductService.AddProduct(product))
                        {
                            return Ok("Product Added Successfully!");
                        }
                        else
                        {
                            return BadRequest("Product Adding Failed!");
                        }
                    }
                }
                else
                {
                    return Unauthorized();
                }
            }//end of add

        [HttpGet, Route("api/Pharmacy/GetProducts")]
        public async Task<IHttpActionResult> GetProducts(ProductModel product)
        {
            CommonResponse validatedResponse = await AuthService.ValidateUserAndToken();
            if (!validatedResponse.IsError)
            {
                var Product = await ProductService.GetProducts(product);
                if (Product.Count > 0)
                {
                    return Ok(Product);
                }
                else
                {
                    return BadRequest("No Products Exists!");
                }
            }
            else
            {
                return Unauthorized();
            }
        }//end of get


        [HttpPost, Route("api/Pharmacy/UpdateProduct")]
        public async Task<IHttpActionResult> UpdateProduct(ProductModel product)
        {
            if (product == null)
            {
                return BadRequest("Please provide valid inputs!");
            }

            CommonResponse validatedResponse = await AuthService.ValidateUserAndToken();
            if (!validatedResponse.IsError)
            {
                if (await ProductService.ProductExist(product.ID))
                {
                    if (await ProductService.UpdateProduct(product))
                    {
                        return Ok("Product Updated Successfully!");
                    }
                    else
                    {
                        return BadRequest("Failed to Update Product!");
                    }
                }
                else
                {
                    return BadRequest("No Such Product Exisits!");
                }
            }
            else
            {
                return Unauthorized();
            }
        }//end of update


        [HttpPost, Route("api/Pharmacy/DeleteProducts")]
        public async Task<IHttpActionResult> DeleteProducts(ProductModel product)
        {
            if (product == null)
            {
                return BadRequest("Please provide valid inputs!");
            }

            CommonResponse validatedResponse = await AuthService.ValidateUserAndToken();
            if (!validatedResponse.IsError)
            {
                if (await ProductService.ProductExist(product.ID))
                {
                    if (await ProductService.DeleteProduct(product))
                    {
                        return Ok("Product Deleted Successfully!");
                    }
                    else
                    {
                        return BadRequest("Failed to Delete Product!");
                    }
                }
                else
                {
                    return BadRequest("No Such Product Exisits!");
                }
            }
            else
            {
                return Unauthorized();
            }
        }//end of Delete

    }
    }
