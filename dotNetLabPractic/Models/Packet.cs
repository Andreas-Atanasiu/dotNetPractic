using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotNetLabPractic.Models
{
    public class Packet
    {
        public int Id { get; set; }
        public string Origin { get; set; }
        public string Sender { get; set; }
        public string Destination { get; set; }
        public string Reciever { get; set; }
        public string Adress { get; set; }
        public double Cost { get; set; }
        public string Code { get; set; } = "";

        public List<PacketHistory> PacketHistories { get; set; }


    }
}
