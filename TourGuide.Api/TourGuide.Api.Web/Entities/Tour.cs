using System.ComponentModel.DataAnnotations;

namespace TourGuide.Api.Web.Entities;

public class Tour
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string City { get; set; }
    public string Theme { get; set; }
    public int Reviews { get; set; }
    public string ImageUrls { get; set; } // Ссылки через запятую
    public int Price { get; set; }
    public string Description { get; set; }

    // Заменяем List<string> на строки с разделителями
    public string Activities { get; set; } // "Активность1,Активность2"
    public string Includes { get; set; } // "Включено1,Включено2"
    public string NotIncludes { get; set; } // "Не включено1,Не включено2"
    public string Safety { get; set; } // "Безопасность1,Безопасность2"

    // Детали как отдельные поля
    public string Languages { get; set; } // "Язык1,Язык2"
    public string Duration { get; set; }
    public string NumberOfPeople { get; set; }

    public string Address { get; set; }
}