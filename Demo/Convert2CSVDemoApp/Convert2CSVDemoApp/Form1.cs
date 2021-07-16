using System;
using System.Data;
using System.Windows.Forms;

namespace Convert2CSVDemoApp
{
    public partial class Body : Form
    {
        public Body()
        {
            InitializeComponent();
        }

        #region FetchButton
        private void FetchButton_Click(object sender, EventArgs e)
        {
            string server = ServerTextBox.Text;
            string database = DatabaseTextBox.Text;
            string table = TableTextBox.Text;
            string user = UserTextBox.Text;
            string password = PasswordTextBox.Text;

            string connectionString = $@"Server={server};Database={database};UID={user};Password={password}";
            string selection = $"select * from {table}";

            using (Convert2CSV.Converters.MSSQLImport mssqlImport = new Convert2CSV.Converters.MSSQLImport(connectionString))
            {
                try
                {
                    DataTable data = mssqlImport.GetDataTable(connectionString, selection);
                    dataGridView1.DataSource = data;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        #endregion

        #region ConvertButton
        private void ConvertButton_Click(object sender, EventArgs e)
        {
            string server = ServerTextBox.Text;
            string database = DatabaseTextBox.Text;
            string table = TableTextBox.Text;
            string user = UserTextBox.Text;
            string password = PasswordTextBox.Text;

            string connectionString = $@"Server={server};Database={database};UID={user};Password={password}";
            string selection = $"select * from {table}";

            //SaveFileDialog sfd = new SaveFileDialog();
            //{
            //string sSelectedPath = fbd.SelectedPath;
            //}


            using (Convert2CSV.Converters.MSSQLImport mssqlImport = new Convert2CSV.Converters.MSSQLImport(connectionString))
            {
                using (Convert2CSV.Exporters.DataExport mssqlExport = new Convert2CSV.Exporters.DataExport())
                    try
                    {
                        //Hardcoded, will change later :)
                        string path = @"F:\Test.csv";
                        DataTable data = mssqlImport.GetDataTable(connectionString, selection);
                        mssqlExport.Convert2CSV(data, path);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
            }
        }
        #endregion
    }
}
