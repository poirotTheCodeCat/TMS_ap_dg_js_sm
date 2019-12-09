/*
 * File Name: ExternalComm.cs
 * Program Name: TMS_ap_dg_js_sm
 * Programmers: Arron Perry, Daniel Grew, John Stanley, Sasha Malesevic
 * First Version: 2019-12-09
 */
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
        private string connectionString; ///< The connection string for connecting to the external CMS database. Gathered from App.config.
        public string serverIp { get; set; } ///< The IP used to connect to the external server. Gathered from App.config.
        public string serverPort { get; set; } ///< The Port used to connect to the external sever. Gathered from App.config.


        public ExternalComm()
        {
            //establish connection string
            serverIp = ConfigurationManager.AppSettings["ipInfo"];
            serverPort = ConfigurationManager.AppSettings["portInfo"];
            connectionString = string.Format(ConfigurationManager.ConnectionStrings["cmConnStr"].ConnectionString, serverIp, serverPort);
        }
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
