using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResultManagment
{
    internal class COURSE
    {
        MyDb  mydb=new MyDb();

        //Create Function Insert Course
        public bool insertCouerse(string courseName, int hourNumber, string discription) 
        {
            SqlCommand cmd = new SqlCommand("INSERT INTO Course(Label,HoursNumber,Description)VALUES(@name,@hrs,@dscr)", mydb.getConnection);

            cmd.Parameters.Add("@name",SqlDbType.VarChar).Value = courseName;
            cmd.Parameters.Add("@hrs",SqlDbType.Int).Value = hourNumber;
            cmd.Parameters.Add("@dscr",SqlDbType.Text).Value=discription;

            mydb.openConnection();

            if (cmd.ExecuteNonQuery() == 1)
            {
                mydb.closeConnection(); 
                return true;
            }
            else 
            {
                mydb.closeConnection();
                return false;
            }

        }
        //create a function to check if the course name already exists in the database 
        //when we edit the course we need to exclude the current course from the name 
        //useing the course id
        //by default we will set the course id to 0
        public bool checkCourseName(string courseName,int courseID=0) {

            SqlCommand cmd = new SqlCommand("SELECT * FROM Course WHERE Label=@cName AND id<>@cid", mydb.getConnection);

            cmd.Parameters.Add("@cid",SqlDbType.Int).Value=courseID;
            cmd.Parameters.Add("@cName",SqlDbType.VarChar).Value=courseName;
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable(); 
            adapter.Fill(dt);

            if (dt.Rows.Count > 0)
            {

                mydb.closeConnection();
                //return false if this course name already exists
                return false;
            }
            else 
            {
                mydb.closeConnection();
                return true;
            
            }

        }

        //function to Remove Course by id
        public bool deleteCourse(int CourseID) 
        {
            SqlCommand cmd = new SqlCommand("DELETE FROM Course WHERE id=@courseID",mydb.getConnection);
            cmd.Parameters.Add("courseID", SqlDbType.Int).Value = CourseID;

            mydb.openConnection();

            if (cmd.ExecuteNonQuery() == 1) 
            {
                mydb.closeConnection();
                return true;
            }
            else
            {
                mydb.closeConnection();
                return false;
            }

        }

        //create a function to get all courses
        public DataTable getAllCoures() 
        {

            SqlCommand cmd = new SqlCommand("SELECT * FROM Course", mydb.getConnection);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            return dt;

        }

        //create a function to get a course by id 
        public DataTable getCouresByID(int courseID)
        {

            SqlCommand cmd = new SqlCommand("SELECT * FROM Course WHERE id=@cid ", mydb.getConnection);

            cmd.Parameters.Add("@cid",SqlDbType.Int).Value=courseID;

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            return dt;

        }

        //create function to edit the selected course
        public bool updateCourse(int courseID, string courseName, int hourNumber, string discription) 
        {

            SqlCommand cmd = new SqlCommand("UPDATE Course SET Label=@name,HoursNumber=@hrs,Description=@dscr WHERE id=@cid", mydb.getConnection);

            cmd.Parameters.Add("@cid", SqlDbType.Int).Value = courseID;
            cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = courseName;
            cmd.Parameters.Add("@hrs", SqlDbType.Int).Value = hourNumber;
            cmd.Parameters.Add("@dscr", SqlDbType.Text).Value = discription;

            mydb.openConnection();

            if (cmd.ExecuteNonQuery() == 1)
            {
                mydb.closeConnection();
                return true;
            }
            else
            {
                mydb.closeConnection();
                return false;
            }
        }

        //create function to execute the count querise

        public string execCount(string query)
        {

            SqlCommand sqlCommand = new SqlCommand(query, mydb.getConnection);
            mydb.openConnection();
            string count = sqlCommand.ExecuteScalar().ToString();
            mydb.closeConnection();

            return count;
        }

        //get the total students

        public string totalCourses()
        {
            return execCount("SELECT COUNT(*) FROM Course");

        }
    }
}
