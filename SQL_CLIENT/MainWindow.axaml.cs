using System;
using System.Threading.Tasks;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Metadata;
using Npgsql;
using System.Collections.Generic;
using SQL_CLIENT.Views;

namespace SQL_CLIENT;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }
    
    private void OnFileClick(object? sender, RoutedEventArgs e)
    {
        // var result = await MessageBoxManager
        //     .GetMessageBoxStandard("Alert", "File menu clicked!!")
        //     .ShowAsync();

        // if (result == ButtonResult.Yes) //여기서 드롭메뉴를 선택하면 File menu click이 메시지 박스로 출력되네
        // {
        //     // Yes 버튼 클릭 시 처리
        //     Console.WriteLine("You Press the Yes!!");
        // }
        // else //그 외에 동작이 필요한가?
        // {
        //     // No 버튼 클릭 시 처리
        //     Console.WriteLine("You Press the NO...");  
        // }
    }
    
    private async void OnOpenClick(object sender, RoutedEventArgs e)
    {
        try
        {
            //PG데이터 가져오기
            var queryResult = await FetchPostgreSQLData();
            //새로운 창을 생성 후 결과 전달
            var resultWindow = new NavigatorView(queryResult);
            await resultWindow.ShowDialog(this);
        }
        catch (Exception ex)
        {
            var errorBox = MessageBoxManager.GetMessageBoxStandard("Connection ERROR","Tried to Connect to Postgresql, " +
                "but could not be established.\n" + ex.Message);
            await errorBox.ShowAsync();
        }
    }

    private async Task<List<string>> FetchPostgreSQLData()
    {
        string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=root;Database=pgbenchtest";
        // string query = "select * from pgbench_accounts";

        var result = new List<string>();

        using (var connection = new NpgsqlConnection(connectionString))
        {
            await connection.OpenAsync();
        
            var command = new NpgsqlCommand("SELECT * FROM pgbench_accounts;", connection);
            var reader = await command.ExecuteReaderAsync();
        
            while (await reader.ReadAsync())
            {
                var rowData = new List<string>();

                // 결과의 모든 컬럼 처리
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    var columnName = reader.GetName(i);  // 컬럼 이름
                    var columnValue = GetColumnValue(reader, i)?.ToString() ?? "NULL"; // 컬럼 값

                    // 컬럼 데이터를 "컬럼이름: 값" 형식으로 저장
                    rowData.Add($"{columnName}: {columnValue}");
                }

                // 행 데이터를 하나의 문자열로 합치고 result에 추가
                result.Add(string.Join(", ", rowData));
            }
        }
        
        return result;
    }
    
    private object GetColumnValue(NpgsqlDataReader reader, int columnIndex)
    {
        if (reader.IsDBNull(columnIndex))
            return null;

        var fieldType = reader.GetFieldType(columnIndex);

        switch (Type.GetTypeCode(fieldType))
        {
            case TypeCode.Int32:
                return reader.GetInt32(columnIndex);
            case TypeCode.String:
                return reader.GetString(columnIndex);
            case TypeCode.DateTime:
                return reader.GetDateTime(columnIndex);
            case TypeCode.Double:
                return reader.GetDouble(columnIndex);
            case TypeCode.Boolean:
                return reader.GetBoolean(columnIndex);
            // 필요한 다른 데이터 타입을 처리
            default:
                return reader.GetValue(columnIndex); // 기본적으로 GetValue()로 반환
        }
    }
    

    private void OnSaveClick(object sender, RoutedEventArgs e)
    {
        // 파일 저장 로직을 처리할 수 있습니다.
        //MessageBox.Show(this, "Save file clicked!", "Menu Action");
        
    }

    private void OnExitClick(object sender, RoutedEventArgs e)
    {
        // 애플리케이션 종료 처리
        this.Close();
    }
}