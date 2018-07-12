using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using Microsoft.Extensions.Options;
using SimpleRegistration.Core.Contracts.Account;
using SimpleRegistration.Core.Entities;

namespace SimpleRegistration.Gateways
{
    public class AccountRepository : IAccountRepository
    {
        private readonly Config _config;

        public AccountRepository(IOptions<Config> config)
        {
            _config = config.Value;
        }
        public bool CheckEmailAvailable(string email)
        {
            bool available = false;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(_config.ConnectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand("CheckEmailInUse", sqlConnection))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.Add(new SqlParameter("@Email", email));
                        sqlCommand.Parameters.Add(new SqlParameter("@Exists", available)).Direction = ParameterDirection.Output;
                        sqlConnection.Open();
                        sqlCommand.ExecuteNonQuery();
                        sqlConnection.Close();

                        available = sqlCommand.Parameters["@Exists"].Value == DBNull.Value ? true : false;
                    }
                }
            }
            catch(Exception ex)
            {
                return false;
            }
            return available;
        }

        public bool CreateAcount(User user, string password)
        {
            try
            {               
                using (SqlConnection sqlConnection = new SqlConnection(_config.ConnectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand("CreateAcount", sqlConnection))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.Add(new SqlParameter("@Email", user.Email));
                        sqlCommand.Parameters.Add(new SqlParameter("@Password", password));
                        sqlCommand.Parameters.Add(new SqlParameter("@CreatedDate", user.CreatedDate));

                        sqlConnection.Open();
                        sqlCommand.ExecuteNonQuery();
                        sqlConnection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
            
        }

        public string HashPassword(string password)
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);
            return Convert.ToBase64String(hashBytes);
        }

        public List<string> ValidateAccount(User user)
        {
            List<string> errors = new List<string>();
            if (!CheckEmailAvailable(user.Email))
            {
                errors.Add("Account already exists");
            }

            return errors;
        }
    }
}