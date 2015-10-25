using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CheckInProgram
{
    public class Student
    {
        int _ID;
        string _Name;
        int _State;
        string _Room;
        public int ID
        {
            get { return _ID; }
        }
        public string Name
        {
            get { return _Name; }
        }
        public int State
        {
            get { return _State; }
        }
        public string Room
        {
            get { return _Room; }
        }
        public Student(object id,object name,object state,object room)
        {
            _ID = int.Parse(id.ToString());
            _Name = name.ToString();
            _State = int.Parse(state.ToString());
            _Room = room.ToString();
        }
        public Student(int id,bool isSplit)
        {
            SqlCommand cmd = new SqlCommand("dbo.SP_CheckIn_DoCheckIn", Program.conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
            cmd.Parameters.Add(new SqlParameter("@ip", SqlDbType.VarChar, 15));
            cmd.Parameters.Add(new SqlParameter("@name", SqlDbType.NVarChar, 20));
            cmd.Parameters.Add(new SqlParameter("@state", SqlDbType.TinyInt));
            cmd.Parameters.Add(new SqlParameter("@room", SqlDbType.NVarChar, 10));
            cmd.Parameters.Add(new SqlParameter("@result", SqlDbType.SmallInt));
            cmd.Parameters["@name"].Direction = ParameterDirection.Output;
            cmd.Parameters["@state"].Direction = ParameterDirection.Output;
            cmd.Parameters["@room"].Direction = ParameterDirection.Output;
            cmd.Parameters["@result"].Direction = ParameterDirection.Output;
            cmd.Parameters["@id"].Value = id;
            cmd.Parameters["@ip"].Value = Program.GetLocalIp();
            Program.conn.Open();
            cmd.ExecuteScalar();
            Program.conn.Close();
            int result = int.Parse(cmd.Parameters["@result"].Value.ToString());
            switch (result)
            {
                case 301:
                case 311:
                    _ID = int.Parse(cmd.Parameters["@id"].Value.ToString());
                    _Name = cmd.Parameters["@name"].Value.ToString();
                    _State = int.Parse(cmd.Parameters["@state"].Value.ToString());
                    _Room = cmd.Parameters["@room"].Value.ToString();
                    Print.infomsg("签到成功，你可以开始你的学习了！", "签到成功");
                    break;
                case 305:
                    if (isSplit)
                    {
                        Print.infomsg("分流系统登陆成功，你可以开始你的自习了！", "登陆成功");
                        Application.Exit();
                        break;
                    }
                    else
                    {
                        goto default;
                    }
                default:
                    if (!isSplit)
                    {
                        Print.show(result);
                        Application.Exit();
                    }
                    break;
            }
        }
        public static string CheckIsOK(int id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("dbo.SP_CheckIn_Check", Program.conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@ip", SqlDbType.VarChar, 15));
                cmd.Parameters.Add(new SqlParameter("@name", SqlDbType.NVarChar, 20));
                cmd.Parameters.Add(new SqlParameter("@result", SqlDbType.SmallInt));
                cmd.Parameters["@name"].Direction = ParameterDirection.Output;
                cmd.Parameters["@result"].Direction = ParameterDirection.Output;
                cmd.Parameters["@id"].Value = id;
                cmd.Parameters["@ip"].Value = Program.GetLocalIp();
                Program.conn.Open();
                cmd.ExecuteScalar();
                Program.conn.Close();
                int result = int.Parse(cmd.Parameters["@result"].Value.ToString());
                switch (result)
                {
                    case 201:
                        return (string)cmd.Parameters["@name"].Value;
                    default:
                        Print.show(result);
                        break;
                }
                return null;
            }
            catch (Exception ex)
            {
                Print.show(ex.Message);
                return null;
            }
            finally
            {
                Program.conn.Close();
            }
        }
        public static int SplitIsOK(int id,string name)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("dbo.SP_CheckIn_Split", Program.conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@ip", SqlDbType.VarChar, 15));
                cmd.Parameters.Add(new SqlParameter("@name", SqlDbType.NVarChar, 20));
                cmd.Parameters.Add(new SqlParameter("@wtime", SqlDbType.SmallInt));
                cmd.Parameters.Add(new SqlParameter("@lstate", SqlDbType.Bit));
                cmd.Parameters.Add(new SqlParameter("@sstate", SqlDbType.Bit));
                cmd.Parameters["@wtime"].Direction = ParameterDirection.Output;
                cmd.Parameters["@lstate"].Direction = ParameterDirection.Output;
                cmd.Parameters["@sstate"].Direction = ParameterDirection.Output;
                cmd.Parameters["@id"].Value = id;
                cmd.Parameters["@ip"].Value = Program.GetLocalIp();
                cmd.Parameters["@name"].Value = name;
                Program.conn.Open();
                cmd.ExecuteScalar();
                Program.conn.Close();
                if (bool.Parse(cmd.Parameters["@lstate"].Value.ToString()) == false)
                {
                    Print.infomsg("分流系统登陆成功，你可以开始你的学习了！", "登陆成功");
                    Application.Exit();
                }
                else
                {
                    if (bool.Parse(cmd.Parameters["@sstate"].Value.ToString()) == true)
                    {
                        if (int.Parse(cmd.Parameters["@wtime"].Value.ToString()) > 20)
                        {
                            Print.infomsg("分流系统登陆成功，你可以开始你的学习了！", "登陆成功");
                            Application.Exit();
                        }
                        else
                        {
                            if (int.Parse(cmd.Parameters["@wtime"].Value.ToString()) != -99)
                                return 99;
                            else
                                return int.Parse(cmd.Parameters["@wtime"].Value.ToString());
                        }
                    }
                    else
                    {
                        return int.Parse(cmd.Parameters["@wtime"].Value.ToString());
                    }
                }
                return 0;
            }
            catch (Exception ex)
            {
                Print.show(ex.Message);
                return 0;
            }
            finally
            {
                Program.conn.Close();
            }
        }
        public bool DatabaseTransport(string sp)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(sp, Program.conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@result", SqlDbType.SmallInt));
                cmd.Parameters["@result"].Direction = ParameterDirection.Output;
                cmd.Parameters["@id"].Value = ID;
                Program.conn.Open();
                cmd.ExecuteNonQuery();
                Program.conn.Close();
                int result = int.Parse(cmd.Parameters["@result"].Value.ToString());
                switch (result)
                {
                    case 401:
                    case 601:
                    case 701:
                    case 801:
                        return true;
                    default:
                        Print.show(result);
                        return false;
                }
            }
            catch (Exception ex)
            {
                Print.show(ex.Message);
                return false;
            }
            finally
            {
                Program.conn.Close();
            }
        }
        public void Query()
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Program.conn;
                cmd.CommandText = "dbo.SP_CheckIn_Query";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("id", SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("normal", SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("late", SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("truency", SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("result", SqlDbType.SmallInt));
                cmd.Parameters["normal"].Direction = ParameterDirection.Output;
                cmd.Parameters["late"].Direction = ParameterDirection.Output;
                cmd.Parameters["truency"].Direction = ParameterDirection.Output;
                cmd.Parameters["result"].Direction = ParameterDirection.Output;
                cmd.Parameters["id"].Value = ID;
                Program.conn.Open();
                cmd.ExecuteNonQuery();
                Program.conn.Close();
                switch (int.Parse(cmd.Parameters["result"].Value.ToString()))
                {
                    case 900:
                    case 902:
                        Print.show(int.Parse(cmd.Parameters["result"].Value.ToString()));
                        break;
                    case 901:
                        Print.infomsg(string.Format("除本次上课记录外，你还有{0}次正常记录,{1}次迟到记录,{2}次旷课记录，如有问题请联系值班员查询详细记录。", cmd.Parameters["normal"].Value, cmd.Parameters["late"].Value, cmd.Parameters["truency"].Value), "查询结果");
                        break;
                }
            }
            finally
            {
                Program.conn.Close();
            }
        }
    }
}
