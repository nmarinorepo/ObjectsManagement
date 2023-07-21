using AutoMapper;
using ObjectsManagement.Domain.Entities;
using ObjectsManagementAPP.Models;

namespace ObjectsManagementAPP.Configuration
{
    public class ObjectMainProfile : Profile
    {
        public ObjectMainProfile()
        {
            CreateMap<ObjectMainEntity, ObjectMainModel>().ReverseMap();
            CreateMap<ObjectRelationshipEntity, ObjectRelationshipModel>().ReverseMap();
        }
    }
}
