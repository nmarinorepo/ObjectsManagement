using ObjectsManagement.Application.Interfaces;
using ObjectsManagement.Application.Repositories;
using ObjectsManagement.Domain.Entities;

namespace ObjectsManagement.Application.Implementations
{
    public class ObjectService : IObjectService
    {
        private IUnitOfWork _unitOfWork;

        public ObjectService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<List<ObjectMainEntity>> GetAllObjects()
        {
            try
            {
                return _unitOfWork.ObjectRepository.GetAll();
            }
            catch (Exception ex)
            {
                
                throw;
            }
            
        }

        public Task<ObjectMainEntity> GetObjectById(int? id)
        {
            return _unitOfWork.ObjectRepository.GetObjectById(id);
        }

        public void ObjectUpdate(ObjectMainEntity objectMain)
        {
            _unitOfWork.ObjectRepository.ObjectUpdate(objectMain);
        }

        public void ObjectRelationshipUpdate(ObjectRelationshipEntity objectRelationship)
        {
            _unitOfWork.ObjectRepository.ObjectRelationshipUpdate(objectRelationship);
        }

        public bool ObjectMainExists(int id)
        {
            var objects = _unitOfWork.ObjectRepository.GetAll();
            return objects.Result.Any(e => e.Id == id);
        }

        public bool ObjectRelationshipExists(int id)
        {
            var objects = _unitOfWork.ObjectRepository.GetAllRelationshipsWithParent();
            return objects.Result.Any(e => e.Id == id);
        }

        public async Task SaveChangesAsync()
        {
           await _unitOfWork.Save();
        }

        public async void ObjectRemove(ObjectMainEntity objectMain)
        {
            _unitOfWork.ObjectRepository.ObjectRemove(objectMain);
        }

        public async void ObjectRelationshipRemove(ObjectRelationshipEntity objectRelationship)
        {
            _unitOfWork.ObjectRepository.ObjectRelationshipRemove(objectRelationship);
        }

        public async void ObjectCreate(ObjectMainEntity objectMain)
        {
            _unitOfWork.ObjectRepository.ObjectCreate(objectMain);
        }

        public Task<List<ObjectRelationshipEntity>> GetAllRelationshipsWithParent()
        {
            return _unitOfWork.ObjectRepository.GetAllRelationshipsWithParent();
        }
    }
}
