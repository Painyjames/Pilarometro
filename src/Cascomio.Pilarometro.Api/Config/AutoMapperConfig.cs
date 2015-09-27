using AutoMapper;
using Cascomio.Pilarometro.DataContract;
using Cascomio.Pilarometro.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cascomio.Pilarometro.Api.Config
{
    public class AutoMapperConfig : Profile
	{

		protected override void Configure()
		{
			Mapper.CreateMap<User, UserDto>();
			Mapper.CreateMap<Coordinates, CoordinatesDto>();
			Mapper.CreateMap<PointOfInterest, PointOfInterestDto>();

			Mapper.CreateMap<UserDto, User>();
			Mapper.CreateMap<CoordinatesDto, Coordinates>();
			Mapper.CreateMap<PointOfInterestDto, PointOfInterest>();
		}

		public override string ProfileName
		{
			get { return GetType().Name; }
		}
	}
}
