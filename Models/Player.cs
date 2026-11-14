using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace wt_lab8_8razor_postgresql_denisenko.Models;

/// <summary>Участник киберспортивного сервиса Tournament Hub.</summary>
public class Player
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Введите никнейм игрока.")]
    [StringLength(30, MinimumLength = 2, ErrorMessage = "Никнейм должен содержать от 2 до 30 символов.")]
    [Display(Name = "Никнейм")]
    public string Nickname { get; set; } = string.Empty;

    [Required(ErrorMessage = "Введите название игры.")]
    [StringLength(60, MinimumLength = 2, ErrorMessage = "Название игры должно содержать от 2 до 60 символов.")]
    [Display(Name = "Игра")]
    public string Game { get; set; } = string.Empty;

    [Range(0, 5000, ErrorMessage = "Рейтинг должен находиться в диапазоне от 0 до 5000.")]
    [Display(Name = "Рейтинг")]
    public int Rating { get; set; }

    [Range(0, 100000, ErrorMessage = "Количество побед не может быть отрицательным.")]
    [Display(Name = "Победы")]
    public int Wins { get; set; }

    [Range(0, 100000, ErrorMessage = "Количество поражений не может быть отрицательным.")]
    [Display(Name = "Поражения")]
    public int Losses { get; set; }

    [Display(Name = "Дата регистрации")]
    public DateTime CreatedAt { get; set; } = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);

    [StringLength(500, ErrorMessage = "Описание не должно превышать 500 символов.")]
    [Display(Name = "Описание")]
    public string Description { get; set; } = string.Empty;

    [NotMapped]
    public double WinRate => Wins + Losses == 0 ? 0 : Wins * 100.0 / (Wins + Losses);
}
