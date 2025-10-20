using System.Text.Json.Serialization;

namespace LibraryApp.Domain.Books;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum BookLanguage
{
    English,
    Swedish,
    German,
    French,
    Spanish,
    Italian
}
