using Microsoft.AspNetCore.Http;
using System.IO;

namespace Application.Logic
{
    public static class ImageConverter
    {
        public static byte[] ConvertToByteArray(IFormFile file)
        {
            if(file.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    file.CopyTo(memoryStream);
                    return memoryStream.ToArray();
                }
            }
            else
            {
                return null;
            }
        }
    }
}
