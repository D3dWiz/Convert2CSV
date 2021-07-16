using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Convert2CSVDemoApp
{
    public partial class Body : Form
    {
        public Body()
        {
            InitializeComponent();
        }
        private void FetchButton_Click(object sender, EventArgs e)
        {
            string Server = ServerTextBox.Text;
            string Database = DatabaseTextBox.Text;
            string Table = TableTextBox.Text;
            string User = UserTextBox.Text;
            string Password = PasswordTextBox.Text;

            string connectionString = $@"Server={Server};Database={Database};UID={User};Password={Password}";
            string selection = $"select * from {Table}";

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
        private void ConvertButton_Click(object sender, EventArgs e)
        {

        }
    }
}
