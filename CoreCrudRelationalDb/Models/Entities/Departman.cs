using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CoreCrudRelationalDb.Models.Entities
{
    public class Departman
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ad alanı zorunludur.")]
        [MaxLength(50, ErrorMessage = "Ad Maksimum 50 karakter olabilir!")]
        public string Ad { get; set; }

        //Navigation/Relational Properties Begin

        public List<Calisan> Calisanlar { get; set; }
    }
}
