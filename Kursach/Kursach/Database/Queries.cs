using Kursach.Database.WarehouseApp.Database;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace Kursach.Database
{
    public static class Queries
    {
        [Obsolete]
        public static string GetAdminForManager(string managerUsername)
{
    string query = @"
        SELECT AccessCodes.CreatedBy 
        FROM Users 
        INNER JOIN AccessCodes ON Users.AccessCodeID = AccessCodes.CodeID 
        WHERE Users.Username = @ManagerUsername";
    return DatabaseHelper.ExecuteScalar(query, command =>
    {
        command.Parameters.AddWithValue("@ManagerUsername", managerUsername);
    })?.ToString();
}

        [Obsolete]
        public static decimal GetCurrentCashAmount(string userUsername)
        {
            string query = @"
        SELECT TOP 1 Amount 
        FROM CashRegister 
        WHERE UserUsername = @UserUsername 
        ORDER BY LastUpdate DESC"; 

            object result = DatabaseHelper.ExecuteScalar(query, command =>
            {
                command.Parameters.AddWithValue("@UserUsername", userUsername);
            });

            return result != null ? Convert.ToDecimal(result) : 0;
        }

        [Obsolete]
        public static void UpdateCashAmount(decimal amount, string operationType, string userUsername)
        {
            decimal currentAmount = GetCurrentCashAmount(userUsername);

            if (currentAmount + amount < 0)
            {
                throw new Exception("Недостаточно средств в кассе для выполнения операции."); 
            }

            string query = @"
        -- Если записи нет, создаем её
        IF NOT EXISTS (SELECT 1 FROM CashRegister WHERE UserUsername = @UserUsername)
        BEGIN
            INSERT INTO CashRegister (UserUsername, Amount, LastUpdate) 
            VALUES (@UserUsername, @Amount, GETDATE());
        END
        ELSE
        BEGIN
            UPDATE CashRegister 
            SET Amount = Amount + @Amount, LastUpdate = GETDATE() 
            WHERE UserUsername = @UserUsername;
        END

        INSERT INTO CashTransactions (Amount, OperationType, AdminUsername) 
        VALUES (@Amount, @OperationType, @UserUsername)";

            DatabaseHelper.ExecuteNonQuery(query, command =>
            {
                command.Parameters.AddWithValue("@Amount", amount);
                command.Parameters.AddWithValue("@OperationType", operationType);
                command.Parameters.AddWithValue("@UserUsername", userUsername);
            });
        }

        [Obsolete]
        public static DataTable GetCashTransactions(string userUsername)
        {
            string query = @"
        SELECT 
            OperationType,
            Amount,
            Timestamp,
            AdminUsername
        FROM CashTransactions
        WHERE AdminUsername = @UserUsername
        ORDER BY Timestamp DESC"; 

            return DatabaseHelper.ExecuteQuery(query, command =>
            {
                command.Parameters.AddWithValue("@UserUsername", userUsername);
            });
        }

        [Obsolete]
        public static DataTable GetReturnHistory(string adminUsername)
        {
            string query = @"
        SELECT 
            ReturnID,
            ProductName,
            ReturnedQuantity,
            ReturnTime,
            AdminUsername
        FROM Returns
        WHERE AdminUsername = @AdminUsername
        ORDER BY ReturnTime DESC";

            return DatabaseHelper.ExecuteQuery(query, command =>
            {
                command.Parameters.AddWithValue("@AdminUsername", adminUsername);
            });
        }

        [Obsolete]
        public static DataTable GetSaleDetails(int saleId)
        {
            string query = @"
        SELECT 
            ProductName,
            Quantity,
            Price,
            0 AS ReturnQuantity -- Добавляем колонку для возврата
        FROM SaleDetails
        WHERE SaleID = @SaleID";

            return DatabaseHelper.ExecuteQuery(query, command =>
            {
                command.Parameters.AddWithValue("@SaleID", saleId);
            });
        }

        [Obsolete]
        public static void AddReturn(int saleId, string productName, int quantity, string adminUsername)
        {
            string query = @"
        INSERT INTO Returns (SaleID, ProductName, ReturnedQuantity, ReturnTime, AdminUsername) 
        VALUES (@SaleID, @ProductName, @ReturnedQuantity, @ReturnTime, @AdminUsername)";

            DatabaseHelper.ExecuteNonQuery(query, command =>
            {
                command.Parameters.AddWithValue("@SaleID", saleId);
                command.Parameters.AddWithValue("@ProductName", productName);
                command.Parameters.AddWithValue("@ReturnedQuantity", quantity);
                command.Parameters.AddWithValue("@ReturnTime", DateTime.Now);
                command.Parameters.AddWithValue("@AdminUsername", adminUsername);
            });
        }

        [Obsolete]
        public static DataTable GetSalesHistory(string adminUsername)
        {
            string query = @"  
            SELECT   
                s.SaleID,  
                s.SaleTime,  
                CASE   
                    WHEN (s.Total - ISNULL((  
                        SELECT SUM(rd.ReturnedQuantity * sd.Price)   
                        FROM Returns rd   
                        JOIN SaleDetails sd ON rd.SaleID = sd.SaleID   
                        WHERE rd.SaleID = s.SaleID  
                    ), 0)) < 0 THEN 0  
                    ELSE (s.Total - ISNULL((  
                        SELECT SUM(rd.ReturnedQuantity * sd.Price)   
                        FROM Returns rd   
                        JOIN SaleDetails sd ON rd.SaleID = sd.SaleID   
                        WHERE rd.SaleID = s.SaleID  
                    ), 0))  
                END AS AdjustedTotal,  
                s.UserUsername,  
                (SELECT STRING_AGG(sd.ProductName + ' (' + CAST(sd.Quantity AS NVARCHAR) + ' шт.)', ', ')   
                 FROM SaleDetails sd   
                 WHERE sd.SaleID = s.SaleID) AS ProductsList  
            FROM Sales s  
            WHERE s.UserUsername = @UserUsername
            ORDER BY s.SaleTime DESC";

            return DatabaseHelper.ExecuteQuery(query, command =>
            {
                command.Parameters.AddWithValue("@UserUsername", adminUsername);
            });
        }

        [Obsolete]
        public static void ReturnProduct(string productName, int returnQuantity, int saleId)
        {
            string updateProductQuery = @"
        UPDATE Products 
        SET Quantity = Quantity + @ReturnQuantity 
        WHERE Name = @ProductName";

            DatabaseHelper.ExecuteNonQuery(updateProductQuery, command =>
            {
                command.Parameters.AddWithValue("@ReturnQuantity", returnQuantity);
                command.Parameters.AddWithValue("@ProductName", productName);
            });

            string updateSaleDetailsQuery = @"
        UPDATE SaleDetails 
        SET Quantity = Quantity - @ReturnQuantity 
        WHERE SaleID = @SaleID AND ProductName = @ProductName";

            DatabaseHelper.ExecuteNonQuery(updateSaleDetailsQuery, command =>
            {
                command.Parameters.AddWithValue("@ReturnQuantity", returnQuantity);
                command.Parameters.AddWithValue("@SaleID", saleId);
                command.Parameters.AddWithValue("@ProductName", productName);
            });
        }

        [Obsolete]
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

        [Obsolete]
        public static void AddSale(List<DataRow> selectedProducts, decimal total, string userUsername)
        {
            string insertSaleQuery = @"
        INSERT INTO Sales (UserUsername, Total, SaleTime) 
        VALUES (@UserUsername, @Total, @SaleTime);
        SELECT SCOPE_IDENTITY();";

            int saleId = Convert.ToInt32(DatabaseHelper.ExecuteScalar(insertSaleQuery, command =>
            {
                command.Parameters.AddWithValue("@UserUsername", userUsername); 
                command.Parameters.AddWithValue("@Total", total);
                command.Parameters.AddWithValue("@SaleTime", DateTime.Now);
            }));

            foreach (var row in selectedProducts)
            {
                string insertSaleDetailsQuery = @"
            INSERT INTO SaleDetails (SaleID, ProductName, Quantity, Price) 
            VALUES (@SaleID, @ProductName, @Quantity, @Price)";

                DatabaseHelper.ExecuteNonQuery(insertSaleDetailsQuery, command =>
                {
                    command.Parameters.AddWithValue("@SaleID", saleId);
                    command.Parameters.AddWithValue("@ProductName", row.Field<string>("Name"));
                    command.Parameters.AddWithValue("@Quantity", row.Field<int>("OrderQuantity"));
                    command.Parameters.AddWithValue("@Price", row.Field<decimal>("Price"));
                });
            }
        }

        [Obsolete]
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

        [Obsolete]
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

        [Obsolete]
        public static string GetUserPasswordHash(string username)
        {
            string query = "SELECT PasswordHash FROM Users WHERE Username = @Username";

            object result = DatabaseHelper.ExecuteScalar(query, command =>
            {
                command.Parameters.AddWithValue("@Username", username);
            });

            return result?.ToString();
        }

        [Obsolete]
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

        [Obsolete]
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

        [Obsolete]
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

        [Obsolete]
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

        [Obsolete]
        public static string? GetUserRole(string username)
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

        [Obsolete]
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

        [Obsolete]
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

        [Obsolete]
        public static void DeleteUser(string username)
        {
            string query = "DELETE FROM Users WHERE Username = @Username";

            DatabaseHelper.ExecuteNonQuery(query, command =>
            {
                command.Parameters.AddWithValue("@Username", username);
            });
        }

        [Obsolete]
        public static void AddProduct(string name, int categoryID, decimal price, int quantity, string createdBy)
        {
            string query = @"
        INSERT INTO Products (Name, CategoryID, Price, Quantity, CreatedBy) 
        VALUES (@Name, @CategoryID, @Price, @Quantity, @CreatedBy)";

            DatabaseHelper.ExecuteNonQuery(query, command =>
            {
                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@CategoryID", categoryID);
                command.Parameters.AddWithValue("@Price", price);
                command.Parameters.AddWithValue("@Quantity", quantity);
                command.Parameters.AddWithValue("@CreatedBy", createdBy);
            });
        }

        [Obsolete]
        public static void DeleteProduct(string productName, string adminUsername)
        {
            string query = @"
        DELETE FROM Products 
        WHERE Name = @Name AND CreatedBy = @CreatedBy";

            DatabaseHelper.ExecuteNonQuery(query, command =>
            {
                command.Parameters.AddWithValue("@Name", productName);
                command.Parameters.AddWithValue("@CreatedBy", adminUsername);
            });
        }

        [Obsolete]
        public static DataTable GetProducts(string adminUsername)
        {
            string query = @"
        SELECT Products.Name, Categories.CategoryName, Products.Price, Products.Quantity 
        FROM Products 
        INNER JOIN Categories ON Products.CategoryID = Categories.CategoryID 
        WHERE Products.CreatedBy = @CreatedBy";

            return DatabaseHelper.ExecuteQuery(query, command =>
            {
                command.Parameters.AddWithValue("@CreatedBy", adminUsername);
            });
        }

        [Obsolete]
        public static void UpdateProductQuantity(string productName, int newQuantity)
        {
            string query = @"
        UPDATE Products 
        SET Quantity = @NewQuantity 
        WHERE Name = @ProductName";

            DatabaseHelper.ExecuteNonQuery(query, command =>
            {
                command.Parameters.AddWithValue("@NewQuantity", newQuantity);
                command.Parameters.AddWithValue("@ProductName", productName);
            });
        }

        [Obsolete]
        public static void AddCategory(string categoryName, string createdBy)
        {
            string query = @"
        INSERT INTO Categories (CategoryName, CreatedBy) 
        VALUES (@CategoryName, @CreatedBy)";

            DatabaseHelper.ExecuteNonQuery(query, command =>
            {
                command.Parameters.AddWithValue("@CategoryName", categoryName);
                command.Parameters.AddWithValue("@CreatedBy", createdBy);
            });
        }

        [Obsolete]
        public static DataTable GetCategories(string adminUsername)
        {
            string query = @"
        SELECT CategoryID, CategoryName 
        FROM Categories 
        WHERE CreatedBy = @CreatedBy";

            return DatabaseHelper.ExecuteQuery(query, command =>
            {
                command.Parameters.AddWithValue("@CreatedBy", adminUsername);
            });
        }

        [Obsolete]
        public static void UpdateProduct(string originalName, string newName, int categoryID, decimal price, int quantity, string adminUsername)
        {
            string query = @"
        UPDATE Products 
        SET Name = @NewName, CategoryID = @CategoryID, Price = @Price, Quantity = @Quantity 
        WHERE Name = @OriginalName AND CreatedBy = @CreatedBy";

            DatabaseHelper.ExecuteNonQuery(query, command =>
            {
                command.Parameters.AddWithValue("@NewName", newName);
                command.Parameters.AddWithValue("@CategoryID", categoryID);
                command.Parameters.AddWithValue("@Price", price);
                command.Parameters.AddWithValue("@Quantity", quantity);
                command.Parameters.AddWithValue("@OriginalName", originalName);
                command.Parameters.AddWithValue("@CreatedBy", adminUsername);
            });
        }

        [Obsolete]
        public static DataTable GetProductsByAdmin(string adminUsername)
        {
            string query = @"
        SELECT Products.Name, Categories.CategoryName, Products.Price, Products.Quantity 
        FROM Products 
        INNER JOIN Categories ON Products.CategoryID = Categories.CategoryID 
        WHERE Products.CreatedBy = @CreatedBy";

            return DatabaseHelper.ExecuteQuery(query, command =>
            {
                command.Parameters.AddWithValue("@CreatedBy", adminUsername);
            });
        }

        [Obsolete]
        public static int? GetUserAccessCodeID(string username)
        {
            string query = @"
        SELECT AccessCodeID 
        FROM Users 
        WHERE Username = @Username";

            object result = DatabaseHelper.ExecuteScalar(query, command =>
            {
                command.Parameters.AddWithValue("@Username", username);
            });

            return result != null ? Convert.ToInt32(result) : (int?)null;
        }
    }
    
}
