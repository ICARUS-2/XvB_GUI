   M S B A M L   `   `   `  ����B  >XVB_GUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:XVB_GUIXVB_GUI  5   	       XVB_GUI.TransactionData   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:XVB_GUI     5   	     � Title$  TransactionData��5   	   $	��500��6!   $	��800��6.   
���   �$��15(�5
   	   .��5      �� .+�5   
   ��  �� Orientation$ 
Horizontal=�6   $��Center=�6.   $��0,10,0,0q�6K   .+�5      �� -   5         ��Name$ 
tb_TXCount��5   H   
�������$��Center=�6+   Payout TX Count:6o   5      &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ 	dg_TXData��5   �   $��Center=�6   $��Center=�60   $	��750��6K   $	��400��6W   $��0,15,0,0q�6d   
���   �
�������   
IsReadOnly .  � 	Resources 5      )  #System.Windows.Controls.DataGridRow �� 
TargetType �� Property �� Value:  4System.Windows.Controls.Primitives.DataGridRowHeader=  7System.Windows.Controls.Primitives.DataGridColumnHeader��%�   '        '  J     '  �     �� 5      "  6   .�5      ��  � ��
 �   �63   5      �� 5      "  6   .�5      ��  � ��
 �   �63   5      �� 5      "  6   .�5      ��  � ��
 �   �63   5      5      5      5      5      