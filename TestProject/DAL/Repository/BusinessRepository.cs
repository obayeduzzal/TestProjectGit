using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestProject.DAL.Interface;
using TestProject.DbFiles;

namespace TestProject.DAL.Repository
{
    public class BusinessRepository: IBusinessRepository
    {
        public IEnumerable<BusinessEntity> GetAllEntities()
        {
            try
            {
                using (TestDbEntities db = new TestDbEntities())
                {
                    var entities = db.BusinessEntities.Where(i => i.Deleted == false).ToList();
                    return entities;
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public BusinessEntity GetABusinessEntity(int? id)
        {
            try
            {
                using (TestDbEntities db = new TestDbEntities())
                {
                    var entity = db.BusinessEntities.Find(id);
                    return entity;
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool Create(BusinessEntity businessEntity)
        {
            try
            {
                using(TestDbEntities db = new TestDbEntities())
                {
                    db.BusinessEntities.Add(businessEntity);
                    db.SaveChanges();
                    return true;
                }
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public bool Update(BusinessEntity businessEntity)
        {
            try
            {
                using(TestDbEntities db = new TestDbEntities())
                {
                    db.Entry(businessEntity).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return true;
                }
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                using(TestDbEntities db = new TestDbEntities())
                {
                    var entity = db.BusinessEntities.Find(id);
                    entity.Deleted = true;
                    db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return true;
                }
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public string GetNewCode()
        {
            try
            {
                using(TestDbEntities db = new TestDbEntities())
                {
                    var newCode = "BE" + db.BusinessEntities.Max(i => i.BusinessId).ToString();
                    return newCode;
                }
            }
            catch(Exception ex)
            {
                return null;
            }
        }
    }
}