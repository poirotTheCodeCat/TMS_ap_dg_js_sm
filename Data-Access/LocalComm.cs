using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS
{
    /// \class LocalComm
    /// <summary>
    /// Responsible for any communications with the local TMS database. This class references 
    /// the Order class as it returns those objects to the classes using LocalComm. It is used by
    /// Admin, Buyer, and Planner classes. 
    /// </summary>
    class LocalComm
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["tmsConnStr"].ConnectionString;
        /// <summary>
        /// This method adds a new Order to the TMS database.
        /// <param name="searchItem">The identifier for the Contract that will be used 
        ///                         to create the new Order</param>
        /// </summary>
        /// <returns>Order that has been created.</returns>
        public Order CreateOrder(string searchItem)
        {
            Order newOrder = null;
            return newOrder;
        }

        /// <summary>
        /// This method queries the TMS database for the requested Orders.
        /// </summary>
        /// <param name="searchItem">The identifier for the Orders that are being retrieved.</param>
        /// <returns>List of Orders requested.</returns>
        public List<Order> GetOrders(string searchItem)
        {
            List<Order> completedOrderList = null;
            return completedOrderList;
        }

        /// <summary>
        /// This method queries the TMS database for the invoice requested.
        /// </summary>
        /// <param name="searchItem">The identifier for the Order that requires
        ///                             an invoice</param>
        /// <returns>String containing the information required for an invoice.</returns>
        public string GetInvoice(string searchItem)
        {
            string invoice = "";
            return invoice;
        }

        /// <summary>
        /// This method updates an Order's status in the TMS database.
        /// </summary>
        /// <param name="searchItem1">The identifier for the Order that requires
        ///                             an update</param>
        /// <param name="searchItem2">The identifier for the Status being added to
        ///                             the Order</param>
        /// <returns>String containing the information required for an invoice.</returns>
        public Order UpdateOrderStatus(string searchItem1, string searchItem2)
        {
            Order order = new Order();
            return order;
        }

        /// <summary>
        /// This method will query the Contract Marketplace database for Contracts. 
        /// </summary>
        /// <returns>List<Contract> of the contracts from the Contract Marketplace.</returns>
        public void AddContract(Contract pending)
        {

            using (var myConn = new MySqlConnection(connectionString))
            {

                var myCommand = new MySqlCommand("AddContract", myConn);
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("@status", pending.ContractStatus);
                myCommand.Parameters.AddWithValue("@name", pending.ClientName);
                myCommand.Parameters.AddWithValue("@jType", pending.JobType);
                myCommand.Parameters.AddWithValue("@vType", pending.VanType);
                myCommand.Parameters.AddWithValue("@quant", pending.Quantity);
                myCommand.Parameters.AddWithValue("@orig", pending.Origin);
                myCommand.Parameters.AddWithValue("@dest", pending.Destination);

                myConn.Open();

                myCommand.ExecuteNonQuery();

            }
        }

        public List<Contract> GetPendingContracts()
        {
            const string sqlStatement = @" SELECT * FROM Contract WHERE ContractStatus=0;";


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
        /// retrieves a list of carriers from the database and generates a list of Carrier objects which it returns
        /// </summary>
        /// <returns></returns>
        public List<Carrier> getCarriers()
        {
            using (var myConn = new MySqlConnection(connectionString))
            {
                const string SqlStatement = @"SELECT * FROM Carriers";

                var myCommand = new MySqlCommand(SqlStatement, myConn);
                var myAdapter = new MySqlDataAdapter
                {
                    SelectCommand = myCommand
                };

                var dataTable = new DataTable();

                myAdapter.Fill(dataTable);
                return GetCarriersList(dataTable);
            }
        }


        /// <summary>
        /// This generates a list of carriers from what is returned from the Carriers Table within the database
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        private List<Carrier> GetCarriersList(DataTable table)
        {
            var carriers = new List<Carrier>();
           

            foreach (DataRow row in table.Rows)
            {
               

                carriers.Add(new Carrier
                {
                    CarrierID = Convert.ToInt32(row["CarrierID"]),
                    CarrierName = row["CarrierName"].ToString(),
                    DepotCity = row["DepotCity"].ToString(),
                    FtlAvail = Convert.ToInt32(row["FtlAvail"]),
                    LtlAvail = Convert.ToInt32(row["LtlAvail"]),
                    FtlRate = Convert.ToDouble(row["FtlRate"]),
                    LtlRate = Convert.ToDouble(row["LtlRate"]),
                    ReefRate = Convert.ToDouble(row["ReefRate"]),
                });
            }
            return carriers;
        }

       
        private List<Contract> DataTableToContractsList(DataTable table)
        {
            var contracts = new List<Contract>();

            foreach (DataRow row in table.Rows)
            {
                contracts.Add(new Contract
                {

                    ClientName = row["CustomerName"].ToString(),
                    ContractStatus = Convert.ToInt32(row["ContractStatus"]),
                    JobType = Convert.ToInt32(row["JobType"]),
                    Quantity = Convert.ToInt32(row["Quantity"]),
                    Origin = row["Origin"].ToString(),
                    Destination = row["Destination"].ToString(),
                    VanType = Convert.ToInt32(row["VanType"]),

                });;
            }

            return contracts;
        }
    }
}
