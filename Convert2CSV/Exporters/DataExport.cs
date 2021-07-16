using System;
using System.IO;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convert2CSV.Exporters
{
    class DataExport : IDisposable
    {
        public static void ToCSV(
        DataTable result, string strFilePath)
        {
            StreamWriter sw = new StreamWriter(strFilePath, false);
    // Header
            for (int i = 0; i < result.Columns.Count; i++)
            {
                sw.Write(result.Columns[i]);
                if (i < result.Columns.Count - 1)
                {
                    sw.Write(",");
                }
            }
            sw.Write(sw.NewLine);
    // Rows
            foreach (DataRow dr in result.Rows)
            {
                for (int i = 0; i < result.Columns.Count; i++)
                {
                    if (!Convert.IsDBNull(dr[i]))
                    {
                        string value = dr[i].ToString();
                        if (value.Contains(','))
                        {
                            value = String.Format("\"{0}\"", value);
                            sw.Write(value);
                        }
                        else
                        {
                            sw.Write(dr[i].ToString());
                        }
                    }
                    if (i < result.Columns.Count - 1)
                    {
                        sw.Write(",");
                    }
                }
                sw.Write(sw.NewLine);
            }
            sw.Close();
        }

        public void Dispose()
        {

        }
    }
}
