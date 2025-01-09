using System;
using Npgsql;

namespace SQL_CLIENT.Services;

public class PGconn
{
    static void pg_try_connect(string[] args)
    {
        string connectionString = "Host=your_host;Port=5432;Username=your_username;Password=your_password;Database=your_database";
        //기본적으로 정보를 받아와서 넣어주기, 자식form에서 입력받으값을 통해 수행
        //테스트를 위해 우선은 Local에서 연결하고 VM으로 외부연결까지 시도
        
        try
        {
            // PostgreSQL 연결 생성
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                Console.WriteLine("Connected to PostgreSQL database!");

                // SQL 쿼리 실행
                string query = "SELECT * FROM pgbench_accounts LIMIT 1";

                using (var command = new NpgsqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader()) //정보 읽어들이기
                    {
                        while (reader.Read())
                        {
                            // 컬럼 데이터 출력 -> 출력방식은 좀 고민해보기
                            Console.WriteLine($"Column1: {reader["column1"]}");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}