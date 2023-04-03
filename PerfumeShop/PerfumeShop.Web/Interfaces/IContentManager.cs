namespace PerfumeShop.Web.Interfaces;

public interface IContentManager
{
    public List<string> NameFiles { get; }
    public void UploadFiles(IFormFileCollection files, string path);
    public void RemoveFile(string contentPath, string nameFile);
}
