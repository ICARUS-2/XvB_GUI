   M S B A M L   `   `   `  ����B  >XVB_GUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:XVB_GUIXVB_GUI  5   	       XVB_GUI.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:XVB_GUI     5   	     � Title$  XvB_GUI��5   	   $	��768��6   $
��1366��6&   
���   �$��15(�5
   	   .��5      �  � ColumnDefinitions 5   
   �� 5      6!   �� 5      6!    � RowDefinitions 5   
   �� 5      $��20 �6   6*   �� 5      $	��105 �6   6+   �� 5      $	��560 �6   6+   �� 5      $��30 �6   6*   .+�5   
   �� $	��Top=�6   $��0��6-    �� Orientation$ 
Horizontal=�6:   $��2��6S   .+�5      �� 
�������CONNECTION STATUS:6?   �� 5      -   5         ��Name$ tb_ConnectionStatus��5   =   
�������$��20,0,0,0q�6+   	PENDING6c   5      � 5   
   $	��Top=�6   $��1��6'   $��0��64   $��2��6D    5      �� 5       6"    5#      �� 5$      6%   �� 5%      6%   �� 5&      6%   �� 5'      6%   �� 5(      6%   .+�5+      �� $��0��6   $��0��6%   
����  �$��2q�6I   $��5q�6]   ._�5,      �� .+�5-      �� $	��Top=�6    $��Center=�68   
���   �
�������$��0,0,0,10q�6{   Network Hashrate6�   �� 5.      -   5        $ tb_NetworkHashrate��5.   k   $��Center=�6    
�������$��Center=�6N   	PENDING6�   5/      50      �� 52      $��1��6   $��0��6%   
����  �$��2q�6I   $��5q�6]   ._�53      �� .+�54      �� $	��Top=�6    $��Center=�68   
���   �
�������$��0,0,0,10q�6{   Pool Hashrate6�   �� 55      -   5        $ tb_PoolHashrate��55   k   $��Center=�6    
�������$��Center=�6N   	PENDING6�   56      57      �� 59      $��2��6   $��0��6%   
����  �$��2q�6I   $��5q�6]   ._�5:      �� .+�5;      �� $	��Top=�6    $��Center=�68   
���   �
�������$��0,0,0,10q�6{   Boost Hashrate6�   �� 5<      -   5        $ tb_BoostHashrate��5<   k   $��Center=�6    $��Center=�6;   
�������	PENDING6�   �� 5=      -   5        $ tb_BoostAddress��5=   k   $��Bottom=�6    $��Center=�6;   
�������	PENDING6�   �� 5>      -   5        $ tb_BoostAddressTime��5>   k   $��Bottom=�6    $��Center=�6;   
�������	PENDING6�   5?      5@      �� 5B      $��3��6   $��0��6%   
����  �$��2q�6I   $��5q�6]   ._�5C      �� .+�5D      �� $	��Top=�6    $��Center=�68   
���   �
�������$��0,0,0,10q�6{   Raffle Hashrate6�   �� 5E      -   5        $ tb_BonusHashrate��5E   k   $��Center=�6    $��Center=�6;   
�������	PENDING6�   5F      5G      �� 5I      $��4��6   $��0��6%   
����  �$��2q�6I   $��5q�6]   ._�5J      �� 
���   �.+�5K      �� $	��Top=�6    $��Center=�68   
���   �
�������$��0,0,0,10q�6{   T/Raffle Hashrate6�   �� 5L      -   5        $ tb_TimeBonusHashrate��5L   k   $��Center=�6    $��Center=�6;   
�������	PENDING6�   �� 5M      -	   5        $ tb_TimeBonusAddress��5M   k   $��Bottom=�6    $��Center=�6;   
�������	PENDING6�   �� 5N      -
   5        $ tb_TimeBonusTime��5N   k   $��Bottom=�6    $��Center=�6;   
�������	PENDING6�   5O      5P      5Q      � 5U   
   $��2��6    5V      �� 5W      6%   .+�5[      �� $��0��6   .+�5]      �� -   5         ��Name$ 	sp_A1Data��5]      .+�5^      �� 
����  �$��2q�64   $��5,3,3,3q�6H   ._�5_      �� $ 
Horizontal=�6%   .+�5a      �� .+�5b   "   �� 
�������$��0,0,0,5q�6?   	Address 16[   �� 5c   "   
�������$��0,0,0,5q�6?   Miner Hashrate6`   �� 5d   "   
�������$��0,0,0,5q�6?   Balance Due6]   �� 5e   "   
�������$��0,0,0,5q�6?   Active Miners6_   �� 5f   "   
�������$��0,0,0,5q�6?   Payout Count6^   �� 5g   "   
�������$��0,0,0,5q�6?   	Boost ETA6[   �� 5h   "   -   5         ��Name$ btn_Address1Edit��5h   w   
�������
����  �
���   �$��2,2,2,2q�6f   .��6�   Edit Address6�   5i      �� 5l      $��30,0,0,0q�6)   .+�5m   "   �� -   5        $ tb_Address1��5m   ?   
�������$��0,0,0,5q�6T   -6h   �� 5n   "   -   5        $ tb_Address1Hashrate��5n   ?   
�������$��0,0,0,5q�6\   -6p   �� 5o   "   $ 
Horizontal=�6-   $��0,0,0,5q�6F   .+�5p   &   �� -   5        $ tb_Address1BalanceNumber��5p   C   
�������-6h   �� 5q   &   -   5        $" tb_Address1BalanceBarCurrent��5q   U   
�������$��15,0,0,0q�6C   6}   �� 5r   &   -   5        $$ tb_Address1BalanceBarRemaining��5r   C   
�������6m   5s   #   �� 5t   "   $ 
Horizontal=�6-   $��0,0,0,5q�6F   .+�5u   &   �� -   5        $ tb_Address1ActiveMiners��5u   C   
�������-6g   5v   #   �� 5w   "   -   5        $ tb_Address1PayoutCount��5w   ?   
�������$��0,0,0,5q�6_   -6s   �� 5x   "   -   5        $ tb_Address1BoostETA��5x   ?   
�������$��0,0,0,5q�6\   -6p   �� 5y   "   $ 
Horizontal=�6-   .+�5z   &   �� -   5        $ btn_Address1TXReport��5z   {   
�������
����  �
���   �$��2,2,2,2q�6j   .��6�   Transaction Report6�   �� 5{   &   -   5        $ btn_Address1BoostReport��5{   |   
�������
����  �
���   �$��10,2,2,2q�6j   .��6�   Boost Report6�   �� 5|   &   -   5        $ btn_Address1Remove��5|   |   
�������
����  �
���   �$��10,2,2,2q�6j   .��6�   Remove Address6�   5}   #   5~      5      5�      5�      �� 5�      -   5        $ 	sp_A2Data��5�   -   $��0��6   .+�5�      �� 
����  �$��2q�64   $��5,3,3,3q�6H   ._�5�      �� $ 
Horizontal=�6%   .+�5�      �� .+�5�   "   �� 
�������$��0,0,0,5q�6?   	Address 26[   �� 5�   "   
�������$��0,0,0,5q�6?   Miner Hashrate6`   �� 5�   "   
�������$��0,0,0,5q�6?   Balance Due6]   �� 5�   "   
�������$��0,0,0,5q�6?   Active Miners6_   �� 5�   "   
�������$��0,0,0,5q�6?   Payout Count6^   �� 5�   "   
�������$��0,0,0,5q�6?   	Boost ETA6[   �� 5�   "   -   5        $ btn_Address2Edit��5�   w   
�������
����  �
���   �$��2,2,2,2q�6f   .��6�   Edit Address6�   5�      �� 5�      $��30,0,0,0q�6)   .+�5�   "   �� -   5        $ tb_Address2��5�   ?   
�������$��0,0,0,5q�6T   -6h   �� 5�   "   -   5        $ tb_Address2Hashrate��5�   ?   
�������$��0,0,0,5q�6\   -6p   �� 5�   "   $ 
Horizontal=�6-   $��0,0,0,5q�6F   .+�5�   &   �� -   5        $ tb_Address2BalanceNumber��5�   C   
�������-6h   �� 5�   &   -   5        $" tb_Address2BalanceBarCurrent��5�   U   
�������$��15,0,0,0q�6C   6}   �� 5�   &   -   5        $$ tb_Address2BalanceBarRemaining��5�   C   
�������6m   5�   #   �� 5�   "   $ 
Horizontal=�6-   $��0,0,0,5q�6F   .+�5�   &   �� -   5        $ tb_Address2ActiveMiners��5�   C   
�������-6g   5�   #   �� 5�   "   -    5        $ tb_Address2PayoutCount��5�   ?   
�������$��0,0,0,5q�6_   -6s   �� 5�   "   -!   5        $ tb_Address2BoostETA��5�   ?   
�������$��0,0,0,5q�6\   -6p   �� 5�   "   $ 
Horizontal=�6-   .+�5�   &   �� -"   5        $ btn_Address2TXReport��5�   {   
�������
����  �
���   �$��2,2,2,2q�6j   .��6�   Transaction Report6�   �� 5�   &   -#   5        $ btn_Address2BoostReport��5�   |   
�������
����  �
���   �$��10,2,2,2q�6j   .��6�   Boost Report6�   �� 5�   &   -$   5        $ btn_Address2Remove��5�   |   
�������
����  �
���   �$��10,2,2,2q�6j   .��6�   Remove Address6�   5�   #   5�      5�      5�      5�      �� 5�      -%   5        $ 	sp_A3Data��5�   -   $��0��6   .+�5�      �� 
����  �$��2q�64   $��5,3,3,3q�6H   ._�5�      �� $ 
Horizontal=�6%   .+�5�      �� .+�5�   "   �� 
�������$��0,0,0,5q�6?   	Address 36[   �� 5�   "   
�������$��0,0,0,5q�6?   Miner Hashrate6`   �� 5�   "   
�������$��0,0,0,5q�6?   Balance Due6]   �� 5�   "   
�������$��0,0,0,5q�6?   Active Miners6_   �� 5�   "   
�������$��0,0,0,5q�6?   Payout Count6^   �� 5�   "   
�������$��0,0,0,5q�6?   	Boost ETA6[   �� 5�   "   -&   5        $ btn_Address3Edit��5�   w   
�������
����  �
���   �$��2,2,2,2q�6f   .��6�   Edit Address6�   5�      �� 5�      $��30,0,0,0q�6)   .+�5�   "   �� -'   5        $ tb_Address3��5�   ?   
�������$��0,0,0,5q�6T   -6h   �� 5�   "   -(   5        $ tb_Address3Hashrate��5�   ?   
�������$��0,0,0,5q�6\   -6p   �� 5�   "   $ 
Horizontal=�6-   $��0,0,0,5q�6F   .+�5�   &   �� -)   5        $ tb_Address3BalanceNumber��5�   C   
�������-6h   �� 5�   &   -*   5        $" tb_Address3BalanceBarCurrent��5�   U   
�������$��15,0,0,0q�6C   6}   �� 5�   &   -+   5        $$ tb_Address3BalanceBarRemaining��5�   C   
�������6m   5�   #   �� 5�   "   $ 
Horizontal=�6-   $��0,0,0,5q�6F   .+�5�   &   �� -,   5        $ tb_Address3ActiveMiners��5�   C   
�������-6g   5�   #   �� 5�   "   --   5        $ tb_Address3PayoutCount��5�   ?   
�������$��0,0,0,5q�6_   -6s   �� 5�   "   -.   5        $ tb_Address3BoostETA��5�   ?   
�������$��0,0,0,5q�6\   -6p   �� 5�   "   $ 
Horizontal=�6-   .+�5�   &   �� -/   5        $ btn_Address3TXReport��5�   {   
�������
����  �
���   �$��2,2,2,2q�6j   .��6�   Transaction Report6�   �� 5�   &   -0   5        $ btn_Address3BoostReport��5�   |   
�������
����  �
���   �$��10,2,2,2q�6j   .��6�   Boost Report6�   �� 5�   &   -1   5        $ btn_Address3Remove��5�   |   
�������
����  �
���   �$��10,2,2,2q�6j   .��6�   Remove Address6�   5�   #   5�      5�      5�      5�      5�      5�      �� 5�   
   
����  �$��2q�6(   $��2��6=   $��1��6J   $��5,3,5,0q�6Z   $	��557��6k   ._�5�   
   � $��	0,8,0,-15q�6    5�      �� 5�      6"   �� 5�      6"   �� 5�      6"    5�      �� 5�      6)   �� 5�      6)   �� 5�      6)   .+�5�      �� $��0��6   $��0��6*   .+�5�      �� 
�������$��Center=�63   Exchange Rates6`   �� 5�      -2   5        $ tb_FiatCurrency��5�   }   
�������$��Center=�63   $��Bottom=�6P   $��0,15,0,0q�6k   	PENDING6�   �� 5�      -3   5        $ tb_BTCCurrency��5�   }   
�������$��Center=�63   $��Bottom=�6P   $��0,15,0,0q�6k   	PENDING6�   �� 5�      -4   5        $ tb_ETHCurrency��5�   }   
�������$��Center=�63   $��Bottom=�6P   $��0,15,0,0q�6k   	PENDING6�   5�      �� 5�      $��0��6   $��1��6*   .+�5�      �� 
�������$��Center=�63   Active Miners6_   �� 5�      -5   5        $ tb_ActiveMiners��5�   b   
�������$��Center=�63   $��0,15,0,0q�6P   	PENDING6�   �� 5�      
�������$��Center=�63   $��0,15,0,0q�6P   Avg HR/Miner6p   �� 5�      -6   5        $ tb_AvgHRPerMiner��5�   b   
�������$��Center=�63   $��0,15,0,0q�6P   	PENDING6�   5�      �� 5�      $��0��6   $��2��6*   .+�5�      �� 
�������$��Center=�63   Blocks Mined6^   �� 5�      -7   5        $ tb_BlocksFoundByPool��5�   b   
�������$��Center=�63   $��0,15,0,0q�6P   	PENDING6�   �� 5�      
�������$��Center=�63   $��0,15,0,0q�6P   Blockchain Height6u   �� 5�      -8   5        $ tb_BlockchainHeight��5�   b   
�������$��Center=�63   $��0,15,0,0q�6P   	PENDING6�   5�      �� 5�      -9   5        $ tb_BlockDataHeader��5�   t   $��1��6   $��0��6)   $��3��69   
�������$��5,0,0,0q�6c   	PENDING6�   &   System.Windows.Controls.DataGrid  5�      -:   5          Name$ dg_BlockData��5�   p   $��1��6   $��0��6(   $��2��68   $��3��6I   $��	5,20,5,20q�6]   
�������
���   �
����  �$   HorizontalScrollBarVisibility$ Disabled=�6�   "	   VerticalScrollBarVisibility$	 Disabled=�6�   
   
IsReadOnly
 .  � 	Resources 5�      )  #System.Windows.Controls.DataGridRow �� 
TargetType �� Property �� Value:  4System.Windows.Controls.Primitives.DataGridRowHeader=  7System.Windows.Controls.Primitives.DataGridColumnHeader��%�   '        '  J     '  �     �� 5�      "  6    .�5�      ��  � ��
 �   �6;   5�      �� 5�      "  6    .�5�      ��  � ��
 �   �6;   5      �� 5     "  6    .�5     ��  � ��
 �   �6;   5     5     5     5     5     �� 5
  
   $��3��6   $��1��6"   $ 
Horizontal=�62   $��Right=�6K   $��30��6g   $��0,0,0,0q�6s   .+�5     �� -;   5        $ btn_Settings��5     
���   �
�������$��0,0,15,0q�6Q   $	��100��6c   
����  �$��2q�6�   .��6�   
Settings6�   �� 5     -<   5        $ btn_DevInfo��5     
���   �
�������
����  �$��2q�6g   $��0,0,5,0q�6{   .��6�   
Dev info6�   5     5     5     