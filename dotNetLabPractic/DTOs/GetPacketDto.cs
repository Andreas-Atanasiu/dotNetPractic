using dotNetLabPractic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotNetLabPractic.DTOs
{
    public class GetPacketDto
    {
        //public int Id { get; set; }
        public string Origin { get; set; }
        public string Sender { get; set; }
        public string Destination { get; set; }
        public string Reciever { get; set; }
        public string Adress { get; set; }
        public double Cost { get; set; }
        public string Code { get; set; } = "";

        public IEnumerable<GetHistoryDto> Comments { get; set; }


        public static GetPacketDto DtoFromModel(Packet packet)
        {
            return new GetPacketDto
            {
                Origin = packet.Origin,
                Sender = packet.Sender,
                Destination = packet.Destination,
                Reciever = packet.Reciever,
                Adress = packet.Adress,
                Cost = packet.Cost,
                Code = packet.Code,

                Comments = packet.PacketHistories.Select(comment => new GetHistoryDto
                {
                    Id = comment.Id,
                    Code = comment.Code,
                    Location = comment.Location,
                    DateArrived = comment.DateArrived,
                    DateLeft = comment.DateLeft


                }),

            };
        }
    }
}
