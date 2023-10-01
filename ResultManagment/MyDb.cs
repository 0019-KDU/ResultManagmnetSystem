using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;

namespace ResultManagment
{
    internal class MyDb
    {
        //the connection
        SqlConnection conn=new SqlConnection("Data Source=LAPTOP-81O6Q9UO\\SQLEXPRESS;Initial Catalog=RadDBConnection;Integrated Security=True");


        //create a function to get a connection
        public SqlConnection getConnection
        {
            get 
            { 
            
            return conn;
            
            }
        
        
        }

        //create a function a to open the connection
        public void openConnection() 
        { 
        
                if (conn.State==ConnectionState.Closed) {
                
                    conn.Open();
            }
        
        }


        //create a function a to close the connection
        public void closeConnection()
        {

            if (conn.State == ConnectionState.Open)
            {

                conn.Close();
            }

        }
    }
}
