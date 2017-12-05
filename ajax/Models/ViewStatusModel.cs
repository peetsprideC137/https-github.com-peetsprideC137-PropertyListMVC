using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ajax.Models
{
    public class ViewStatusModel
    {
        public BidApproved tblBidApproved { get; set; }
        //public Owner tblOwner { get; set; }
        public PropertyDetail tblPropertyDetails { get; set; }
        public OwnerAddOn tblowneraddon { get; set; }
     
    }
}