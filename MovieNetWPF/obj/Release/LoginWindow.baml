   M S B A M L   `   `   `  ���� F  BMovieNetWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:MovieNetWPFMovieNetWPF      MovieNetWPF.LoginWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     +localclr-namespace:MovieNetWPF     -     �Name$  logwin�� � Title$ LoginWindow��$	��450��$	��800��   MainViewModel � DataContext# [  .���  � RowDefinitions �� $��* ��� $��* � � ColumnDefinitions �� $��* ��� $��* �.+��� $
��Left=�$
��Wrap=�$��Login :��$	��Top=�$��160,140,0,0q��� $
��auto�� �� TextWrapping$
 Wrap=�$	��Top=�$	��150��$
��Left=�$��200,140,46.8,0q�����  �� Path$ LogLogin� �� Mode$ TwoWay=��� $
��Left=�$
��Wrap=�$��
Password :��$	��Top=�$��139,180,0,0q�G� -   $��logpwd��$
��Left=�$��23��$��200,180,0,0q�$	��Top=�$	��150���� $��Login��$
��Left=�$	��Top=�$��75��$��200,211,0,0q�$��0��$��2������ $ Login�����  �� ElementName$ logpwd���� $��1��$
��Left=�$��23��$��150.2,140,0,0q�$
 Wrap=�$	��Top=�$	��150������ $ RegLogin�$ TwoWay=��� $��1��$
��Left=�$��110,140,0,0q�$
��Wrap=�$��Login :��$	��Top=��� $��1��$
��Left=�$
��Wrap=�$��
Password :��$	��Top=�$��89.2,183,0,0q��� $��1��$
��Left=�$��23��$��150,180,0,0q�$	��Top=�$	��150������ $ RegPassword�$ TwoWay=�G� -   $��regpwd��$��1��$
��Left=�$��23��$��150,180,0,0q�$	��Top=�$	��150���� $��Register��$��1��$
��Left=�$��150.2,211,0,0q�$	��Top=�$��75��$��2������ $ Register����� $ regpwd���� $
��Left=�$��
139,35,0,0q�$
��Wrap=�$��	Connexion��$	��Top=�$��28(��� $��1��$
��Left=�$��110.2,35,0,0q�$
��Wrap=�$��Création nouveau compte��$	��Top=�$��20(�