using System.Data;
using Dapper;
using UtilsClasses;
using System.Data.SqlClient;

namespace RESTServer.DataUtils
{
    /// <summary>
    /// Class that is used for talking to the database
    /// </summary>
    public class DataAccess : IDataAccess
    {
        /// <summary>
        /// Connection string
        /// </summary>
        private string cnnVal;
        /// <summary>
        /// Construcror for DataAcess class, populates the cnnVal variable
        /// </summary>
        public DataAccess() => cnnVal = Helper.CnnValue("ProjectDB");

        /// <summary>
        /// Function that is used to load data from the DB
        /// </summary>
        /// <typeparam name="T">T is stands for type of desired object from DB</typeparam>
        /// <typeparam name="U">U stands for params sent to the DB, usually for searching record by ID</typeparam>
        /// <param name="sp">Stored procedure</param>
        /// <param name="parameters"></param>
        /// <returns>Returns the Task of IEnumerable of T type</returns>
        public async Task<IEnumerable<T>> LoadData<T, U>(string sp, U parameters)
        {
            using (IDbConnection connection = new SqlConnection(cnnVal))
            {
                return await connection.QueryAsync<T>(sp, parameters, commandType: CommandType.StoredProcedure);
            }

        }
        /// <summary>
        /// Function that is used to save data to the DB
        /// </summary>
        /// <typeparam name="T">T stands for params sent to the DB, usually for passing parameters into stored procedure</typeparam>
        /// <param name="sp">Stored procedure</param>
        /// <param name="parameters">Parameters for stored procedure</param>
        /// <returns></returns>
        public async Task SaveData<T>(string sp, T parameters)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(cnnVal);
                await connection.ExecuteAsync(sp, parameters, commandType: CommandType.StoredProcedure);
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
            }
        }
    }
}
