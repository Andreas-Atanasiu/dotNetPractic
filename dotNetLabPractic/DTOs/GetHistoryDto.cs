using dotNetLabPractic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotNetLabPractic.DTOs
{
    public class GetHistoryDto
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Location { get; set; }
        public DateTime DateArrived { get; set; }
        public DateTime DateLeft { get; set; }

        public static GetHistoryDto FromPacketHistory(PacketHistory packetHistory)
        {
            return new GetHistoryDto
            {
                Id = packetHistory.Id,
                Code = packetHistory.Code,
                Location = packetHistory.Location,
                DateArrived = packetHistory.DateArrived,
                DateLeft = packetHistory.DateLeft

            };
        }
    }
}
