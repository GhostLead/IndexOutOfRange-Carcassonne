   M S B A M L   `   `   `  ����W  SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089   DataGridRowStyle1  �� 
TargetType)   #System.Windows.Controls.DataGridRow �� Property �� Value �� 
TargetType ��Name J� SnapsToDevicePixels?  9System.Windows.Controls.Primitives.SelectiveScrollingGrid � ColumnDefinitions � RowDefinitions?  9System.Windows.Controls.Primitives.DataGridCellsPresenter    
ItemsPanelA  ;System.Windows.Controls.Primitives.DataGridDetailsPresenter	    DetailsVisibility$
   SelectiveScrollingOrientation;  5System.Windows.Controls.SelectiveScrollingOrientation   Vertical �� ConverterParameter&   System.Windows.Controls.DataGrid#   RowDetailsScrollingConverter �� 	Converter �� RelativeSource �� AncestorType:  4System.Windows.Controls.Primitives.DataGridRowHeader7  1System.Windows.Controls.DataGridHeadersVisibility
   Row!   HeadersVisibilityConverter �� Triggers P� Property P� Value �� 
ResourceId �� TypeInTargetAssembly ��Name �Name J� Opacity � Points �� Stretch �� Triggers �� 
TargetName �� Padding  ��Name!   SelectAllCommand
"   AllG  ASystem.Windows.Controls.Primitives.DataGridColumnHeadersPresenter#  Name$   Column% ��Name& �� CanContentScroll' ��Name( �� ScrollableHeight) � Maximum* �� Orientation+ �� ViewportHeight, �� ViewportSize*- �� #ComputedVerticalScrollBarVisibility. � Value/ �� Mode0 �� ScrollableWidth1 �� ViewportWidth,2 �� %ComputedHorizontalScrollBarVisibility3 ]� 
Conditions4 �� Property5 �� Value  FocusVisual6 �� StrokeDashArray  Button.Static.Background7 �� Color  Button.Static.Border  Button.MouseOver.Background  Button.MouseOver.Border  Button.Pressed.Background  Button.Pressed.Border  Button.Disabled.Background 	 Button.Disabled.Border 
 Button.Disabled.Foreground8 ��Name�� 5      F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      :x,http://schemas.microsoft.com/winfx/2006/xaml      5      %   &	        (�� _    5)      $" DataGridSelectAllButtonStyle��"  )'       &	 `    &	     &	 6    &	 Y    &	 |    &	 �    &	 �    &	 �    &		     &	
 +    '�� N    7  7  7  7  7  7  7  7 	 7 
 �� 5      "    6&   .�5   
   ��  � ��# � ��6'   �� 5   
    �@J�SnapsToDevicePixels$
 true��60   �� 5   
    �@=�ErrorTemplate 65   Z� �� 5   
    �@  ValidationErrorTemplate 5      �� 5	      .R�5
      �� 
���  ��$��2,0,0,0q�61   $��!��6B   $��Center=�6K   5      5      �� 5   
    � �� 5      �� 5      "   6"   .R�5      �� $ 
DGR_Border��6   #��z��61   #��z��6[   #��z��6�   $
 True��6�   ._�5         5      �� 5   "   $
��Auto �63   �� 5   "   $��* �63    5      �� 5   "   $��* �60   �� 5   "   $
��Auto �60   .+�5        $��1��65   #��z 6E   # z 6o     5      $��1��67   $��1��6G   #y�z	 6k  
 6T   �� *AreRowDetailsFrozen+# Z # Z  �� "  6k    5      $��2��60   $
 Vertical=�6A   y�6�   �� *HeadersVisibility+# Z # Z  �� "  5      5      5       5"       5#   
   P� 5$       �@  	IsNewItem$
 True��6+   . �5%      ��  � �� 6+   �� *NewItemMargin+ �� "  5&      5(      �� 5)      "  ��6{   .�5*   
   ��  � �� 5+      �� 5,      " ��6"   .R�5-      � .+�5.      �� $ Border��6$   #��� ��64   $
 True��6u   � 5/      $ Arrow��6"   
���   �$��Right=�6>   $��8,8,3,3q�6Z   $
 0.15Q�6k    �   ��� ��� ��� ��� $ Uniform=�6�   $��Bottom=�6�   50       51      P� 52       �@J�IsMouseOver$
 True��69   . �53      �� $ Border��67   # � ��6K    � ��54      P� 55       � ��$
 True��67   . �56      �� $ Border��65   # � ��6I    � ��57      P� 58       � {�$ False��67   . �59      �� $ Arrow��6;    � y�$ 	Collapsed=�6N   5:      5<      5>      5?      �� 5@      "   6   .�5A   
   ��  � ��# � ��6'   �� 5B   
    � ��# � ��6'   �� 5C   
    � �� �	#FF688CAF6(   �� 5D   
    � ��$ 1q�6,   �� 5E   
     �@ RowDetailsVisibilityMode$ VisibleWhenSelected=�65   �� 5F   
    � ��$ False��6:   �� 5G   
    �@ GridLinesVisibility$ 
Horizontal=�60   �� 5H   
    �@ HeadersVisibility$ Column=�6.   �� 5I   
    �@ SelectionMode$ Single=�6*   �� 5J   
    �@��PanningMode$
 Both=�65   �� 5K   
    �@��IsFlicksEnabled$ False��63   �� 5L   
    � �� 5M      �� 5N      "  6"   .R�5O      �� #��z��6   #��z��6G   #��z��6s   # z��6�   $
 True��6�   ._�5P      �� $  DG_ScrollViewer��6'   $|�false��6@   ��5Q      �� 5R   "   " ��62   .R�5S   &   �  5T   *   �� 5U   .   $
��Auto �6?   �� 5V   .   $��* �6?   �� 5W   .   $
��Auto �6?    5Y   *   �� 5Z   .   $
��Auto �6<   �� 5[   .   $��* �6<   �� 5\   .   $
��Auto �6<   .+�5^   *   �� #��Z! 61   $|�false��6`   ��6r   C� *�� $" DataGridSelectAllButtonStyle��"  +y�6�   �� *HeadersVisibility+# Z" # Z  �� "  ��6�  �� *CellsPanelHorizontalOffset+ �� "    5_   *   $!# PART_ColumnHeadersPresenter��6I   $��1��6n   y�6~   �� *HeadersVisibility+# Z$ # Z  �� "  �� 5`   *   $!% PART_ScrollContentPresenter��6A   #& z��6f   $��2��6�   $��1��6�   �� 5a   *   $' PART_VerticalScrollBar��64   $��2��6T   #) z( 6d   $* Vertical=�6�   $��1��6�   #, z+ 6  #y�z- 6D  . 6�   �� *VerticalOffset+$/ OneWay=� �� *TemplatedParent+6D  � 5b   *   $��1��6/   $��2��6?    5c   .   �� 5d   2   ��6C   �� **(NonFrozenColumnsViewportHorizontalOffset+ �� "  �� 5e   2   $��* �6C   .+�5g   .   �� $' PART_HorizontalScrollBar��68   $��1��6Z   #) z0 6j   $* 
Horizontal=�6�   #, z1 6  #y�z2 6?  . 6�   �� *HorizontalOffset+$/ OneWay=� �� *TemplatedParent+6?  5h   +   5i   '   5j   #   .��5l      �� # z 6-   5m      5n      5o      5q       5r   
   ]� 5s      3 5t      �� 5u      4 �@��
IsGrouping$
5 true��66   �� 5v      "4 �@%�IsVirtualizingWhenGrouping$5 false��6X   ./�5x      ��  � ��$ false��6B   5y      5{      �� 5|      .�5}   
   ��  � �� 5~      �� 5      .R�5�      �� $��2q�6    $	6 1 2R�6+   #��� ��6A   $
 true��6�   $��1��6�   5�      5�      5�      �� 5�      $7 	#FFDDDDDD��67   �� 5�      $7 	#FF707070��63   �� 5�      $7 	#FFBEE6FD��6:   �� 5�      $7 	#FF3C7FB1��66   �� 5�      $7 	#FFC4E5F6��68   �� 5�      $7 	#FF2C628B��64   �� 5�      $7 	#FFF4F4F4��69   �� 5�      $7 	#FFADB2B5��65   �� 5�      $7 	#FF838383��69   �� 5�      "  ��6   .�5�   
   ��  �@�FocusVisualStyle8   6-   �� 5�   
    � ��8  6'   �� 5�   
    � ��8  6(   �� 5�   
    � ��# � ��6'   �� 5�   
    � ��$ 1q�6,   �� 5�   
    � ��$ Center=�67   �� 5�   
    � ��$ Center=�65   �� 5�   
    � ��$ 1q�6$   �� 5�   
    � �� 5�      �� 5�      " ��6"   .R�5�      �� $ border��6   #��z��6-   #��z��6W   #��z��6�   $
 true��6�   ._�5�      �� $8 contentPresenter��6+   $|�False��6E   #��z��6W   #��z��6�   $
��True��6�   # z 6�   #��z��6  5�       5�      P� 5�       �@��IsDefaulted$
 true��69   . �5�      �� $ border��6<   # � ��6P    � ��5�      P� 5�       �@J�IsMouseOver$
 true��69   . �5�      �� $ border��6<   8   � ��6P   5�      P� 5�       � ��$
 true��67   . �5�      �� $ border��6;   8   � ��6O   �� 5�      $ border��6<   8   � ��6P   5�      P� 5�       � {�$ false��67   . �5�      �� $ border��6;   8   � ��6O   �� 5�      $ border��6<   8   � ��6P   �� 5�      $ contentPresenter��6G   8   � ��6e   5�      5�      5�      5�      5�      