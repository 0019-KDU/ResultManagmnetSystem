using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResultManagment
{
    internal class SCORE
    {
        MyDb mydb=new MyDb();

        //creata a fuction to add new Score

        public bool insertScore(int studentID,int courseID,float score,string discription) 
        {
           SqlCommand cmd = new SqlCommand("INSERT INTO score(studentid,courseid,score,discription)VALUES(@sid,@cid,@scr,@dscr)",mydb.getConnection);

            cmd.Parameters.Add("@sid",SqlDbType.Int).Value=studentID;
            cmd.Parameters.Add("@cid",SqlDbType.Int).Value=courseID;
            cmd.Parameters.Add("@scr",SqlDbType.Float).Value=score;
            cmd.Parameters.Add("@dscr",SqlDbType.VarChar).Value=discription;


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

        //create a function to check if a score is already asigned to this student in this score 
        public bool studentScoreExists(int studentID,int courseID)
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM score WHERE studentid=@sid AND courseid=@cid",mydb.getConnection);

            cmd.Parameters.Add("@sid", SqlDbType.Int).Value = studentID;
            cmd.Parameters.Add("@cid", SqlDbType.Int).Value = courseID;

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable(); 
            adapter.Fill(dt);

            if (dt.Rows.Count==0)
            {

                return false;
            }
            else
            {
               
                return true;
            }

        }

        //create function to get students score
        public DataTable getstudentsScore() 
        {
            SqlCommand cmd = new SqlCommand("SELECT score.studentid,Addstd.FirstName,Addstd.LastName,score.courseid,Course.Label,score.score FROM Addstd INNER JOIN score ON Addstd.id=score.studentid INNER JOIN Course ON score.courseid=Course.id",mydb.getConnection);
            SqlDataAdapter adapter= new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt) ;

            return dt ;
        }

        //function to Remove score by student and course
        public bool deleteScore(int studentid,int Courseid)
        {
            SqlCommand cmd = new SqlCommand("DELETE FROM score WHERE studentid=@sid AND courseid=@cid", mydb.getConnection);

            cmd.Parameters.Add("sid", SqlDbType.Int).Value =studentid;
            cmd.Parameters.Add("cid", SqlDbType.Int).Value = Courseid;

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

        //create a function to get the average score by course
        public DataTable avgScoreByCourse()
        {
            SqlCommand cmd = new SqlCommand("SELECT Course.Label,avg(score.score)AS'Average Score'FROM Course,score WHERE course.id=score.courseid GROUP BY Course.Label", mydb.getConnection);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            return dt;

        }

        //get course scores 
        public DataTable getCoresScores(int courseID)
        {
            SqlCommand cmd = new SqlCommand("SELECT score.studentid,Addstd.Firstname,Addstd.LastName,score.courseid,Course.Label,score.score FROM Addstd INNER JOIN score ON Addstd.id=score.studentid INNER JOIN Course ON score.courseid=Course.id WHERE score.courseid="+courseID, mydb.getConnection);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            return dt;

        }

        //get students scores 
        public DataTable getStudentScores(int studentID)
        {
            SqlCommand cmd = new SqlCommand("SELECT score.studentid,Addstd.Firstname,Addstd.LastName,score.courseid,Course.Label,score.score FROM Addstd INNER JOIN score ON Addstd.id=score.studentid INNER JOIN Course ON score.courseid=Course.id WHERE score.studentid=" + studentID, mydb.getConnection);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            return dt;

        }
    }
}
