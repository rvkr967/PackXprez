using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infosys.PackXPrez.DataAccessLayer;
using Infosys.PackXPrez.BusinessLayer;
using Infosys.PackXPrez.Common;

namespace Infosys.PackXPrez.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            ll();
            //TestGetPackageHistory();
            //TestValidateCredentials();
            //TestServiceAvailability();
            //TestRegisterShipment();
            //TestTrackShipment();
        }
        public static void ll()
        {
            PackXPrezBL dal = new PackXPrezBL();
            UserReg reg = new UserReg();
            reg.name = "bb bbbbb";
            reg.emailId = "vvvvvv12v@gmail.com";
            reg.password = "vvvvvvvvv";
            reg.mobile = 1234567890;
            reg.buildingNo = "32";
            reg.streetNo = "gggg";
            reg.locality = "ggggggg";
            reg.pincode = 123456;

            System.Console.WriteLine(dal.RegisterUser(reg)); 
        }
        public static void tt()
        {
            PackXPrezBL dal = new PackXPrezBL();
            System.Console.WriteLine("p q");
            int p=Convert.ToInt32(System.Console.ReadLine());
            int q = Convert.ToInt32(System.Console.ReadLine());
            System.Console.WriteLine(dal.ServiceValidation(p,q));
            System.Console.WriteLine("123");
        }
        public static void TestGetPackageHistory()
        {
            PackXPrezBL dal = new PackXPrezBL();
            System.Console.WriteLine("ENTER EMAILD");
            string emailId = System.Console.ReadLine();
            List<PackageHistory> packageHistory = dal.GetPackageHistories(emailId);
            foreach(var i in packageHistory)
            {
                System.Console.WriteLine(i.transactionId);
                System.Console.WriteLine(i.number);
                System.Console.WriteLine(i.toLocation);
                System.Console.WriteLine(i.fromLocation);
                System.Console.WriteLine(i.status);
            }
            //if (packageHistory.Rows.Count != 0)
            //{
            //    foreach (DataRow row in packageHistory.Rows)
            //    {

            //        System.Console.WriteLine("TransactionID: " + row[0].ToString());
            //        System.Console.WriteLine("AWB Number: " + row[1].ToString());
            //        System.Console.WriteLine("From Location: " + row[2].ToString() + "," + row[3].ToString() + "," + row[4].ToString() + "," + row[5].ToString());
            //        System.Console.WriteLine("To Location: " + row[6].ToString());
            //        System.Console.WriteLine("Status: " + row[7].ToString());
            //        System.Console.WriteLine("_________________________________________________________");
            //    }
            //}
            //else
            //{
            //    System.Console.WriteLine("No history available");
            //}
        }

        public static void TestValidateCredentials()
        {
            Repository dal = new Repository();
            System.Console.WriteLine("ENTER EMAILD");
            string emailId = System.Console.ReadLine();
            System.Console.WriteLine("ENTER PASSWORD");
            string password = System.Console.ReadLine();

            int roleId = dal.ValidateCredentials(emailId, password);
            if (roleId == 1) { System.Console.WriteLine("Logged in as ADMIN"); }
            else if (roleId == 2) { System.Console.WriteLine("Logged in as CUSTOMER"); }
            else { System.Console.WriteLine("Invalid Credntials"); }
        }

        public static void TestServiceAvailability()
        {
            Repository dal = new Repository();
            System.Console.WriteLine("ENTER Pickup Picode");
            int pickpin = Convert.ToInt32(System.Console.ReadLine());
            System.Console.WriteLine("ENTER Delivery Pincode");
            int delpin = Convert.ToInt32(System.Console.ReadLine());

            int kms = dal.ValidateServiceAvailability(pickpin, delpin);
            if (kms > 0) { System.Console.WriteLine("servive available"); }
            else { System.Console.WriteLine("service unavailable"); }
        }

        public static void TestRegisterShipment()
        {
            Repository dal = new Repository();
            System.Console.WriteLine("Enter emailid,shippingtype,length,breadth,height,weight,deliverytype,timeslot,pickaddressid,deliveryaddress,charges");
            string a = System.Console.ReadLine();
            string b = System.Console.ReadLine();
            short c = Convert.ToInt16(System.Console.ReadLine());
            short d = Convert.ToInt16(System.Console.ReadLine());
            short e = Convert.ToInt16(System.Console.ReadLine());
            int f = Convert.ToInt32(System.Console.ReadLine());
            string g = System.Console.ReadLine();
            string h = System.Console.ReadLine();
            int i = Convert.ToInt32(System.Console.ReadLine());
            string j = System.Console.ReadLine();
            int k = Convert.ToInt32(System.Console.ReadLine());
            int tranid = dal.RegisterShipment(a, b, c, d, e, f, g, h, i, j, k,1);
            if (tranid != -1)
            {
                System.Console.WriteLine("Shipment registered, TransactionId: "+tranid);
            }
            else { System.Console.WriteLine("Shipment not registered Try again"); }
        }

        public static void TestTrackShipment()
        {
            Repository dal = new Repository();
            System.Console.WriteLine("Enter AWBNumber:");
            long num =Convert.ToInt64( System.Console.ReadLine());

            string status = dal.TrackShipment(num);
            if (status != null)
            {
                System.Console.WriteLine("Status of shipment is:"+status);
            }
            else
            {
                System.Console.WriteLine("Invalid AWBNumber");
            }
        }
    }
}