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
    public partial class PrintScoreForm : Form
    {
        public PrintScoreForm()
        {
            InitializeComponent();
        }


        Student student=new Student();  
        SCORE score=new SCORE();
        COURSE course=new COURSE(); 
        private void PrintScoreForm_Load(object sender, EventArgs e)
        {   
            //populate datagridview with students data
            dataGridView1.DataSource = student.getStudents(new System.Data.SqlClient.SqlCommand("SELECT id,FirstName,LastName FROM Addstd"));

            //populate datagridview with scores data 
            dataGridViewStudentScore.DataSource = score.getstudentsScore();

            //populate listbox with courses data 
            listBoxCourses.DataSource = course.getAllCoures();
            listBoxCourses.DisplayMember = "Label";
            listBoxCourses.ValueMember = "ID";
        }

        //when you select a course from the list box all scores assigned to this course will be displayed in the datagridview
        private void listBoxCourses_Click(object sender, EventArgs e)
        {
            dataGridViewStudentScore.DataSource = score.getCoresScores(int.Parse(listBoxCourses.SelectedValue.ToString()));
        }

        //Display the seletced student score
        private void dataGridView1_Click(object sender, EventArgs e)
        {
            dataGridViewStudentScore.DataSource = score.getStudentScores(int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString()));
        }

        //populate datagridview with scores data 
        private void labelRest_Click(object sender, EventArgs e)
        {
           
            dataGridViewStudentScore.DataSource = score.getstudentsScore();
        }

        private void buttonPrint_Click(object sender, EventArgs e)
        {
            //our file path
            //the file name=scores_list.text
            //location=in the desktop
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\scores_list.txt";

            using (var writer = new StreamWriter(path))
            {

                //check if the file exists
                if (!File.Exists(path))
                {
                    File.Create(path);

                }

                //rows
                for (int i = 0; i <dataGridViewStudentScore.Rows.Count; i++)
                {
                    //the column
                    for (int j = 0; j < dataGridViewStudentScore.Columns.Count; j++)
                    {
                        writer.Write("\t" +dataGridViewStudentScore.Rows[i].Cells[j].Value.ToString() + "\t" + "            |");

                    }

                    //make a new line
                    writer.WriteLine("");

                    //make a sepration
                    writer.WriteLine("-----------------------------------------------------------------------------------------------------------------------------");


                }
                writer.Close();
                MessageBox.Show("Data Exported");
            }
        }

        private void listBoxCourses_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
   }

