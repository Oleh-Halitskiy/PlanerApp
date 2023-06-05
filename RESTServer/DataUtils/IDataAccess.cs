namespace RESTServer.DataUtils
{
    /// <summary>
    /// Interface for implementing methods in DataAccess class
    /// </summary>
    public interface IDataAccess
    {
        /// <summary>
        /// Function that is used to load data from the DB
        /// </summary>
        /// <typeparam name="T">T is stands for type of desired object from DB</typeparam>
        /// <typeparam name="U">U stands for params sent to the DB, usually for searching record by ID</typeparam>
        /// <param name="sp">Stored procedure</param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> LoadData<T, U>(string sp, U parameters);

        /// <summary>
        /// Function that is used to save data to the DB
        /// </summary>
        /// <typeparam name="T">T stands for params sent to the DB, usually for passing parameters into stored procedure</typeparam>
        /// <param name="sp">Stored procedure</param>
        /// <param name="parameters">Parameters for stored procedure</param>
        /// <returns></returns>
        Task SaveData<T>(string sp, T parameters);
    }
}