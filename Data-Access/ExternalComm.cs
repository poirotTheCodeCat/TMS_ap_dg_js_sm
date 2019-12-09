using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.Business_Layer;

namespace TMS
{
    /// \class ExternalComm
    /// <summary>
    /// Responsible for any communications outside of the TMS System such as the Contract Marketplace database.
    /// This class is used by the Buyer class which will be requesting Contracts from the Contract Marketplace.
    /// </summary>
    public class ExternalComm
    {
        private string connectionString = string.Format(ConfigurationManager.ConnectionStrings["cmConnStr"].ConnectionString, serverIp, port);
        static string serverIp = ConfigurationManager.AppSettings["ipInfo"];
        static string port = ConfigurationManager.AppSettings["portInfo"];

        /// 
        /// <summary>
        /// This method will query the Contract Marketplace database for Contracts. 
        /// </summary>
        /// <returns>List<Contract> of the contracts from the Contract Marketplace.</returns>
        public List<Contract> GetMarketplaceContracts()
        {
            const string sqlStatement = @" SELECT * FROM Contract;";


            using (var myConn = new MySqlConnection(connectionString))
            {

                var myCommand = new MySqlCommand(sqlStatement, myConn);


                //For offline connection we weill use  MySqlDataAdapter class.  
                var myAdapter = new MySqlDataAdapter
                {
                    SelectCommand = myCommand
                };

                var dataTable = new DataTable();

                myAdapter.Fill(dataTable);

                var contracts = DataTableToContractsList(dataTable);

                return contracts;
            }
        }

        /// <summary>
        /// This method extracts the contents of a datatable and inputs them into a class
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        private List<Contract> DataTableToContractsList(DataTable table)
        {
            var contracts = new List<Contract>();

            foreach (DataRow row in table.Rows)
            {
                contracts.Add(new Contract
                {
                    
                    ClientName= row["Client_Name"].ToString(),
                    JobType = Convert.ToInt32(row["Job_Type"]),
                    Quantity = Convert.ToInt32(row["Quantity"]),
                    Origin = row["Origin"].ToString(),
                    Destination = row["Destination"].ToString(),
                    VanType = Convert.ToInt32(row["Van_Type"]),

                });
            }

            return contracts;
        }
    }
}
