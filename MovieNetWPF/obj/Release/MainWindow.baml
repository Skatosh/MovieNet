   M S B A M L   `   `   `  ����   MovieNetWPF    MovieNetWPF.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  b ^GalaSoft.MvvmLight.Platform, Version=5.4.1.0, Culture=neutral, PublicKeyToken=5f873c45e98af8a1.command http://www.galasoft.ch/mvvmlight    � Title$  MovieNet��$	��450��$	��800��.  (System.Windows.Interactivity.Interaction   Triggers /  )System.Windows.Interactivity.EventTrigger     	EventName$ Closing��.  (System.Windows.Interactivity.TriggerBase   Actions. /  )GalaSoft.MvvmLight.Command.EventToCommand     PassEventArgsToCommand .    Command �� *ClosingCommand+ � 	Resources ��%    .���  � RowDefinitions �� $
��Auto ��� $��* � � ColumnDefinitions �� $��2* ��� $��3* �.+��� $��Movie��$��Center=�$��5q��� 	 �� DisplayMemberPath$	 Title��
 �� 
IsEditable
 .  �� IsTextSearchEnabled .   �� StaysOpenOnEdit . $��50 5 5 0q�����  �� Path$ FilteredMovies� �� Mode$ TwoWay=� �� SelectedItem �� $ CurrentMovie�$ TwoWay=� �� Text �� *ComboTextFilter+�� $��2��$��
Disconnect��$	��150��$��Right=����� $ 
Disconnect��� $��2��$��
My Profile��$	��150��$��Right=�$��0,0,65,0q����� $ 	MyProfile�&   System.Windows.Controls.DataGrid  $��1��   AutoGenerateColumns .     
IsReadOnly . $��5q����� $ Notes� �� $ CurrentNote�   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ Rating��   Width5  /System.Windows.Controls.DataGridLengthConverter$
 Auto 1	  +System.Windows.Controls.DataGridBoundColumn 	  Binding �� $ Rating�  $
 User��$
 Auto  �� $
 User�  $ Comment��$
 Auto  �� $ Comment�� $��1��$��2�� �� ����  �� 
TargetType" ��.���  �� Property � �� �� Value$
 Auto � �� Triggers ��  �� Binding �� *CurrentMovie+ �� Value Z� .a���  � ��$ 0 ��� ���� " ��.���  � ��$
 Auto � ��  �� *CurrentMovie+ Z� .a���  � ��$ 0 ��� ���� " ��.���  � ��$
 Auto � ��  �� *CurrentMovie+ Z� .a���  � ��$ 0 ��� ���� " ��.���  � ��$
 Auto � ��  �� *CurrentMovie+ Z� .a���  � ��$ 0 ��� ���� " ��.���  � ��$
 Auto � ��  �� *CurrentMovie+ Z� .a���  � ��$ 0 ��� ���� " ��.���  � ��$
 Auto � ��  �� *CurrentNote+ Z� .a���  � ��$ 0 ��� ���� " ��.���  � ��$
 Auto � ��  �� *CurrentNote+ Z� .a���  � ��$ 0 ��� ���� " ��.���  � ��$
 Auto � ��  �� *CurrentNote+ Z� .a���  � ��$ 0 ��� $��40 ��� ���� " ��.���  � ��$
 Auto � ��  �� *CurrentMovie+ Z� .a���  � ��$ 0 ��� ���� " ��.���  � ��$
 Auto � ��  �� *CurrentMovie+ Z� .a���  � ��$ 0 ��� $
��Auto � �� $
��auto ��� $��1* ��� $��1* �.+��� $��Title : ���� $��1��$��2������ $ CurrentMovie.Title�$ TwoWay=��� $��Genre : ��$��1���� $��1��$��1��$��2������ $ CurrentMovie.Genre�$ TwoWay=��� $��
Summary : ��$��2���� $��1��$��2��$��2������ $ CurrentMovie.Summary�$ TwoWay=��� $��Rating Average : ��$��3����  �� 
IsReadOnly . $��1��$��3��$��3��$��0q����� $ NoteAverage�$ TwoWay=��� $��Update Movie��$��1��$��2��$��4������ $ UpdateMovie��� $��
Comment : ��$��5��$��0,10,0,0q��� $��1��$��5��$��2��$��0,10,0,0q� �� $ NoteUpdateReadOnly����� $ CurrentNote.Comment�$ TwoWay=��� $��	Rating : ��$��6���� $��1��$��6��$��2�� �� $ NoteUpdateReadOnly����� $ CurrentNote.Rating�$ TwoWay=��� $��Update Note��$��1��$��2��$��7������ $ 
UpdateNote��� $��Add New Movie��$��1��$��1��$��8��$��Bottom=�$��25������ $ MovieGen��� $��Add New Rating��$��1��$��1��$��9��$��0,10,0,0q�$��Bottom=�$��25������ $ NoteGen��� $��Delete Movie��$��1��$��1��$��10��$��0,10,0,0q�$��25������ $ DelMovie��� $��1��$��2��$��11�� J� 	AllowDrop . $��0 20 0 0q����� *Tips+$ TwoWay=�