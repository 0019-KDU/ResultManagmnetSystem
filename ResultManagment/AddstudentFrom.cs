using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ResultManagment
{
    public partial class AddstudentFrom : Form
    {
        public AddstudentFrom()
        {
            InitializeComponent();
        }

        private void AddstudentFrom_Load(object sender, EventArgs e)
        {

        }

        private void buttonImageUploading_Click(object sender, EventArgs e)
        {
            //browse image your from computer

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Select Image(*.jpg;*.png;*.gif)|*.jpg;*.png;*.gif";

            if (openFileDialog.ShowDialog() == DialogResult.OK) { 
            
            pictureBox1.Image=Image.FromFile(openFileDialog.FileName);
            
            }

        }

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

                if (student.inserAddstd(fname, lname, badate,gender, phone, address, pic))
                {
                    MessageBox.Show("New Student added", "Add Student", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtFname.Text = "";
                    txtLname.Text = "";
                    txtadd.Text = "";
                    txtadd.Text = "";
                    pictureBox1.Image = null;
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

        //create a function verify data

        bool verif() {

            if ((txtFname.Text.Trim() == "") ||
                (txtLname.Text.Trim() == "") ||
                (txtPhone.Text.Trim() == "") ||
                (txtadd.Text.Trim() == "" )||
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

        private void buttonCansal_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void radioButtonMale_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButtonMale_Click(object sender, EventArgs e)
        {
          
        }
    }
}
