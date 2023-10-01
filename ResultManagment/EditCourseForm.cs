using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ResultManagment
{
    public partial class EditCourseForm : Form
    {
        public EditCourseForm()
        {
            InitializeComponent();
        }

        COURSE course = new COURSE();
        private void EditCourseForm_Load(object sender, EventArgs e)
        {
            //populate the combobox with courses

            comboBoxCourse.DataSource = course.getAllCoures();
            comboBoxCourse.DisplayMember = "Label";
            comboBoxCourse.ValueMember = "id";

            //set the selected combox item to nothing
            comboBoxCourse.SelectedItem = null;
        }

        //create the function to populate the combobox
        //and select the current course
        public void fillCombo(int index) 
        {

            //index is the combobox item index 
            comboBoxCourse.DataSource = course.getAllCoures();
            comboBoxCourse.DisplayMember = "Label";
            comboBoxCourse.ValueMember = "id";

            comboBoxCourse.SelectedIndex = index;

        }

        private void buttonEditCourse_Click(object sender, EventArgs e)
        {
            try
            {

                //Update the selected course
                string name = txtLabel.Text;
                int hrs = (int)numericUpDown1.Value;
                string descr = txtDiscription.Text;
                int id = (int)comboBoxCourse.SelectedValue;

                if (name.Trim() != "") 
                {
                    //check if this course name already exists and it's not current course using the id
                    if (!course.checkCourseName(name, id))
                    {
                        MessageBox.Show("No Course Selected ", "Edit Course", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (course.updateCourse(id, name, hrs, descr))
                    {
                        MessageBox.Show("Course Updated", "Edit Course", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        fillCombo(comboBoxCourse.SelectedIndex);
                    }
                    else
                    {
                        MessageBox.Show("Course Not Updated", "Edit Course", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Enter the Course Name", "Edit Course", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }



            }
            catch
            {
                MessageBox.Show("This Course Name Already Exists", "Add Course", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void comboBoxCourse_SelectedIndexChanged(object sender, EventArgs e)
        {

            try {

                //Display the selected course data
                int id = Convert.ToInt32(comboBoxCourse.SelectedValue);
                DataTable dt = new DataTable();
                dt = course.getCouresByID(id);
                txtLabel.Text = dt.Rows[0][1].ToString();
                numericUpDown1.Value = Int32.Parse(dt.Rows[0][2].ToString());
                txtDiscription.Text = dt.Rows[0][3].ToString();

            }
            catch 
            { 
            
            }
            

        }
    }
}
