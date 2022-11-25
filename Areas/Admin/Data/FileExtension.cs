namespace EduHomeProject.Areas.Admin.Data
{
    public static class FileExtension
    {
        public static bool IsImage(this IFormFile file)
        {
            return file.ContentType.Contains("image");
        }

        public static bool IsAllowedSize(this IFormFile file, int mb)
        {
            if (file.Length > mb * 1024 * 1024)
                return false;

            return true;
        }

        public async static Task<string> GenerateFile(this IFormFile file, string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            var unicalName = $"{Guid.NewGuid()}-{file.FileName}";

            using FileStream fs = new(Path.Combine(path, unicalName), FileMode.Create);
            await file.CopyToAsync(fs);

            return unicalName;
        }
    }
}
