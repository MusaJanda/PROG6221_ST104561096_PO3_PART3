   M S B A M L   `   `   `  ����R  NCybersecurityChatBotGUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:CybersecurityChatBotGUICybersecurityChatBotGUI  5   	   (    "CybersecurityChatBotGUI.MainWindow   5   	   W SPresentationCore, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   7local%clr-namespace:CybersecurityChatBotGUI     5   	     � Title$  Cybersecurity Chatbot��5   	   $	��700��6'   $	��900��64   $	��600��5	   	   $	��800��6    � WindowStartupLocation$ CenterScreen=�5
   	   .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6   ���#282C34 �� Padding$ 10q�63   ���#007ACC$��0,0,0,2q�6V   ._�5      ��  �� Orientation$ Vertical=�6   .+�5      �� $%��Cybersecurity Awareness Chatbot��6   
�������$��28(�6V   $
��Bold"�6d   $��Center=�6v   �� 5      -   5         ��Name$ staticLogoTextBlock��5      ���#007ACC$��Consolas)�6N   $��10(�6d   $��Center=�6r   $
��Wrap=�6�   $��0,5,0,0q�6�   5      5      � 5   
   $��1��6   $��10q�6    � ColumnDefinitions 5      �� 5      $��* �6#   �� 5      $
��Auto �6#   .+�5       �� $��0��6   ���#DDDDDD$��1q�6;    �� CornerRadius$ 5��6O   $ 10q�6`   ._�5!      �� -   5         ��Name$ chatDisplayRichTextBox��5!      	 �� 
IsReadOnly$
	 True��6>   "
 �� VerticalScrollBarVisibility$

 Auto=�6P    � 	Resources 5"       �� 
TargetType �� Property �� Value��%X   'J�       �� 5#      " J�6    .�5$      ��  �@��Margin$ 0q�67   5%      5&      5'      5(      �� 5*      $��1��6   $	��150��6%   $��10,0,0,0q�61   ���#DDDDDD$��1q�6Y   $ 5��6m   $ 5q�6~   ���#F8F8F8._�5+      �� -   5        $ asciiArtImageTextBlock��5+      $��Consolas)�6<   $��8(�6R   $
��Wrap=�6_   $��Stretch=�6s   5,      5-      �� 5/   
   $��2��6   ���#F0F0F0$ 10q�63   ���#DDDDDD$��0,1,0,0q�6V   ._�50      �  51      �� 52      $��* �6'   �� 53      $
��Auto �6'   .+�55      �� $��0��6   ���#CCCCCC$��1q�6?   $ 3��6S   ._�56      �� -   5         ��Name$ userInputTextBox��56      $��14(�57      $��5q�6,   $��Center=�68    � ToolTip$; 5Type your message here and press Enter or click Send.��58      59      �� 5:      -   5        $��1��5:      $
��Send��6)   $��10,0,0,0q�68   $
��15,5q�6J   ���#007ACC
�������$
��Bold"�5;   A    � Cursor$
 Hand��5<   2   5=      5>      �� 5@   
   $��3��6   $ 
Horizontal=�6"   $��Center=�6;   $��	0,10,0,10q�6X   .+�5A      �� -   5        $��View My Tasks��5A      $��5q�6H   $
��10,5q�6S   �� 5B      -   5        $��
Start Quiz��5B      $��5q�6E   $
��10,5q�6P   �� 5C      -   5        $��View Activity Log��5C      $��5q�6R   $
��10,5q�6]   �� 5D      -	   5        $��Clear Chat History��5D      $��5q�6J   $
��10,5q�6U   5E      5F      5G      