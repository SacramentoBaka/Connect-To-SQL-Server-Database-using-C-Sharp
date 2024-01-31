using System;


using System.Data.SqlClient;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Connect_To_SQL_Database
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Global Declaration of SqlConnection and SqlCommand
        SqlConnection connection = new SqlConnection("Data Source=SACRAMENTO\\SQLEXPRESS;Initial Catalog=Employees;Integrated Security=True;TrustServerCertificate=True");
        SqlCommand command = new SqlCommand();
        int identifier;

        private void Form1_Load(object sender, EventArgs e)
        {
            displayData();// Calling the Display Method.
        }

        private void save_Click(object sender, EventArgs e)
        {
            // Saving and Displaying the Data from The Users Table
            connection.Open();
            string query = "INSERT INTO Users VALUES ('" + textBox1.Text + "','" + textBox2.Text + "','" + comboBox1.Text + "')";
            command = new SqlCommand(query, connection);
            command.ExecuteNonQuery();
            connection.Close();

            MessageBox.Show("Entry Added Successfully");
            displayData(); // Calling the Display Method.
       
        }

        // Method to Display data from the Data Users
        public void displayData()
        {

            connection.Open();
            string query = "SELECT * FROM Users";
            command = new SqlCommand(query, connection);
            command.ExecuteNonQuery();

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            connection.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            connection.Open();
            string query = "DELETE FROM Users WHERE ID = " + identifier + "";
            command = new SqlCommand(query, connection);
            command.ExecuteNonQuery();
            connection.Close();

            MessageBox.Show("User ID NO: " + identifier + " Deleted successfully..");
            displayData();
            textBox1.Text = "";
            textBox2.Text = "";
            comboBox1.Text = "";
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           

            connection.Open();

            identifier = Convert.ToInt32(dataGridView1.SelectedCells[0].Value.ToString());

            MessageBox.Show("Viewing User ID NO: " + identifier);

            command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT * FROM Users WHERE ID = '" + identifier + "'";
            command.ExecuteNonQuery();

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;

            SqlDataReader dataRow = command.ExecuteReader(CommandBehavior.CloseConnection);

            while (dataRow.Read())
            {
                textBox1.Text = dataRow.GetString(1).ToString();
                textBox2.Text = dataRow.GetString(2).ToString();
                comboBox1.Text = dataRow.GetString(3).ToString();

            }
            connection.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            displayData();
        }
    }
}
