using AutoMapper;
using InventoryManagementSystem.App.Entities;

namespace InventoryManagementSystem.App.MappingProfiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, User>();

    }
}