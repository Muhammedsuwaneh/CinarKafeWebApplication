using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using CinarKafe.Models;
using CinarKafe.Dtos;

namespace CinarKafe.App_Start
{
    /// <summary>
    /// Dependency injection implementasyonlari
    /// </summary>
    public class MappingProfile : Profile
    {

        public MappingProfile() { 

              Mapper.CreateMap<Garson, GarsonDto>();
              Mapper.CreateMap<GarsonDto, Garson>().ForMember(c => c.Id, opt => opt.Ignore()); 
        }
    }
}