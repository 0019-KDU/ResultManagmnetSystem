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
    public partial class AddScoreForm : Form
    {
        public AddScoreForm()
        {
            InitializeComponent();
        }


        SCORE score = new SCORE();
        COURSE course =new COURSE();
        Student student = new Student();

        //on from load
        private void AddScoreForm_Load(object sender, EventArgs e)
        {
            //populate the combobox with course name
            comboBoxCourse.DataSource = course.getAllCoures();
            comboBoxCourse.DisplayMember = "Label";
            comboBoxCourse.ValueMember = "id";

            //populate the datagridview with student data(id,first name,lastname)
            SqlCommand cmd = new SqlCommand("SELECT id,FirstName,LastName FROM Addstd");
            dataGridView1.DataSource = student.getStudents(cmd);
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            //get the id of the selected student
            textBoxStudentID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
        }

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
                else {
                 
                    MessageBox.Show("the score for this course Are Already Set", "Add Score", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Add Score", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
