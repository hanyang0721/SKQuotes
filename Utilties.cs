using System;
using System.Data.SqlClient;

namespace SKQuote
{
    class Utilties
    {
        //morning_tradesession is 1 is only for SKQuote
        public const int morning_tradesession = 1;
        public const int night_tradesession = 0;

        public void RecordLog(string connectionstr, string message)
        {
            using (SqlConnection connection = new SqlConnection(connectionstr))
            {
                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Parameters.Add(new SqlParameter("message", message));
                //sqlcmd.Parameters.Add(new SqlParameter("dt", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff")));
                connection.Open();
                sqlcmd.Connection = connection;
                sqlcmd.CommandText = "INSERT INTO [dbo].[SystemLog] (ExecTime, Message) VALUES (GETDATE(), CAST(@message as varchar(256)) )";
                sqlcmd.ExecuteNonQuery();
                connection.Close();
            }
        }

        public int GetTradeSession()
        {
            var tCurrent = TimeSpan.Parse(DateTime.Now.ToString("HH:mm"));
            var t1 = TimeSpan.Parse("08:45");
            var t2 = TimeSpan.Parse("13:45");
            //var t3 = TimeSpan.Parse("15:00");
            return tCurrent >= t1 && tCurrent < t2 ? morning_tradesession : night_tradesession;
        }


    }
}
