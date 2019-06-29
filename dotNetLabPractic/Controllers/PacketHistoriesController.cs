using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotNetLabPractic.DTOs;
using dotNetLabPractic.Services;
using Microsoft.AspNetCore.Mvc;

namespace dotNetLabPractic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacketHistoriesController : Controller
    {
        private IPacketHistoriesService packetHistoriesService;

        public PacketHistoriesController(IPacketHistoriesService service)
        {
            this.packetHistoriesService = service;
        }

        [HttpGet]
        public IEnumerable<GetHistoryDto> Get()
        {
            return packetHistoriesService.GetAllHistories();
        }

    }

}