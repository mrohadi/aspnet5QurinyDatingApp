using System;
using System.Linq;
using AutoMapper;
using WebAPI.DTOs;
using WebAPI.Entities;
using WebAPI.Extensions;

namespace WebAPI.Helpers
{
    public class AuthoMapperProfiles : Profile
    {
        public AuthoMapperProfiles()
        {
            // Mapping from AppUser to MemberDto
            CreateMap<AppUser, MemberDto>()
                .ForMember(
                    dest => dest.PhotoUrl, 
                    opt => opt.MapFrom(src => src.Photos.FirstOrDefault(x => x.IsMain).Url))
                .ForMember(
                    dest => dest.Age,
                    opt => opt.MapFrom(src => src.DateOfBirth.CalculateAge()));
            // Mapping from Photo to PhotoDto
            CreateMap<Photo, PhotoDto>();
            // Mapping from MemberUpdateDto to AppUser
            CreateMap<MemberUpdateDto, AppUser>();
            // Mapping from RegisterDto to AppUser
            CreateMap<RegisterDto, AppUser>();
            // Mapping from Message to MessageDto
            CreateMap<Message, MessageDto>()
                .ForMember(
                    dest => dest.SenderPhotoUrl, 
                    opt => opt.MapFrom(src => src.Sender.Photos.FirstOrDefault(x => x.IsMain).Url))
                .ForMember(
                    dest => dest.RecipientPhotoUrl,
                    opt => opt.MapFrom(src => src.Recipient.Photos.FirstOrDefault(x => x.IsMain).Url));
        }
    }
}