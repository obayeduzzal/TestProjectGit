using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestProject.BLL.Interface;
using TestProject.DAL.Interface;
using TestProject.DAL.Repository;
using TestProject.DbFiles;

namespace TestProject.BLL.Manager
{
    public class BusinessMananger : IBusinessMananger
    {
        IBusinessRepository iBusinessRepository = new BusinessRepository();

        public IEnumerable<BusinessEntity> GetAllBusinessEntity()
        {
            var entities = iBusinessRepository.GetAllEntities();
            return entities;
        }

        public BusinessEntity GetABusinessEntity(int? id)
        {
            var entity = iBusinessRepository.GetABusinessEntity(id);
            return entity;
        }

        public bool CreateOrUpdate(BusinessEntity businessEntity)
        {
            if(businessEntity.BusinessId == 0)
            {
                businessEntity.CreatedOnUtc = DateTime.UtcNow.Date;
                businessEntity.Deleted = false;
                var created = iBusinessRepository.Create(businessEntity);
                if (created)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                businessEntity.UpdatedOnUtc = DateTime.UtcNow.Date;
                businessEntity.Deleted = false;
                var updated = iBusinessRepository.Update(businessEntity);
                if (updated)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool Delete(int id)
        {
            var deleted = iBusinessRepository.Delete(id);
            if (deleted)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public string GetNewCode()
        {
            var newCode = iBusinessRepository.GetNewCode();
            return newCode;
        }
    }
}