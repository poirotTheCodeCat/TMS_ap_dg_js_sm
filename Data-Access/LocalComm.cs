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


        public void UpdateContract(Contract contract)
        {
            using (var myConn = new MySqlConnection(connectionString))
            {

                var myCommand = new MySqlCommand("UpdateContract", myConn);
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("@id", contract.ContractID);
                myCommand.Parameters.AddWithValue("@endT", contract.EndTime.ToString());
                myCommand.Parameters.AddWithValue("@p", contract.Price);
                myCommand.Parameters.AddWithValue("@buyerSel", contract.BuyerSelected);
                myCommand.Parameters.AddWithValue("@plannerConf", contract.PlannerConfirmed);

                myConn.Open();

                myCommand.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// This method will query add a Contract to the TMS Database
        /// </summary>
        /// <returns>None.</returns>
        public void AddContract(Contract pending)
        {

            using (var myConn = new MySqlConnection(connectionString))
            {

                var myCommand = new MySqlCommand("AddContract", myConn);
                myCommand.CommandType = CommandType.StoredProcedure;
                //myCommand.Parameters.AddWithValue("@status", pending.ContractStatus);
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

        /// <summary>
        /// This method will request the tms database create a backup sql script to the local machine.
        /// </summary>
        /// <param name="filePath">The local save location for the sql file.</param>
        internal void BackUpDb(string filePath)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    using (MySqlBackup mb = new MySqlBackup(cmd))
                    {
                        cmd.Connection = conn;
                        conn.Open();
                        mb.ExportToFile(filePath);
                        conn.Close();
                    }
                }
            }
        }

        /// <summary>
        /// This method will add an Orderline to the TMS Database 
        /// </summary>
        /// <returns>None.</returns>
        public void AddTrip(int contract, int carrier)
        {
            using (var myConn = new MySqlConnection(connectionString))
            {

                var myCommand = new MySqlCommand("AddTrip");

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

        public List<Contract> GetLocalContracts()
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
        /// retrieves a list of carriers from the database and generates a list of Carrier objects which it returns
        /// </summary>
        /// <returns></returns>
        public List<Carrier> GetCarriers()
        {
            using (var myConn = new MySqlConnection(connectionString))
            {
                const string SqlStatement = @"SELECT * FROM Carrier";

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

        public List<TransportCorridor> GetRoutes()
        {
            using (var myConn = new MySqlConnection(connectionString))
            {
                const string SqlStatement = @"SELECT * FROM TransportCorridor";

                var myCommand = new MySqlCommand(SqlStatement, myConn);
                var myAdapter = new MySqlDataAdapter
                {
                    SelectCommand = myCommand
                };

                var dataTable = new DataTable();

                myAdapter.Fill(dataTable);

                return DataTableToRoutes(dataTable);
            }
            
        }

        /// <summary>
        /// retrieves a list of carriers from the database and generates a list of Carrier objects which it returns
        /// </summary>
        /// <returns></returns>
        public void UpdateCarrierFTL(Carrier carrier)
        {
            using (var myConn = new MySqlConnection(connectionString))
            {
                var myCommand = new MySqlCommand("UpdateCarrierFTL", myConn);
                myCommand.CommandType = CommandType.StoredProcedure;

                myCommand.Parameters.AddWithValue("@id", carrier.CarrierID);
                myCommand.Parameters.AddWithValue("@value", carrier.FtlAvail);

                myConn.Open();

                myCommand.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// retrieves a list of carriers from the database and generates a list of Carrier objects which it returns
        /// </summary>
        /// <returns></returns>
        public void UpdateCarrierLTL(Carrier carrier)
        {
            using (var myConn = new MySqlConnection(connectionString))
            {
                var myCommand = new MySqlCommand("UpdateCarrierLTL", myConn);
                myCommand.CommandType = CommandType.StoredProcedure;

                myCommand.Parameters.AddWithValue("@id", carrier.CarrierID);
                myCommand.Parameters.AddWithValue("@value", carrier.LtlAvail);

                myConn.Open();

                myCommand.ExecuteNonQuery();
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
                    FtlAvail = Convert.ToInt32(row["FtlAvail"]),
                    LtlAvail = Convert.ToInt32(row["LtlAvail"]),
                    FtlRate = Convert.ToDouble(row["FtlRate"]),
                    LtlRate = Convert.ToDouble(row["LtlRate"]),
                    ReefRate = Convert.ToDouble(row["ReefRate"]),
                }) ;
            }
            return carriers;
        }

        /// <summary>
        /// This generates a list of city names based on the carrier_ID of a carrier -> returns all cities where they are
        /// located
        /// </summary>
        /// <param name="Carrier_ID"></param>
        /// <returns></returns>
        public List<string> GetCarrierCities(int id)
        {
            using (var myConn = new MySqlConnection(connectionString))
            {
                const string SqlStatement = @"SELECT CityID FROM CarrierCities
                                            WHERE CarrierID = @id;";

                var myCommand = new MySqlCommand(SqlStatement, myConn);
                myCommand.Parameters.AddWithValue("@id", id);
                var myAdapter = new MySqlDataAdapter
                {
                    SelectCommand = myCommand
                };

                var dataTable = new DataTable();

                myAdapter.Fill(dataTable);
                return DataTableToCityList(dataTable);
            }
        }

        /// <summary>
        /// generates the list cities used by a certain carrier within the CarrierCities table 
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>

        private List<TransportCorridor> DataTableToRoutes(DataTable table)
        {
            List<TransportCorridor> routes = new List<TransportCorridor>();
            foreach (DataRow row in table.Rows)
            {
                routes.Add(new TransportCorridor
                {
                   
                    CityName = row["CityName"].ToString(),
                    Distance = Convert.ToInt32(row["Distance"]),
                    TimeBetween = Convert.ToDouble(row["TimeBetween"]),
                    West = row["West"].ToString(),
                    East = row["East"].ToString()
                });
            }
            return routes;
        }

        private List<string> DataTableToCityList(DataTable table)
        {
            List<string> cityList = new List<string>();
            foreach (DataRow row in table.Rows)
            {
                cityList.Add(row["CityID"].ToString());
            }
            return cityList;
        }
        private List<Contract> DataTableToContractsList(DataTable table)
        {
            var contracts = new List<Contract>();

            foreach (DataRow row in table.Rows)
            {
                contracts.Add(new Contract
                {
                    ContractID = Convert.ToInt32(row["ContractID"]),
                    ClientName = row["CustomerName"].ToString(),
                    JobType = Convert.ToInt32(row["JobType"]),
                    Quantity = Convert.ToInt32(row["Quantity"]),
                    Origin = row["Origin"].ToString(),
                    Destination = row["Destination"].ToString(),
                    VanType = Convert.ToInt32(row["VanType"]),
                    EndTime = string.IsNullOrWhiteSpace(row["EndTime"].ToString()) ? default(DateTime?) : DateTime.Parse(row["EndTime"].ToString()),
                    Price = Convert.ToDouble(row["Price"]),
                    BuyerSelected = Convert.ToInt32(row["BuyerSelected"]),
                    PlannerConfirmed = Convert.ToInt32(row["PlannerConfirmed"])
                });
            }

            return contracts;
        }
        /// <summary>
        /// This generates a list of city names based on the carrier_ID of a carrier -> returns all cities where they are
        /// located
        /// </summary>
        /// <param name="Carrier_ID"></param>
        /// <returns></returns>
        public List<double> GetRates()
        {
            using (var myConn = new MySqlConnection(connectionString))
            {
                const string SqlStatement = @"SELECT MarkUp FROM Rate;";

                var myCommand = new MySqlCommand(SqlStatement, myConn);
                
                var myAdapter = new MySqlDataAdapter
                {
                    SelectCommand = myCommand
                };

                var dataTable = new DataTable();

                myAdapter.Fill(dataTable);
                return DataTableToRateList(dataTable);
            }
        }

        private List<double> DataTableToRateList(DataTable table)
        {
            var rates = new List<double>();

            foreach (DataRow row in table.Rows)
            {
                rates.Add(Convert.ToDouble(row["MarkUp"]));
            }

            return rates;
        }

    }
}
