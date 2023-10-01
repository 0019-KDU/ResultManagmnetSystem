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
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        MyDb MyDb=new MyDb();
        public static string ut;
        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textun.Text == "" || textpw.Text == "") 
            {
                MessageBox.Show("Provide Username or Password");
            }
            try 
            {
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT UserType FROM Userlg WHERE UserName LIKE'"+textun.Text+"' AND Password LIKE'"+textpw.Text+"'",MyDb.getConnection);
                DataTable dt = new DataTable(); 
                sqlDataAdapter.Fill(dt);
                if (dt.Rows.Count == 1)
                {
                    ut = dt.Rows[0][0].ToString();
                    if (ut == "ADMIN")
                    {
                        MainFrom mainFrom = new MainFrom();
                        mainFrom.Show();
                        this.Hide();
                    }
                    else if (ut == "USER") 
                    {
                        UserForm userForm = new UserForm(); 
                        userForm.Show();
                        this.Hide();
                    }

                }
                else 
                {
                    MessageBox.Show("Check Your Username Or Password","Error Message",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    textun.Text = "";
                    textpw.Text = "";
                }

            }
            catch(Exception ex)  
            { 
                
            }
        }

        private void textun_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
