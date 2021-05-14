using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infosys.PackXPrez.Common
{
    public class Registration
    {
        [Required]
        public string emailId;
        [Required]
        public string shippingType;
        [Required]
        public short length;
        [Required]
        public short breadth;
        [Required]
        public short height;
        [Required]
        public int weight;
        [Required]
        public string deliveryType;
        [Required]
        public string timeSlot;
        [Required]
        public int pickAddressId;
        [Required]
        public string deliveryAddress;
        [Required]
        public bool packingService;
    }
}
