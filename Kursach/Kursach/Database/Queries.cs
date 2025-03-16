using Kursach.Database.WarehouseApp.Database;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Kursach.Database
{
    public static class Queries
    {
        public static bool ValidateUser(string username, string passwordHash)
        {
            string query = "SELECT COUNT(*) FROM Users WHERE Username = @Username AND PasswordHash = @PasswordHash";
            object result = DatabaseHelper.ExecuteScalar(query, command =>
            {
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@PasswordHash", passwordHash);
            });
            return Convert.ToInt32(result) > 0;
        }

        private static bool IsAccessCodeExists(string accessCode, int roleID)
        {
            string query = @"
        SELECT COUNT(*) 
        FROM AccessCodes 
        WHERE Code = @Code AND RoleID = @RoleID";

            object result = DatabaseHelper.ExecuteScalar(query, command =>
            {
                command.Parameters.AddWithValue("@Code", accessCode);
                command.Parameters.AddWithValue("@RoleID", roleID);
            });

            return Convert.ToInt32(result) > 0;
        }

        public static void RegisterUser(string username, string password, int roleID, string accessCode = null)
        {
            string hashedPassword = PasswordHelper.HashPassword(password);

            if (roleID != 1) 
            {
                if (!IsAccessCodeExists(accessCode, roleID))
                {
                    throw new ArgumentException("Неверный или несуществующий код доступа.");
                }

                int? accessCodeID = GetAccessCodeID(accessCode, roleID);
                if (accessCodeID == null)
                {
                    throw new ArgumentException("Неверный код доступа.");
                }
            }

            string query = @"
        INSERT INTO Users (Username, PasswordHash, RoleID, AccessCodeID) 
        VALUES (@Username, @PasswordHash, @RoleID, @AccessCodeID)";

            DatabaseHelper.ExecuteNonQuery(query, command =>
            {
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@PasswordHash", hashedPassword);
                command.Parameters.AddWithValue("@RoleID", roleID);
                command.Parameters.AddWithValue("@AccessCodeID", roleID == 1 ? DBNull.Value : (object)GetAccessCodeID(accessCode, roleID));
            });
        }

        public static string GetUserPasswordHash(string username)
        {
            string query = "SELECT PasswordHash FROM Users WHERE Username = @Username";

            object result = DatabaseHelper.ExecuteScalar(query, command =>
            {
                command.Parameters.AddWithValue("@Username", username);
            });

            return result?.ToString();
        }


        private static int? GetAccessCodeID(string code, int roleID)
        {
            string query = @"
        SELECT CodeID 
        FROM AccessCodes 
        WHERE Code = @Code AND RoleID = @RoleID";

            object result = DatabaseHelper.ExecuteScalar(query, command =>
            {
                command.Parameters.AddWithValue("@Code", code);
                command.Parameters.AddWithValue("@RoleID", roleID);
            });

            return result == null ? (int?)null : Convert.ToInt32(result);
        }

        public static void SaveAccessCode(string code, int roleID, string createdBy)
        {
            string query = @"
        INSERT INTO AccessCodes (Code, RoleID, CreatedBy) 
        VALUES (@Code, @RoleID, @CreatedBy)";

            DatabaseHelper.ExecuteNonQuery(query, command =>
            {
                command.Parameters.AddWithValue("@Code", code);
                command.Parameters.AddWithValue("@RoleID", roleID);
                command.Parameters.AddWithValue("@CreatedBy", createdBy);
            });
        }

        public static bool IsCodeExistsForAdmin(int roleID, string adminUsername)
        {
            string query = @"
        SELECT COUNT(*) 
        FROM AccessCodes 
        WHERE RoleID = @RoleID AND CreatedBy = @CreatedBy";

            object result = DatabaseHelper.ExecuteScalar(query, command =>
            {
                command.Parameters.AddWithValue("@RoleID", roleID);
                command.Parameters.AddWithValue("@CreatedBy", adminUsername);
            });

            return Convert.ToInt32(result) > 0;
        }

        public static void UpdateAccessCode(string newCode, int roleID, string adminUsername)
        {
            string query = @"
        UPDATE AccessCodes 
        SET Code = @Code 
        WHERE RoleID = @RoleID AND CreatedBy = @CreatedBy";

            DatabaseHelper.ExecuteNonQuery(query, command =>
            {
                command.Parameters.AddWithValue("@Code", newCode);
                command.Parameters.AddWithValue("@RoleID", roleID);
                command.Parameters.AddWithValue("@CreatedBy", adminUsername);
            });
        }

        public static string GetUserRole(string username)
        {
            string query = @"
        SELECT Roles.RoleName 
        FROM Users 
        INNER JOIN Roles ON Users.RoleID = Roles.RoleID 
        WHERE Users.Username = @Username";

            object result = DatabaseHelper.ExecuteScalar(query, command =>
            {
                command.Parameters.AddWithValue("@Username", username);
            });

            return result?.ToString();
        }

        public static bool ValidateAccessCode(string code, int roleID)
        {
            string query = "SELECT COUNT(*) FROM AccessCodes WHERE Code = @Code AND RoleID = @RoleID";
            object result = DatabaseHelper.ExecuteScalar(query, command =>
            {
                command.Parameters.AddWithValue("@Code", code);
                command.Parameters.AddWithValue("@RoleID", roleID);
            });
            return Convert.ToInt32(result) > 0;
        }

        public static DataTable GetUsersByAdmin(string adminUsername)
        {
            string query = @"
        SELECT Users.Username, Roles.RoleName 
        FROM Users 
        INNER JOIN AccessCodes ON Users.AccessCodeID = AccessCodes.CodeID 
        INNER JOIN Roles ON Users.RoleID = Roles.RoleID 
        WHERE AccessCodes.CreatedBy = @CreatedBy";

            return DatabaseHelper.ExecuteQuery(query, command =>
            {
                command.Parameters.AddWithValue("@CreatedBy", adminUsername);
            });
        }

        public static void DeleteUser(string username)
        {
            string query = "DELETE FROM Users WHERE Username = @Username";

            DatabaseHelper.ExecuteNonQuery(query, command =>
            {
                command.Parameters.AddWithValue("@Username", username);
            });
        }
    }
    
}
