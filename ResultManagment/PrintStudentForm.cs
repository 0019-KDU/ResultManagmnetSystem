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
    public partial class PrintStudentForm : Form
    {
        public PrintStudentForm()
        {
            InitializeComponent();
        }

        Student student = new Student();
        private void PrintStudentForm_Load(object sender, EventArgs e)
        {
            fillGrid(new SqlCommand("SELECT * FROM Addstd"));

            if (radioButtonNo.Checked)
            {
                dateTimePicker1.Enabled = false;
                dateTimePicker2.Enabled = false;


            }
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




        }

        private void radioButtonNo_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePicker1.Enabled = false;
            dateTimePicker2.Enabled = false;
        }

        private void radioButtonYes_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePicker1.Enabled = true;
            dateTimePicker2.Enabled = true;
        }

        private void buttonGo_Click(object sender, EventArgs e)
        {
            //Display data on the dataGridview denpending on what the user have selected 

            SqlCommand command;
            string query;

            //check if the radiobutton yes checked
            //that mean the user want to use a dat range

            if (radioButtonYes.Checked)
            {
                //get the data values
                string date1 = dateTimePicker1.Value.ToString("yyyy-MM-dd");
                string date2 = dateTimePicker2.Value.ToString("yyyy-MM-dd");

                if (radioButtonMale.Checked)
                {
                    query = "SELECT * FROM Addstd WHERE Birthdate BETWEEN '" + date1 + "' AND '" + date2 + "' AND Gender='Male'";
                }
                else if (radioButtonFemale.Checked)
                {
                    query = "SELECT * FROM Addstd WHERE Birthdate BETWEEN '" + date1 + "' AND '" + date2 + "' AND Gender='Female'";
                }
                else
                {

                    query = "SELECT * FROM Addstd WHERE Birthdate BETWEEN '" + date1 + "' AND '" + date2 + "'";
                }

                command = new SqlCommand(query);
                fillGrid(command);
            }

            else
            {
                if (radioButtonMale.Checked)
                {
                    query = "SELECT * FROM Addstd WHERE  Gender='Male'";
                }
                else if (radioButtonFemale.Checked)
                {
                    query = "SELECT * FROM Addstd WHERE Gender='Female'";
                }
                else
                {

                    query = "SELECT * FROM Addstd";
                }

                command = new SqlCommand(query);
                fillGrid(command);
            }
        }

        //print data from datagridview to text file
        private void buttonPrint_Click(object sender, EventArgs e)
        {
            //our file path
            //the file name=students_list.text
            //location=in the desktop
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\students_list.txt";

            using (var writer = new StreamWriter(path))
            {

                //check if the file exists
                if (!File.Exists(path)) 
                { 
                    File.Create(path);
                
                }

                DateTime bdate;

                //rows
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    //the column
                    for (int j = 0; j < dataGridView1.Columns.Count-1; j++)
                        
                    {   
                        //the birthday coloumn
                        if (j == 3)
                        {
                            bdate = Convert.ToDateTime(dataGridView1.Rows[i].Cells[j].Value.ToString());
                            writer.Write("\t" + bdate.ToString("yyyy-MM-dd") + "\t" + "|");

                        }

                        //the last coloumn
                        else if (j == dataGridView1.Columns.Count - 2) 
                        {
                            writer.Write("\t" + dataGridView1.Rows[i].Cells[j].Value.ToString());

                        }

                        else
                        {
                            writer.Write("\t" + dataGridView1.Rows[i].Cells[j].Value.ToString() + "\t" + "|");
                        }

                        
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
    }
}
