using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.Business_Layer;
using TMS;

namespace TMS
{
    /// \class Buyer
    /// <summary>
    /// Represents a Buyer user of the TMS program. The Buyer makes use of the LocalComm class 
    /// to update the TMS database and the ExternalComm class to communicate with the Contract
    /// Marketplace. It also makes use of the Order class for managing Orders.
    /// </summary>
    public class Buyer
    {
        /// <summary>
        /// This method will allow the Buyer to get contracts from the Contract Marketplace database. 
        /// </summary>
        /// <returns>Listmof the Contracts received from the Contract Marketplace.</returns>
        public List<Contract> GetMarketplaceContracts()
        {
            List<Contract> contractList = new ExternalComm().GetMarketplaceContracts();
            return contractList; 
        }


        /// <summary>
        /// This method will allow the Buyer to generate an invoice as a text file for the 
        /// requested completedOrder.
        /// </summary>
        /// /// <param name="contract">The identifier for the Order that requires
        ///                             an invoice</param>
        /// <returns>None.</returns>
        public void GenerateInvoice(Contract contract)
        {
            string pathString = "~\\Invoices\\Invoice" + contract.ContractID.ToString() + ".txt";
            string[] lines;
            double distance = new Planner().CalculateDistance(contract.Origin, contract.Destination);
            string[] buildLine =
            {
                "Invoice\n", "Order ID: " + contract.ContractID.ToString(),
                "\nClient Name: " + contract.ClientName,
                "\nCompleted Date: " + contract.EndTime,
                "\nVan Type: " + contract.vanTypeString,
                "\nOrigin City: " + contract.Origin,
                "\nDestination City: " + contract.Destination,
                "\nDistance Travelled: " + distance.ToString("F") + " km",
                "\n\nTotal Cost: $" + contract.Price.ToString("F")
            };

            lines = buildLine;
       
            if (!System.IO.File.Exists(pathString))
            {
                var newFile = System.IO.File.Create(pathString);
                newFile.Close();
            }

            //else open file and write each line to it
            using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(pathString, true))
            {
                foreach (var line in lines)
                {
                    file.Write(line);
                }
            }
        }

    public void AddContract(Contract newContract)
        {
            LocalComm comm = new LocalComm();
            comm.AddContract(newContract);
        }

    }
}
