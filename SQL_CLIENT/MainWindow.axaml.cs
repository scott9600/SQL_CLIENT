using System;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace SQL_CLIENT;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }
    
    
    
    private async void OnFileClick(object? sender, RoutedEventArgs e)
    {
        var result = await MessageBoxManager
            .GetMessageBoxStandard("Menu Action", "File menu clicked!")
            .ShowAsync();

        if (result == ButtonResult.Yes) //여기서 드롭메뉴를 선택하면 File menu click이 메시지 박스로 출력되네
        {
            // Yes 버튼 클릭 시 처리
            Console.WriteLine("You Press the Yes!!");
        }
        else //그 외에 동작이 필요한가?
        {
            // No 버튼 클릭 시 처리
            Console.WriteLine("You Press the NO...");  
        }
    }
    
    private void OnOpenClick(object sender, RoutedEventArgs e)
    {
        // 파일 열기 로직을 처리할 수 있습니다.
        
        //MessageBox.Show(this, "Open file clicked!", "Menu Action");
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