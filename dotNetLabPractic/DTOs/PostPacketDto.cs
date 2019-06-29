using dotNetLabPractic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotNetLabPractic.DTOs
{
    public class PostPacketDto
    {
        public string Origin { get; set; }
        public string Sender { get; set; }
        public string Destination { get; set; }
        public string Reciever { get; set; }
        public string Adress { get; set; }
        public double Cost { get; set; }
        public string Code { get; set; } = "";

        public static Packet ModelFromDto(PostPacketDto packetDto)
        {
            return new Packet
            {
                Origin = packetDto.Origin,
                Sender = packetDto.Sender,
                Destination = packetDto.Destination,
                Reciever = packetDto.Reciever,
                Adress = packetDto.Adress,
                Cost = packetDto.Cost,
                Code = packetDto.Code
            };
        }
    }
}
