using System.ComponentModel.DataAnnotations;

namespace ProjectTestingNote.DTO
{
    public record NoteCreateDto([property: Required] string? Title,
        string? Context, DateTime CreatedAt);

    public record NoteDTO(Guid Id, string? Title, string? Context, DateTime CreatedAt);

    public record NoteReadDTO(string? Title,string? Context, DateTime CreatedAt);
}
