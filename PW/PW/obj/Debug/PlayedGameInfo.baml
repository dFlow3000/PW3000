   M S B A M L   `   `   `  ����L  HPreiswattera_3000, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:Preiswattera_3000Preiswattera_3000  5   	   &     Preiswattera_3000.PlayedGameInfo   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:Preiswattera_3000     5   	   -   5          � WindowStyle$
  None=�5	   	    � 
ResizeMode$ NoResize=�5
   	    � WindowStartupLocation$ CenterScreen=�5   	    � Title$ Spiel-Information��5   	   $��299.199��6#   $��488.617��64   ��5      �� 5   
    �� EndPoint$ 0.5,1&�6    �� 
StartPoint$ 0.5,0&�6/   .��5      �  � Color$ 	#FFE04040��6    � Offset$ 0.012Q�6-   � 5      $ 	#FFAA0303��6   $ 1Q�6-   5      .��5      �  � RowDefinitions 5   
   �� 5      	 � ColumnDefinitions	 5   
   �� 5      $	��556 �6   .+�5   
   �� -   5        
 ��Name$
 
lbl_oTeam1��5      $��Team1��6$   $
��Left=�64   $��	10,68,0,0q�6O   $	��Top=�6b   $	��189��6z   $��Right=�6�   $��36��6�   $
��Bold"�6�   $��18(�6�   �� 5   
   -   5        $
 
lbl_oTeam2��5      $��Team2��6$   $
��Left=�64   $��
281,69,0,0q�6O   $	��Top=�6c   $	��190��6{   $
��Left=�6�   $��36��6�   $
��Bold"�6�   $��18(�6�   �� 5   
   -   5        $
 lbl_sVs��5      $��gegen��6!   $
��Left=�61   $��
204,69,0,0q�6L   $	��Top=�6`   $��Center=�6x   $��72��6�   $��36��6�   $
��Bold"�6�   $��Italic$�6�   $��18(�6�   �� 5   
   ���	#FFF4F4F5$
��Left=�6%   $��24��6@   $��239,101,0,0q�6L   
���   �$	��Top=�6p   $��1��6�   �� 5   
   -   5        $
 lbl_sWinOrLost��5      $�� ��6(   $
��Left=�63   $��
10,139,0,0q�6N   $	��Top=�6b   $	��461��6z   $��Center=�6�   $��20(�6�   $
��Bold"�6�   �� 5   
   -   5        $
 lbl_oGamePointsTeam1��5      $�� ��6.   $
��Left=�69   $��144,188,0,0q�6T   $	��Top=�6i   $��75��6�   $��Center=�6�   $��24(�6�   $
��Bold"�6�   �� 5    
   -   5        $
 lbl_oGamePointsTeam2��5       $�� ��6.   $
��Left=�69   $��260,188,0,0q�6T   $	��Top=�6i   $��75��6�   $��Center=�6�   $��24(�6�   $
��Bold"�6�   �� 5!   
   ���	#FFF4F4F5$
��Left=�6%   $��53��6@   $��239,186,0,0q�6L   
���   �$	��Top=�6p   $��1��6�   �� 5"   
   -   5         ��Name$ btn_ClosePlayedGameInfo��5"         DefaultIAButton#��[  62   $��16(�6[   $��
Schließen��6i   $
��Left=�6}   $��183,248,0,0q�6�   $	��Top=�6�   $	��116��6�   $��38��6�   6�   �� 5#   
   -	   5        $
 lbl_oGameInfo��5#      $�� ��6'   $
��Left=�62   $��0,36,0,0q�6M   $	��Top=�6_   $	��489��6w   $��Center=�6�   $
��Bold"�6�   $��20(�6�   �� 5$   
   -
   5         ��Name$ TitleBar��5$      .+�5%      ��   titleBar#��[ 6   $
��Left=�6:   $��34��6U   $	��Top=�6a   $	��489��6y   $��2��6�   �� 5&      $��
Spiel-Info��6     ExtraWindowTitelbarLabel#��[ 6)   $	��362��6[   $��34��6g    �� Left$ 42��6s   �� 5'      $��34��6   $��74��6    $��Icons\Lupe.png��6+   $	 -17��6C   �� 5(      -   5        $ btn_MainWindowClose��5(        TitleBarCloseButton#��[ 62   $��X��6_   $��1��6k   $��Center=�6{   $��Center=�6�   $
��Left=�6�   $��37��6�   $	 452��6   $��34��62  ��6	  Z� 62  �� 5)      -   5        $ btn_MainWindowClose_Copy��5)        TitleBarMinButton#��[ 67   $��_��6b   $��1��6n   $��Center=�6~   $��Center=�6�   $	��Top=�6�   $��34��6�   $	 409��6&  $��36��68  ��6  Z� 68  5*      5+      5,      