using dotNetLabPractic.DTOs;
using dotNetLabPractic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotNetLabPractic.Services
{
    public interface IPacketHistoriesService
    {
        IEnumerable<GetHistoryDto> GetAllHistories();
    }

    public class PacketHistoriesService
    {
        private UsersDbContext context;

        public PacketHistoriesService(UsersDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<GetHistoryDto> GetAllHistories()
        {
            IQueryable<PacketHistory> result = context.PacketHistories;
            return result.Select(packetHistory => GetHistoryDto.FromPacketHistory(packetHistory));
        }
    }
}
