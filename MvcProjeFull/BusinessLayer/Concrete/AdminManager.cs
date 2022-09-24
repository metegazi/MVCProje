using BusinessLayer.Abstact;
using DataAccessLayer.Abstract;
using EntityLayer.Concrate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class AdminManager : IAdminService
    {
        IAdminDal _adminDal;

        public AdminManager(IAdminDal adminDal)
        {
            _adminDal = adminDal;
        }

        public void AdminAdd(Admin admin)
        {
            admin.AdminStatus = true;
            _adminDal.Insert(admin);
        }

        public void AdminDelete(Admin admin)
        {
            if (admin.AdminStatus != true)
            {
                admin.AdminStatus = true;
            }
            else
            {
                admin.AdminStatus = false;
            }
            _adminDal.Update(admin);
        }

        public void AdminUpdate(Admin admin)
        {
            admin.AdminStatus = true;
            _adminDal.Update(admin);
        }

        public Admin GetByID(int id)
        {
            return _adminDal.Get(x => x.id == id);
        }
        public Admin GetByUser(string adminUserName, string adminPassword)
        {
            return _adminDal.Get2(x => x.AdminUserName == adminUserName && x.AdminPassword == adminPassword && 
            x.AdminStatus == true);
        }

        public List<Admin> GetList()
        {
            return _adminDal.List(x=>x.AdminRole != "A");
        }

        public Admin GetRolesForUser(string UserName)
        {
            return _adminDal.Get2(x => x.AdminUserName == UserName);
        }

        /*public Admin GetByUser(Admin admin)
        {
            return _adminDal.GetUser(x => x.AdminUserName == admin.AdminUserName && x.AdminPassword == admin.AdminPassword);
        }*/
    }
}
