namespace LIMMA.Interfaces
{
    public interface IConfiguration
    {

        string BaseUrl { get; }
        string User { get; }
        string Password { get; }
    }
}