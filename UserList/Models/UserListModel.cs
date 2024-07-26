using System.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;

using System.Data.SqlClient;
using System.Reflection;

namespace UserList.Models
{
    public class showUserList
    {
        private readonly IConfiguration _configuration;
        SqlDataAdapter da;
        DataSet ds = new DataSet();

        public showUserList(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public object showdata()
        {
            string connectionstring = _configuration.GetConnectionString("DefaultConnectionString");
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();
            SqlCommand cmd = new SqlCommand("select * from userlistdata", connection);
            da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            connection.Close();
            return ds;
        }
        public void AddData( string firstname,string lastname,int salary,string gender)
        {
            string connectionstring = _configuration.GetConnectionString("DefaultConnectionString");
            SqlConnection connection = new SqlConnection(connectionstring);
            SqlCommand cmd = new SqlCommand("insert into userlistdata(firstname,lastname,salary,gender)" + "values (@firstname,@lastname,@salary,@gender)", connection);
            connection.Open();
            cmd.Parameters.AddWithValue("@firstname", firstname);
            cmd.Parameters.AddWithValue("@lastname", lastname);
            cmd.Parameters.AddWithValue("@salary", salary);
            cmd.Parameters.AddWithValue("@gender", gender);
            cmd.ExecuteNonQuery();

            connection.Close();
        }
        public void DeleteData(int id)
        {
            string connectionstring = _configuration.GetConnectionString("DefaultConnectionString");
            SqlConnection connection = new SqlConnection(connectionstring);
            SqlCommand cmd = new SqlCommand("delete from userlistdata where userid=@id;", connection);
            connection.Open();
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();

            connection.Close();
        }
        public void EditData(int userid, string firstName, string lastName, int salary, string gender)
        {
            string connectionstring = _configuration.GetConnectionString("DefaultConnectionString");
            SqlConnection connection = new SqlConnection(connectionstring);
            SqlCommand cmd = new SqlCommand("UPDATE userlistdata SET firstname = @firstname, lastname= @lastname, salary=@salary ,gender=@gender where  userid=@id", connection);
            connection.Open();
            cmd.Parameters.AddWithValue("@id", userid);
            cmd.Parameters.AddWithValue("@firstname", firstName);
            cmd.Parameters.AddWithValue("@lastname", lastName);
            cmd.Parameters.AddWithValue("@salary", salary);
            cmd.Parameters.AddWithValue("@gender", gender);
            cmd.ExecuteNonQuery();

            connection.Close();
        }
        public class UserListData
        {
            public int userid { get; set; }
            public string firstName{get; set;}
            public string lastName { get; set;}
            public int salary { get; set;}

            public string gender { get; set;}
         
        }

    }

    }
