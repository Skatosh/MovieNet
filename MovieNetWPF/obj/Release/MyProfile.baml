   M S B A M L   `   `   `  ���� F  BMovieNetWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:MovieNetWPFMovieNetWPF      MovieNetWPF.MyProfile   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     +localclr-namespace:MovieNetWPF       � Title$  	MyProfile��$	��450��$	��800��.���  J� RenderTransformOrigin$ 0.501,0.314&� � RowDefinitions �� $��* � � ColumnDefinitions �� $��* ��� $��* �.+��� $
��Left=�$��	93,41,0,0q�$
��Wrap=�$��Your profile��$	��Top=�$��28(�$	��147���� $
��Left=�$
��Wrap=�$��Login :��$	��Top=�$��
52,140,0,0q�$��36���� $
��auto�� �� TextWrapping$
 Wrap=�$	��Top=�$	��152��$
��Left=�$��
93,140,0,0q� �� 
IsReadOnly . 
�����������  �� Path$ CurrentUser.Login� �� Mode$ TwoWay=��� $
��Left=�$
��Wrap=�$��New Password :��$	��Top=�$��	5,180,0,0q�$	��100���� $
��Left=�$��23��$��
93,180,0,0q�$
 Wrap=�$	��Top=�$	��152������ $ CurrentUser.Password�$ OneWayToSource=��� $��Update User��$
��Left=�$	��Top=�$��76��$��
93,208,0,0q�$��0������ $ 
UpdateUser��� $
��Left=�$��61.2,41,0,0q�$
��Wrap=�$��Your rating��$	��Top=�$��28(�$��1��$ 0.072,0.474&�&   System.Windows.Controls.DataGrid     AutoGenerateColumns .  	   
IsReadOnly	 . $��274,98,9.6,9.6q�$��2��$��0q�$
   HorizontalScrollBarVisibility$

 Auto=�$��2������ $ MyNotes�   Columns 4  .System.Windows.Controls.DataGridTemplateColumn     CellTemplate �� .R�� .+��� $��Delete������ $ DataContext.DelNote� �� RelativeSource ��  �� AncestorType"  ���� 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ Movie��   Width5  /System.Windows.Controls.DataGridLengthConverter$
 auto 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� $ Movie�  $ Rating��$
 auto  �� $ Rating�  $ Comment��$
 Auto  �� $ Comment�