   M S B A M L   `   `   `  ����R  NCybersecurityChatBotGUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:CybersecurityChatBotGUICybersecurityChatBotGUI  5   	   (    "CybersecurityChatBotGUI.QuizWindow   5   	   W SPresentationCore, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   7local%clr-namespace:CybersecurityChatBotGUI     5   	     � Title$  Cybersecurity Quiz��5   	   $	��450��6$   $	��600��61    � WindowStartupLocation$ CenterScreen=�5	   	   .��5
      � $��10q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6   $��Cybersecurity Quiz��6!   $��24(�6;   $
��Bold"�6I   $��Center=�6[   $��0,0,0,10q�6x   �� 5   
   -   5         ��Name$ QuizContent��5   "   $��1��6   $��0,0,0,10q�67   .+�5      �� -   5         ��Name$ QuestionTextBlock��5      $
��Wrap=�63   $��16(�6G   $��0,0,0,10q�6U   �� 5      -   5        $ OptionsPanel��5      5      �� 5   
   -   5        $ FeedbackTextBlock��5   !   $��2��6   
���  ��$
��Bold"�6M   $��0,0,0,10q�6_   �� 5   
   $��3��6    �� Orientation$ 
Horizontal=�6"   $��Center=�6;   .+�5      �� -   5        $��Submit Answer��5      $��5q�6H   �� 5      -   5        $��Next Question��5      $��5q�6H   �� 5      -   5        $��Finish Quiz��5      $��5q�6D   5      5       5!      