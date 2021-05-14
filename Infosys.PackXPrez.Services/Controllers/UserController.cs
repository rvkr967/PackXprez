using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Infosys.PackXPrez.BusinessLayer;
using Infosys.PackXPrez.Common;
using Infosys.PackXPrez.DataAccessLayer;
using Newtonsoft.Json;

namespace Infosys.PackXPrez.Services.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : Controller
    {
        [HttpPost]
        public string UserValidation(User userObj)
        {
            try
            {
                PackXPrezBL packXPrezBL = new PackXPrezBL();
                string role = JsonConvert.SerializeObject(packXPrezBL.LoginValidation(userObj.emailId, userObj.password));
                return role;
            }
            catch (Exception)
            {
                return null;
            }
        }
        [HttpGet]
        public string GetPackageHistory(string emailId)
        {
            try
            {
                PackXPrezBL packXPrezBL = new PackXPrezBL();
                var history = packXPrezBL.GetPackageHistories(emailId);
                string jsonstring = JsonConvert.SerializeObject(history);
                return jsonstring;
            }
            catch (Exception)
            {

                return null;
            }
        }
        [HttpGet]
        public string GetOrderStatus(long num)
        {
            try
            {
                PackXPrezBL packXPrezBL = new PackXPrezBL();
                string stat = JsonConvert.SerializeObject(packXPrezBL.GetShippingStatus(num));

                return stat;
            }
            catch (Exception)
            {
                return null;
            }
        }
        [HttpGet]
        public int ServiceValidation(int num,int num1)
        {
            PackXPrezBL dal = new PackXPrezBL();
            int k = dal.ServiceValidation(num, num1);
            return k;
        }
        [HttpGet]
        public string GetAddresses(string emailId)
        {
            try
            {
                PackXPrezBL dal = new PackXPrezBL();
                string addresses = JsonConvert.SerializeObject(dal.GetAddresses(emailId));
                return addresses;
            }
            catch (Exception)
            {
                return null;
            }
        }
        [HttpPost]
        public int RegisterShipment(Registration regObj)
        {
            try
            {
                PackXPrezBL packXPrezBL = new PackXPrezBL();
                int tranId = packXPrezBL.ShipmentRegistration(regObj.emailId, regObj.shippingType, regObj.length, regObj.breadth, regObj.height, regObj.weight, regObj.deliveryType, regObj.timeSlot, regObj.pickAddressId, regObj.deliveryAddress, regObj.packingService);
                return tranId;
            }
            catch(Exception)
            {
                return -99;
            }
        }
        [HttpPost]
        public int UserRegistration(UserReg userregObj)
        {
            try
            {
                PackXPrezBL packXPrezBL = new PackXPrezBL();
                bool reg = packXPrezBL.RegisterUser(userregObj);
                if (reg) { return 1; }
                else { return -1; }
            }
            catch (Exception)
            {
                return -2;
            }
        }

    }
}
//<List<PackageHistory>>