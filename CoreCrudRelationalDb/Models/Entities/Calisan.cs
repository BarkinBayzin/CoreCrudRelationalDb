using System.ComponentModel.DataAnnotations;

namespace CoreCrudRelationalDb.Models.Entities
{
    public class Calisan
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ad alanı zorunludur.")]
        [MaxLength(50,ErrorMessage ="Tam Ad Maksimum 50 karakter olabilir!")]
        public string TamAd { get; set; }

        [Required(ErrorMessage ="Departman alanı zorunludur!")]
        public int DepartmanId { get; set; }

        //Navigation/Relational Properties Begin

        public Departman Departman { get; set; }
    }
}
