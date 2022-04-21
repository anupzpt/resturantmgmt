using DMS.DAL.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Helper
{
    public class SQLHelper
    {
        public string ConfigName => "DMSEntities";
        #region "Save Excel to DB"

        public async Task<bool> SaveFile2DB(string filePath, string SQL, string DestTable)
        {
            //String strConnection = "Data Source=.\\SQLEXPRESS;AttachDbFilename='C:\\Users\\Hemant\\documents\\visual studio 2010\\Projects\\CRMdata\\CRMdata\\App_Data\\Database1.mdf';Integrated Security=True;User Instance=True";

            String excelConnString = String.Format(Properties.Settings.Default.ExcelConnectionString, filePath);


            //Create Connection to Excel work book 
            using (OleDbConnection excelConnection = new OleDbConnection(excelConnString))
            {
                excelConnection.Open();
                string myTableName = excelConnection.GetSchema("Tables").Rows[0]["TABLE_NAME"].ToString();

                //Create OleDbCommand to fetch data from Excel 
                using (OleDbCommand cmd = new OleDbCommand(string.Format(SQL, myTableName), excelConnection))
                {
                    using (OleDbDataReader dReader = cmd.ExecuteReader())
                    {
                        string cStr = ConfigurationManager.ConnectionStrings[ConfigName].ConnectionString;
                        using (SqlBulkCopy sqlBulk = new SqlBulkCopy(cStr))
                        {
                            //Give your Destination table name 
                            sqlBulk.DestinationTableName = DestTable;
                            await sqlBulk.WriteToServerAsync(dReader);
                        }
                    }
                }
            }
            return true;
        }
        #endregion
    }
}
