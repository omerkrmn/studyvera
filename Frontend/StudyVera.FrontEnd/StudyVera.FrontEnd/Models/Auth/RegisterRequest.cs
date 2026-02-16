using StudyVera.FrontEnd.Enums;
using System.ComponentModel.DataAnnotations;

namespace StudyVera.FrontEnd.Models.Auth;
public class RegisterRequest
{
    [Required(ErrorMessage = "Ad alanı zorunludur.")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Soyad alanı zorunludur.")]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Kullanıcı adı zorunludur.")]
    public string UserName { get; set; } = string.Empty;

    [Required(ErrorMessage = "E-posta alanı zorunludur."), EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Şifre alanı zorunludur."), MinLength(6, ErrorMessage = "Şifreniz en az 6 karakter olmalıdır.")]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "Hedef sınav seçimi zorunludur.")]
    public TargetExam TargetExam { get; set; }

    public override string ToString()
    {
        return $"{FirstName}, {LastName}, {UserName}, {Email}, {Password}, {TargetExam}";
    }
}
