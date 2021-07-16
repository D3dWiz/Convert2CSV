using System;
using System.Data;
using System.Data.SqlClient;

namespace Convert2CSV.Converters
{
    public class MSSQLImport : BaseDbImport, IDisposable
    {
        #region Constructors
        public MSSQLImport(string connectionstring)
        {

            ConnectionString = connectionstring;

        }
        #endregion
        #region Methods
        /// <summary>
        /// Gets a specific table.
        /// </summary>
        public override DataTable GetDataTable(string connectionString, string selection)
        {
            var data = new DataTable();
            // Connects to the SQL with a connectionString provided by the business layer.
            var con = new SqlConnection(connectionString);
            try
            {
                con.Open();
                var command = new SqlCommand(selection, con);

                using (var adapter = new SqlDataAdapter(command))
                {
                    adapter.Fill(data);
                }
            }
            catch (Exception ex)
            { 
                // Throwing errors is for the business layer ;)
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
            return data;
        }

        /// <summary>
        /// Returns schema info.
        /// </summary>
        // Untested!
        public override void getSchemaTable()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {

                    if (con.State != ConnectionState.Open)
                        con.Open();
                    // Leaving it hardcoded with the hope that nobody renames the damn folders.
                    SchemaTable = con.GetSchema("Tables");

                }
            }
            catch (Exception ex)
            {
                // Throwing errors is for the business layer ;)
            }

        }

        /// <summary>
        /// Returns only the results from a specific query.
        /// </summary>
        // Untested!
        public override DataTable GetQueryResult(string dbQuery)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {

                    if (con.State != ConnectionState.Open)
                        con.Open();

                    using (SqlDataAdapter adapter = new SqlDataAdapter(dbQuery, con))
                    {

                        adapter.Fill(SQLTable);

                    }

                    SQLTable.PrimaryKey = null;

                }
            }
            catch (Exception ex)
            {
                // Throwing errors is for the business layer ;)
            }
            return SQLTable;
        }
        #endregion
        public void Dispose()
        {
        }
    }
}