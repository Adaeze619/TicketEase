using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketEase.Common.Utilities
{
	public class PasswordGenerator
	{
		public static string GeneratePassword(string email, string companyName)
		{

			email = email.Split("@")[0];
			if(email.Length > 4 ) email.Substring(4);
			if (companyName.Length > 4) companyName.Substring(4).Trim();

			string randomString = GenerateRandomString(4); // You can adjust the length as needed

			string password = companyName + randomString + email;
			
			return password;

		}



		static string GenerateRandomString(int length)
		{
			// Characters to use for the random string
			const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

			// Random number generator
			Random random = new Random();

			// Generate the random string
			var randomString = new StringBuilder(length);
			for (int i = 0; i < length; i++)
			{
				randomString.Append(chars[random.Next(chars.Length)]);
			}

			return randomString.ToString();
		}
	}
}
