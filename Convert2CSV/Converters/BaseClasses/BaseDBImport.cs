using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convert2CSV.Converters
{
    public abstract class BaseDBImport
    {
        #region Declarations

        public string ConnectionString { get; set; }
        public DataTable SQLTable { get; set; }
        public DataTable SchemaTable { get; set; }

        #endregion

        #region Constructors

        public BaseDbImport()
        {
            ConnectionString = "";
            SQLTable = new DataTable();
        }
        #endregion

        #region Methods
        public abstract DataTable GetDataTable(string schema, string tablename);
        public abstract void getSchemaTable();
        public abstract DataTable GetQueryResult(string sqlQuery);

        #endregion
    }
}
