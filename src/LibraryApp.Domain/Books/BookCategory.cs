using System.Text.Json.Serialization;

namespace LibraryApp.Domain.Books;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum BookCategory
{
    Fantasy,
    Romance,
    Thriller,
    Children,
    History,
    Biography,
    Comics,
    SoftwareDevelopment
}
