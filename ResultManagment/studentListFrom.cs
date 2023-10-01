using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ResultManagment
{
    public partial class studentListFrom : Form
    {
        public studentListFrom()
        {
            InitializeComponent();
        }


        Student student=new Student();  
        private void studentListFrom_Load(object sender, EventArgs e)
        {
            //Populate the datagridview with student data

            SqlCommand cmd= new SqlCommand("SELECT * FROM Addstd");
            dataGridView1.ReadOnly = true;  
            DataGridViewImageColumn column = new DataGridViewImageColumn();
            dataGridView1.RowTemplate.Height = 80;
            dataGridView1.DataSource = student.getStudents(cmd);
            column =(DataGridViewImageColumn)dataGridView1.Columns[7];
            column.ImageLayout = DataGridViewImageCellLayout.Stretch;
            dataGridView1.AllowUserToAddRows = false;

        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            //display the selected student in a new form to edit/remove

            UpdateDeleteStudentForm updel=new UpdateDeleteStudentForm();

            updel.txtId.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            updel.txtFname.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            updel.txtLname.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            updel.dateTimePicker1.Value= (DateTime)dataGridView1.CurrentRow.Cells[3].Value;

            //gender
            if (dataGridView1.CurrentRow.Cells[4].Value.ToString() == "Female") 
            { 
            
                updel.radioButtonFemale.Checked = true;
            }

            updel.txtPhone.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            updel.txtadd.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();

            //image
            byte[] pic;
            pic = (byte[])dataGridView1.CurrentRow.Cells[7].Value;
            MemoryStream picture = new MemoryStream(pic);
            updel.pictureBox1.Image = Image.FromStream(picture);
            updel.Show();

        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            //refresh the dataGridview data

            SqlCommand cmd = new SqlCommand("SELECT * FROM Addstd");
            dataGridView1.ReadOnly = true;
            DataGridViewImageColumn column = new DataGridViewImageColumn();
            dataGridView1.RowTemplate.Height = 80;
            dataGridView1.DataSource = student.getStudents(cmd);
            column = (DataGridViewImageColumn)dataGridView1.Columns[7];
            column.ImageLayout = DataGridViewImageCellLayout.Stretch;
            dataGridView1.AllowUserToAddRows = false;



        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
