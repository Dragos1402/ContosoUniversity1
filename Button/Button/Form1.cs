using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;

namespace Button
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public void button1_Click(object sender, EventArgs e)
        {
            
           string connectionString = "Data Source=LAPTOP-IIRNA9FS\\SQLEXPRESS01;Initial Catalog=ContosoUniversity;Trusted_Connection=True";
            string sql = "SELECT InstructorID, Location FROM dbo.OfficeAssignment";
            try 
            {
                using (var connection = new SqlConnection(connectionString)) 
                { connection.Open();
                    SqlCommand cmd = new SqlCommand(sql, connection);
                    
                    { using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                DataTable dt = new DataTable();
                                dt.Load(reader);
                               dataGridView1.DataSource = dt;
                            }
                            
                        }
                    }
                }
                
            }
            catch (Exception dt)
            {
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string connectionString= "Data Source=LAPTOP-IIRNA9FS\\SQLEXPRESS01;Initial Catalog=ContosoUniversity;Trusted_Connection=True";
            string sql = "SELECT * FROM dbo.Student";
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand(sql, connection);
                    {
                        using(var reader = cmd.ExecuteReader())
                        {
                            while(reader.Read())
                            {
                                DataTable dataTable = new DataTable();
                                dataTable.Load(reader);
                                dataGridView1.DataSource = dataTable;
                            }
                        }
                    }
                }
            }
            catch (Exception dataTable)
            {
            }
        }
    }
}