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
    public partial class UpdateDeleteStudentForm : Form
    {
        public UpdateDeleteStudentForm()
        {
            InitializeComponent();
        }

        Student student = new Student();

        private void buttonImageUploading_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Select Image(*.jpg;*.png;*.gif)|*.jpg;*.png;*.gif";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {

                pictureBox1.Image = Image.FromFile(openFileDialog.FileName);

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


        private void buttonEdit_Click(object sender, EventArgs e)
        {
            //update the selected student
            try 
            {

                int id = Convert.ToInt32(txtId.Text);
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
            }catch
            {
                MessageBox.Show("Please Enter a Valid Student ID", "Delete Student", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            
        }

        private void buttonCansal_Click(object sender, EventArgs e)
        {
            //Remove the selected student

            try
            {

                int id = Convert.ToInt32(txtId.Text);

                //show a conformation massage before deleteing student

                if (MessageBox.Show("Are you sure you want to the delete Student", "Delete Student", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (student.deleteStudent(id))
                    {

                        MessageBox.Show("Student Deleted", "Delete Student", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        //claer fields
                        txtId.Text = "";
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


        private void buttonFind_Click(object sender, EventArgs e)
        {
            //Search student by id

            try
            {

                int id = Convert.ToInt32(txtId.Text);

                SqlCommand cmd = new SqlCommand("SELECT id,FirstName,LastName,Birthdate,Gender,Phone,Address,Picture FROM Addstd WHERE id=" + id);

                DataTable table = student.getStudents(cmd);

                if (table.Rows.Count > 0)
                {
                    txtFname.Text = table.Rows[0]["FirstName"].ToString();
                    txtLname.Text = table.Rows[0]["LastName"].ToString();
                    txtPhone.Text = table.Rows[0]["Phone"].ToString();
                    txtadd.Text = table.Rows[0]["Address"].ToString();

                    dateTimePicker1.Value = (DateTime)table.Rows[0]["Birthdate"];

                    //gender

                    if (table.Rows[0]["Gender"].ToString() == "Female")
                    {

                        radioButtonFemale.Checked = true;
                    }
                    else
                    {
                        radioButtonMale.Checked = true;

                    }

                    //image

                    byte[] pic = (byte[])table.Rows[0]["Picture"];
                    MemoryStream Picture = new MemoryStream(pic);
                    pictureBox1.Image = Image.FromStream(Picture);

                }

            }
            catch 
            { 
            
                MessageBox.Show("Enter a Valid Student ID","Invalid ID",MessageBoxButtons.OK,MessageBoxIcon.Error);
            
            }

        }

        //Allow only numbers on key press
        private void txtId_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            { 
                e.Handled = true;
            
            }
        }

        private void UpdateDeleteStudentForm_Load(object sender, EventArgs e)
        {

        }
    }
}
