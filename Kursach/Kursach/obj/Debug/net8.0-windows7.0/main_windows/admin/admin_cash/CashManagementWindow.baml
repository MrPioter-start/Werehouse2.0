   M S B A M L   `   `   `  ����B  >Kursach, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:Kursach.main_windowsKursach.main_windows  5   	   /    )Kursach.main_windows.CashManagementWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   4local"clr-namespace:Kursach.main_windows     5   	     � Title$'  !Управление кассой��5   	   $	��350��6#   $	��400��60   ���	#FF2D2D30
������� � WindowStartupLocation$ CenterScreen=�5
   	   .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��Сумма:��6   
�������$��16(�65   $��
20,10,0,10q�6C   �� 5   
   -   5         ��Name$ AmountTextBox��5      ���	#FF4D4D4D
����������	#FF555555$��1q�5   &   $��5q�6:   $	��200��5      $��14(�6   $��100,41,100,48q�6(   $��3��6?   �� 5   
   -   5         ��Name$ OperationTypeComboBox��5      ���	#FF4D4D4D
���   ����	#FF555555$��1q�5   '   $��5q�6;   $	��200��5      $��14(�6   $��100,20,100,0q�6)   $��2��6?   .@�5      �� $��Пополнение��6    �� 
IsSelected$
 True��60   �� 5      $��Снятие��6   5      �� 5   
    �� Orientation$ Vertical=�6   $��Center=�6,   $��Center=�6I   $��10q�6d   $��3��6p   .+�5       �� -   5        $��Выполнить��5       ���	#FF00A651
�������$��0q�5"      $
��10,5q�6%   $��14(�64   $��0,0,0,10q�5#      $	��200��6#   $��40��6/   �� 5%      -   5        $%��История операций��5%      ���	#FF4D4D4D
�������$��0q�5'      $
��10,5q�6%   $��14(�64   $��0,0,0,10q�5(      $	��200��6#   $��40��6/   �� 5)      -   5        $)��#История возвтратов��5)      ���	#FF3C3C3C
�������$��0q�5+      $
��10,5q�6%   $��14(�64   $	��200��5,      $��40��6   5-      5.      5/      