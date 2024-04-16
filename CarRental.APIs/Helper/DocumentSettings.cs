namespace CarRental.APIs.Helper
{
    public class DocumentSettings
    {
        public static string UploadFile(IFormFile file, string folderName)
        {
            // 1. Get Located Folder Path
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/files", folderName);

            // 2. Get File Name and Make it Unique
            string fileName = $"{Guid.NewGuid()}{file.FileName}";

            // 3. Get File Path
            string filePath = Path.Combine(folderPath, fileName);

            // 4. Save File as Streams : [Data Per Time]
            using var fs = new FileStream(filePath, FileMode.Create);
            file.CopyTo(fs);

            // 5. Return File Path in the "images" format
            string relativePath = Path.Combine("files/images/", fileName);

            return relativePath;
        }

        public static void DeleteFile(string fileName, string folderName)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/files", folderName, fileName);

            if (File.Exists(filePath))
                File.Delete(filePath);
        }
    }
}
