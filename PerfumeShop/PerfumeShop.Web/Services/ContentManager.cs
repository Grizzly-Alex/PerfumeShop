namespace PerfumeShop.Web.Services;

public sealed class ContentManager : IContentManager
{
    public List<string> NameFiles { get; private set; }
    private readonly string _webRootPath;
    private readonly ILogger<ContentManager> _logger;
    private readonly IWebHostEnvironment _hostEnvironment;


    public ContentManager(
        IWebHostEnvironment hostEnvironment,
        ILogger<ContentManager> logger)
    {
        NameFiles = new();
        _hostEnvironment = hostEnvironment;
        _webRootPath = _hostEnvironment.WebRootPath;
        _logger = logger;
    }

    public void UploadFiles(IFormFileCollection files, string path)
    {
        try
        {
            string upload = string.Concat(_webRootPath, path);

            foreach (var file in files)
            {
                string fileName = Guid.NewGuid().ToString();
                string extension = Path.GetExtension(file.FileName);
                string fullFileName = string.Concat(fileName, extension);

                NameFiles.Add(fullFileName);

                using (var fileStream = new FileStream(Path.Combine(upload, fullFileName), FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
            }

            NameFiles.ForEach(value => _logger.LogInformation($"File {value} added successfully"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Files upload is failed");
        }
    }

    public void RemoveFile(string contentPath, string nameFile)
    {
        try
        {
            if (nameFile is not null)
            {
                string _filePath = Path.Combine(string.Concat(_webRootPath, contentPath), nameFile);

                if (File.Exists(_filePath))
                {
                    File.Delete(_filePath);
                }
            }
        }
        catch (IOException ex)
        {
            _logger.LogError(ex, $"File not found");
        }
    }
}
