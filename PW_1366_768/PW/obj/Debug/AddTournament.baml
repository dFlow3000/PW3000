   M S B A M L   `   `   `  ����U  QPreiswattera_3000_1366x768, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:Preiswattera_3000Preiswattera_3000  5      %    Preiswattera_3000.AddTournament   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      1localclr-namespace:Preiswattera_3000     5      -   5        
������ .��5      �   � ColumnDefinitions  5   
   �� 5      $	��750 �6    � RowDefinitions 5   
   �� 5      $��87 �6   �� 5      $	��169 �6   �� 5      $	��156 �6   �� 5      $	��119 �6   �� 5      $	��367 �6   .+�5   
   �� -   5         ��Name$ lbl_TnmtName��5      $2��,Legen Sie einen Namen für Ihr Turnier fest:��6&   $��0��6\   $��1��6l   $��
0,0,-279,0q�6y   $	��Top=�6�   $��67��6�   $��36(�6�   $��Center=�6�   $
��Left=�6�   $
��1030��6�   �� 5   
   -   5        $ lbl_TnmtRunCnt��5      $0��*Legen Sie die Anzahl der Durchgänge fest:��6(   $��0��6\   $��2��6l   $
��Left=�6y   $	��Top=�6�   $��64��6�   $
��1030��6�   $��36(�6�   $��
0,0,-279,0q�6�   $��Center=�6�   �� 5   
   -   5        $ lbl_TnmtGameCnt��5      $9��3Legen Sie die Anzahl der Spiele pro Durchgang fest:��6)   $��0��6g   $��3��6w   $
��Left=�6�   $	��Top=�6�   $��59��6�   $
��1020��6�   $��36(�6�   $��
0,0,-269,0q�6�   $��Center=�6�   �� 5   
   -   5         ��Name$ tbx_TnmtName��5      $��202,72,-71,0q�6(   $��1��6>    �� TextWrapping$
 Wrap=�6K   $�� ��6_   $	��620��6g   $
��Left=�6s   $	��Top=�6�   $��40��6�   $��24(�6�   $��Center=�6�   �� 5   
   -   5        $ tbx_TnmtRunCnt��5      $��202,69,-71,0q�6*   $��2��6@   $��0��6M   $
 Wrap=�6]   $�� ��6q   $	��620��6y   $
��Left=�6�   $	��Top=�6�   $��40��6�   $��24(�6�   $��Center=�6�   �� 5   
   -   5        $ tbx_TnmtGameCnt��5      $��202,64,-71,0q�6+   $��3��6A   $
 Wrap=�6N   $��3��6b   $	��Top=�6k   $��40��6�   $��24(�6�   $��Center=�6�   {�.  6�   �� 5   
   -   5         ��Name$ btn_StartTnmt��5         DefaultIAButton#��[  6(   $��94��6Q   $��
351,17,0,0q�6]   $��4��6q   $��0��6~   $	��326��6�   $
��Left=�6�   $	��Top=�6�   $��48(�6�   .��5      �� $ ��Icons\createTnmt_Black.png��6   $	��102��68   $	��219��6E    J� RenderTransformOrigin$ 0.5,0.5&�6Q   5      5       5!      