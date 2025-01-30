namespace InventoryManagementSystem.App.ImageService;
public interface IImageService
{
    Task<string> UploadImage(IFormFile imageFile, string folderName);
    void DeleteOlderImage(string imageUrl, string folderName);
}
