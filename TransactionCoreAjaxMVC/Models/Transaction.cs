using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TransactionCoreAjaxMVC.Models
{
    public class Transaction
    {
        [Key]
        public int TransctionId { get; set; }
        [Column(TypeName = "nvarchar(12)")]
        public string AccountNumber { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string BeneficiaryName { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string BankName { get; set; }
        [Column(TypeName = "nvarchar(11)")]
        public string SWIFTCode { get; set; }
        [Column(TypeName = "nvarchar(12)")]
        public int Amount { get; set; }
    }
}
