using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestProject.BLL.Interface;
using TestProject.BLL.Manager;
using TestProject.DbFiles;

namespace TestProject.Controllers
{
    public class BusinessEntityController : Controller
    {
        IBusinessMananger iBusinessManager = new BusinessMananger();
        // GET: BusinessEntity
        public ActionResult Index()
        {
            return View(GetAll());
        }

        IEnumerable<BusinessEntity> GetAll()
        {
            var entities = iBusinessManager.GetAllBusinessEntity();
            return entities;
        }

        public ActionResult GetABusinessEntity(int businessId)
        {
            var entity = iBusinessManager.GetABusinessEntity(businessId);
            return Json(entity, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreateOrUpdate(int? id)
        {
            BusinessEntity businessEntity = new BusinessEntity();
            if (id != 0)
            {
                businessEntity = iBusinessManager.GetABusinessEntity(id);
            }
            return View(businessEntity);
        }

        [HttpPost]
        public ActionResult CreateOrUpdate(BusinessEntity businessEntity, HttpPostedFileBase imageFile)
        {
            if(imageFile != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(imageFile.FileName);
                string extension = Path.GetExtension(imageFile.FileName);
                fileName = fileName + DateTime.Now.ToString("yyyyMMddHHmmssfff") + businessEntity.Code.ToString() + extension;
                businessEntity.Logo = "~/FileUpload/Image/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/FileUpload/Image/"), fileName);
                imageFile.SaveAs(fileName);
            }            
            var businessId = businessEntity.BusinessId;
            if (imageFile == null && businessId > 0)
            {
                var enitity = iBusinessManager.GetABusinessEntity(unchecked((int)businessId));
                businessEntity.Logo = enitity.Logo;
            }
            var submitted = iBusinessManager.CreateOrUpdate(businessEntity);
            if (submitted)
            {
                if(businessId == 0)
                {
                    return Json(new { success = true, message = "Created Successfully!" } ,JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = true, message = "Updated Successfully!" }, JsonRequestBehavior.AllowGet);
                }               
            }
            else
            {
                return Json(new { success = false, message = "Submission Failed" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Delete(int id)
        {
            var entity = iBusinessManager.GetABusinessEntity(id);
            if(entity != null)
            {
                var deleted = iBusinessManager.Delete(id);
                if (deleted)
                {
                    return Json(new { success = true,  message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, message = "Failed To Delete" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { success = false, message = "Data Not Found!" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetImage(int businessId)
        {
            var data = new BusinessEntity();
            if (businessId > 0)
            {
                data = iBusinessManager.GetABusinessEntity(businessId);
            }

            var base64 = "";
            if (string.IsNullOrEmpty(data.Logo))
            {
                base64 = null;
            }
            else
            {
                if (System.IO.File.Exists(Server.MapPath(data.Logo)))
                {
                    byte[] imageByteData = System.IO.File.ReadAllBytes(Server.MapPath(data.Logo));
                    base64 = Convert.ToBase64String(imageByteData);
                }
            }
            return Json(base64, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetNewCode()
        {
            var code = iBusinessManager.GetNewCode();
            return Json(code, JsonRequestBehavior.AllowGet);
        }
    }
}