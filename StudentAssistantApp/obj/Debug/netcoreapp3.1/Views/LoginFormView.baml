   M S B A M L   `   `   `  ����  StudentAssistantApp-    'StudentAssistantApp.Views.LoginFormView   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   R NCaliburn.Micro.Platform, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null(calhttp://www.caliburnproject.org  5   	   b ^MaterialDesignThemes.Wpf, Version=3.2.0.1979, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	      MaterialDesignBody#���   5	   	   $��Regular"�5
   	   $��14(�5   	   &   System.Windows.Media.TextOptions    TextFormattingMode$  Ideal=�5   	      TextRenderingMode$
 Auto=�5   	     MaterialDesignFont#���  5   	    � 
ResizeMode$ NoResize=�6;    � Title$ LoginFormView��5   	   $	��550��6   $	��340��6,    � WindowStartupLocation$ CenterScreen=�5   	    � AllowsTransparency$
 True��6.    � WindowStyle$
 None=�6H   ��5   8   Z� .��5      �� ���#28324b �� CornerRadius$ 15��6"   ._�5   
   � .+�5      )  #MaterialDesignThemes.Wpf.DialogHost     IsOpen 6(   �� *IsDialogProgressOpen+	   DialogContent	 5      � 5       % !MaterialDesignCircularProgressBar#��[ 6"   
 � Value$
 35Q�6]    � IsIndeterminate$
 True��6h   $��30��6   $��30��6�   5        5       6(   �� *IsDialogOpen+	 5      � 5      $	��550��6   $	��340��6(   ���#28324b.+�5      �� $��40q�6%   .+�5      �� 
�������$��
Nowe konto��67   $��22(�6L   $��Center=�6Z   $��0 40 0 0q�6w   �� 5      $��20 30q�6)   .+�5   "   �� -   5         ��Name$ NewUser��5   *   )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Nazwa użytkownika��6T   
������� �� 
CaretBrush
 �����   
Foreground
 �����.  (MaterialDesignThemes.Wpf.TextFieldAssist   UnderlineBrush
 ��������#fff % !MaterialDesignFloatingHintTextBox#��[ 5#      $
��0 20q�5$      ��5   ;   �� *	NewUser+5$      G� 5%   "   -   5        $ Hasło��5%   .   
������� G� 
CaretBrush
 �����
 �����
 ��������#fff ) %MaterialDesignFloatingHintPasswordBox#��[ 5*   "   $
��0 20q�5+   "   G� 5,   "   -   5        $ Potwierdź hasło��5,   .   
�������
 �����
 �����
 ��������#fff#��[ 51   "   $
��0 20q�52   "   53      �� 54      $��0 40 0 0q�6)   .+�55   "   ��  �� Orientation$ 
Horizontal=�6-   $��Center=�6F   .+�56   &   �� -   5         ��Name$ CloseDialog��56   -   $��Anuluj��6B   $
��15 0q�6S   
���   ����#ffb525���#ffb525   CloseDialogCommand#��Z 57   )   �� 58   &   -   5        $ 
AddAccount��58   -   $��
Potwierdź��6A   $
��15 0q�6U   
���   ����#ffb525���#ffb525  Caliburn.Micro.Message   Attach$) #[Event Click] = [Action AddAccount]��59   )   5:   #   5;      5<      5=      .��5@      �� $��40q�6   .+�5A      �� 
�������$��Student Assistant��6/   $��22(�6K   $��Center=�6Y   $��0 40 0 0q�6v   �� 5B      $��20 30q�6!   .+�5C      �� -   5        $ Username��5C   "   $ Login��64   
�������
 �����
 �����
 ��������#fff#��[ 5G      $
��0 20q�5H      G� 5I      -   5         G�Name$ UserPassword��5I   &   $ Hasło��6<   
�������
 �����
 �����
 ��������#fff#��[ 5N   "   $
��0 20q�5O   "    � Cursor$ IBeam��60   5P      �� 5Q      $��0 40 0 0q�6!   .+�5R      �� $ 
Horizontal=�6%   $��Center=�6>   .+�5S      �� -   5        $ Close��5S   %   $��Zamknij��64   $
��15 0q�6F   
���   ����#ffb525���#ffb5256|   �� 5T      -	   5        $ LogIn��5T   %   $��Zaloguj��64   $
��15 0q�6F   
���   ����#ffb525���#ffb5256|   5U      �� 5V      -
   5        $ 
NewAccount��5V   !   $��
Nowe konto��65   $��50 20q�6J   
���   ����#ffb525���#ffb525   OpenDialogCommand#��Z 5W   !   5X      5Y      5Z      5[      5\      5]      