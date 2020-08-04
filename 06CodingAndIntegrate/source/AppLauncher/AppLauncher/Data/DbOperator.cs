using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Infrastructure.Design;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLauncher.Data
{
    class DbOperator
    {
        private SQLiteConnection _con;

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="newPassword">新密码</param>
        public void ChangePassword(string newPassword)
        {
            _con = new SQLiteConnection
            {
                ConnectionString = ConfigurationManager.ConnectionStrings["dbConn"].ConnectionString
            };
            try
            {
                _con.Open();
            }
            catch (Exception ex)
            {
                throw new Exception("无法连接到数据库!" + ex.Message);
            }
            //_con.ChangePassword(newPassword);
            _con.Close();
        }
    }
}
