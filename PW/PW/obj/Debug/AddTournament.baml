   M S B A M L   `   `   `  ����L  HPreiswattera_3000, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:Preiswattera_3000Preiswattera_3000  5      %    Preiswattera_3000.AddTournament   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      1localclr-namespace:Preiswattera_3000     5      -   5        
������ .��5      �   � ColumnDefinitions  5   
   �� 5      $	��750 �6    � RowDefinitions 5   
   �� 5      $��87 �6   �� 5      $	��169 �6   �� 5      $	��156 �6   �� 5      $	��190 �6   �� 5      $	��296 �6   .+�5   
   �� -   5         ��Name$ lbl_TnmtName��5      $2��,Legen Sie einen Namen für Ihr Turnier fest:��6&   $��0��6\   $��1��6l   $��
0,0,-379,0q�6y   $	��Top=�6�   $��67��6�   $��36(�6�   $��Center=�6�   $
��Left=�6�   $
��1130��6�   �� 5   
   -   5        $ lbl_TnmtRunCnt��5      $0��*Legen Sie die Anzahl der Durchgänge fest:��6(   $��0��6\   $��2��6l   $
��Left=�6y   $	��Top=�6�   $��64��6�   $
��1130��6�   $��36(�6�   $��
0,0,-379,0q�6�   $��Center=�6�   �� 5   
   -   5        $ lbl_TnmtGameCnt��5      $9��3Legen Sie die Anzahl der Spiele pro Durchgang fest:��6)   $��0��6g   $��3��6w   $
��Left=�6�   $	��Top=�6�   $��59��6�   $
��1130��6�   $��36(�6�   $��
0,0,-379,0q�6�   $��Center=�6�   �� 5   
   -   5         ��Name$ tbx_TnmtName��5      $��260,86,-129,0q�6(   $��1��6?    �� TextWrapping$
 Wrap=�6L   $�� ��6`   $	��620��6h   $
��Left=�6t   $	��Top=�6�   $��40��6�   $��24(�6�   $��Center=�6�   �� 5   
   -   5        $ tbx_TnmtRunCnt��5      $��260,84,-129,0q�6*   $��2��6A   $��0��6N   $
 Wrap=�6^   $�� ��6r   $	��620��6z   $
��Left=�6�   $	��Top=�6�   $��40��6�   $��24(�6�   $��Center=�6�   �� 5   
   -   5        $ tbx_TnmtGameCnt��5      $��260,82,-129,0q�6+   $��3��6B   $
 Wrap=�6O   $��3��6c   $	��Top=�6l   $��40��6�   $��24(�6�   $��Center=�6�   {�.  6�   �� 5   
   -   5         ��Name$ btn_StartTnmt��5         DefaultIAButton#��[  6(   $	��142��6Q   $��	367,0,0,0q�6^   $��4��6q   $��0��6~   $	��384��6�   $
��Left=�6�   $	��Top=�6�   $��48(�6�   .��5      �� $ ��Icons\createTnmt_Black.png��6   $	��152��68   $	��229��6E   5      5       5!      