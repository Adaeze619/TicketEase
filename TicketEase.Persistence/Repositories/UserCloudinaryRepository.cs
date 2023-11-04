using Microsoft.EntityFrameworkCore;
using System.Security.Principal;

namespace TicketEase.Persistence.Repositories
{
    public class UserCloudinaryRepository
    {
        public async Task<string> UploadContactImage(string contactId, IFormFile image)
        {
            var user = await GetByIdAsync(contactId);

            if (user == null)
            {
                return "User not found";
            }

            var cloudinary = new Cloudinary(new Account(
                "dlpryp6af",
                "969623236923961",
                "QL5lf-M_syJrxGJdJzbu2oRMAZA"
            ));

            var upload = new ImageUploadParams
            {
                File = new FileDescription(image.FileName, image.OpenReadStream())
            };
            var uploadResult = await cloudinary.UploadAsync(upload);

            user.ImageUrl = uploadResult.SecureUri.AbsoluteUri;
            myContactDbContext.Entry(user).State = EntityState.Modified;

            try
            {
                await myContactDbContext.SaveChangesAsync();
                return "success";
            }
            catch (Exception ex)
            {
                // Log the exception for debugging and troubleshooting
                Console.WriteLine($"Error: {ex}");
                return "Database update error occurred";
            }
        }
        Collapse
        has context menu
    }
}
