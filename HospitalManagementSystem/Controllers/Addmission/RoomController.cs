using HospitalManagementSystem.Models.Addmission;
using HospitalManagementSystem.Services.Addmission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace HospitalManagementSystem.Controllers.Addmission
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class RoomController : ApiController
    {
        [HttpPost, Route("api/addmission/AddRoom")]
        public async Task<IHttpActionResult> AddRoom(RoomModel room)
        {
            if (room == null)
            {
                return BadRequest("Please provide valid inputs!");
            }

            if (await RoomService.AddRoom(room))
            {
                return Ok("Room Saved Successfully!");
            }
            else
            {
                return BadRequest("Failed to Save Room!");
            }
        }

        [HttpPost, Route("api/addmission/UpdateRoom")]
        public async Task<IHttpActionResult> UpdateRoom(RoomModel room)
        {
            if (room == null)
            {
                return BadRequest("Please provide valid inputs!");
            }

            if (await RoomService.UpdateRoom(room))
            {
                return Ok("Room Updated Successfully!");
            }
            else
            {
                return BadRequest("Failed to Update Ward!");
            }
        }

        [HttpPost, Route("api/addmission/DeleteRoom")]
        public async Task<IHttpActionResult> DeleteRoom(RoomModel room)
        {
            if (room == null)
            {
                return BadRequest("Please provide valid inputs!");
            }

            if (await RoomService.DeleteRoom(room))
            {
                return Ok("Room Deleted Successfully!");
            }
            else
            {
                return BadRequest("Failed to Delete Room!");
            }
        }


        [HttpGet, Route("api/addmission/GetRooms")]
        public async Task<IHttpActionResult> GetRooms()
        {

            var rooms = await RoomService.GetRooms();
            if (rooms.Count > 0)
            {
                return Ok(rooms);
            }
            else
            {
                return BadRequest("No Rooms Found!");
            }

        }

        [HttpPost, Route("api/addmission/GetAvailableRooms")]
        public async Task<IHttpActionResult> GetAvailableRooms(RoomModel ward)
        {

            var rooms = await RoomService.GetAvailableRoomsByWard(ward);
            if (rooms.Count > 0)
            {
                return Ok(rooms);
            }
            else
            {
                return BadRequest("No Rooms Available!");
            }

        }


        [HttpPost, Route("api/addmission/GetRoomPrice")]
        public async Task<IHttpActionResult> GetRoomPrice(RoomModel room)
        {

            var rooms = await RoomService.GetRoomPrice(room);
            if (rooms.Count > 0)
            {
                return Ok(rooms);
            }
            else
            {
                return BadRequest("No Rooms Available!");
            }

        }

    }

}