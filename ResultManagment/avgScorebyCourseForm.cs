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
    public partial class avgScorebyCourseForm : Form
    {
        public avgScorebyCourseForm()
        {
            InitializeComponent();
        }

        private void avgScorebyCourseForm_Load(object sender, EventArgs e)
        {
            //popolate the datagridview with average score by course
            SCORE scourse = new SCORE();
            dataGridView1.DataSource = scourse.avgScoreByCourse();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
