using System;
using System.IO;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convert2CSV.Exporters
{
    public class DataExport : IDisposable
    {
        public void Convert2CSV(DataTable data, string strFilePath)
        {
            StreamWriter sw = new StreamWriter(strFilePath, false);

#region Header
            for (int i = 0; i < data.Columns.Count; i++)
            {
                sw.Write(data.Columns[i]);
                if (i < data.Columns.Count - 1)
                {
                    sw.Write(",");
                }
            }
            sw.Write(sw.NewLine);
#endregion

#region Rows
            foreach (DataRow dr in data.Rows)
            {
                for (int i = 0; i < data.Columns.Count; i++)
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
                    if (i < data.Columns.Count - 1)
                    {
                        sw.Write(",");
                    }
                }
                sw.Write(sw.NewLine);
            }
            sw.Close();
        }
#endregion

        public void Dispose()
        {

        }
    }
}
