using dotNetLabPractic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotNetLabPractic.DTOs
{
    public class PacketCountDto
    {
        public string Sender { get; set; }
        public double Cost { get; set; }

        public static GetPacketDto DtoFromModel(Packet packet)
        {
            return new GetPacketDto
            {
                Sender = packet.Sender,
                Cost = packet.Cost
            };
        }
    }
}
