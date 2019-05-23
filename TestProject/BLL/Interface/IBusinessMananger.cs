using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject.DbFiles;

namespace TestProject.BLL.Interface
{
    public interface IBusinessMananger
    {
        IEnumerable<BusinessEntity> GetAllBusinessEntity();
        BusinessEntity GetABusinessEntity(int? id);
        bool CreateOrUpdate(BusinessEntity businessEntity);
        bool Delete(int id);
        string GetNewCode();
    }
}
