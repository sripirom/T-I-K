using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TIK.Core.Domain;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace TIK.Persistance.SQLite
{
    public class SQLiteRepository<T, TId> : IRepository<T, TId>
        where T : BaseModel<TId>
    {
        private readonly string _tableName;
        private readonly string _connection;

        internal IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_connection);
            }
        } 
         
        public SQLiteRepository(string tableName, string connection)
        {
            _tableName = tableName;
            _connection = connection;
        }

        internal virtual dynamic Mapping(T item)
        {
            return item;
        }

        public TId Add(T entry)
        {
            using (IDbConnection cn = Connection)
            {
                var parameters = (object)Mapping(entry);
                cn.Open();
                //entry.Id = cn.Insert<TId>(_tableName, parameters);
            }

            return entry.Id;
        }

        public TId Save(T entry)
        {
            using (IDbConnection cn = Connection)
            {
                var parameters = (object)Mapping(entry);
                cn.Open();
                //cn.Update(_tableName, parameters);
            }

            return entry.Id;
        }

        public bool Delete(TId id)
        {
            using (IDbConnection cn = Connection)
            {
                cn.Open();
                cn.Execute("DELETE FROM " + _tableName + " WHERE ID=@ID", new { ID = id });
            }
            return true;
        }

        public T Get(TId id)
        {
            T item = default(T);

            using (IDbConnection cn = Connection)
            {
                cn.Open();
                item = cn.Query<T>("SELECT * FROM " + _tableName + " WHERE ID=@ID", new { ID = id }).SingleOrDefault();
            }

            return item;
        }

        public IEnumerable<T> List()
        {
            IEnumerable<T> items = null;

            using (IDbConnection cn = Connection)
            {
                cn.Open();
                items = cn.Query<T>("SELECT * FROM " + _tableName);
            }

            return items;
        }



        public IEnumerable<T> Search(IEnumerable<Tuple<Expression<Func<T, object>>, object>> paramValue)
        {
            IEnumerable<T> items = null;

            var predicate = paramValue.FirstOrDefault();

           /* predicate.Item1.Compile().Invoke(a).Equals(predicate.Item2)
            // extract the dynamic sql query and parameters from predicate
                    // QueryResult result = DynamicQuery.GetDynamicQuery(_tableName, predicate.Item1.Compile());

            using (IDbConnection cn = Connection)
            {
                cn.Open();
                items = cn.Query<T>(result.Sql, (object)result.Param);
            }
            */
            return items;
        }

        public IEnumerable<T> List(int skip = 0, int size = 20)
        {
            throw new NotImplementedException();
        }
    }
}
