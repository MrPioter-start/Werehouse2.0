   M S B A M L   `   `   `  ����  Kursach8    2Kursach.main_windows.admin.ProductManagementWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$+  %Управление товарами��5   	   $	��664��6%   $	��800��62   ���	#FF2D2D30
������� � WindowStartupLocation$ CenterScreen=�5   	   .��5      �  � RowDefinitions 5   
   �� 5	      $
��Auto �6   �� 5
      $��* �6   .+�5   
   ]� ���	#FF3C3C3C$��60��6+   $��0��67   .+�5      ��  �� Orientation$ 
Horizontal=�6   $��Center=�62   $
��20,0q�6M   .+�5      �� -   5        $!��Добавить товар��5      ���	#FF00A651
�������$��40��5      $	��150��6%   $
��10,5q�61    � Cursor$
 Hand��6@   $��0q�5      $
��Bold"�6-   $��14(�6?   �� 5      -   5        $��Категории��5      ���#63DD8D
�������$��40��5      $	��120��6%   $
��10,5q�61   $
 Hand��6@   $��0q�5      $
��Bold"�6-   $��14(�6?   $
��10,0q�6M   5      �� 5      $ 
Horizontal=�6   $��Right=�62   $��Center=�6N   $��0,0,20,0q�6i   .+�5      �� $��Поиск:��6   
�������$��Center=�6=   $��0,0,10,0q�6X   �� 5      -   5         ��Name$ SearchTextBox��5      ���	#FF4D4D4D
����������	#FF555555$��1q�5   2   $��5q�6F   $��30��6R   $	��200��5      $��14(�6&   5      5       &   System.Windows.Controls.DataGrid  5"   
   -   5          Name$ ProductsDataGrid��5"         AutoGenerateColumns$ False��6-   ���	#FF3C3C3C
�������$��0q�5$         GridLinesVisibility$
 None=�6'   $��
20,0,20,20q�5%      	   CanUserAddRows$	 False��6'   $��1��6>   
   Columns
 5&      0  *System.Windows.Controls.DataGridTextColumn  5'      ,  &System.Windows.Controls.DataGridColumn   Header$ Название��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6P      
IsReadOnly$
 True��6Z   1  +System.Windows.Controls.DataGridBoundColumn   Binding 67   �� *Name+6Z     5(      $ Категория��6%   $ * 6Y   $
 True��6c    68   �� *CategoryName+6c     5)      $ Цена��6%   $ * 6M   $
 True��6W    63   �� *Price+6W     5*      $ Количество��6%   $ * 6V   $
 True��6`    69   �� *
Quantity+6`   4  .System.Windows.Controls.DataGridTemplateColumn  5+      $ Действия��6)   $	 200 6;      CellTemplate 5,      �� 5-      .R�5.      �� $ 
Horizontal=�6)   .+�5/   "   �� -   5        $��Удалить��5/   )   ���	#FFD4AF37
�������$��0q�51      $
��10,5q�6-   $��0,0,20,0q�6<   $
��Bold"�52      $��14(�6+   $
 Hand��69   �� 54   "   -   5        $��Изменить��54   )   ���	#FF00A651
�������$��0q�56      $
��10,5q�6-   $��10,0,0,0q�6<   $
��Bold"�57      $��14(�6+   $
 Hand��69   58      59      5;         	CellStyle 5>      �� 5?       �� 
TargetType*  $System.Windows.Controls.DataGridCell"  6   .�5@      ��  �� Property � �� �� Value$ 0q�68   �� 5A       � ��$ 10q�60   �� 5B       � ��$ 14(�61   �� 5C       � ��$
 Left=�6<   �� 5D       � ��$ 10,0,0,0q�6/   5E         ColumnHeaderStyle 5H      �� 5I      =  7System.Windows.Controls.Primitives.DataGridColumnHeader"  6   .�5J      ��  � �� �	#FF4D4D4D63   �� 5K       � ��
 �����63   �� 5L       � ��$
 Bold"�63   �� 5M       � ��$ 16(�61   �� 5N       � ��$ 10q�60   �� 5O       � ��$ 0q�68   5P         RowStyle 5S      �� 5T      )	  #System.Windows.Controls.DataGridRow" 	 6   .�5U      ��  � �� �	#FF3C3C3C63   �� 5V       � ��
 �����63   �� 5W       � ��$ 14(�61   �� 5X       � ��$ 0q�68   5Y      5[      5\      5]      