   M S B A M L   `   `   `  ����B  >XVB_GUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:XVB_GUIXVB_GUI  5   	       XVB_GUI.OptionsWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:XVB_GUI     5   	     � Title$  OptionsWindow��5   	   $	��400��6   $	��800��6,   
���   �
�������$��20(�5
   	   .��5      �  � RowDefinitions 5   
   �� 5      $��90 �6   6*   �� 5      6   �� 5      6    � ColumnDefinitions 5   
   �� 5      6!   �� 5      6!   �� 5      6!   .+�5   
   �� $��0��6   $��1��6!   $��50(�61   $��Center=�6?   	Options6e   �� 5   
   $��1��6   $��0��6"   .+�5      �� $��Center=�6   Log data to AppData file6O   �� 5      -   5         ��Name$ chb_LogData��5   *   $��0,15,0,0q�6   $��Center=�6?   6^   5      �� 5   
   $��1��6   $��1��6"   $y�Hidden=�62   .+�5       �� $��Center=�6   Logfile path6C   �� 5!      -   5         ��Name$ tb_PathDisplay��5!   �   $��0,15,0,0q�6    �� IsReadOnlyCaretVisible .  �� 
IsReadOnly . 
�������
���   �$��2q�6~   
����  �6�   �� 5"      -   5         ��Name$ btn_SelectFilePath��5"   �   $��0,15,0,0q�6   $��60��6'   
����  �
���   �$��2q�6\   
�������.��6�   Select6�   5#      �� 5%   
   $��1��6   $��2��6"   .+�5&      �� $��Center=�6   Select currency6F   �� 5'      -   5         ��Name$ cb_SelectCurrency��5'   5   $��0,15,0,0q�6   $	��200��6)   
���   �5)      5*      �� 5,   
   $��2��6   $��1��6"   $��Bottom=�62   .+�5-      �� -   5        $ btn_SaveExit��5-   x   
���   �
�������
����  �$��2q�6R   $��0,0,0,15q�6f   .��6�   Save and exit6�   �� 5.      -   5        $ btn_QuitNoSave��5.   f   
���   �
�������
����  �$��2q�6R   .��6�   Exit without saving6�   5/      50      51      