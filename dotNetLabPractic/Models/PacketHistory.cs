using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotNetLabPractic.Models
{
    public class PacketHistory
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Location { get; set; }
        public DateTime DateArrived { get; set; }
        public DateTime DateLeft { get; set; }
        public Packet Packet { get; set;}


    }
}
