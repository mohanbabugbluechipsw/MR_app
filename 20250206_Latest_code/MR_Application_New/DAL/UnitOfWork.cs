using DAL.IRepositories;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Model_New.Models;

namespace DAL
{

    public class UnitOfWork<TContext> : IDisposable
       where TContext : DbContext, new()
    {
        private DbContext context;
        private IGenericRepository<TblUser, DbContext> tbl_User;


        private IGenericRepository<TblUserType, DbContext> tbl_UserType;


        private IGenericRepository<TblUslworkLevel, DbContext> tbl_UslworkLevel;


        private IGenericRepository<TblSystemUser, DbContext> tbl_SystemUser;


        private IGenericRepository<TblDepartment, DbContext> tbl_Department;


        private IGenericRepository<TblOfficeLocation, DbContext> tbl_OfficeLocation;

        private IGenericRepository<OutLetMasterDetail, DbContext> OutLetMasterDetails;

        private IGenericRepository<TblDistributor, DbContext> tbl_Distributor;

        private IGenericRepository<TblMrmaster, DbContext> tbl_Mrmaster;




        public IGenericRepository<TblUser, DbContext> Tbl_User
        {
            get
            {
                if (this.tbl_User == null)
                {
                    this.tbl_User = new GenericRepository<TblUser, DbContext>(context);
                }
                return tbl_User;
            }
        }



        public IGenericRepository<TblUserType, DbContext> Tbl_UserType
        {
            get
            {
                if (this.tbl_UserType == null)
                {
                    this.tbl_UserType = new GenericRepository<TblUserType, DbContext>(context);
                }
                return tbl_UserType;
            }
        }


        public IGenericRepository<TblUslworkLevel, DbContext> Tbl_UslworkLevel
        {
            get
            {
                if (this.tbl_UslworkLevel == null)
                {
                    this.tbl_UslworkLevel = new GenericRepository<TblUslworkLevel, DbContext>(context);
                }
                return tbl_UslworkLevel;
            }
        }



        public IGenericRepository<TblSystemUser, DbContext> Tbl_SystemUser
        {
            get
            {
                if (this.tbl_SystemUser == null)
                {
                    this.tbl_SystemUser = new GenericRepository<TblSystemUser, DbContext>(context);
                }
                return tbl_SystemUser;
            }
        }



        public IGenericRepository<TblDepartment, DbContext> Tbl_Department
        {
            get
            {
                if (this.tbl_Department == null)
                {
                    this.tbl_Department = new GenericRepository<TblDepartment, DbContext>(context);
                }
                return tbl_Department;
            }
        }



        public IGenericRepository<TblOfficeLocation, DbContext> Tbl_OfficeLocation
        {
            get
            {
                if (this.tbl_OfficeLocation == null)
                {
                    this.tbl_OfficeLocation = new GenericRepository<TblOfficeLocation, DbContext>(context);
                }
                return tbl_OfficeLocation;
            }
        }



        public IGenericRepository<OutLetMasterDetail, DbContext> OutL_etMasterDetails
        {
            get
            {
                if (this.OutLetMasterDetails == null)
                {
                    this.OutLetMasterDetails = new GenericRepository<OutLetMasterDetail, DbContext>(context);
                }
                return OutLetMasterDetails;
            }
        }


        public IGenericRepository<TblDistributor, DbContext> Tbl_Distributor
        {
            get
            {
                if (this.tbl_Distributor == null)
                {
                    this.tbl_Distributor = new GenericRepository<TblDistributor, DbContext>(context);
                }
                return tbl_Distributor;
            }
        }




        public IGenericRepository<TblMrmaster, DbContext> Tbl_Mrmaster
        {
            get
            {
                if (this.tbl_Mrmaster == null)
                {
                    this.tbl_Mrmaster = new GenericRepository<TblMrmaster, DbContext>(context);
                }
                return tbl_Mrmaster;
            }
        }



        public UnitOfWork()
        {
            context = new TContext();
        }


        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }


        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

