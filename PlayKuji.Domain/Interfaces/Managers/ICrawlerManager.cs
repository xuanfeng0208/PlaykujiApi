namespace PlayKuji.Domain.Interfaces.Managers
{
    public interface ICrawlerManager
    {
        string GetHtml(string url);
    }
}
