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
    public partial class RemoveCourseForm : Form
    {
        public RemoveCourseForm()
        {
            InitializeComponent();
        }

        private void buttonRemoveCourse_Click(object sender, EventArgs e)
        {
            try
            {
                int courseID = Convert.ToInt32(textBoxCourseID.Text);

                COURSE course = new COURSE();

                if (MessageBox.Show("Are You Sure You Want to Remove This Course", "Delete Course", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (course.deleteCourse(courseID))
                    {
                        MessageBox.Show("Course Deleted", "Remove Course", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Course Not Deleted", "Remove Course", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
            catch
            {

                MessageBox.Show("Enter Valid Numeric Value", "Remove Course", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
           
        }

        private void textBoxCourseID_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
