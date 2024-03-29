using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using DTO;

namespace DAO
{
    public class TacGiaDAO
    {
        public static DataTable GetTable()
        {
            DataTable dataTable = new DataTable();
            OleDbConnection connection = DataProvider.CreateConnection();
            string cmdText = "Select * from TACGIA";
            OleDbDataAdapter adpter = new OleDbDataAdapter(cmdText, connection);
            adpter.Fill(dataTable);

            connection.Close();
            return dataTable;
        }

        public static IList GetList()
        {
            ArrayList arrList = new ArrayList();

            TacGiaDTO TACGIA = null;

            OleDbConnection connection = DataProvider.CreateConnection();
            string cmdText = "Select * from TACGIA";
            OleDbCommand command = new OleDbCommand(cmdText, connection);
            OleDbDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                TACGIA = new TacGiaDTO();

                TACGIA.MaTG = (int)reader["MaTG"];
                TACGIA.TenTG = (string)reader["TenTG"];
                arrList.Add(TACGIA);
            }
            reader.Close();
            connection.Close();
            return arrList;
        }
        public void UpdateTable(DataTable dataTable)
        {
            OleDbConnection connection = DataProvider.CreateConnection();
            string cmdText = "Select * from TACGIA";
            OleDbDataAdapter adapter = new OleDbDataAdapter(cmdText, connection);
            OleDbCommandBuilder cmdBuilder = new OleDbCommandBuilder(adapter);
            adapter.Update(dataTable);

            connection.Close();
        }
        public static void Insert(TacGiaDTO TACGIA)
        {
            OleDbConnection connection = DataProvider.CreateConnection();
            string cmdText = "Insert into TACGIA(TenTG) values(?)";
            OleDbCommand command = new OleDbCommand(cmdText, connection);

            command.Parameters.Add("@TenTG", OleDbType.VarWChar);
            command.Parameters["@TenTG"].Value = TACGIA.TenTG;

            command.ExecuteNonQuery();

            command.CommandText = "select @@IDENTITY";
            TACGIA.MaTG = (int)command.ExecuteScalar();

            connection.Close();
        }
        public static bool Modify(TacGiaDTO tacGia)
        {
            bool result = false;

            OleDbConnection connection = DataProvider.CreateConnection();
            string cmdText = "Update TACGIA set TenTG = ? where MaTG = ?";
            OleDbCommand command = new OleDbCommand(cmdText, connection);

            command.Parameters.Add("@TenTG", OleDbType.VarWChar);
            command.Parameters.Add("@MaTG", OleDbType.Integer);

            command.Parameters["@TenTG"].Value = tacGia.TenTG;
            command.Parameters["@MaTG"].Value = tacGia.MaTG;

            int row = command.ExecuteNonQuery();

            if (row > 0)
            {
                result = true;
            }

            return result;
        }

        public static bool Delete(int MaTG)
        {
            bool result = false;

            OleDbConnection connection = DataProvider.CreateConnection();
            string cmdText = "delete from TACGIA where MaTG = ?";
            OleDbCommand command = new OleDbCommand(cmdText, connection);
            command.Parameters.Add("@MaTG", OleDbType.Integer);
            command.Parameters["@MaTG"].Value = MaTG;

            int row = command.ExecuteNonQuery();

            if (row > 0)
            {
                result = true;
            }
            connection.Close();
            return result;
        }

        public TacGiaDTO LayTenTacGia(int maTG)
        {
            

            TacGiaDTO TACGIA = null;

            OleDbConnection connection = DataProvider.CreateConnection();
            string cmdText = "Select * from TACGIA where MaTG = ?";
            OleDbCommand command = new OleDbCommand(cmdText, connection);
            command.Parameters.Add("@MaTG", OleDbType.Integer);

            command.Parameters["@MaTG"].Value = maTG;
            OleDbDataReader reader = command.ExecuteReader();



            while (reader.Read())
            {
                TACGIA = new TacGiaDTO();

                TACGIA.MaTG = (int)reader["MaTG"];
                TACGIA.TenTG = (string)reader["TenTG"];
            }
            reader.Close();
            connection.Close();
            return TACGIA;
        }
    }
}
