   M S B A M L   `   `   `  ����U  QPreiswattera_3000_1366x768, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:Preiswattera_3000Preiswattera_3000  5   	   "    Preiswattera_3000.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:Preiswattera_3000     5   	   -   5          � Title$  Preiswattera 3000��5	   	   $	��700��6#   $
��1300��60    � 
ResizeMode$ CanMinimize=�5
   	    � WindowStyle$
 None=�6"    � AllowsTransparency . ��5      �� 5   
    �� EndPoint$ 0.5,1&�6    �� 
StartPoint$ 0.5,0&�6/   .��5      �  � Color$ 	#FFA8A8A8��6    � Offset$ 0.513Q�6-   � 5      $ 	#FF535353��6   $ 1Q�6-   � 5      $ 	#FFA8A7A7��6   5      .��5      � $
��Left=�6   $	��700��6&   $	��Top=�63   $
��1300��6K    � RowDefinitions 5   
   �� 5      $	��17* �6   �� 5      $
��383* �6   	 � ColumnDefinitions	 5   
   �� 5      $	��250 �6   �� 5      $
��1150 �6   .+�5   
   �� 
���2�2�$	��1.5q�5      
�������5      �� 5    
   $��Icons\cards.png��6   $��2��6)   
 J� Opacity$

 0.02Q�6:   $��1��6I   $��10,10,100,10q�6Y   �� 5!   
   -   5         ��Name$ MainContent��5!      $��1��6.   $��1��6>   $��10,0,110,18q�6K   ��5"      �� 5#      $ 0.5,1&�6&   $ 0.5,0&�67   .��5$      � $ Black��6#   $ 0Q�61   � 5%      $ White��6#   $ 1Q�61   5&      5(      ]� 5)   
   -   5         ]�Name$ ActionMenue��5)      $��0��6)   $��1��69   $y�Visible=�6F   $
��Auto��6[    J� OpacityMask �	#FFB9B0B0.+�5*      �� .+�5+      �� -   5         ��Name$ btn_GoToAddTeam��5+         ActionMenueButton#��[  62   $
��Left=�6]   $	��245��6x   $	��134��6�   {�. $��28(�6�   .��5,      �� -   5         ��Name$ 
addTeamImg��5,      $��Icons\addTeam_schrift.png��60   $	��124��6S   $	��235��6`   5-      �� 5.      -   5        $ btn_GoToShowTeam��5.      #��[  63   $
��Left=�6^   $	��245��6y   $	��134��6�   {�. $��28(�6�   .��5/      �� -   5        $ showTeamImg��5/      $ ��Icons\showTeam_schrift.png��61   $	��124��6U   $	��235��6b   50      �� 51      -   5        $ btn_GoToTnmtData��51      {�.    DisabledButton#��[ 6F   $
��Left=�6n   $	��245��6�   $	��134��6�   $��28(�6�   .��52      �� -	   5        $ insertGameImg��52      $"��Icons\insertGame_schrift.png��63   $	��124��6Y   $	��235��6f   $	
 0.5Q�6r   53      �� 54      -
   5        $ btn_GoToEvaluation��54      #��[  65   $
��Left=�6`   $	��245��6{   $	��134��6�   {�. $��28(�6�   .��55      �� -   5        $ evaImg��55      $��Icons\Eva_schrift.png��6,   $	��124��6K   $	��235��6X   56      �� 57      -   5        $ btn_GoTournamentMenue��57      #��[  68   $
��Left=�6c   $	��245��6~   $	��134��6�   {�. $��28(�6�   .��58      �� -   5        $ tmntInfoImg��58      $��Icons\TnmtInfo.png��61   $	��124��6M   $	��235��6Z   59      5:      5;      �� 5<   
   -   5         ��Name$ cnvs_PWHeader��5<      $y�Hidden=�6(   $��0,0,0,18q�6<   $��1��6N   .+�5=      �� -   5        $ btn_PWHeader��5=        ShowHeaderButtonStyle#��[ 6+   $	��250��6Z   $	��768��6f   .��5>      �� $��Icons\start.png��6   $	��184��61   $	��250��6>    �� Left$
 -126��6J   
 �� Top$
 -118��6]   5?      5@      �� 5A   
   -   5        $ cnvs_MessageBar��5A      $y�Hidden=�6*   $��0,0,0,18q�6>   $��1��6P   .+�5B      �� -   5         ��Name$ rec_Background��5B      ���	#FFB2B2B2$
��Left=�6A   $	��768��6\   
���   �$	��Top=�6x   $
��1400��6�   $��2��6�   $	
 0.8Q�6�   �� 5C      -   5        $ rec_Bar��5C      $
��Left=�6)   $	��412��6D   $	��Top=�6Q   $
��1580��6i   $��2��6v   $	 136��6�   
�������$	 -80��6�    J� Effect 5D      -  'System.Windows.Media.Effects.BlurEffect  5E         Radius$ 90Q�6!   5G      �� 5H      -   5        $ bar_ErroImg��5H      $y�Hidden=�6)   $	��100��6=   $ 78��6J   $	 296��6[   $	��100��6l   $��Icons\error-icon-4.png��6x   �� 5I      -   5        $ bar_InfoImg��5I      $y�Hidden=�6)   $	��100��6=   $ 78��6J   $	 296��6[   $	��100��6l   $+��%Icons\information-1481584_960_720.png��6x   �� 5J      -   5        $ bar_WarnImg��5J      $y�Hidden=�6)   $	��100��6=   $ 78��6J   $	 296��6[   $	��100��6l   $#��Icons\warning-icon-png-12.jpg��6x   �� 5K      -   5        $ 
bar_AskImg��5K      $y�Hidden=�6(   $	��100��6<   $ 78��6I   $	 296��6Z   $	��100��6k   $-��'Icons\question-mark-1750942_960_720.png��6w   �� 5L      -   5        $ bar_DebugImg��5L      $y�Hidden=�6*   $	��100��6>   $ 78��6K   $	 296��6\   $	��100��6m   $��Icons\debug_img.png��6y   �� 5M      -   5         ��Name$ lbl_oMessageHeader��5M      $��Message Typ��60   $��30(�6F   $��2��6T   $
��Left=�6h   $
��Left=�6�   $	��Top=�6�   $
��1138��6�   $	 250��6�   $	 241��6�   �� 5N      -   5        $ lbl_oMessageText��5N      $��Message Typ��6.   $��16(�6D   $
��Left=�6R   $
��Left=�6n   $	��Top=�6�   $
��1138��6�   $	��132��6�   $��1��6�   $	 250��6�   $	 296��6�   �� 5O      -   5        $ btn_Ok��5O      $��OK��6%     DefaultIAButton#��[ 63   $��1��6\   $
��Left=�6l   $	��Top=�6�   $��74��6�   $	 664��6�   $	 442��6�   6�   5P      �� 5Q   
   -   5        $ TitleBar��5Q      .+�5R      ��   titleBar#��[ 6   $
��Left=�6:   $��34��6U   $	��Top=�6a   $
��1300��6y   $��2��6�   �� 5S        titleBarWindowBorder#��[ 6   $
��Left=�6F   $	��665��6a   $	��Top=�6n   $��1��6�   $��2��6�   $ 34��6�   �� 5T      #��[ 6   $
��Left=�6F   $	��666��6a   $	��Top=�6n   $��1��6�   $��2��6�   $ 34��6�   $
 1299��6�   �� 5U      #��[ 6   $
��Left=�6F   $��1��6a   $	��Top=�6l   $
��1299��6�   $��2��6�   $	 699��6�    J� RenderTransformOrigin$ 0.5,0.5&�6�   �� 5V      $��Preiswattera 3000��6     MainWindowTitelbarLabel#��[ 60   $	��362��6a   $��36��6m   $ 57��6y   $ -2��6�   �� 5W      $��31��6   $��52��6    $��Icons\icon_little.png��6+   �� 5X      -   5        $ btn_MainWindowClose��5X        TitleBarCloseButton#��[ 62   $�� ��6_   $��1��6j   $��Center=�6z   $��Center=�6�   $
��Left=�6�   $
 1269��6  $ 3��6'  $��28��66  ��6�   Z� 66  �� 5Y      -   5        $ btn_MainWindowClose_Copy��5Y        TitleBarMinButton#��[ 67   $�� ��6b   $��1��6m   $��Center=�6}   $��Center=�6�   $	��Top=�6�   $
 1237��6  $ 3��6-  $ 0.966,0.621&�6<  $��28��6`  ��6  Z� 6`  �� 5Z      -   5        $ btn_QuickSettings��5Z       	 TitleBarSettingsButton#��[	 60   $��Center=�6`   $��Center=�6�   $	��Top=�6�   $
 1185��6�   $ 2��6	  $ 0.966,0.621&�6  $��28��6<  $��34��6H   K� ZIndex$ 10��6S  ��6�   Z� .��5[      �� $��Icons\settings_little.png��6   $��26��6;   $��34��6G   $ 0.5,0.5&�6R   5\      �� 5]      -   5        $ cnvs_debugModeActiv��5]      $y�Hidden=�62   .+�5^      �� $��Icons\debug_mode.png��6   $��34��66   $��49��6B   $	 581��6M   $ -1��6_   �� 5_      $��
DEBUG-MODE��6   $	 630��6-   $ -1��6?   $��20(�6O   5`      �� 5a      -    5        $ cnvs_QuickSettings��5a      $y�Hidden=�61   $��88��6E   $��73��6Q   $
 1145��6\   $ 31��6o   $ 1��6   .+�5b      �� -!   5        $! btn_BackgroundQuickSettings��5b       $
  TitleBarSettingsBackgroundButton#��[
 6>   $	��130��6x   $	��141��6�   $	 -67��6�   .��5c      �� .+�5d      �� -"   5        $ btn_ColorRed��5d   !   #��[	 67   $��24��6g   $	 -60��6s   $	 -62��6�   $	��119��6�   .��5e      �� .+�5f   "   �� $��Icons\colorSwitch.png��6(   $��26��6G   $��40��6S   $	 -58��6^   $	 -13��6p   �� 5g   "   $	��Rot��6(   $	 -17��66   $	 -14��6H   $��27��6Y   $��16(�6e   5h      5i      �� 5j      -#   5        $ btn_ColorGray��5j   !   #��[	 68   $��24��6h   $	 -60��6t   $	 -38��6�   $	��119��6�   .��5k      �� .+�5l   "   �� $��Icons\colorSwitch.png��6(   $��26��6G   $��40��6S   $	 -58��6^   $	 -13��6p   �� 5m   "   $
��Grau��6(   $	 -17��67   $	 -14��6I   $��27��6Z   $��16(�6f   5n      5o      �� 5p      -$   5        $ btn_ColorBlue��5p   !   #��[	 68   $��24��6h   $	 -60��6t   $	 -14��6�   $	��119��6�   .��5q      �� .+�5r   "   �� $��Icons\colorSwitch.png��6(   $��26��6G   $��40��6S   $	 -58��6^   $	 -13��6p   �� 5s   "   $
��Blau��6(   $	 -17��67   $	 -14��6I   $��27��6Z   $��16(�6f   5t      5u      �� 5v      -%   5        $ btn_ColorGreen��5v   !   #��[	 69   $��24��6i   $	 -60��6u   $ 10��6�   $	��119��6�   .��5w      �� .+�5x   "   �� $��Icons\colorSwitch.png��6(   $��26��6G   $��40��6S   $	 -58��6^   $	 -13��6p   �� 5y   "   $��Grün��6(   $	 -17��67   $	 -14��6I   $��27��6Z   $��16(�6f   5z      5{      �� 5|      -&   5        $ btn_Settings��5|   !   $��Einstellungen��67   #��[	 6O   $��24��6   $	 -60��6�   $ 34��6�   $	��119��6�   6�   5}      5~      5      �� 5�      -'   5        $ btn_QuickRun��5�      #��[	 6+   $��Center=�6[   $��Center=�6}   $	��Top=�6�   $
 1145��6�   $ 2��6�   $ 0.966,0.621&�6  $��28��62  $��34��6>  $ 10��6I  ��6�   Z� .��5�      �� $��Icons\jump.png��6   $��26��60   $��24��6<   $ 0.5,0.5&�6G   5�      5�      �� 5�   
   -(   5        $ Saver��5�      $y�Hidden=�6    $��1��64   $��1��6D   $��	0,0,110,0q�6Q   .+�5�      �� -)   5        $ rec_BackgroundSaver��5�      
��� � �$
��Left=�6A   $	��262��6\   
���   �$	��Top=�6x   $
��1060��6�   $��2��6�   $	
 0.5Q�6�   $	 210��6�   $ -2��6�    5�        5�      $	 150Q�6!   5�      �� 5�      $	��203��6   $��Center=�6!   $��Center=�6>   $��Icons\save.png��6Y   $	 334��6q   $	 249��6�   $	��382��6�   5�      �� 5�   
   -*   5        $ Waiter��5�      $y�Hidden=�6!   $��1��65   $��1��6E   $��	0,0,110,0q�6R   .+�5�      �� -+   5        $ rec_BackgroundWaiter��5�      ���	#FF2889EA$
��Left=�6G   $	��262��6b   
���   �$	��Top=�6~   $
��1060��6�   $��2��6�   $	
 0.5Q�6�   $	 210��6�   $ -2��6�    5�        5�      $	 150Q�6!   5�      �� 5�      $	��203��6   $��Center=�6!   $��Center=�6>   $��Icons\wait.png��6Y   $	 342��6q   $	 237��6�   $	��382��6�   5�      5�      5�      