using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cemet.Models
{
    public class ViewModelItem
    {
        [Required]
        public int ItemId { get; set; }
        [Required]
        public string Nama { get; set; }
        [Required]
        [Display(Name="Kode Sendiri")]
        public string KodeSendiri { get; set; }
        [Required]
        public string Barcode { get; set; }
        [Required]
        public int Satuan_SatuanId { get; set; }

        [Display(Name = "Satuan")]
        public string SatuanNama { get; set; }
    }
}