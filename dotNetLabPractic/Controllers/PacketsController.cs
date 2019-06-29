using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotNetLabPractic.DTOs;
using dotNetLabPractic.Models;
using dotNetLabPractic.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotNetLabPractic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacketsController : ControllerBase
    {
        private IUsersService usersService;
        private IPacketsService packetsService;

        public PacketsController(IPacketsService packetsService, IUsersService usersService)
        {
            this.packetsService = packetsService;
            this.usersService = usersService;
        }

        [Authorize(Roles = "Regular,Moderator,Admin")]
        [HttpGet]
        public IEnumerable<GetPacketDto> GetPackets()
        {
            return packetsService.GetAllPackets();
        }

        [Authorize(Roles = "Regular,Moderator,Admin")]
        [HttpGet]
        [Route("[controller]/GetAllPacketsFromSender")]
        public IEnumerable<GetPacketDto> GetAllPacketsFromSender([FromQuery]string sender)
        {
            return packetsService.GetAllPacketsFromSender(sender);
        }

        [Authorize(Roles = "Regular,Moderator,Admin")]
        [HttpGet]
        [Route("[controller]/GetAllPacketsGroupBySender")]
        public IEnumerable<GetPacketDto> GetAllPacketsGroupBySender()
        {
            return packetsService.GetAllPacketsGroupBySender();
        }

        [Authorize(Roles = "Regular,Moderator,Admin")]
        [HttpGet]
        [Route("[controller]/GetAllRecieversOrderByCost")]
        public Array GetAllRecieversOrderByCost()
        {
            return packetsService.GetAllRecieversOrderByCost();
        }

        [Authorize(Roles = "Regular,Moderator,Admin")]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var found = packetsService.GetById(id);
            if (found == null)
            {
                return NotFound();
            }
            return Ok(found);
        }

        [Authorize(Roles = "Moderator,Admin")]
        [HttpPost]
        public void Post([FromBody] PostPacketDto packet)
        {
            packetsService.CreatePacket(packet);
        }

        [Authorize(Roles = "Moderator,Admin")]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Packet packet)
        {
            var result = packetsService.UpdatePacket(id, packet);
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = packetsService.DeletePacket(id);

            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }


    }
}