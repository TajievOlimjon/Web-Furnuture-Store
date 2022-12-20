namespace WebShopFurniture.ShopFurniture.IServices
{
    public interface IFileService
    {
        string AddFile(IFormFile fileName);
        string DeleteFile(string fileName);
        string UpdateFile(IFormFile fileName);
    }
}
