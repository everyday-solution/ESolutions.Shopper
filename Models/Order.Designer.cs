//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ESolutions.Shopper.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Order
    {
        public int Id { get; set; }
        public int ArticleId { get; set; }
        public System.DateTime OrderDate { get; set; }
        public decimal Amount { get; set; }
        public decimal Price { get; set; }
        public string Currency { get; set; }
        public decimal ExchangeRate { get; set; }
        public Nullable<System.DateTime> ArrivalDate { get; set; }
        public int SupplierId { get; set; }
        public decimal FixCostsPercentage { get; set; }
        public Nullable<System.DateTime> ExpectedDateOfDelivery { get; set; }
    
        public virtual Article Article { get; set; }
        public virtual Supplier Supplier { get; set; }
    }
}