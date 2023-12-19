using System.ComponentModel.DataAnnotations;

namespace FinanceSystem.Common;

/// <summary>
/// Общий класс для возвращения коллекций с пагинацией
/// </summary>
/// <typeparam name="T">Тип коллекции</typeparam>
public class Page<T>
{
    /// <summary>
    /// Массив элементов
    /// </summary>
    [Required]
    [MinLength(0)]
    public IReadOnlyCollection<T> Items { get; set; } = Array.Empty<T>();
    
    /// <summary>
    /// Всего найденных объектов
    /// </summary>
    public int Total { get; set; }
    
    /// <summary>
    /// Всего объектов на странице
    /// </summary>
    public int TotalOnPage { get; set; }

    /// <summary>
    /// Пустой список объектов
    /// </summary>
    public static Page<T> Empty => new() { Items = Array.Empty<T>(), Total = 0, TotalOnPage = 0 };
}