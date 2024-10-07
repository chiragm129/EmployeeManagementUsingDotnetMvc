using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace BirdiSysAssignment
{
    public class AuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            //Dummy user validation to generate token
            if (context.UserName != "admin" || context.Password != "adminpass")
            {
                context.SetError("invalid_grant", "The username or password is incorrect.");
                return;
            }

            //else we can create credentials table use that credentials for generate the token 
            //as we are not using login functionality in our app thats why we are using this dummy credentials for generate the token

            //public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
            //{
            //context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            //// Validate credentials against database
            //bool isValidUser = false;
            //string role = "";

            //// Use Windows Authentication to connect to SQL Server
            //using (var connection = new SqlConnection("EmployeeSystemEntities"))
            //{
            //    await connection.OpenAsync();

            //    // Replace with actual SQL query to validate user
            //    string query = "SELECT Role FROM Users WHERE Username = @Username AND Password = @Password";

            //    using (var command = new SqlCommand(query, connection))
            //    {
            //        command.Parameters.AddWithValue("@Username", context.UserName);
            //        command.Parameters.AddWithValue("@Password", context.Password);  // Store hashed passwords securely

            //        using (var reader = await command.ExecuteReaderAsync())
            //        {
            //            if (reader.HasRows)
            //            {
            //                while (await reader.ReadAsync())
            //                {
            //                    role = reader["Role"].ToString();
            //                    isValidUser = true;
            //                }
            //            }
            //        }
            //    }
            //}

            //if (!isValidUser)
            //{
            //    context.SetError("invalid_grant", "The username or password is incorrect.");
            //    return;
            //}

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim(ClaimTypes.Role, "Admin"));
            identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
            
            context.Validated(identity);
        }
    }
}