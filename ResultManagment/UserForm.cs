using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ResultManagment
{
    public partial class UserForm : Form
    {
        public UserForm()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection("Data Source=LAPTOP-81O6Q9UO\\SQLEXPRESS;Initial Catalog=RadDBConnection;Integrated Security=True");

        Student student = new Student();
        SCORE score = new SCORE();
        COURSE course = new COURSE();

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {

                conn.Open();

                SqlCommand cmd = new SqlCommand("SELECT score.studentid,Addstd.FirstName,Addstd.LastName,score.courseid,Course.Label,score.score FROM Addstd INNER JOIN score ON Addstd.id=score.studentid INNER JOIN Course ON score.courseid=Course.id WHERE studentid='" + textBox1.Text + "'", conn);

                //

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                DataTable dt = new DataTable();

                //adapter.SelectCommand = cmd;

                dt.Clear();

                adapter.Fill(dt);

                dataGridView1.DataSource = dt;

                conn.Close();   
            }
            else 
            {
                MessageBox.Show("Missing Information..Please check Again","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }

            textBox1.Clear();
        }
    }
}
