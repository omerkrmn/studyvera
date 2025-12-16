using System.ComponentModel.DataAnnotations;

namespace StudyVera.FrontEnd.ComponentModel.DataAnnotations;



public class ContactFormModel
{
    [Required(ErrorMessage = "Adınız ve soyadınız zorunludur.")]
    [StringLength(100, ErrorMessage = "İsim alanı en fazla 100 karakter olmalıdır.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "E-posta adresi zorunludur.")]
    [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi girin.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Konu alanı zorunludur.")]
    [StringLength(150, ErrorMessage = "Konu en fazla 150 karakter olabilir.")]
    public string Subject { get; set; }

    [Required(ErrorMessage = "Lütfen mesajınızı yazın.")]
    public string Message { get; set; }
}