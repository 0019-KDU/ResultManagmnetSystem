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
    public partial class PrintCourseForm : Form
    {
        public PrintCourseForm()
        {
            InitializeComponent();
        }

        private void PrintCourseForm_Load(object sender, EventArgs e)
        {
            //populate datagridview with course
            COURSE course = new COURSE();
            dataGridView1.DataSource = course.getAllCoures();


        }


        //print data from datagridview to text file
        private void buttonPrint_Click(object sender, EventArgs e)
        {
            //our file path
            //the file name=courses_list.text
            //location=in the desktop
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\courses_list.txt";

            using (var writer = new StreamWriter(path))
            {

                //check if the file exists
                if (!File.Exists(path))
                {
                    File.Create(path);

                }

                //rows
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    //the column
                    for (int j = 0; j < dataGridView1.Columns.Count - 1; j++)
                    {
                        writer.Write("\t" + dataGridView1.Rows[i].Cells[j].Value.ToString() + "\t" + "            |");

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
