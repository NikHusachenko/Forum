namespace Forum.Services.CurrentUserContext
{
    public interface ICurrentUserContext
    {
        long? Id { get; }
        string? Login { get; }
        string? Email { get; }
    }
}