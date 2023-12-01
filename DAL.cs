using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payrole
{
    public class DAL
    {
        public ConnectionState Connection = ConnectionState.Closed;//bydefault connection is closed until it is open
        public bool isPROcall = false;
        List<SqlParameter> paralist = new List<SqlParameter>();
        public SqlConnection GetConnection()
        {
            string ConnectionString = ConfigurationManager.AppSettings
                ["SqlConnectionString"];//square brac denote key
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            return con;
        }
        public void ClearParameters()
        {
            paralist.Clear();
        }
        public void AddParameters(string paraname,string value)
        {
            paralist.Add(new SqlParameter(paraname,value));
        }
        private SqlCommand GetCommand(string Query)
        {
            SqlCommand cmd = new SqlCommand();
            if (isPROcall)
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddRange(paralist.ToArray());
            }
            else
            {
                cmd.CommandType = CommandType.Text;
            }
           // SqlCommand cmd = new SqlCommand();
            cmd.CommandText = Query;
            cmd.Connection = GetConnection();
            return cmd;

        }
        public DataSet GetTables(string Query)
        {
            SqlDataAdapter da = new SqlDataAdapter(GetCommand(Query));
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }
        public DataTable GetTable(string Query)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = GetCommand(Query);

            cmd.Connection.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            if(rdr!=null && rdr.HasRows)
            {
                dt.Load(rdr);

                cmd.Connection.Close();
            }
            return dt;
        }
        public object GetID(string Query)
        {
            SqlCommand cmd = GetCommand(Query);
            cmd.Connection.Open();
            object retval = cmd.ExecuteScalar();
            cmd.Connection.Close();
            return retval;
        }
        public int ExecuteQuery(string Query)
        {
            SqlCommand cmd = GetCommand(Query);
            cmd.Connection.Open();
            int retval = cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            return retval;

        }
        public SqlDataReader GetReader(string Query)
        {
            SqlCommand cmd = GetCommand(Query);
            cmd.Connection.Open();
            SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            return rdr;
        }
    }
}
