   M S B A M L   `   `   `  ����F  BCarcassonne, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:CarcassonneCarcassonne  5   	        Carcassonne.CarcassoneGame   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:Carcassonne     5   	     � Title$  Carcassonne��5   	   $	��768��6   $
��1280��6*   .��5	      � -   5         �Name$
 grid��5	      
��� ���.+�5   
   A� -   5        $��ugTabla��5      
������� J� Opacity$	 0.7Q�67   $�� 10 10 790 10 q�6E   �� 5   
   -   5         ��Name$ rctLeforditottKartya��5      
���   �$��Right=�6A   $��Center=�6]   $�� 0 -360 415 0 q�6x   $	��220��6�   $	��220��6�   $��4��6�   �� 5   
   -   5        $ rctFelforditottKartya��5      
���   �$��Right=�6B   $��Center=�6^   $�� 0 150 415 0 q�6y   $	��220��6�   $	��220��6�   $��4��6�   �� 5   
   -   5        $ rctFelforditottKartyaFent��5      
���   �$��Right=�6F   $	��Top=�6b   $�� 0 63 407 0 q�6z   $	��220��6�   $	��220��6�   $��4��6�   �� 5   
   -   5        $  rctFelforditottKartyaKozep��5      
���   �$��Right=�6G   $	��Top=�6c   $�� 0 48 399 0 q�6{   $	��220��6�   $	��220��6�   $��4��6�   �� 5   
   -   5         ��Name$ btnStartGame��5      $��Kezdés��6'   $��40(�68   $��DemiBold"�6F   $�� 145 0 0 0 q�6\   $
��Left=�6q   $��Center=�6�   $	��200��6�   $��80��6�   6�   �� 5   
   -   5        $ rctLine1��5      
�������$��60��65   $��4��6@   
�������$��Right=�6X   $	��Top=�6t   $��
 0 32 20 0q�6�   �� 5   
   -	   5        $ rctLine2��5      
�������$��70��66   $��4��6A   
�������$��Right=�6Y   $	��Top=�6u   $��
 0 48 15 0q�6�   �� 5   
   -
   5        $ rctLine3��5      
�������$��60��66   $��4��6A   
�������$��Right=�6Y   $	��Top=�6u   $��
 0 65 20 0q�6�   A� 5   
   -   5         A�Name$ 	eclCircle��5      $��70��6%   $��70��60   
�������$��4��6K   $��Right=�6_   $	��Top=�6{   $��
 0 15 15 0q�6�   �� 5   
   -   5        $ btnExpander��5      $��70��6&   $��70��61   $�� 0 15 15 0 q�6>   $��Right=�6S   $	��Top=�6o   $ 0Q�6�   6�   �� 5   
   -   5        $ btnSave��5      $y�Hidden=�6"   $��Mentés��67   $��17(�6H   $
��Bold"�6V   $	��100��6h   $��50��6t   $��Right=�6�   $	��Top=�6�   $�� 0 110 20 0 q�6�   
���   �$��2q�6�   6�   �� 5    
   -   5        $ btnExit��5       $y�Hidden=�6"   $��	Kilépés��66   $��17(�6H   $
��Bold"�6V   $	��100��6h   $��50��6t   $��Right=�6�   $	��Top=�6�   $�� 0 170 20 0 q�6�   
���   �$��2q�6�   6�   �� 5!   
   -   5        $ btnLoad��5!      $y�Hidden=�6"   $��
Betöltés��67   $��17(�6J   $
��Bold"�6X   $	��100��6j   $��50��6v   $��Right=�6�   $	��Top=�6�   $��
0 230 20 0q�6�   
���   �$��2q�6�   6�   �� 5"   
   -   5        $ 	btnFinish��5"      $y�Hidden=�6$   $��
Befejezés��68   $��17(�6L   $
��Bold"�6Z   $	��100��6l   $��50��6x   $��Right=�6�   $	��Top=�6�   $��
0 290 20 0q�6�   
���   �$��2q�6�   6�   �� 5&   
   -   5        $ btnJobbraFordit��5&      $��20(�6+   $��30��6:   $��30��6E   $��795,580,415,102q�6o   �� 5'   
   -   5        $ btnBalraFordit��5'      $��20(�6*   $��30��68   $��30��6C   $��640,580,570,102q�6O   6h   �� 5*   
   -   5         ��Name$ lblNameSend��5*   $   $y�Hidden=�6   $��Bottom=�69   $��Center=�6T   $��Játékos neve:��6q   
�������$��50(�6�   6�   �� 5+   
   -   5         ��Name$ txtNameSend��5+   &   $y�Hidden=�6   $	��250��6;   $��50��6G   $��20(�6S   $��815,692,215,10q�6a   6{   �� 5,   
   -   5        $ btnNameSend��5,   @   $y�Hidden=�6   $��OK��6%   $��25(�62   $��1070,684,140,8q�6U    J� RenderTransformOrigin$ 
0.204,0.38&�6�   5.      5/      