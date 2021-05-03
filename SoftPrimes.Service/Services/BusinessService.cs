using AutoMapper;
using AutoMapper.QueryableExtensions;
using SoftPrimes.BLL.BaseObjects.ReSoftPrimesitoriesInterfaces;
using SoftPrimes.Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftPrimes.Service.Services
{
    public abstract class BusinessService : IBusinessService
    {

        protected readonly IUnitOfWork _UnitOfWork;
        protected readonly IMapper _Mapper;
        protected readonly bool IsAuditEnabled;

        public BusinessService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _UnitOfWork = unitOfWork;
            _Mapper = mapper;

        }

    }

    public abstract class BusinessService<TDbEntity, TDetailsDTO> : BusinessService, IBusinessService<TDbEntity, TDetailsDTO>
             where TDbEntity : class
             where TDetailsDTO : class

    {
        public BusinessService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }



        public virtual List<T> GetAll<T>()
        {
            IQueryable query = this._UnitOfWork.GetRepository<TDbEntity>().GetAll();
            if (typeof(TDbEntity) == typeof(T))
                return query.Cast<T>().ToList();
            else
                return query.ProjectTo<T>(_Mapper.ConfigurationProvider).ToList();
        }
        public virtual List<TDbEntity> GetAllWithoutInclude()
        {
            IQueryable query = this._UnitOfWork.GetRepository<TDbEntity>().GetAllWithoutInclude();
            return query.Cast<TDbEntity>().ToList(); ;
        }

        public object GetPropertyValue(object car, string propertyName)
        {
            return car.GetType().GetProperties()
               .Single(pi => pi.Name.Contains(propertyName))
               .GetValue(car, null);
        }

        public virtual TDbEntity GetDetails(object Id)
        {
            if (Id == null) return null;

            //var Mapping = _Mapper.ConfigurationProvider.FindTypeMapFor(typeof(TDbEntity), typeof(TDetailsDTO));
            //if (Mapping == null)
            //{
            //    Mapping = _Mapper.ConfigurationProvider.ResolveTypeMap(typeof(TDbEntity), typeof(TDetailsDTO));
            //}

            var EntityObject = this._UnitOfWork.GetRepository<TDbEntity>().Find(Id);
            return EntityObject;
            //if (typeof(TDbEntity) == typeof(TDetailsDTO))
            //    return EntityObject as TDetailsDTO;
            //else
            //    return _Mapper.Map(EntityObject, typeof(TDbEntity), typeof(TDetailsDTO)) as TDetailsDTO; ;
        }

        public virtual TDbEntity Insert(TDbEntity entities)
        {

            // var entity = _Mapper.Map(entities, typeof(TDetailsDTO), typeof(TDbEntity)) as TDbEntity;
            this._UnitOfWork.GetRepository<TDbEntity>().Insert(entities);
            this._UnitOfWork.SaveChanges();
            //var Newentities = _Mapper.Map(entity, typeof(TDbEntity), typeof(TDetailsDTO)) as TDetailsDTO;
            return entities;
        }

        //private void SetProperty(object obj, string property, object value)
        //{
        //    try
        //    {
        //        var prop = obj.GetType().GetProperty(property, BindingFlags.Public | BindingFlags.Instance);
        //        if (prop != null && prop.CanWrite)
        //            prop.SetValue(obj, value, null);
        //    }
        //    catch { } //property not exist or inserted value doesn't match the property type!
        //}


        //public virtual TDetailsDTO InsertSingleRecord(TDetailsDTO entity)
        //{
        //    var TDbEntity = entity;
        //    var ToBereturned = this._UnitOfWork.RePointOfSaleitory<TDbEntity>().Insert(TDbEntity as TDbEntity);
        //    return _Mapper.Map(ToBereturned, typeof(TDetailsDTO), typeof(TDetailsDTO)) as TDetailsDTO;
        //}

        public virtual int Delete(int Id)
        {
            this._UnitOfWork.GetRepository<TDbEntity>().Delete(Id);
            this._UnitOfWork.SaveChanges();
            return Id;
        }

        public virtual TDbEntity Update(TDbEntity Entity)
        {

            //foreach (var Entity in Entities)
            //{
            //    //To Copy Data not Sent From and To UI
            var PrimaryKeysValues = this._UnitOfWork.GetRepository<TDbEntity>().GetKey<TDbEntity>(_Mapper.Map(Entity, typeof(TDetailsDTO), typeof(TDbEntity)) as TDbEntity);
            //var OldEntity = _UnitOfWork.GetRepository<TDbEntity>().Find(PrimaryKeysValues);
            //   object MappedEntity = _Mapper.Map(Entity, OldEntity, typeof(TDetailsDTO), typeof(TDbEntity));
            this._UnitOfWork.GetRepository<TDbEntity>().Update(Entity as TDbEntity);
            //}
            this._UnitOfWork.SaveChanges();
            #region commented
            try
            {
                this._UnitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            // var Newentities = _Mapper.Map(MappedEntity, typeof(TDbEntity), typeof(TDetailsDTO)) as TDetailsDTO;
            #endregion
            return Entity;
        }

        //public bool CheckIfExist(object id)
        //{
        //    return this._UnitOfWork.GetRepository<TDbEntity>().GetAll().Any(c=>c.)(checkUniqueDTO);
        //}


    }
}