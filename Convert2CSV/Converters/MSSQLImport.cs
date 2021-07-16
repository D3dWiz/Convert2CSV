using System;
using System.Data;
using System.Data.SqlClient;

namespace Convert2CSV.Converters
{
    public class MSSQLImport : BaseDBImport, IDisposable
    {
        public override DataTable GetDataTable(string connectionString, string selectQuery)
        {
            var returnDT = new DataTable();

            var con = new SqlConnection(connectionString);
            try
            {
                con.Open();
                var command = new SqlCommand(selectQuery, con);

                using (var adapter = new SqlDataAdapter(command))
                {
                    adapter.Fill(returnDT);
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
            return returnDT;
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

        public override DataTable GetQueryResult(string sqlQuery)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {

                    if (con.State != ConnectionState.Open)
                        con.Open();

                    using (SqlDataAdapter adapter = new SqlDataAdapter(sqlQuery, con))
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

        public void Dispose()
        {

        }
    }
}