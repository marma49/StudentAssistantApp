   M S B A M L   `   `   `  ����N  JStudentAssistantApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:StudentAssistantApp.ViewsStudentAssistantApp.Views  5   	   .    (StudentAssistantApp.Views.MainWindowView   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   9local'clr-namespace:StudentAssistantApp.Views     5   	   R NCaliburn.Micro.Platform, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null(calhttp://www.caliburnproject.org  5   	   b ^MaterialDesignThemes.Wpf, Version=3.2.0.1979, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5
   	     � Title$  
MainWindow��5	   	   $	��800��6   $
��1000��6)      MaterialDesignBody#���   5   	   $��Regular"�5   	   $��13(�5   	   &   System.Windows.Media.TextOptions   TextFormattingMode$ Ideal=�5   	      TextRenderingMode$
 Auto=�5   	     MaterialDesignPaper#���  5   	     MaterialDesignFont#���  5   	   $	��600��5   	   $	��800��5   	    � WindowStartupLocation$ CenterScreen=�6    � WindowState 5   	   �� *WindowState+ �� Mode$ TwoWay=�.��5      �  � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6    � RowDefinitions 5!   
   �� 5#      $��* �6   .+�59   
   � $��0��6   $��1��6   .+�5:      � $
��Left=�6   $��79��6.   $	��Top=�6:   $	��170��6R   ���#1c2335.+�5;      �� $��
StudentApp��6   $��Center=�6.   $��Center=�6K   
�������$��24(�6y   $
��Bold"�6�   $��Arial)�6�   5<      � 5>      $
��Left=�6   $	��170��6.   ���#21293e$��0,79,0,0q�6O   .+�5?      �� $��0,0,0,0q�6   .+�5@      �� -   5         ��Name$ ListViewMenu��5@      ���	#FF89C0F7$��Disabled=�6L   $��16(�6�   .@�5A      �� -   5        	 ��Name$	 	ItemNotes��5A   '   $��80��6:     Caliburn.Micro.Message
   Attach$+
 %[Event GotFocus] = [Action LoadNotes]��6F   .��5B      ��  �� Orientation$ 
Horizontal=�6)   .+�5C   "   '  !MaterialDesignThemes.Wpf.PackIcon     Kind$
 Note=�6:   $��30��6F   $��30��6R   $��10q�6]   �� 5D   "   $��Notatki��6,   $��Center=�6;   $��5q�6V   5E      5F      �� 5G      -   5        $	 
ItemGrades��5G   '   $��80��6;   $,
 &[Event GotFocus] = [Action LoadGrades]��6G   .��5H      �� $ 
Horizontal=�6)   .+�5I   "     $ Grade=�6:   $��30��6G   $��30��6S   $��10q�6^   �� 5J   "   $��Oceny��6,   $��Center=�69   $��5q�6T   5K      5L      �� 5M      -   5        $	 	ItemTasks��5M   '   $��80��6:   $+
 %[Event GotFocus] = [Action LoadTasks]��6F   .��5N      �� $ 
Horizontal=�6)   .+�5O   "     $ Subtasks=�6:   $��30��6J   $��30��6V   $��10q�6a   �� 5P   "   $��Zadania��6,   $��Center=�6;   $��5q�6V   5Q      5R      �� 5S      -   5        $	 ItemСalendar��5S   '   $��80��6=   $.
 ([Event GotFocus] = [Action LoadCalendar]��6I   .��5T      �� $ 
Horizontal=�6)   .+�5U   "     $ Calendar=�6:   $��30��6J   $��30��6V   $��10q�6a   �� 5V   "   $��	Kalendarz��6,   $��Center=�6=   $��5q�6X   5W      5X      �� 5Y      -   5        $	 
ItemLogOut��5Y   '   $��80��6;   $'
 ![Event MouseUp] = [Action LogOut]��6G   .��5Z      �� $ 
Horizontal=�6)   .+�5[   "     $ Login=�6:   $��30��6G   $��30��6S   $��10q�6^   �� 5\   "   $��Wyloguj się��6,   $��Center=�6?   $��5q�6Z   5]      5^      5_      5`      5a      5b      �� 5c   
   -   5         ��Name$ 
ActiveItem��5c      $��1��6-   $��1��6=   5d      5e      