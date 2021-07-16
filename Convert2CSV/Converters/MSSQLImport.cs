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
        //gets a specific table
        public override DataTable GetDataTable(string connectionString, string selection)
        {
            var data = new DataTable();

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
                throw ex;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
            return data;
        }

        public override void getSchemaTable()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {

                    if (con.State != ConnectionState.Open)
                        con.Open();

                    SchemaTable = con.GetSchema("Tables");

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

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
                throw ex;
            }

            return SQLTable;
        }
        #endregion
        public void Dispose()
        {

        }
    }
}