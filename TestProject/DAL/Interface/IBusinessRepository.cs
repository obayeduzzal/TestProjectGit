using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject.DbFiles;

namespace TestProject.DAL.Interface
{
    public interface IBusinessRepository
    {
        IEnumerable<BusinessEntity> GetAllEntities();
        BusinessEntity GetABusinessEntity(int? id);
        bool Create(BusinessEntity businessEntity);
        bool Update(BusinessEntity businessEntity);
        bool Delete(int id);
        string GetNewCode();
    }
}
