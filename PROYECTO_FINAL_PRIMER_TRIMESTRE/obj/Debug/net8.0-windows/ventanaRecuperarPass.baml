   M S B A M L   `   `   `  ����#  PROYECTO_FINAL_PRIMER_TRIMESTRE:    4PROYECTO_FINAL_PRIMER_TRIMESTRE.ventanaRecuperarPass   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Recuperar Contraseña��5   	   $	��500��6&   $	��400��63    � WindowStartupLocation$ CenterScreen=�6?   ��5      �� 5   
    �� 
StartPoint$	 0,0&�6    �� EndPoint$	 1,1&�6/   .��5      �  � Color$ #F3E2A9��6    � Offset$	 0.0Q�6+   � 5      $ #FFF9E6��6   $	 1.0Q�6+   5	       � 	Resources 5      3  -System.Windows.Media.Effects.DropShadowEffect   ShadowEffect   
BlurRadius   ShadowDepth	   Opacity
   Color �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius �� Triggers P� Property P� Value��%D  &	        '�� I     7     5   
   $ 10Q�60   $ 5Q�6@   $		 0.5Q�6P   $
 Black��6^   �� 5   
   " ��6   .�5      ��  � ��$ 10,0,0,0q�6'   �� 5       � ��$ 5q�6(   �� 5       � ��$ 14(�6)   �� 5       � ��
 �����6+   �� 5       �@J�Effect8   6'   �� 5       �@�Cursor$
 Hand��6'   �� 5       � ��$ 0q�60   �� 5       � �� 5      �� 5      $	 0,0&�6*   $	 0,1&�6;   .��5      � $ #7B68EE��6'   $	 0.0Q�67   � 5      $ #48D1CC��6'   $	 1.0Q�67   5      5      �� 5        � �� 5!      �� 5"      " ��6&   .R�5#      �� #��z��6!   $ 5��6K   ._�5$      �� $��Center=�6/   $��Center=�6L   5%       5&      P� 5'       �@J�IsMouseOver$
 True��6=   . �5(   "   ��  � �� 5)   &   �� 5*   *   $	 0,0&�6>   $	 0,1&�6O   .��5+   .   � $ Orange��6;   $	 0.0Q�6J   � 5,   .   $ SaddleBrown��6;   $	 1.0Q�6O   5-   +   5/   #   �� 50   "    � ��
 �   �6?   51      53      55      56      57      .��59      (�  (� Stretch$ Uniform=�6   .��5:   
   � $��20q�6    � RowDefinitions 5;      �� 5<      $
��Auto �6    �� 5=      $
��Auto �6    �� 5>      $
��Auto �6    �� 5?      $
��Auto �6    �� 5@      $
��Auto �6    .+�5D      �� $��Recuperar contraseña��6   $��24(�64   $
��Bold"�6B   
���   �$��0,0,0,20q�6g   $��0��6y    �� TextAlignment$ Center=�6�   �� 5G       �� Orientation$ 
Horizontal=�6   $��1��62   $��Center=�6?   $��0,0,0,20q�6\   .+�5H      	� -   5         	�Name$ radioClaveUnica��5H      $��Usar clave secreta��67    d� 	IsChecked$
 TrueY�6T   $
��10,0q�6e   6s   	� 5I      -   5        $ radioCorreoElectronico��5I      $��Usar correo electrónico��6>   $
��10,0q�6`   6n   5J      �� 5M      -   5         ��Name$ seccionClaveUnica��5M      $��2��64   $y�Visible=�6A   .+�5N      �� $!��Introduzca su clave secreta��6   $��16(�6?   
���   �$��0,0,0,5q�6`   �� 5O      -   5         ��Name$ cajaClaveUnica��5O      $��40��62   $��16(�6>   �� 5P      -   5        $��Recuperar con clave��5P      $	��200��67   $��40��6C   $��Center=�6v   $��0,10,0,0q�6�   5Q      �� 5T      -   5        $ seccionCorreoElectronico��5T      $��3��6;   $y�	Collapsed=�6H   .+�5U      �� $'��!Introduzca su correo electrónico��6   $��16(�6D   
���   �$��0,0,0,5q�6e   �� 5V      -   5        $ cajaCorreoElectronico��5V      $��40��69   $��16(�6E   �� 5W      -   5        $$��Enviar correo de recuperación��5W      $	��250��6A   $��40��6M   $��Center=�6y   $��0,10,0,0q�6�   5X      �� 5[      -	   5        $��Volver��5[      $��4��6&   $	��100��63   $��40��6?   $��Center=�6e   $��0,30,0,0q�6�   5\      5]      5^      