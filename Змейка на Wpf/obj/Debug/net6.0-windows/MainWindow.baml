   M S B A M L   `   `   `  ����P  LЗмейка на Wpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:Змейка_на_WpfЗмейка_на_Wpf  5   	   &     Змейка_на_Wpf.MainWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   5local#clr-namespace:Змейка_на_Wpf     5   	   -   5          � Title$  snake��5   	   $	��500��6   $	��800��6$   $	��300��60   $	��300��6?      BackgroundColor#��[  6O     	TextColor#��[ 6}     MainFont#��[ 6�    � WindowStartupLocation$ CenterScreen=�6�   .��5	      (� .��5
   
   �  � RowDefinitions 5      �� 5      $
��auto �6    �� 5      $��* �6    .+�5      �� -   5         ��Name$ 	ScoreText��5      $��Right=�6+   $��Center=�6G   $��SCORE 0��6b   $��22(�6q   $��10q�6   �� 5      -   5         ��Name$ 
GridBorder��5      $��1��6)     GridLineColor#��[ 66   $	��1.5q�6c   $��Center=�6y   $��Center=�6�    �� EdgeMode$ Aliased=�6�   ._�5      A� -   5         A�Name$ GameGrid��5      $	��400��60   $	��400��6<     GridBackgroundColor#��[ 6I    J� SnapsToDevicePixels$
 True��6{    �� BitmapScalingMode$ HighQuality=�6�   $��
20,0,20,20q�6�   5      �� 5      -   5        $ Overlay��5      $��1��6&     OverlayColor#��[ 63   $ Aliased=�6  ��6^   �� 	 �� Path$	 ActualWidth�
 �� ElementName$
 
GridBorder����6�   �� $	 ActualHeight�$
 
GridBorder����6�   �� $	 Margin�$
 
GridBorder��._�5      �� -   5        $ OverlayText��5      $��PRESS ANY KEY TO START��61   $��26(�6O   $��Center=�6]   $��Center=�6z   $
��Wrap=�6�   5      �� 5      -   5        $ Pause��5      $��1��6$   #��[ 61   $ Aliased=�6  ��6\   �� $	 ActualWidth�$
 
GridBorder����6�   �� $	 ActualHeight�$
 
GridBorder����6�   �� $	 Margin�$
 
GridBorder��._�5      �� -   5        $ 	PauseText��5      $��PRESS ESCAPE TO CONTINUE��6/   $��26(�6O   $��Center=�6]   $��Center=�6z   $
��Wrap=�6�   5      �� 5      -	   5        $ BestText��5      $
��Left=�6*   $��Center=�6E   $��BEST 0��6`   $��22(�6n   $��25,0,0,0q�6|   5      5      5      