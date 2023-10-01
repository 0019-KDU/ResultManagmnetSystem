using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ResultManagment
{
    public partial class ManageStudentForm : Form
    {
        public ManageStudentForm()
        {
            InitializeComponent();
        }

        Student student=new Student();
        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void ManageStudentForm_Load(object sender, EventArgs e)
        {
            //populate datagridview with student data
            fillGrid(new SqlCommand("SELECT * FROM Addstd"));
        }

        //create a function to populate the datagridview

        public void fillGrid(SqlCommand command) 
        {

            dataGridView1.ReadOnly = true;
            DataGridViewImageColumn column = new DataGridViewImageColumn();
            dataGridView1.RowTemplate.Height = 80;
            dataGridView1.DataSource = student.getStudents(command);

            column = (DataGridViewImageColumn)dataGridView1.Columns[7];
            column.ImageLayout = DataGridViewImageCellLayout.Stretch;

            dataGridView1.AllowUserToAddRows = false;

            //show the total students depending on dgv rows
            labelTotalStudent.Text="Total Students:"+dataGridView1.Rows.Count;


        }

        //Display student data on dataGridview click
        private void dataGridView1_Click(object sender, EventArgs e)
        {
            textBoxID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtFname.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtLname.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();

            dateTimePicker1.Value = (DateTime)dataGridView1.CurrentRow.Cells[3].Value;

            if (dataGridView1.CurrentRow.Cells[4].Value.ToString() == "Female")
            {
                radioButtonFemale.Checked = true;

            }
            else 
            {
                radioButtonMale.Checked = false;
            }

            txtPhone.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            txtadd.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();

            byte[] pic;
            pic = (byte[])dataGridView1.CurrentRow.Cells[7].Value;
            MemoryStream picture = new MemoryStream(pic);
            pictureBox1.Image=Image.FromStream(picture);

        }

        //clear all fields
        private void buttonReset_Click(object sender, EventArgs e)
        {
            textBoxID.Text = "";
            txtFname.Text = "";
            txtLname.Text = "";
            txtPhone.Text = "";
            txtadd.Text = "";
            radioButtonMale.Checked = true;
            dateTimePicker1.Value = DateTime.Now;
            pictureBox1.Image=null;
        }


        //search and display students in datagridview
        private void buttonSearch_Click(object sender, EventArgs e)
        {
            string query = "SELECT * FROM Addstd WHERE CONCAT(FirstName,LastName,Address) LIKE '%" + textBoxSearch.Text + "%'";
            SqlCommand cmd = new SqlCommand(query);
            fillGrid(cmd);
        }

        //browse and display image from your computer to the picturebox
        private void buttonImageUploading_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Select Image(*.jpg;*.png;*.gif)|*.jpg;*.png;*.gif";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {

                pictureBox1.Image = Image.FromFile(openFileDialog.FileName);

            }
        }


        //save the image in your computer
        private void buttonDownload_Click(object sender, EventArgs e)
        {
            SaveFileDialog svf= new SaveFileDialog();
            //set the file name
            svf.FileName="Student_"+textBoxID.Text;

            //check if the picturebox is empty
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("No Image In the PictureBox");
            }
            else if (svf.ShowDialog()==DialogResult.OK) 
            { 
                pictureBox1.Image.Save(svf.FileName+("."+ImageFormat.Jpeg.ToString()));
            }

        }


        //Add a new Student
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            //add new student
            Student student = new Student();

            string fname = txtFname.Text;
            string lname = txtLname.Text;
            DateTime badate = dateTimePicker1.Value;
            string phone = txtPhone.Text;
            string address = txtadd.Text;
            string gender = "";

            if (radioButtonFemale.Checked == true)
            {
                gender = "Female";
            }
            else
            {
                gender = "Male";
            }
            if (radioButtonMale.Checked == true)
            {
                gender = "Male";
            }
            else
            {
                gender = "Female";
            }

            MemoryStream pic = new MemoryStream();

            //we need to check tha age of student
            //the student age must be between 10-100

            int born_year = dateTimePicker1.Value.Year;
            int this_year = DateTime.Now.Year;

            if (((this_year - born_year) < 10 || ((this_year - born_year) > 100)))
            {
                MessageBox.Show("The Student Age Must be between 10 and 100 Year", "Invalid Birthdate", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else if (verif())
            {
                pictureBox1.Image.Save(pic, pictureBox1.Image.RawFormat);

                if (student.inserAddstd(fname, lname, badate, gender, phone, address, pic))
                {
                    MessageBox.Show("New Student added", "Add Student", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    fillGrid(new SqlCommand("SELECT * FROM Addstd"));
                }
                else
                {
                    MessageBox.Show("Error", "Add Student", MessageBoxButtons.OK, MessageBoxIcon.Error);


                }
            }
            else
            {

                MessageBox.Show("Error", "Add Student", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);


            }
        }

        //Remove the selected Student
        private void buttonRemove_Click(object sender, EventArgs e)
        {
            //Remove the selected student

            try
            {

                int id = Convert.ToInt32(textBoxID.Text);

                //show a conformation massage before deleteing student

                if (MessageBox.Show("Are you sure you want to the delete Student", "Delete Student", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (student.deleteStudent(id))
                    {

                        MessageBox.Show("Student Deleted", "Delete Student", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        fillGrid(new SqlCommand("SELECT * FROM Addstd"));
                        //claer fields
                        textBoxID.Text = "";
                        txtFname.Text = "";
                        txtLname.Text = "";
                        txtPhone.Text = "";
                        txtadd.Text = "";
                        dateTimePicker1.Value = DateTime.Now;
                        pictureBox1.Image = null;

                    }
                    else
                    {
                        MessageBox.Show("Student Not deleted", "Delete Student", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }

                }

            }
            catch
            {

                MessageBox.Show("Please Enter a Valid Student ID", "Delete Student", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        //edit the selected student
        private void buttonEdit_Click(object sender, EventArgs e)
        {
            //update the selected student
            try
            {

                int id = Convert.ToInt32(textBoxID.Text);
                string fname = txtFname.Text;
                string lname = txtLname.Text;
                DateTime badate = dateTimePicker1.Value;
                string phone = txtPhone.Text;
                string address = txtadd.Text;
                string gender = "";

                if (radioButtonFemale.Checked == true)
                {
                    gender = "Female";
                }
                else
                {
                    gender = "Male";
                }
                if (radioButtonMale.Checked == true)
                {
                    gender = "Male";
                }
                else
                {
                    gender = "Female";
                }


                MemoryStream pic = new MemoryStream();


                //we need to check tha age of student
                //the student age must be between 10-100

                int born_year = dateTimePicker1.Value.Year;
                int this_year = DateTime.Now.Year;

                if (((this_year - born_year) < 10 || ((this_year - born_year) > 100)))
                {
                    MessageBox.Show("The Student Age Must be between 10 and 100 Year", "Invalid Birthdate", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                else if (verif())
                {
                    pictureBox1.Image.Save(pic, pictureBox1.Image.RawFormat);

                    if (student.updateStudent(id, fname, lname, badate, gender, phone, address, pic))
                    {
                        MessageBox.Show("Student Information Updated", "Edit Student", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        fillGrid(new SqlCommand("SELECT * FROM Addstd"));
                    }
                    else
                    {
                        MessageBox.Show("Error", "Edit Student", MessageBoxButtons.OK, MessageBoxIcon.Error);


                    }
                }
                else
                {

                    MessageBox.Show("Error", "Edit Student", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);


                }
            }
            catch
            {
                MessageBox.Show("Please Enter a Valid Student ID", "Delete Student", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        //create a function verify data

        bool verif()
        {

            if ((txtFname.Text.Trim() == "") ||
                (txtLname.Text.Trim() == "") ||
                (txtPhone.Text.Trim() == "") ||
                (txtadd.Text.Trim() == "") ||
                (pictureBox1.Image == null)
                )
            {
                return false;

            }
            else
            {
                return true;
            }

        }


    }
}
