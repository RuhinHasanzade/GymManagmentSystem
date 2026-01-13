using System.Threading.Tasks;

namespace FinalPratic2.Helpers
{
    public static class FileHelper
    {
        public static async Task<string> FileUploadAsync(this IFormFile file , string folderPath)
        {
            string uniqueFileName =  Guid.NewGuid().ToString() + file.FileName;

            string path = Path.Combine(folderPath, uniqueFileName);

            using FileStream stream = new(path,FileMode.Create);

            await file.CopyToAsync(stream);

            return uniqueFileName;
        }

        public static void FileDelete(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}
