using DataAccessLayer;
using Entity;
using Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class BaseRepository<T, TKey> where T : class, IEntity<TKey>
    {
        private CateringContext _db;

        public BaseRepository(CateringContext db)
        {
            _db = db;
        }


        /*public List<PersonType> GetPersonTypes(Member m)
        {
            return _db.Set<Member>().Where();
        }*/

        public List<T> GetAll()
        {
            return _db.Set<T>().ToList();
        }

        public T GetOne(TKey id)
        {
            return _db.Set<T>().Find(id);
        }

        public bool Add(T record)
        {
            try
            {
                _db.Set<T>().Add(record); //savechanges'ı unityofwork'te
                return true;
            }
            //biri genel hataları, biri entityframworkun spesifik hatalarını yakalasın :
            catch (DbEntityValidationException ex)
            {
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Delete(TKey id)
        {
            try
            {
                T t = GetOne(id);
                _db.Set<T>().Remove(t);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Update(T newRecord)
        {
            try
            {
                T old = GetOne(newRecord.Id);
                _db.Entry(old).CurrentValues.SetValues(newRecord);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}
