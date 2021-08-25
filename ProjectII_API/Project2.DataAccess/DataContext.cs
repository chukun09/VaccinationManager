using MySqlConnector;
using Project2.Common.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Net.Http;

namespace Project2.DataAccess
{
    public class DataContext<Entity>
    {
        #region Constructors
        /// <summary>
        /// Các thành phần dùng chung
        /// </summary>
        string _connectionString;
        public IDbConnection DbConnection;
        public DataContext()
        {
            //Kết nối Db:
            _connectionString = "" + "Host = 127.0.0.1;" +
               "Port = 3306;" +
               "Database =project2_20203;" +
               "User Id = long;" +
               "Password= 12345678;" + "Charset= utf8";
            //Khởi tạo kết nối:
            DbConnection = new MySqlConnection(_connectionString);
        }
        #endregion
        #region Methods
        public List<Entity> GetAll()
        {
            ///kết nối và lấy dữ liệu từ db
            var className = typeof(Entity).Name;
            ///Trả về dữ liệu
            var sqlCommand = $"SELECT * FROM {className}";
            var entites = DbConnection.Query<Entity>(sqlCommand).ToList();
            return entites;
        }
        public List<Object> GetAllExtend()
        {
            ///kết nối và lấy dữ liệu từ db
            var className = typeof(Entity).Name;
            ///Trả về dữ liệu
            var sqlCommand = $"SELECT * FROM {className}, User Where {className}.userId = User.userId";
            var entites = DbConnection.Query<Object>(sqlCommand).ToList();
            return entites;
        }
        public List<Object> GetByIdExtend(Guid Id)
        {
            ///kết nối và lấy dữ liệu từ db
            var className = typeof(Entity).Name;
            ///Trả về dữ liệu
            var sqlCommand = $"SELECT * FROM {className}, User Where {className}.userId = User.userId AND User.userId = @Id";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Id", Id);
            var entites = DbConnection.Query<Object>(sqlCommand, parameters).ToList();
            return entites;
        }
        public Entity GetById(Guid Id)
        {
            ///kết nối và lấy dữ liệu từ db
            var className = typeof(Entity).Name.ToLower();
            ///Trả về dữ liệu
            var sqlCommand = $"SELECT * FROM {className} WHERE {className}Id = @{className}Id";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add($"@{className}Id", Id);
            var entites = DbConnection.QueryFirstOrDefault<Entity>(sqlCommand, parameters);
            return entites;
        }
        public Entity GetByName(String name)
        {
            ///kết nối và lấy dữ liệu từ db
            var className = typeof(Entity).Name.ToLower();
            ///Trả về dữ liệu
            var sqlCommand = $"SELECT * FROM {className} WHERE name = @{className}name";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add($"@{className}name", name);
            var entites = DbConnection.QueryFirstOrDefault<Entity>(sqlCommand, parameters);
            return entites;
        }
        public void DeleteById(Guid Id)
        {
            ///kết nối và lấy dữ liệu từ db
            var className = typeof(Entity).Name;
            //Trả về kết quả
            var sqlCommand = $"DELETE FROM {className} WHERE {className}Id = @{className}Id";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add($"{className}Id", Id.ToString(), DbType.String);
            DbConnection.Execute(sqlCommand, parameters);
        }
        public int Insert(Entity entity)
        {
            var className = typeof(Entity).Name;
            var columnName = string.Empty;
            var parameter = new DynamicParameters();
            var param = string.Empty;
            var properties = entity.GetType().GetProperties();
            foreach (var prop in properties)
            {
                var propName = prop.Name;
                if (propName.ToLower() == $"{className}Id".ToLower() && (prop.PropertyType == typeof(Guid) || prop.PropertyType == typeof(Guid?)))
                {
                    prop.SetValue(entity, Guid.NewGuid());
                }
                var propValue = prop.GetValue(entity);
                columnName += propName + ",";
                param += $"@{propName},";
                parameter.Add($"@{propName}", propValue);
            }
            columnName = columnName.Remove(columnName.Count() - 1, 1);
            param = param.Remove(param.Count() - 1, 1);
            var sqlCommand = $"INSERT INTO `{className}`({columnName}) VALUES({param})";
            var rowEffects = DbConnection.Execute(sqlCommand, parameter, commandType: CommandType.Text);
            return rowEffects;
        }

        public int Update(Entity Entity)
        {
            var className = typeof(Entity).Name;
            var columnName = string.Empty;
            var parameter = new DynamicParameters();
            var param = string.Empty;
            var nameandParam = string.Empty;
            var id = Guid.NewGuid();
            var properties = Entity.GetType().GetProperties();
            foreach (var prop in properties)
            {
                var propName = prop.Name;
                if (propName.ToLower() == $"{className}Id".ToLower() && (prop.PropertyType == typeof(Guid) || prop.PropertyType == typeof(Guid?)))
                {
                    id = (Guid)prop.GetValue(Entity);
                    parameter.Add("@id", id);
                    continue;
                }
                var propValue = prop.GetValue(Entity);
                columnName = propName + "= ";
                param = $"@{propName},";
                nameandParam = nameandParam + columnName + param;
                parameter.Add($"@{propName}", propValue);
            }
            nameandParam = nameandParam.Remove(nameandParam.Count() - 1, 1);
            var sqlCommand = $"UPDATE `{className}` SET {nameandParam} WHERE {className}Id =@id;";
            var rowEffects = DbConnection.Execute(sqlCommand, parameter, commandType: CommandType.Text);
            return rowEffects;
        }
        #endregion
    }
}
