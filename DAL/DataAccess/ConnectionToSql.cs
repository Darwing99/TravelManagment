using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataAccess
{
    public abstract class ConnectionToSql
    {
      

            private readonly string _connectionString;
            public ConnectionToSql() => _connectionString = $"Server=localhost;Initial Catalog=TravelManagementDB;" +
                                                            $"User ID=sa;Password=4CC3s0;Connect Timeout=30;" +
                                                            "Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;" +
                                                            "MultiSubnetFailover=False".ToString().Trim();
            protected string GetConnectionString()
            {
                return _connectionString;
            }



            protected SqlConnection GetConnection()
            {
                return new SqlConnection(GetConnectionString());
            }


            protected async Task<int> ExecuteStoredProcedure(string procedureName, SqlParameter[]? parameters = null)
            {
                using var connection = GetConnection();
                if (connection.State != ConnectionState.Open)
                {
                    await connection.OpenAsync();
                }


                using SqlCommand command = new(procedureName, connection);
                command.CommandType = CommandType.StoredProcedure;


                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }
                try
                {
                    int rowsAffected = await command.ExecuteNonQueryAsync();

                    return rowsAffected;
                }
                catch (SqlException)
                {

                    return 0;
                }
            }


            protected async Task<int> ExecuteQuery(string query, SqlParameter[]? parameters = null)
            {
                using var connection = GetConnection();
                if (connection.State != ConnectionState.Open)
                {
                    await connection.OpenAsync();
                }
                using SqlTransaction transaction = connection.BeginTransaction();


                using SqlCommand command = new(query, connection);
                command.Transaction = transaction;


                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }
                try
                {
                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    await transaction.CommitAsync();

                    return rowsAffected;
                }
                catch (SqlException)
                {
                    await transaction.RollbackAsync();

                    return 0;
                }


            }

            /// <summary>
            /// method to get users
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="query"></param>
            /// <param name="mapper"></param>
            /// <param name="parameters"></param>
            /// <param name="useLocalConnectionOnFailure"></param>
            /// <returns></returns>
            protected async Task<List<T>> ExecuteQuerySelect<T>(string query, Func<SqlDataReader, T> mapper, SqlParameter[]? parameters = null)
            {
                List<T> result = new();

                using var connection = GetConnection();
                if (connection.State != ConnectionState.Open)
                {
                    await connection.OpenAsync();
                }

                using SqlTransaction transaction = connection.BeginTransaction();
                using SqlCommand command = new(query, connection);
                command.Transaction = transaction;
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }

                try
                {
                    using SqlDataReader reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        result.Add(mapper(reader));
                    }

                    await reader.CloseAsync();

                    await transaction.CommitAsync();
                }
                catch (SqlException)
                {
                    await transaction.RollbackAsync();
                    throw;
                }

                return result;
            }


            protected async Task<T?> ExecuteQuerySelectSingle<T>(string query, Func<SqlDataReader, T> mapper, SqlParameter[]? parameters = null) where T : class
            {
                T? result = null;

                using var connection = GetConnection();
                if (connection.State != ConnectionState.Open)
                {
                    await connection.OpenAsync();
                }

                using SqlTransaction transaction = connection.BeginTransaction();
                using SqlCommand command = new(query, connection);
                command.Transaction = transaction;
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }

                try
                {
                    using SqlDataReader reader = await command.ExecuteReaderAsync();
                    if (await reader.ReadAsync())
                    {
                        result = mapper(reader);
                    }

                    await reader.CloseAsync();

                    await transaction.CommitAsync();
                }
                catch (SqlException)
                {
                    await transaction.RollbackAsync();
                    throw;
                }

                return result;
            }
            /// <summary>
            /// method to get data
            /// </summary>
            /// <param name="query"></param>
            /// <param name="mapper"></param>
            /// <param name="parameters"></param>
            /// <returns></returns>
            protected async Task<object?> ExecuteQuerySelectSingleAsync(string query, Func<SqlDataReader, object> mapper, SqlParameter[]? parameters = null)
            {
                object? result = null;

                using var connection = GetConnection();
                if (connection.State != ConnectionState.Open)
                {
                    await connection.OpenAsync();
                }

                using SqlTransaction transaction = connection.BeginTransaction();
                using SqlCommand command = new(query, connection);
                command.Transaction = transaction;
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }

                try
                {
                    using SqlDataReader reader = await command.ExecuteReaderAsync();
                    if (await reader.ReadAsync())
                    {
                        result = mapper(reader);
                    }

                    await reader.CloseAsync();

                    await transaction.CommitAsync();
                }
                catch (SqlException)
                {
                    await transaction.RollbackAsync();
                    throw;
                }

                return result;
            }
            protected object ExecuteQuerySelectSingle(string query, Func<SqlDataReader, object> mapper, SqlParameter[]? parameters = null)
            {
                object? result = null;

                using var connection = GetConnection();
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                using SqlTransaction transaction = connection.BeginTransaction();
                using SqlCommand command = new(query, connection);
                command.Transaction = transaction;
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }

                try
                {
                    using SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        result = mapper(reader);
                    }

                    reader.Close();

                    transaction.Commit();
                }
                catch (SqlException)
                {
                    transaction.Rollback();
                    throw;
                }

                return result;
            }
            /// <summary>
            /// Method to insert bulk data
            /// </summary>
            /// <param name="table"></param>
            /// <param name="nameTable"></param>
            protected async Task InsertBulkData(DataTable table, string nameTable)
            {
                using var connection = GetConnection();

                using SqlTransaction transaction = connection.BeginTransaction();
                using SqlBulkCopy bulkCopy = new(connection, SqlBulkCopyOptions.Default, transaction);
                try
                {
                    bulkCopy.DestinationTableName = nameTable;
                    await bulkCopy.WriteToServerAsync(table);
                    await transaction.CommitAsync();
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();

                    throw;
                }
            }

            protected async Task UpdateBulkData(DataTable table, string nameTable, string[] columnasClave)
            {
                using var connection = GetConnection();

                using SqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    string tempTableName = "#TempTable_" + Guid.NewGuid().ToString().Replace("-", "");
                    SqlCommand createTempTableCommand = new SqlCommand($"CREATE TABLE {tempTableName} AS SELECT * FROM {nameTable} WHERE 1 = 0", connection, transaction);
                    await createTempTableCommand.ExecuteNonQueryAsync();

                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.Default, transaction))
                    {
                        bulkCopy.DestinationTableName = tempTableName;
                        await bulkCopy.WriteToServerAsync(table);
                    }

                    string updateQuery = $"MERGE INTO {nameTable} AS Target " +
                                        $"USING {tempTableName} AS Source " +
                                        $"ON {string.Join(" AND ", columnasClave.Select(col => $"Target.{col} = Source.{col}"))} " +
                                        $"WHEN MATCHED THEN UPDATE SET " +
                                        $"{string.Join(", ", table.Columns.Cast<DataColumn>().Select(col => $"Target.{col.ColumnName} = Source.{col.ColumnName}"))};";
                    SqlCommand updateCommand = new SqlCommand(updateQuery, connection, transaction);
                    await updateCommand.ExecuteNonQueryAsync();

                    SqlCommand dropTempTableCommand = new SqlCommand($"DROP TABLE {tempTableName}", connection, transaction);
                    await dropTempTableCommand.ExecuteNonQueryAsync();

                    await transaction.CommitAsync();
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }



        }
    }