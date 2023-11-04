using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketEase.Persistence.Context;
using TicketEase.Domain.Entities;
using TicketEase.Application.Interfaces.Repositories;

namespace TicketEase.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext dataContext;

        public UserRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }



        public async Task<User?> GetByIdAsync(string id)
        {
            return await dataContext.Users.FirstOrDefaultAsync(x => x.Id == id);
        }




        public async Task<string> UploadContactImage(string Id, IFormFile image)
        {
            var user = await GetByIdAsync(Id);

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
            dataContext.Entry(user).State = EntityState.Modified;

            try
            {
                await dataContext.SaveChangesAsync();
                return "success";
            }
            catch (Exception ex)
            {
                // Log the exception for debugging and troubleshooting
                Console.WriteLine($"Error: {ex}");
                return "Database update error occurred";
            }
        }
    }
}
