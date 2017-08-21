namespace EZPost.Web.Models
{
    public class DhlBagModel
    {
        public string BagNumber { set; get; }
        public int TotalShipment { set; get; }
        public decimal TotalWeight { set; get; }
    }
}