using dotNetLabPractic.DTOs;
using dotNetLabPractic.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotNetLabPractic.Services
{
    public interface IPacketsService
    {
        IEnumerable<GetPacketDto> GetAllPackets();
        Packet GetById(int id);
        Packet CreatePacket(PostPacketDto packet);
        Packet UpdatePacket(int id, Packet packet);
        Packet DeletePacket(int id);

        IEnumerable<GetPacketDto> GetAllPacketsFromSender(String sender);
        IEnumerable<GetPacketDto> GetAllPacketsGroupBySender();
        Array GetAllRecieversOrderByCost();

    }

    public class PacketsService : IPacketsService
    {
        private UsersDbContext context;

        public PacketsService(UsersDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<GetPacketDto> GetAllPackets()
        {
            IQueryable<Packet> result = context.Packets
                                        .OrderBy(packet => packet.Id)
                                        .Include(packet => packet.PacketHistories);
            return result.Select(packet => GetPacketDto.DtoFromModel(packet));
        }

        public Packet GetById(int id)
        {
            return context.Packets.FirstOrDefault(ex => ex.Id == id);

        }

        public Packet CreatePacket(PostPacketDto packetDto)
        {
            Packet packetModel = PostPacketDto.ModelFromDto(packetDto);
            context.Packets.Add(packetModel);
            context.SaveChanges();
            return packetModel;
        }

        public Packet UpdatePacket(int id, Packet packet)
        {
            var existing = context.Packets.AsNoTracking().FirstOrDefault(pk => pk.Id == id);

            if (existing == null)
            {
                context.Packets.Add(packet);
                context.SaveChanges();
                return packet;
            }

            packet.Id = id;
            context.Packets.Update(packet);
            context.SaveChanges();
            return packet;
        }

        public Packet DeletePacket(int id)
        {
            var existing = context.Packets
                //.Include(ex => ex.Comments)
                .FirstOrDefault(packet => packet.Id == id);

            if (existing == null)
            {
                return null;
            }

            context.Packets.Remove(existing);
            context.SaveChanges();
            return existing;
        }

        public IEnumerable<GetPacketDto> GetAllPacketsFromSender(String sender)
        {
            var result = context.Packets.Where(pk => pk.Sender == sender);
            return result.Select(packet => GetPacketDto.DtoFromModel(packet));

        }

        public IEnumerable<GetPacketDto> GetAllPacketsGroupBySender()
        {
            //var result = context.Packets.Where(pk => pk.Sender == sender);
            //return result.Select(packet => GetPacketDto.DtoFromModel(packet));
            var result = context.Packets
                .OrderBy(pk => pk.Sender);
                 //.ToDictionary(x => x.Key, x => x);
            
             return result.Select(packet => GetPacketDto.DtoFromModel(packet));

        }

        public Array GetAllRecieversOrderByCost()
        {
            var recieverGroups = context.Packets.Select(packet => PacketCountDto.DtoFromModel(packet));

            var recGrp = recieverGroups
                         .GroupBy(pk => pk.Sender)
                         .Select(grp => new
                         {
                             sender = grp.Key,
                             total = grp.Count()
                         })
                         .ToArray();
            return recGrp;
        
           
        }



    }
}
