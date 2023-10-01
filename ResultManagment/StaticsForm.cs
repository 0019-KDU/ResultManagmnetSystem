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
    public partial class StaticsForm : Form
    {
        public StaticsForm()
        {
            InitializeComponent();
        }

        Color panTotalColor;
        Color panMaleColor;
        Color panFemaleColor;
        private void StaticsForm_Load(object sender, EventArgs e)
        {
            //get the panels color
            panTotalColor=panelTotal.BackColor;
            panMaleColor=panelMale.BackColor;   
            panFemaleColor=panelFemale.BackColor;

            //display the values
            Student student = new Student();
            double totalstudents=Convert.ToDouble(student.totalStudent());
            double totalMaleStudent = Convert.ToDouble(student.totalMaleStudent());
            double totalFemalestudent = Convert.ToDouble(student.totalFemaleStudent());

            //count the %
            double malePercentage = totalMaleStudent * 100 / totalstudents;
            double femalePercentage = totalFemalestudent * 100 / totalstudents;

            labelTotal.Text = "Total Students:" + totalstudents.ToString();
            labelMale.Text ="Male:"+malePercentage.ToString("0.00")+"%";
            labelFemale.Text="Female:"+femalePercentage.ToString("0.00")+"%";
        }

        private void labelTotal_MouseEnter(object sender, EventArgs e)
        {
            panelTotal.BackColor=Color.White;
            labelTotal.ForeColor=panTotalColor;

        }

        private void labelTotal_MouseLeave(object sender, EventArgs e)
        {
            panelTotal.BackColor = panTotalColor;
            labelTotal.ForeColor = Color.White;

        }

        private void labelMale_MouseEnter(object sender, EventArgs e)
        {
            panelMale.BackColor = Color.White;
            labelMale.ForeColor = panMaleColor;
        }

        private void labelMale_MouseLeave(object sender, EventArgs e)
        {
            panelMale.BackColor = panMaleColor;
            labelMale.ForeColor = Color.White;
        }

        private void labelFemale_MouseEnter(object sender, EventArgs e)
        {
            panelFemale.BackColor = Color.White;
            labelFemale.ForeColor = panFemaleColor;
        }

        private void labelFemale_MouseLeave(object sender, EventArgs e)
        {
            panelFemale.BackColor = panFemaleColor;
            labelFemale.ForeColor = Color.White;
        }
    }
}
