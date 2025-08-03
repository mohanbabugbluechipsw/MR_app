using BLL.ViewModels;
using DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Model_New.Models;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class CommonService
    {
        private UnitOfWork<MrAppDbNewContext> unitOfWork;

        private readonly ILogger<CommonService> _logger;




        private DbSet<TblSystemUser> SysUserDbSet;
        private DbSet<TblUser> UserDbSet;


        public CommonService()
        {
            unitOfWork = new UnitOfWork<MrAppDbNewContext>();




            var mrAppDbNewContext = new MrAppDbNewContext();
            SysUserDbSet = mrAppDbNewContext.TblSystemUsers;
            UserDbSet = mrAppDbNewContext.TblUsers;
        }

        public List<TblUser> GetUsers(string Email, string Password)
        {
            var users = UserDbSet
                .Where(user => user.EmpEmail == Email && user.Password == Password)
                .ToList(); // Returns a list of users

            return users;
        }

        public List<TblSystemUser> GetSystemUser(string Empno, bool isActive)
        {

            var SysUser = SysUserDbSet
           .Where(user => user.EmpNo == Empno && user.IsActive == isActive)
           .ToList();
            return SysUser;
        }


        //public List<TblDistributor> GetDistributorUser(string Empno, bool isActive)
        //{

        //    var SysUser = SysUserDbSet
        //   .Where(user => user.EmpNo == Empno && user.IsActive == isActive)
        //   .ToList();
        //    return SysUser;
        //}




    }
}
