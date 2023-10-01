using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResultManagment
{
    internal class Student
    {

        MyDb db=new MyDb();
        //create functuion to add new student to the databse

        public bool inserAddstd(string fname, string lname, DateTime bdate,string gender, string phone, string address, MemoryStream picture) 
        {
            SqlCommand cmd = new SqlCommand("INSERT INTO Addstd(FirstName,LastName,Birthdate,Gender,Phone,Address,Picture) VALUES(@fn,@ln,@bdt,@gdr,@phn,@add,@pic)", db.getConnection);
            cmd.Parameters.Add("@fn", SqlDbType.VarChar).Value = fname;
            cmd.Parameters.Add("@ln", SqlDbType.VarChar).Value = lname;
            cmd.Parameters.Add("@bdt", SqlDbType.Date).Value = bdate;
            cmd.Parameters.Add("@gdr",SqlDbType.VarChar).Value=gender;
            cmd.Parameters.Add("@phn", SqlDbType.VarChar).Value = phone;
            cmd.Parameters.Add("@add", SqlDbType.Text).Value = address;
            cmd.Parameters.Add("@pic", SqlDbType.Image).Value = picture.ToArray();

            db.openConnection();

            if (cmd.ExecuteNonQuery() == 1)
            {

                db.closeConnection();
                return true;
            }
            else 
            { 
                db.closeConnection();
                return false;

            
            }

            
        }

        //create  a function to return table of student data

        public DataTable getStudents(SqlCommand cmd) 
        {

            cmd.Connection = db.getConnection;
            SqlDataAdapter  adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            return dt;

        }

        //create function to update students information

        public bool updateStudent(int id,string fname, string lname, DateTime bdate, string gender, string phone, string address, MemoryStream picture)
        {
            
                SqlCommand cmd = new SqlCommand("UPDATE Addstd SET Firstname=@fn,LastName=@ln,Birthdate=@bdt,Gender=@gdr,Phone=@phn,Address=@add,Picture=@pic WHERE id=@id", db.getConnection);
                cmd.Parameters.Add("@id", SqlDbType.Int).Value =id;
                cmd.Parameters.Add("@fn",SqlDbType.VarChar).Value = fname;
                cmd.Parameters.Add("@ln", SqlDbType.VarChar).Value = lname;
                cmd.Parameters.Add("@bdt", SqlDbType.Date).Value = bdate;
                cmd.Parameters.Add("@gdr", SqlDbType.VarChar).Value = gender;
                cmd.Parameters.Add("@phn", SqlDbType.VarChar).Value = phone;
                cmd.Parameters.Add("@add", SqlDbType.Text).Value = address;
                cmd.Parameters.Add("@pic", SqlDbType.Image).Value = picture.ToArray();

                db.openConnection();

                if (cmd.ExecuteNonQuery() == 1)
                {

                    db.closeConnection();
                    return true;
                }
                else
                {
                    db.closeConnection();
                    return false;


                }

            }

        //create function to deleted the selected student

        public bool deleteStudent(int id) 
        {
            SqlCommand cmd = new SqlCommand("DELETE FROM Addstd WHERE id=@stdid", db.getConnection);

            cmd.Parameters.Add("@stdid", SqlDbType.Int).Value = id;

            db.openConnection();

            if (cmd.ExecuteNonQuery() == 1)
            {

                db.closeConnection();
                return true;
            }
            else
            {
                db.closeConnection();
                return false;


            }

        }

        //create function to execute the count querise

        public string execCount(string query) 
        {
        
            SqlCommand sqlCommand = new SqlCommand(query, db.getConnection);
            db.openConnection();
            string count=sqlCommand.ExecuteScalar().ToString();
            db.closeConnection();

            return count;
        }

        //get the total students

        public string totalStudent() 
        {
            return execCount("SELECT COUNT(*) FROM Addstd");
        
        }

        //get the total male Students

        public string totalMaleStudent() 
        {
            return execCount("SELECT COUNT(*) FROM Addstd WHERE Gender='Male'");
        
        }

        //get the total female Students

        public string totalFemaleStudent()
        {
            return execCount("SELECT COUNT(*) FROM Addstd WHERE Gender='Female'");

        }
    }
}
