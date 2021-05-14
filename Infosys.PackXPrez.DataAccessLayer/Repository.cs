using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace Infosys.PackXPrez.DataAccessLayer
{
    public class Repository
    {
        SqlConnection conPackXPrez;
        SqlCommand cmdPackXPrez;
        SqlDataAdapter daPackXPrez;

        public Repository()
        {
            conPackXPrez = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=PackXprezDB; Integrated Security=SSPI");
        }

        public bool TestConnection()
        {
            SqlConnection conPackXPrez = new SqlConnection(ConfigurationManager.ConnectionStrings["conPackXPrez"].ToString());
            bool status = false;
            try
            {
                if (conPackXPrez.State == ConnectionState.Closed)
                {
                    conPackXPrez.Open();
                    status = true;
                }
            }
            catch (Exception)
            {
                status = false;
            }
            finally
            {
                conPackXPrez.Close();
            }
            return status;
        }

        public int ValidateCredentials(string emailId, string password)
        {
            int returnVal;
            cmdPackXPrez = new SqlCommand(@"SELECT [dbo].ufn_ValidateUserCredentials(@EmailId,@Password)", conPackXPrez);

            cmdPackXPrez.Parameters.AddWithValue("@EmailId", emailId);
            cmdPackXPrez.Parameters.AddWithValue("@Password", password);
            try
            {
                conPackXPrez.Open();
                returnVal = Convert.ToInt16(cmdPackXPrez.ExecuteScalar());
            }
            catch (Exception)
            {
                returnVal = -1;
            }
            finally
            {
                conPackXPrez.Close();
            }
            return returnVal;
        }

        public int ValidateServiceAvailability(int pickupPincode, int deliveryPincode)
        {
            int returnVal;
            cmdPackXPrez = new SqlCommand(@"SELECT [dbo].ufn_ValidateServiceAvailability(@PickupPincode,@DeliveryPincode)", conPackXPrez);

            cmdPackXPrez.Parameters.AddWithValue("@PickupPincode", pickupPincode);
            cmdPackXPrez.Parameters.AddWithValue("@DeliveryPincode", deliveryPincode);

            try
            {
                conPackXPrez.Open();
                returnVal = Convert.ToInt32(cmdPackXPrez.ExecuteScalar());
            }
            catch (Exception)
            {
                returnVal = 0;
            }
            finally
            {
                conPackXPrez.Close();
            }
            return returnVal;
        }

        public DataTable GetAddresses(string emailId)
        {
            DataTable addresses = new DataTable();
            try
            {
                cmdPackXPrez = new SqlCommand(@"SELECT * FROM ufn_GetAddresses(@EmailId)", conPackXPrez);
                cmdPackXPrez.Parameters.AddWithValue("@EmailId", emailId);
                daPackXPrez = new SqlDataAdapter(cmdPackXPrez);
                daPackXPrez.Fill(addresses);
            }
            catch (Exception)
            {
                addresses = null;
            }
            return addresses;
        }

        public int RegisterShipment(string emailId, string shippingType, short length, short breadth, short height, int weight,
            string deliveryType, string timeSlot, int pickAddressId, string deliveryAddress, int charges,short pks)
        {
            short insurance;
            if (charges > 999) { insurance = 1; }
            else { insurance = 0; }
            int tranId;

            cmdPackXPrez = new SqlCommand("usp_AddShippingOrders", conPackXPrez);
            cmdPackXPrez.CommandType = CommandType.StoredProcedure;

            cmdPackXPrez.Parameters.AddWithValue("@EmailId", emailId);
            cmdPackXPrez.Parameters.AddWithValue("@ShippingType", shippingType);
            cmdPackXPrez.Parameters.AddWithValue("@Length", length);
            cmdPackXPrez.Parameters.AddWithValue("@Breadth", breadth);
            cmdPackXPrez.Parameters.AddWithValue("@Height", height);
            cmdPackXPrez.Parameters.AddWithValue("@Weight", weight);
            cmdPackXPrez.Parameters.AddWithValue("@DeliveryType", deliveryType);
            cmdPackXPrez.Parameters.AddWithValue("@Timeslot", timeSlot);
            cmdPackXPrez.Parameters.AddWithValue("@PickAddressId", pickAddressId);
            cmdPackXPrez.Parameters.AddWithValue("@DeliveryAddress", deliveryAddress);
            cmdPackXPrez.Parameters.AddWithValue("@Charges", charges);
            cmdPackXPrez.Parameters.AddWithValue("@Insurance", insurance);
            cmdPackXPrez.Parameters.AddWithValue("@PackingService", pks);

            SqlParameter prmReturn = new SqlParameter();
            prmReturn.Direction = ParameterDirection.ReturnValue;
            cmdPackXPrez.Parameters.Add(prmReturn);

            try
            {
                conPackXPrez.Open();
                cmdPackXPrez.ExecuteNonQuery();
                tranId = Convert.ToInt32(prmReturn.Value);
            }
            catch (Exception)
            {
                tranId = -1;
            }
            finally
            {
                conPackXPrez.Close();
            }
            return tranId;
        } 

        public string TrackShipment(long trackingId)
        {
            string status;

            cmdPackXPrez = new SqlCommand(@"SELECT [dbo].ufn_TrackShipment(@AWBNumber)", conPackXPrez);

            cmdPackXPrez.Parameters.AddWithValue("@AWBNumber", trackingId);

            try
            {
                conPackXPrez.Open();
                status = Convert.ToString(cmdPackXPrez.ExecuteScalar());
            }
            catch (Exception)
            {
                status = "";
            }
            finally
            {
                conPackXPrez.Close();
            }
            return status;
        }

        public DataTable GetPackageHistory(string emailId)
        {
            DataTable packageHistory = new DataTable();

            try
            {
                cmdPackXPrez = new SqlCommand(@"SELECT * FROM ufn_GetPackageHistory(@EmailId)", conPackXPrez);
                cmdPackXPrez.Parameters.AddWithValue("@EmailId", emailId);
                daPackXPrez = new SqlDataAdapter(cmdPackXPrez);
                daPackXPrez.Fill(packageHistory);
            }
            catch (Exception)
            {
                packageHistory = null;
            }
            return packageHistory;
        }

        public bool RegisterUserdal(string name,string emailId,string password,int mobile,string bdno,string stno,string locality
            ,int pincode)
        {
            int line;
            cmdPackXPrez = new SqlCommand("usp_UserRegistration", conPackXPrez);
            cmdPackXPrez.CommandType = CommandType.StoredProcedure;

            cmdPackXPrez.Parameters.AddWithValue("@Name", name);
            cmdPackXPrez.Parameters.AddWithValue("@EmailId", emailId);
            cmdPackXPrez.Parameters.AddWithValue("@UserPassword", password);
            cmdPackXPrez.Parameters.AddWithValue("@ContactNumber", mobile);
            cmdPackXPrez.Parameters.AddWithValue("@BuildingNo", bdno);
            cmdPackXPrez.Parameters.AddWithValue("@StreetNo", stno);
            cmdPackXPrez.Parameters.AddWithValue("@Locality", locality);
            cmdPackXPrez.Parameters.AddWithValue("@Pincode", pincode);

            SqlParameter prm = new SqlParameter();
            prm.Direction = ParameterDirection.ReturnValue;
            cmdPackXPrez.Parameters.Add(prm);

            try
            {
                conPackXPrez.Open();
                cmdPackXPrez.ExecuteNonQuery();
                line= Convert.ToInt32(prm.Value);

                if (line == 1)
                {
                    return true;
                }
                else { return false; }
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                conPackXPrez.Close();
            }
        }
    }
}
