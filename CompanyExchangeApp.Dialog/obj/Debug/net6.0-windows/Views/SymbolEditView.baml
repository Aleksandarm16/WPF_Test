   M S B A M L   `   `   `  ����  CompanyExchangeApp.Dialog4    .CompanyExchangeApp.Dialog.Views.SymbolEditView   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      T PPrism.Wpf, Version=8.1.97.5141, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59$prismhttp://prismlibrary.com/  5      $	��400��5      $	��500��5      !  Prism.Mvvm.ViewModelLocator    AutoWireViewModel  True5      .��5      �� .+�5	   
   �� $��30��5
      $	��100��5      ��5	      �� *Symbol.Name+ �� UpdateSourceTrigger$ PropertyChanged=�5      �� 5   
   $��30��5      $	��100��5      ��5      �� *Symbol.Ticker+$ PropertyChanged=�5      �� 5   
   $��30��5      $	��100��5      ��5      �� *Symbol.Isin+$ PropertyChanged=�5      �� 5   
   $��30��5      $	��100��5      ��5      �� *Symbol.CurrencyCode+$ PropertyChanged=�" �� ValidatesOnNotifyDataErrors$
 True��5      (  "System.Windows.Controls.DatePicker  5   
   $��30��6e   $	��100��6q      SelectedDate 6   �� *SelectedDateAdded+$ PropertyChanged=�6q   �� 5   
   $��30��5      $	��100��5      ��5      �� *Symbol.Price+ �� StringFormat$ N��$ PropertyChanged=� �� ValidatesOnDataErrors$
 True��5        5   
   $��30��5      $	��100��5       5      �� *SelectedPriceDate+5      �� 5    
    �� DisplayMemberPath$
 Name��5!      $ �� IsSynchronizedWithCurrentItem$
 TrueY�5"      $��10q�5$      $	��Top=�5%      $
��Left=�5&      $	��200��5'      ��5       �� *	Exchanges+ �� SelectedItem 5#      �� *SelectedExchange+	 �� Mode$	 TwoWay=�5'      �� 5(   
   $
 Name��5)      $
 TrueY�5*      $��10q�5,      $	��Top=�5-      $
��Left=�5.      $	��200��5/      ��5(      �� *Types+ 5+      �� *SelectedType+$	 TwoWay=�
 �� NotifyOnSourceUpdated$

 True��5/      �� 50   
   $��Cancel��51      $��30��52      $	��100��53      ��50      �� *CloseDialogCommand+53      �� 54   
   $
��Save��55      $��30��56      $	��100��57      ��54      �� *SaveDialogCommand+57      58      59      