   M S B A M L   `   `   `  ����V  RAutomatedTestingApplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullI)clr-namespace:AutomatedTestingApplicationAutomatedTestingApplication  5   	   _4clr-namespace:AutomatedTestingApplication.Converters&AutomatedTestingApplication.Converters  5   	   ,    &AutomatedTestingApplication.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ;local)clr-namespace:AutomatedTestingApplication     5   	   K
converters4clr-namespace:AutomatedTestingApplication.Converters     5   	     � Title$F  @Автоматизация тестирования знаний��5	   	   $	��450��63   $	��800��6@    � 
ResizeMode$ NoResize=�5
   	    � 	Resources 5      =  7AutomatedTestingApplication.Converters.SubjectConverter   SubjectConverter:  4AutomatedTestingApplication.Converters.TestConverter  TestConverter;  5AutomatedTestingApplication.Converters.GroupConverter  GroupConverter:  4AutomatedTestingApplication.Converters.RoleConverter  RoleConverter��%t   &	        &	      &	 &     &	 9       5   
   6&     5   
   6#     5   
   6$     5   
   6#   5      .��5      �  � ColumnDefinitions 5   
   �� 5      $��* �6   6+    � RowDefinitions 5   
   �� 5      $��* �6   6)   .+�5   
   � -   5         �Name$ AuthorizationGrid��5      .+�5      � $	��440��6   $	��795��6    .+�5      ��   
StackPanel#��[ 6   .+�5      ��  �� Orientation$ 
Horizontal=�6!   .+�5      �� $��	0,0,7.5,0q�6    .��63   Логин:6;   �� 5      -   5         ��Name$ LoginTextBox��5   "     TextBox#��[ 68   5       �� 5!      $ 
Horizontal=�6!   .+�5"      �� .��6    Пароль:6)   G� 5#      -   5         G�Name$ PasswordPassBox��5#   &   $��Center=�6?   $��Center=�6c   	 G� PasswordChar$	 *��6�   $	��200��6�   $��3q�6�   5$      �� 5%      .+�5&      �� -   5        
 ��Name$
 LogInButton��5&   !   $��
Войти��66    
 Button#��[ 6F   6f   5'      5(      5)      5*      � 5+   
   -   5        $ StudentGrid��5+   #   $y�Hidden=�6   .+�5,      �� -   5        $��StudentTabControl��5,        
TabControl#��[ 62   ���	#FFD8DBFF.@�5-      �� -   5        $��
Тесты��5-        TabItem#��[ 6)   .��5.      � $	��435��6   .+�5/      &   System.Windows.Controls.DataGrid  -   5        $��StudentTestDataGrid��5/   #      
IsReadOnly$
 true��6>      AutoGenerateColumns$ False��6P      CanUserAddRows$ False��6l      CanUserDeleteRows$ False��6�   $��15,40,15,40q�6�      Columns 50      0  *System.Windows.Controls.DataGridTextColumn  51   "   ,  &System.Windows.Controls.DataGridColumn   Header$! Название теста��65   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6M   �� *TestId+ �� 	Converter# [  �� Mode$ TwoWay=�  52   "   $ Предмет��65    6F   �� *SubjectName+4	  .System.Windows.Controls.DataGridTemplateColumn	  53   "    	  CellTemplate 54   &   �� 55   *   .R�56   .   �� $
 StartTestButton��65   -	   5        .��56   l   Пройти тест6y   57   +   59   #   	  5:   "    5;   &   �� 5<   *   .R�5=   .   �� $
 CheckTestResultButton��65   -
   5        .��5=   x   Результат6�   5>   +   5@   #   5B      5C      5D      5E      �� 5F      -   5        $
 LogOutStudentButton��5F       	 
ExitButton#��[	 62   .��6y   
Выход6�   5G      � 5H   
   -   5        $ TeacherGrid��5H   #   $y�Hidden=�6   .+�5I      �� -   5        $��TeacherTabControl��5I      #��[ 62   ���	#FFD8DBFF.@�5J      �� -   5        $��Тесты групп��5J      #��[ 6/   .��5K      � $	��435��6   .+�5L        -   5        $��TeacherTestListDataGrid��5L   #   $
 true��6B   $ False��6T   $ False��6p   $ False��6�   $��15,40,15,65q�6�    5M        5N   "   $! Название теста��65    6M   �� *TestId+# [ $ TwoWay=�  5O   "   $ Группа��65    6E   �� *	GroupId+# [ $ TwoWay=�	  5P   "    5Q   &   �� 5R   *   .R�5S   .   �� $
 TestGroupResultButton��65   -   5        .��5S   x   Результаты6�   5T   +   5V   #   5X      �� 5Y      $ 
Horizontal=�6%   $
��Left=�6>   $��Bottom=�6Y   $��	15,0,0,35q�6t   .+�5Z      �� -   5        $
 TeacherAddTestGroupButton��5Z   %   $��Добавить��6H   $��0,0,5,0q�6[   #��[ 6l   6�   �� 5[      -   5        $"
 TeacherDeleteTestGroupButton��5[   %   $��Удалить��6K   $��0,0,5,0q�6]   #��[ 6n   6�   5\      5]      5^      �� 5_      -   5        $#��Редактор тестов��5_      #��[ 63   .��5`      � $	��435��6   .+�5a        -   5        $��TestEditorDataGrid��5a   #   $
 true��6=   $ False��6O   $ False��6k   $ False��6�   $��15,40,15,65q�6�    5b        5c   "   $! Название теста��65    6M   �� *Name+	  5d   "    5e   &   �� 5f   *   .R�5g   .   �� -   5        .��5g   X   Вопросы6a   5h   +   5j   #   5l      �� 5m      $ 
Horizontal=�6%   $
��Left=�6>   $��Bottom=�6Y   $��	15,0,0,35q�6t   .+�5n      �� -   5        $
 TeacherAddTestButton��5n   %   $��Добавить��6C   $��0,0,5,0q�6V   #��[ 6g   6�   �� 5o      -   5        $
 TeacherDeleteTestButton��5o   %   $��Удалить��6F   $��0,0,5,0q�6X   #��[ 6i   6�   5p      5q      5r      5s      �� 5t      -   5        $
 LogOutTeacherButton��5t      #��[	 62   .��6y   
Выход6�   5u      � 5v   
   -   5        $ 	AdminGrid��5v   #   $y�Hidden=�6   .+�5w      �� -   5        $��AdminTabControl��5w      #��[ 60   ���	#FFD8DBFF.@�5x      �� -   5        $��Пользователи��5x      #��[ 60   .��5y      � $	��435��6   .+�5z        -   5        $��UserDataGrid��5z   #   $
 true��67   $ False��6I   $ False��6e   $ False��6|   $��15,40,15,65q�6�    5{        5|   "   $ Фамилия��65    6F   �� *	Surname+  5}   "   $ Имя��65    6B   �� *Name+  5~   "   $ Отчество��65    6G   �� *
Patronymic+  5   "   $ 
Логин��65    6D   �� *Login+  5�   "   $ Пароль��65    6E   �� *
Password+  5�   "   $ Роль��65    6C   �� *RoleId+# [ $ TwoWay=�  5�   "   $ Группа��65    6E   �� *	GroupId+# [ $ TwoWay=�  5�   "   $ Предмет��65    6F   �� *	SubjectId+# [  $ TwoWay=�5�      �� 5�      $ 
Horizontal=�6%   $
��Left=�6>   $��Bottom=�6Y   $��	15,0,0,35q�6t   .+�5�      �� -   5        $
 UserAddButton��5�   %   $��Добавить��6<   $��0,0,5,0q�6O   #��[ 6`   6�   �� 5�      -   5        $
 UserDeleteButton��5�   %   $��Удалить��6?   $��0,0,5,0q�6Q   #��[ 6b   6�   �� 5�      -   5        $
 UserChangeButton��5�   %   $��Изменить��6?   $��0,0,5,0q�6R   #��[ 6c   6�   5�      5�      5�      �� 5�      -    5        $��Группы��5�      #��[ 6*   .��5�      � $	��435��6   .+�5�        -!   5        $��GroupDataGrid��5�   #   $
 true��68   $ False��6J   $ False��6f   $ False��6}   $��15,40,15,65q�6�    5�        5�   "   $ Группа��65    6E   �� *Name+5�      �� 5�      $ 
Horizontal=�6%   $
��Left=�6>   $��Bottom=�6Y   $��	15,0,0,35q�6t   .+�5�      �� -"   5        $
 GroupAddButton��5�   %   $��Добавить��6=   $��0,0,5,0q�6P   #��[ 6a   6�   �� 5�      -#   5        $
 GroupDeleteButton��5�   %   $��Удалить��6@   $��0,0,5,0q�6R   #��[ 6c   6�   5�      5�      5�      �� 5�      -$   5        $��Предметы��5�      #��[ 6,   .��5�      � $	��435��6   .+�5�        -%   5        $��SubjectDataGrid��5�   #   $
 true��6:   $ False��6L   $ False��6h   $ False��6   $��15,40,15,65q�6�    5�        5�   "   $ Предмет��65    6F   �� *Name+5�      �� 5�      $ 
Horizontal=�6%   $
��Left=�6>   $��Bottom=�6Y   $��	15,0,0,35q�6t   .+�5�      �� -&   5        $
 SubjectAddButton��5�   %   $��Добавить��6?   $��0,0,5,0q�6R   #��[ 6c   6�   �� 5�      -'   5        $
 SubjectDeleteButton��5�   %   $��Удалить��6B   $��0,0,5,0q�6T   #��[ 6e   6�   5�      5�      5�      �� 5�      -(   5        $��
Тесты��5�      #��[ 6)   .��5�      � $	��435��6   .+�5�        -)   5        $��TestListDataGrid��5�   #   $
 true��6;   $ False��6M   $ False��6i   $ False��6�   $��15,40,15,65q�6�    5�        5�   "   $! Название теста��65    6M   �� *Id+# [ $ TwoWay=�  5�   "   $ Фамилия��65    6F   �� *TeacherSurname+  5�   "   $ Предмет��65    6F   �� *SubjectName+	  5�   "    5�   &   �� 5�   *   .R�5�   .   �� $
 AdminQuestionButton��65   -*   5        .��5�   t   Вопросы6}   5�   +   5�   #   5�      �� 5�      $ 
Horizontal=�6%   $
��Left=�6>   $��Bottom=�6Y   $��	15,0,0,35q�6t   .+�5�      �� -+   5        $
 TestDeleteAdminButton��5�   %   $��Удалить��6D   $��0,0,5,0q�6V   #��[ 6�   5�      5�      5�      �� 5�      -,   5        $��
LogTabItem��5�      #��[ 6.   .��5�      � $	��435��6   .+�5�        --   5        $��LogDataGrid��5�   #   $
 true��66   $ False��6H   $ False��6d   $ False��6{   $��15,40,15,65q�6�    5�        5�   "   $ User Id��65    6F   �� *UserId+  5�   "   $ 
Время��65    6D   �� *Time+  5�   "   $ Лог��65    6B   �� *	LogName+5�      �� 5�      $ 
Horizontal=�6%   $
��Left=�6>   $��Bottom=�6Y   $��	15,0,0,35q�6t   .+�5�      �� -.   5        $
 BackupDbButton��5�   %   #��[	 6=   $��0,0,5,0q�6a   .��6�   	Backup Db6�   �� 5�      -/   5        $
 BackupLogDbButton��5�   %   #��[	 6@   $��0,0,5,0q�6d   .��6�   Журнал Db6�   5�      5�      5�      �� 5�      $��Запросы��6   #��[ 6+   .��5�      � $	��435��6   .+�5�      �� $��11,45,15,15q�6    .��65   Sql запрос6A   �� 5�      -0   5        $ SqlRequestTextBox��5�   "   $	��Top=�6=   $��50��6U   $��15,70,15,15q�6�   �� 5�      $��11,120,15,15q�6    .��66   Результат6A     5�      -1   5        $��SqlRequestDataGrid��5�   #   $
 true��6=   $
 True��6O   $ False��6j   $ False��6�   $��15,145,15,65q�6�    5�      5�      5�      5�      5�      �� 5�      -2   5        $
 LogOutAdminButton��5�      #��[	 60   .��6t   
Выход6{   5�      � 5�   
   -3   5        $ UserGrid��5�   #   $y�Hidden=�6   .+�5�      �� $��Center=�6   $��Center=�66   .+�5�      �� $ 
Horizontal=�6   .+�5�      �� .��6   Фамилия:6&   �� 5�      -4   5        $ SurnameTextBox��5�      #��[ 66   $��5q�6W   5�      �� 5�      $ 
Horizontal=�6   .+�5�      �� .��6   	Имя:6"   �� 5�      -5   5        $ NameTextBox��5�      #��[ 63   $��31,5,5,5q�6T   5�      �� 5�      $ 
Horizontal=�6   .+�5�      �� .��6   Отчество:6'   �� 5�      -6   5        $ PatronymicTextBox��5�      #��[ 69   $��4,5,5,5q�6Z   5�      �� 5�      $ 
Horizontal=�6   .+�5�      �� .��6   Логин:6$   �� 5�      -7   5        $ LogInTextBox��5�      #��[ 64   $��21,5,5,5q�6U   5�      �� 5�      $ 
Horizontal=�6   .+�5�      �� .��6   Пароль:6%   �� 5�      -8   5        $ PasswordTextBox��5�      #��[ 67   $��14,5,5,5q�6X   5�      �� 5�      $ 
Horizontal=�6   .+�5�      �� .��6   	Роль:6#   �� 5�      -9   5         ��Name$ RoleComboBox��5�      $��Center=�66   $��Center=�6Z    �� DisplayMemberPath$
 Name��6|   $	��200��6�   $��18��6�   $��29,3,3,3q�6�   5�      �� 5�      $ 
Horizontal=�6   .+�5�      �� .��6   Группа6$   �� 5�      -:   5        $ GroupComboBox��5�      $��Center=�66   $��Center=�6Z   ${�False��6|   $
 Name��6�   $	��200��6�   $��18��6�   $��19,3,3,3q�6�   5�      �� 5�      $ 
Horizontal=�6   .+�5�      �� .��6   Предмет6%   �� 5�      -;   5        $ SubjectComboBox��5�      $��Center=�68   $��Center=�6\   ${�False��6~   $
 Name��6�   $	��200��6�   $��18��6�   $��	9.5,3,3,3q�6�   5�      �� 5�      $��Center=�6   $��Center=�6:   $ 
Horizontal=�6U   .+�5�      �� -<   5        $
 SaveUserButton��5�      $��Сохранить��65   #��[ 6J   $��3q�6j   6u   �� 5�      -=   5        $
 BackUserButton��5�      $��
Назад��65   #��[ 6F   $��3q�6f   6q   5�      5     5     � 5  
   ->   5        $ 	GroupGrid��5  #   $y�Hidden=�6   .+�5     �� $��25q�6   $��Center=�6%   $��Center=�6B   .+�5     �� $ 
Horizontal=�6   .+�5     �� .��6   Группа:6%   �� 5     -?   5        $ GroupTextBox��5     #��[ 64   $	��195��6U   $��5q�6a   5     �� 5	     $��Center=�6   $��Center=�6:   $ 
Horizontal=�6U   .+�5
     �� -@   5        $
 SaveGroupButton��5
     $��Сохранить��66   #��[ 6K   $��3q�6k   6v   �� 5     -A   5        $
 BackGroupButton��5     $��
Назад��66   #��[ 6G   $��3q�6g   6r   5     5     5     � 5  
   -B   5        $ SubjectGrid��5  #   $y�Hidden=�6   .+�5     �� $��25q�6   $��Center=�6%   $��Center=�6B   .+�5     �� $ 
Horizontal=�6   .+�5     �� .��6   Предмет:6&   �� 5     -C   5        $ SubjectTextBox��5     #��[ 66   $	��195��6W   $��5q�6c   5     �� 5     $��Center=�6   $��Center=�6:   $ 
Horizontal=�6U   .+�5     �� -D   5        $
 SaveSubjectButton��5     $��Сохранить��68   #��[ 6M   $��3q�6m   6x   �� 5     -E   5        $
 BackSubjectButton��5     $��
Назад��68   #��[ 6I   $��3q�6i   6t   5     5     5     � 5  
   -F   5        $ TestGrid��5  #   $y�Hidden=�6   5     � 5  
   -G   5        $ RequestGrid��5  #   $y�Hidden=�6   5      � 5!  
   -H   5        $ TestEditorGrid��5!  #   $y�Hidden=�6   .+�5"     �� $��25q�6   $��Center=�6%   $��Center=�6B   .+�5#     �� $ 
Horizontal=�6   .+�5$     �� .��6   	Тест:6#   �� 5%     -I   5        $ TestNameTextBox��5%     #��[ 67   $	��195��6X   $��5q�6d   5&     �� 5'     $��Center=�6   $��Center=�6:   $ 
Horizontal=�6U   .+�5(     �� -J   5        $
 SaveTestEditorButton��5(     $��Сохранить��6;   #��[ 6P   $��3q�6p   6{   �� 5)     -K   5        $
 BackTestEditorButton��5)     $��
Назад��6;   #��[ 6L   $��3q�6l   6w   5*     5+     5,     � 5-  
   -L   5        $ QuestionGrid��5-  #   $y�Hidden=�6   .+�5.     � $	��200��6   $
��Left=�6   .+�5/     �� -M   5         ��Name$ questionListBox��5/     $��15,15,15,50q�63   50     �� 51     $��22q�6   $��Bottom=�6)   $ 
Horizontal=�6D   .+�52     �� -N   5        $
 AddQuestionButton��52     $��Добавить��68   #��[ 6L   $��3q�6l   6w   �� 53     -O   5        $
 DeleteQuestionButton��53     $��Удалить��6;   #��[ 6N   $��3q�6n   6y   54     55     � 56     $	��600��6   $��	200,0,0,0q�6   .+�57     �� $��Вопрос:��6   $
��Left=�6*   $��25,5,0,0q�6E   $	��Top=�6W   �� 58     $��Ответ:��6   $
��Left=�6)   $��	25,80,0,0q�6D   $	��Top=�6W   �� 59     $C��=Дополнительные варианты ответов:��6   $
��Left=�6C   $��
25,155,0,0q�6^   $	��Top=�6r   �� 5:     -P   5        $ QuestionTextBox��5:     $��Center=�63   $��0,30,0,0q�6P    �� TextWrapping$
 Wrap=�6b   $	��Top=�6v   $	��540��6�   $��50��6�   �� 5;     -Q   5        $ AnswerTextBox��5;     $��Center=�61   $��	0,105,0,0q�6N   $
 Wrap=�6a   $	��Top=�6u   $	��540��6�   $��50��6�   �� 5<     -R   5        $ WrongAnswerTextBox��5<     $��Center=�66   $��	0,185,0,0q�6S   $
 Wrap=�6f   $	��Top=�6z   $	��540��6�   $	��176��6�   6�   �� 5=     $��22q�6   $��Bottom=�6)   $ 
Horizontal=�6D   .+�5>     �� -S   5        $
 UpdateQuestionButton��5>     $��Сохранить��67   #��[ 6L   $��8,3,3,3q�6l   6}   �� 5?     -T   5        $
 BackQuestionButton��5?     $��
Назад��65   #��[ 6F   $��	395,3,3,3q�6f   6y   5@     5A     5B     � 5C  
   -U   5        $ JunctionGroupTestGrid��5C  #   $y�Hidden=�6   .+�5D     �� $��25q�6   $��Center=�6%   $��Center=�6B   .+�5E     �� $ 
Horizontal=�6   .+�5F     �� .��6   	Тест:6#   �� 5G     -V   5        $ TestComboBox��5G     $��Center=�65   $��Center=�6Y   $
 Name��6{   $	��200��6�   $��18��6�   $��19,3,3,3q�6�   5H     �� 5I     $ 
Horizontal=�6   .+�5J     �� .��6   Группа:6%   �� 5K     -W   5        $ GroupTestComboBox��5K     $��Center=�6:   $��Center=�6^   $
 Name��6�   $	��200��6�   $��18��6�   $��4,3,3,3q�6�   5L     �� 5M     $��Center=�6   $��Center=�6:   $ 
Horizontal=�6U   .+�5N     �� -X   5        $!
 JunctionTestGroupSaveButton��5N     $��Сохранить��6B   #��[ 6W   $��3q�6w   6�   �� 5O     -Y   5        $!
 JunctionTestGroupBackButton��5O     $��
Назад��6B   #��[ 6S   $��3q�6s   6~   5P     5Q     5R     � 5S  
   -Z   5        $ StartTestGrid��5S  #   $y�Hidden=�6   .+�5T     �� $��Center=�6   $��Center=�64   $ Vertical=�6Q   .+�5U     �� -[   5         ��Name$ NameTestLabel��5U     61   �� 5V     $��Center=�6   $��Center=�6:   $ 
Horizontal=�6U   .+�5W     �� -\   5        $
 StartTestStudentButton��5W     $��Начать��6=   $��3q�6N   #��[ 6~   �� 5X     -]   5        $
 BackTestButton��5X     $��
Назад��65   $��3q�6E   #��[ 6m   5Y     5Z     5[     � 5\  
   -^   5        $ TestQuestionGrid��5\  #   $y�Hidden=�6   .+�5]     �� $��25q�6   $	��260��6%   $	��250��61   $��Center=�6>   $��Center=�6[   .+�5^     �� $ Vertical=�6   .+�5_     �� -_   5        $ QuestionNameLabel��5_     69   �� 5`     -`   5        $ QuestionTestListBox��5`     $��5q�6;   $	��180��6F   5a     �� 5b     $��Center=�6!   $��Center=�6?   $ 
Horizontal=�6Z   .+�5c     �� -a   5        $
 AcceptQuestionButton��5c  !   $��Выбрать��6?   #��[ 6R   $��3q�6r   6}   5d     5e     5f     5g     � 5h  
   -b   5        $ StudentResultGrid��5h  #   $y�Hidden=�6   .+�5i     �� $��Center=�6   $��Center=�66   $ Vertical=�6Q   .+�5j     �� $��Center=�6   $��Center=�65   .��6P   Результаты6\   �� 5k     -c   5        $ StudentNameLabel��5k     64   �� 5l     -d   5        $ StudentTestNameLabel��5l     68   �� 5m     -e   5        $ AmountOfQuestionLabel��5m     69   �� 5n     -f   5        $ AmountOfRightAnwsersLabel��5n     6=   �� 5o     -g   5        $ 	TimeLabel��5o     6-   �� 5p     $��Center=�6   $��Center=�6:   $ 
Horizontal=�6U   .+�5q     �� -h   5        $
 BackStudentButton��5q     $��
Назад��68   $��3q�6H   #��[ 6s   �� 5r     -i   5        $
 PrintResultStudentButton��5r     $��Печать��6?   $��3q�6P   #��[ 6�   5s     5t     5u     � 5v  
   -j   5        $ GroupResultGrid��5v  #   $y�Hidden=�6   .+�5w       -k   5        $!��TestResultGroupListDataGrid��5w     $
 true��6:   $ False��6L   $ False��6h   $ False��6   $��15,15,15,41q�6�    5x       5y     $ Фамилия��6)    6:   �� *	Surname+  5z     $" Кол-во вопросов��6)    6B   �� *AmountOfQuestions+  5{     $  Кол-во ответов��6)    6B   �� *AmountOfAnswers+  5|     $' !Затраченное время��6)    6E   �� *Time+5~     �� 5     $ 
Horizontal=�6   $
��Left=�62   $��Bottom=�6M   $��	15,0,0,11q�6h   .+�5�     �� -l   5        $!
 TestResultDeleteGroupButton��5�     $��Удалить��6>   $��0,0,5,0q�6P   #��[ 6a   6�   �� 5�     -m   5        $
 TestResultBackButton��5�     $��
Назад��67   $��0,0,5,0q�6G   #��[ 6X   6x   5�     5�     5�     5�     