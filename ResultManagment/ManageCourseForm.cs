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
    public partial class ManageCourseForm : Form
    {
        public ManageCourseForm()
        {
            InitializeComponent();
        }

        COURSE course = new COURSE();
        int pos;
        private void ManageCourseForm_Load(object sender, EventArgs e)
        {
            reloadlistBoxData();
        }

        //create a function to load listBox with Course
        public void reloadlistBoxData()
        {
            listBoxCourses.DataSource = course.getAllCoures();
            listBoxCourses.ValueMember = "id";
            listBoxCourses.DisplayMember = "Label";

            //unselct the item from listbox
            listBoxCourses.SelectedItem = null;

            //Display the total Course
            labelTotalCourses.Text = "Total Course :" + course.totalCourses();


        }

        //create a function to display course data depending on the index
        void showData(int index)
        {
            DataRow dr = course.getAllCoures().Rows[index];
            listBoxCourses.SelectedIndex = index;
            textBoxID.Text = dr.ItemArray[0].ToString();
            txtLabel.Text = dr.ItemArray[1].ToString();
            numericUpDown1.Value = int.Parse(dr.ItemArray[2].ToString());
            txtDiscription.Text = dr.ItemArray[3].ToString();


        }

        private void listBoxCourses_Click(object sender, EventArgs e)
        {
            //Display the select course data
            pos = listBoxCourses.SelectedIndex;
            showData(pos);
        }


        //button first
        private void buttonFirst_Click(object sender, EventArgs e)
        {
            pos = 0;
            showData(pos);
        }

        //button next
        private void buttonNext_Click(object sender, EventArgs e)
        {
            if (pos < (course.getAllCoures().Rows.Count - 1)) 
               {
                pos = pos + 1;
                showData(pos);
            }

            
        }
        //button previous
        private void buttonPrevious_Click(object sender, EventArgs e)
        {
            if (pos > 0)
            {
                pos = pos - 1;
                showData(pos);
            }
        }

        //button last
        private void buttonLast_Click(object sender, EventArgs e)
        {
            pos=course.getAllCoures().Rows.Count - 1;
            showData(pos);  
        }

        //button add course
        private void buttonAddCourse_Click(object sender, EventArgs e)
        {
            string courseLabel = txtLabel.Text;
            int hours = (int)numericUpDown1.Value;
            string description = txtDiscription.Text;

            COURSE course = new COURSE();

            if (courseLabel.Trim() == "")
            {
                MessageBox.Show("Add a Course name", "Add Course", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            else if (course.checkCourseName(courseLabel))
            {
                if (course.insertCouerse(courseLabel, hours, description))
                {
                    MessageBox.Show("New Course Inserted", "Add Course", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    reloadlistBoxData();
                }
                else
                {
                    MessageBox.Show("Course Not Inserted", "Add Course", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            else
            {
                MessageBox.Show("This Course Name Already Exists", "Add Course", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }

           


        //Button edit course
        private void buttonEdit_Click(object sender, EventArgs e)
        {
            try
            {

                //Update the selected course
                string name = txtLabel.Text;
                int hrs = (int)numericUpDown1.Value;
                string descr = txtDiscription.Text;
                int id = Convert.ToInt32(textBoxID.Text);


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
                        reloadlistBoxData();
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
            pos = 0;
        }

        //button Remove course
        private void buttonRemoveCourse_Click(object sender, EventArgs e)
        {

            try
            {
                int courseID = Convert.ToInt32(textBoxID.Text);

                COURSE course = new COURSE();

                if (MessageBox.Show("Are You Sure You Want to Remove This Course", "Delete Course", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (course.deleteCourse(courseID))
                    {
                        MessageBox.Show("Course Deleted", "Remove Course", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        reloadlistBoxData();
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


        /*
         * you can use this but i will use the click event instead
        private void listBoxCourses_SelectedIndexChanged(object sender, EventArgs e)
        {
            try 
            {

                //Display the select course data
                pos = listBoxCourses.SelectedIndex;
                showData(pos);

            }
            catch 
            { 
            
            }*/

    }
    }

