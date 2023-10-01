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
    public partial class ManageScoreForm : Form
    {
        public ManageScoreForm()
        {
            InitializeComponent();
        }

        SCORE score=new SCORE();
        Student student=new Student();  
        COURSE course=new COURSE();

        string data = "score";
        private void ManageScoreForm_Load(object sender, EventArgs e)
        {
            //populate the combobox with courses
            comboBoxCourse.DataSource=course.getAllCoures();
            comboBoxCourse.DisplayMember = "label";
            comboBoxCourse.ValueMember = "id";

            //populate the datagridview with student score
            dataGridView1.DataSource = score.getstudentsScore();
        }

        //Display student data on datagridview
        private void buttonShowStudent_Click(object sender, EventArgs e)
        {
            data = "student";
            SqlCommand cmd = new SqlCommand("SELECT id,FirstName,LastName,Birthdate FROM Addstd");
            dataGridView1.DataSource = student.getStudents(cmd);
        }


        //Display scores data on datagridview
        private void buttonShowScore_Click(object sender, EventArgs e)
        {
            data = "score";
            dataGridView1.DataSource = score.getstudentsScore();
        }

        //get the data from datagridview
        private void dataGridView1_Click(object sender, EventArgs e)
        {
            getDataFromDatagridview();
        }

        //create a function to get a data from datagridview

        public void getDataFromDatagridview() {

            //if the user select to show student data than we will show only the student id 
            if (data == "student")
            {
                textBoxStudentID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            }
            //if the user select to show student data than we will show  the student id +select the course from the combobox
            else if (data=="score") {
                textBoxStudentID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                comboBoxCourse.SelectedValue = dataGridView1.CurrentRow.Cells[3].Value;
            }
        }

        //button add score
        private void buttonAddScore_Click(object sender, EventArgs e)
        {
            //Add New Score
            try
            {
                int studentID = Convert.ToInt32(textBoxStudentID.Text);
                int courseID = Convert.ToInt32(comboBoxCourse.SelectedValue);
                float scoreValue = Convert.ToSingle(txtScore.Text);
                string discription = txtDiscription.Text;



                //check if a score is already asigned to this student in this score 
                if (!score.studentScoreExists(studentID, courseID))
                {
                    if (score.insertScore(studentID, courseID, scoreValue, discription))
                    {
                        MessageBox.Show("Student Score Inseterd", "Add Score", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Student Score Not Inseterd", "Add Score", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {

                    MessageBox.Show("the score for this course Are Already Set", "Add Score", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Add Score", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //button remove score
        private void buttonRemoveScore_Click(object sender, EventArgs e)
        {
            //remove the selected score
            int studentid = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            int courseid = int.Parse(dataGridView1.CurrentRow.Cells[3].Value.ToString());

            if (MessageBox.Show("Do You Want To Delete This Score", "Delete Score", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (score.deleteScore(studentid, courseid))
                {
                    MessageBox.Show("Score Deleted", "Remove Score", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.DataSource = score.getstudentsScore();
                }
                else
                {
                    MessageBox.Show("Score Not Deleted", "Remove Score", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        //show new from with the avaerage score by course 
        private void buttonAVGscore_Click(object sender, EventArgs e)
        {
            avgScorebyCourseForm avgScore=new avgScorebyCourseForm();
            avgScore.Show(this);
        }
    }
}
