   M S B A M L   `   `   `  ����@  <Burse, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:BurseBurse  5          Burse.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      U QMaterialDesignThemes.Wpf, Version=2.3.0.823, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      localclr-namespace:Burse  5         MaterialDesignBody#���   5      $��Regular"�5	      $��13(�5
      &   System.Windows.Media.TextOptions    TextFormattingMode$  Ideal=�5         TextRenderingMode$
 Auto=�5        MaterialDesignPaper#���  5        MaterialDesignFont#���  5       � Title$ 
Биржа��5      $	��500��6   $	��809��6#    � 
ResizeMode$ CanMinimize=�60    � WindowStartupLocation$ CenterScreen=�6I   .��5      � .+�5   
   �� $%��Последняя сделка��6   $��10,73,613,360q�6,    �� TextAlignment$ Center=�6C   $
��Wrap=�6Z   $	��180��6n   $��18(�6z   �� 5      $
��Left=�6   $	��124��64   $��
20,116,0,0q�6A   $	��Top=�6U   $	��161��6m   .+�5      �� $��Сбербанк��6   $
��Left=�6(   $	��Top=�6C   $	��160��6[   $ Center=�6g   $��18��6~   $
��Wrap=�6�   �� 5      -   5         ��Name$ TextBlockSber��5      $
��Wrap=�6/   $	��Top=�6C   $��54��6[   $��48(�6g   $ Center=�6u   ���	#FF2121216�   �� 5      $��RUB, ₽��6   $
��Left=�6&   $	��Top=�6A   $	��160��6Y   $ Center=�6e   $��17��6|   �� 5      $%��Количество акций��6   $
��Left=�60   $	��Top=�6K   $	��160��6c   $ Center=�6o   $��15��6�   �� 5      -   5        $��VolSber��5      $
��Left=�6(   $	��Top=�6C   $	��160��6[   $ Center=�6g   $��21��6~   $
��Wrap=�6�   5      �� 5   
   $
��Left=�6   $	��160��60   $��
20,252,0,0q�6=   $	��Top=�6Q   $	��161��6i   .+�5      �� $��Газпром��6   $
��Left=�6'   $	��Top=�6B   $	��160��6Z   $ Center=�6f   $��18��6}   $
��Wrap=�6�   �� 5      -   5        $ TextBlockGazp��5      $
��Wrap=�6/   $	��Top=�6C   $��54��6[   $��48(�6g   $ Center=�6u   ���	#FF2121216�   �� 5      $��RUB, ₽��6   $
��Left=�6&   $	��Top=�6A   $	��160��6Y   $ Center=�6e   $��17��6|   �� 5      $%��Количество акций��6   $
��Left=�60   $	��Top=�6K   $	��160��6c   $ Center=�6o   $��15��6�   �� 5       -   5        $��VolGazp��5       $
��Left=�6'   $	��Top=�6B   $	��160��6Z   $ Center=�6f   $��21��6}   $
��Wrap=�6�   �� 5!      -   5        $ 
LastReload��5!      $.��(Последнее обновление:��6,   $
��Left=�6I   $	��Top=�6d   $	��160��6|   $ Center=�6�   $��35��6�   5"      (  "MaterialDesignThemes.Wpf.ColorZone  5%   
      Mode$ Inverted=�6#   $��16q�63   ���	#FF673AB7$��52��6W   +  %MaterialDesignThemes.Wpf.ShadowAssist   ShadowDepth$ Depth2=�6c   .��5&      ]� .+�5'      '  !MaterialDesignThemes.Wpf.PopupBox  $��Right=�6*   	   PlacementMode$	 BottomAndAlignRightEdges=�6A   $��28��6j   $
��0,-4q�6u   $��28��6�   
 � Cursor$

 Hand��6�      PopupContent. 5(      o� .@�5)      m� -   5        $��
Выйти��5)   #   $��30��6I   $	��125��6U   $

 Hand��6a   5*      5+      �� 5,       �� Orientation$ 
Horizontal=�6   .+�5-      d� -   5        $��	ToggleBut��5-   #    ' #MaterialDesignHamburgerToggleButton#���  65   $��28��6�   $��28��6�   $��	0,-5,0,-3q�6�   $

 Hand��6�   �� 5.      -   5        $��Сaption��5.       $��Center=�6/   $��16 0 0 0q�6J   $��16(�6\   �� 6k   $�
Биржа��6o   6   5/      50      51        52   
   -	   5        $��ContextMenu��52   #   $	��200��66   $	��419��6B   $��-200,52,803,0q�6O   .��53      o� $
��Left=�6   $	��419��6.   $	��Top=�6;   $	��200��6S   ��54      �� 55       �� EndPoint$ 0.5,1&�6*    �� 
StartPoint$ 0.5,0&�6;   .��56      �  � Color$ White��6'    � Offset$ 0.274Q�65   � 57      $ 	#FFFAFAFA��6'   $ 1Q�69   58      .@�5:      m� -
   5        $
��Home��5:      $��50��6)   $	��200��65   $

 Hand��6A   6O   m� 5;      -   5        $��
BursePage1��5;      $'��!Max/Min котировки дня��6-   $��50��6L   $	��368��6X   $

 Hand��6d   6r   m� 5<      -   5        $��
BursePage2��5<      $)��#Сред. размер сделок��6-   $��50��6J   $	��368��6V   $

 Hand��6b   6p   m� 5=      -   5        $��
BursePage3��5=      $.��(Знач. за день в разн. г.��6-   $��50��6O   $	��368��6[   $

 Hand��6g   6u   m� 5>      -   5        $��
BursePage4��5>      $*��$Ср. знач. промежутка��6-   $��50��6K   $	��368��6W   $

 Hand��6c   m� 5?      -   5        $��TelegramPage��5?      $��Telegram бот��6/   $��50��6E   $	��368��6Q   $

 Hand��6]   ���	#FF517594
�������6�   5@      5A       � 5B   
   -   5          �Name$
 Main��5B        � NavigationUIVisibility$ Hidden=�6   $��
200,52,0,0q�6>   5C      5D      