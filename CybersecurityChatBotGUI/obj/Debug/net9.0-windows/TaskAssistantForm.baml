   M S B A M L   `   `   `  ����R  NCybersecurityChatBotGUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:CybersecurityChatBotGUICybersecurityChatBotGUI  5   	   /    )CybersecurityChatBotGUI.TaskAssistantForm   5   	   W SPresentationCore, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   7local%clr-namespace:CybersecurityChatBotGUI     5   	     � Title$"  Cybersecurity Task Assistant��5   	   $	��650��6.   $	��800��6;    � WindowStartupLocation$ CenterScreen=�5	   	    � 	Resources 5      3   -CybersecurityChatBotGUI.BoolToStatusConverter   BoolToStatusConverter��%   &	          5   
   6&   5      .��5      � $��10q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6   $��My Cybersecurity Tasks��6!   $��24(�6?   $
��Bold"�6M   $��Center=�6_   $��0,0,0,10q�6|   �� 5   
   -   5         ��Name$ tasksListView��5       $��1��6   $��0,0,0,10q�6U   ��67   �� *Tasks+ �� View 5      �� 5      .P�5      �� $��Status��6%    �� Width$ 60��65    �� CellTemplate 5      �� 5      .R�5   "   �� ��6,   �� *IsCompleted+ �� 	Converter# [  5       5"      �� 5#      $��Title��6%   $	 200��64    5$      �� 5%      .R�5&   "   �� ��6,   �� *Title+��5'   &   �� 5(   *   	 �� 
TargetType"	 ��60   
 �� Triggers
 5)   .   �� 5*   2    �� Binding 6>   �� *IsCompleted+ �� Value True6^   .a�5+   6   ��  �� Property � �� �� Value$ Strikethrough{�6X   �� 5,   6    � ��
 �����6S   5-   3   5/   +   51   #   52      54      �� 55      $��Description��6%   $	 350��6g    �� DisplayMemberBinding 6:   �� *Description+6g   �� 56      $��Due Date��6%   $	 100��6�    67   �� *	DueDate+ �� StringFormat$ {0:yyyy-MM-dd}��6�   57      59      �� 5;   
   $��2��6    �� Orientation$ Vertical=�6"   $��0,0,0,10q�69   .+�5<      �� $��Add New Task:��6   $
��Bold"�6-   $��0,0,0,5q�6?   � 5=       � ColumnDefinitions 5>      �� 5?      $
��Auto �6'   �� 5@      $��* �6'    5B      �� 5C      $
��Auto �6$   �� 5D      $
��Auto �6$   �� 5E      $
��Auto �6$   .+�5H      �� $��0��6   $��0��6)   $��Title:��69   $��Center=�6G   $��0,0,5,0q�6b   �� 5I      -   5         ��Name$ txtTaskTitle��5I   7   $��0��6   $��1��6'   $��0,0,0,5q�6M   �� 5K      $��1��6   $��0��6)   $��Description:��69   $��Center=�6M   $��0,0,5,0q�6h   �� 5L      -   5        $ txtTaskDescription��5L   7   $��1��6   $��1��6'   $��0,0,0,5q�6S    �� AcceptsReturn$
 True��6d    �� TextWrapping$
 Wrap=�6y   $��40��6�   �� 5N      -   5         ��Name$ chkEnableDueDate��5N   8   $��2��6   $��0��6(   $��Set Due Date��6R   $��Center=�6i   $��0,0,5,0q�6�   5O   >   (  "System.Windows.Controls.DatePicker  5P      -   5          Name$ 	dpDueDate��5P   :   $��2��6   $��1��6*   ${�False��6M   $��0,0,0,5q�6_   5R      5S      �� 5U   
   $��3��6   $ 
Horizontal=�6"   $��Center=�6;   .+�5V      �� -   5        $��Add Task��5V      $��5q�6A   �� 5W      -   5        $��Mark Complete/Incomplete��5W      $��5q�6V   �� 5X      -   5        $��Delete Task��5X      $��5q�6G   �� 5Y      -	   5        $��Close��5Y      $��5q�6<   5Z      5[      5\      