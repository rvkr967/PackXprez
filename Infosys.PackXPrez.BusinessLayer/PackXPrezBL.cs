using Infosys.PackXPrez.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Infosys.PackXPrez.Common;

namespace Infosys.PackXPrez.BusinessLayer
{
    public class PackXPrezBL
    {
        public static int KMS;

        public int CalucalteCharges(string deliveryOption, bool packingSevice, int weight, int volume)
        {
            int pickupCharges = 200;
            int distance = KMS;
            int charges = 7 * distance;
            if (deliveryOption == "Overnight") { charges += 500; }
            else if (deliveryOption == "Express") { charges += 100; }
            if (packingSevice) { charges += 500; }
            if (weight > 5) { charges += weight * 50; }
            if (volume > 100) { charges += volume * 50; }
            return charges+pickupCharges;
        }

        public string LoginValidation(string emailId, string password)
        {
            try
            {
                Repository repository = new Repository();
                int roleId = repository.ValidateCredentials(emailId, password);
                if (roleId == 1) { return "Admin"; }
                else if (roleId == 2) { return "Customer"; }
                else { return "Invalid Credentials"; }
            }
            catch (Exception)
            {
                return "Invalid Credentials";
            }
        }

        public int ServiceValidation(int pickpin, int delpin)
        {
            int distance;
            try
            {
                Repository repository = new Repository();
                distance = repository.ValidateServiceAvailability(pickpin, delpin);
                KMS = distance;
            }
            catch (Exception)
            {
                distance = 0;
            }
            return distance;
        }

        public List<PackageHistory> GetPackageHistories(string emailId)
        {
            var history=new List<PackageHistory>();
            DataTable pkghistory;
            try
            {
                Repository repository = new Repository();
                pkghistory = repository.GetPackageHistory(emailId);
                if (pkghistory.Rows.Count != 0)
                {
                    foreach (DataRow row in pkghistory.Rows)
                    {
                        var hist = new PackageHistory();
                        hist.transactionId = Convert.ToInt64(row[0].ToString());
                        hist.number = row[1].ToString();
                        hist.fromLocation = row[2].ToString() +","+ row[3].ToString() + "," + row[4].ToString() + "," + row[5].ToString();
                        hist.toLocation = row[6].ToString();
                        hist.status = row[7].ToString();
                        history.Add(hist);
                    }  
                }
                return history;
            }
            catch (Exception)
            {
                return null; 
            }
        }

        public List<Addresses> GetAddresses(string emailId)
        {
            var addresses = new List<Addresses>();
            DataTable usraddresses;
            try
            {
                Repository repository = new Repository();
                usraddresses = repository.GetAddresses(emailId);
                if (usraddresses.Rows.Count != 0)
                {
                    foreach(DataRow row in usraddresses.Rows)
                    {
                        var addr = new Addresses();
                        addr.addressId = Convert.ToInt64(row[0].ToString());
                        addr.buildingNo = row[1].ToString();
                        addr.streetNo = row[2].ToString();
                        addr.locality = row[3].ToString();
                        addr.pincode = Convert.ToInt32(row[4].ToString());
                        addresses.Add(addr);
                    }
                }
                return addresses;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public string GetShippingStatus(long trakingId)
        {
            string status;
            try
            {
                Repository repository = new Repository();
                status = repository.TrackShipment(trakingId);
            }
            catch (Exception)
            {
                status = null;
            }
            return status;

        }

        public int ShipmentRegistration(string emailId, string shippingType, short length, short breadth, short height, int weight,
            string deliveryType, string timeSlot, int pickAddressId, string deliveryAddress,bool packingService)
        {
            short pks;
            if (packingService) { pks = 1; }
            else { pks = 0; }
            int vol = length * breadth * height;
            int charges = CalucalteCharges(deliveryType, packingService, weight, vol);

            int tranId;
            try
            {
                Repository repository = new Repository();
                tranId = repository.RegisterShipment(emailId, shippingType, length, breadth, height, weight, deliveryType, timeSlot, pickAddressId, deliveryAddress, charges, pks);
            }
            catch (Exception)
            {
                tranId = -99;
            }
            return tranId;
        }

        public bool RegisterUser(UserReg userregObj)
        {
            try
            {
                Repository repository = new Repository();
                bool stat = repository.RegisterUserdal(userregObj.name, userregObj.emailId, userregObj.password, userregObj.mobile, userregObj.buildingNo, userregObj.streetNo, userregObj.locality, userregObj.pincode);
                return stat;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
