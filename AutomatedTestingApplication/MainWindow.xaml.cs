using AutomatedTestingApplication.Converters;
using AutomatedTestingApplication.Model;
using Microsoft.Office.Interop.Word;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using static System.Net.Mime.MediaTypeNames;


namespace AutomatedTestingApplication
{
   public partial class MainWindow : System.Windows.Window
    {
      private User _userId;
      private bool _changeUserInfo = false;
      private User _currentUserInfo;
      private int _currentTestId;
      private List<NumberQuestion> _numberQuestions = new List<NumberQuestion>();
      private List<Question> _questionList = new List<Question>();
      private DateTime _startTestTime;
      private DateTime _finistTestTime;
      private int _indexQuestion = 0;
      private int _rightAnswers = 0;
      private string _connectionString = ConfigurationManager.ConnectionStrings["DataContext"].ConnectionString;
      public MainWindow()
      {
         InitializeComponent();
         _ConnectionPath();
      }
      private void LogInButton_Click(object sender, RoutedEventArgs e)
      {
         try
         {
            using (DataContext db = new DataContext())
            {
               var user = db.User.FirstOrDefault(x => x.Login == LoginTextBox.Text && x.Password == PasswordPassBox.Password);
               _currentUserInfo = user;
               if (user != null)
               {
                  _userId = user;
                  var role = db.Role.First(x => x.Id == user.RoleId);
                  if (role.Name == "Admin")
                  {
                     AdminGrid.Visibility = Visibility.Visible;
                     AuthorizationGrid.Visibility = Visibility.Hidden;
                     DirectoryTabItemFill("Пользователи");
                  }
                  else if (role.Name == "Student")
                  {
                     StudentGrid.Visibility = Visibility.Visible;
                     AuthorizationGrid.Visibility = Visibility.Hidden;
                     DirectoryTabItemFill("Тесты");
                  }
                  else if (role.Name == "Teacher")
                  {
                     TeacherGrid.Visibility = Visibility.Visible;
                     AuthorizationGrid.Visibility = Visibility.Hidden;
                     DirectoryTabItemFill("Тесты групп");
                  }
                  AddLogInfo($"Пользователь id: {_currentUserInfo.Id} - авторизовался");
               }
               else
               {
                  throw new ArgumentException("Логин или пароль введены неверно!");
               }
            }
         }
         catch (Exception ex)
         {
            MessageBox.Show(ex.Message);
         }
      }
      private void _ConnectionPath()
      {
         string ServerInfo = string.Empty;
         var list = RegistryValueDataReader.GetLocalSqlServerInstanceNames();
         var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
         var path = $"data source={Environment.MachineName}\\{list[0].ToString()};initial catalog=AutomatedTesting;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";
         var connectionStringsSection = (ConnectionStringsSection)config.GetSection("connectionStrings");
         if (connectionStringsSection.ConnectionStrings["DataContext"].ConnectionString != path)
         {
            connectionStringsSection.ConnectionStrings["DataContext"].ConnectionString = path;
            config.Save();
         }
      }
      private void Test_MouseUp(object sender, MouseButtonEventArgs e)
      {
         DirectoryTabItemFill("Тесты");
      }
      private void LogTabItem_MouseUp(object sender, MouseButtonEventArgs e)
      {
         DirectoryTabItemFill("Логи");
      }
      private void AdminTestTabItem_MouseUp(object sender, MouseButtonEventArgs e)
      {
         DirectoryTabItemFill("Список тестов");
      }
      private void TestResults_MouseUp(object sender, MouseButtonEventArgs e)
      {
         DirectoryTabItemFill("Результаты");
      }
      private void TeacherTestGroup_MouseUp(object sender, MouseButtonEventArgs e)
      {
         DirectoryTabItemFill("Тесты групп");
      }
      private void TestEditor_MouseUp(object sender, MouseButtonEventArgs e)
      {
         DirectoryTabItemFill("Редактор тестов");
      }
      private void UserTabItem_MouseUp(object sender, MouseButtonEventArgs e)
      {
         DirectoryTabItemFill("Пользователи");
      }
      private void GroupTabItem_MouseUp(object sender, MouseButtonEventArgs e)
      {
         DirectoryTabItemFill("Группы");
      }
      private void SubjectTabItem_MouseUp(object sender, MouseButtonEventArgs e)
      {
         DirectoryTabItemFill("Предметы");
      }
      private void DirectoryTabItemFill(string header)
      {
         using (DataContext db = new DataContext())
         {
            if (header == "Тесты")
            {
               var items = db.JunctionGroupTest.Where(x => x.GroupId == _currentUserInfo.GroupId).ToList();
               List<TestDTO> testDTOs = new List<TestDTO>();
               foreach (var item in items)
               {
                  TestDTO testDTO = new TestDTO();
                  testDTO.TestId = item.TestId;
                  int userId = db.Test.First(x => x.Id == item.TestId).UserId;
                  int? subjectId = db.User.First(x => x.Id == userId).SubjectId;
                  testDTO.SubjectName = db.Subject.First(x => x.Id == subjectId).Name;
                  testDTOs.Add(testDTO);
               }
               StudentTestDataGrid.ItemsSource = testDTOs;
            }
            else if (header == "Группы")
            {
               var items = db.Group.ToList();
               GroupDataGrid.ItemsSource = items;
            }
            else if (header == "Предметы")
            {
               var items = db.Subject.ToList();
               SubjectDataGrid.ItemsSource = items;
            }
            else if (header == "Пользователи")
            {
               var items = db.User.ToList();
               UserDataGrid.ItemsSource = items;
            }
            else if (header == "Тесты групп")
            {
               List<JunctionGroupTest> items = new();
               var teacherTestList = db.Test.Where(x => x.UserId == _userId.Id).ToList();
               foreach (var teacherTest in teacherTestList)
               {
                  if (db.JunctionGroupTest.Any(x => x.TestId == teacherTest.Id))
                  {
                     var testList = db.JunctionGroupTest.Where(x => x.TestId == teacherTest.Id).ToList();
                     foreach (var test in testList)
                     {
                        items.Add(test);
                     }
                  }
               }
               TeacherTestListDataGrid.ItemsSource = items;
            }
            else if (header == "Редактор тестов")
            {
               var items = db.Test.Where(x => x.UserId == _userId.Id).ToList();
               TestEditorDataGrid.ItemsSource = items;
            }
            else if (header == "Логи")
            {
               var items = db.Log.ToList();
               LogDataGrid.ItemsSource = items;
            }
            else if (header == "Список тестов")
            {
               var tests = db.Test.ToList();
               List<AdminTestDTO> adminTestDTOs = new List<AdminTestDTO>();
               foreach (var test in tests)
               {
                  AdminTestDTO adminTestDTO = new AdminTestDTO();
                  adminTestDTO.Id = test.Id;
                  adminTestDTO.TeacherSurname = db.User.First(x => x.Id == test.UserId).Surname;
                  adminTestDTO.SubjectName = db.Subject.First(x => x.Id == db.User.FirstOrDefault(x => x.Id == test.UserId).SubjectId).Name;
                  adminTestDTOs.Add(adminTestDTO);
               }
               TestListDataGrid.ItemsSource = adminTestDTOs;
            }
         }
      }
      private void GroupSaveButton_Click(object sender, RoutedEventArgs e)
      {
         try
         {
            using (DataContext db = new DataContext())
            {
               if (GroupTextBox.Text == "" || !Regex.IsMatch(GroupTextBox.Text, @"[A-Z]{1}[0-9]{3}"))
                  throw new ArgumentException("Ошибка. Вы не заполнили поле номер группы");
               if (db.Group.Where(x => x.Name == GroupTextBox.Text).Count() > 0)
                  throw new ArgumentException("Данная группа уже существует");

               db.Group.Add(new Model.Group
               {
                  Name = GroupTextBox.Text
               });
               db.SaveChanges();

               AddLogInfo($"Пользователь добавил группу {GroupTextBox.Text}");
            }

            GroupGrid.Visibility = Visibility.Hidden;
            AdminGrid.Visibility = Visibility.Visible;
            DirectoryTabItemFill("Группы");
         }
         catch (Exception ex)
         {
            MessageBox.Show(ex.Message);
         }
      }
      private void GroupAddButton_Click(object sender, RoutedEventArgs e)
      {
         GroupGrid.Visibility = Visibility.Visible;
         AdminGrid.Visibility = Visibility.Hidden;
         GroupTextBox.Text = string.Empty;
      }
      private void GroupBackButton_Click(object sender, RoutedEventArgs e)
      {
         GroupGrid.Visibility = Visibility.Hidden;
         AdminGrid.Visibility = Visibility.Visible;
      }
      private void GroupDeleteButton_Click(object sender, RoutedEventArgs e)
      {
         try
         {
            if (GroupDataGrid.SelectedItem == null)
               throw new ArgumentException("Выберите строку");

            var items = GroupDataGrid.ItemsSource as List<Model.Group>;
            var item = GroupDataGrid.SelectedItem as Model.Group;

            using (DataContext db = new DataContext())
            {
               if (db.JunctionGroupTest.Where(x => x.GroupId == item.Id).Count() > 0)
                  throw new ArgumentException("Выбранную вами группу удалить невозможно");

               var group = db.Group.Find(item.Id);

               db.Group.Remove(group);
               db.SaveChanges();
            }

            AddLogInfo($"Пользователь удалил группу {item.Name}");

            items.Remove(item);

            GroupDataGrid.ItemsSource = items;
            GroupDataGrid.Items.Refresh();
         }
         catch (Exception ex)
         {
            MessageBox.Show(ex.Message);
         }
      }
      private void SubjectAddButton_Click(object sender, RoutedEventArgs e)
      {
         SubjectGrid.Visibility = Visibility.Visible;
         AdminGrid.Visibility = Visibility.Hidden;
         SubjectTextBox.Text = string.Empty;
      }
      private void SubjectSaveButton_Click(object sender, RoutedEventArgs e)
      {
         try
         {
            using (DataContext db = new DataContext())
            {
               if (SubjectTextBox.Text == "" || !Regex.IsMatch(SubjectTextBox.Text, @"[А-яA-z]"))
                  throw new ArgumentException("Ошибка. Вы не заполнили поле предмет");
               if (db.Subject.Where(x => x.Name == SubjectTextBox.Text).Count() > 0)
                  throw new ArgumentException("Данный предмет уже существует");

               db.Subject.Add(new Model.Subject
               {
                  Name = SubjectTextBox.Text
               });
               db.SaveChanges();
            }

            AddLogInfo($"Пользователь добавил предмет {SubjectTextBox.Text}");
            SubjectGrid.Visibility = Visibility.Hidden;
            AdminGrid.Visibility = Visibility.Visible;
            DirectoryTabItemFill("Предметы");
         }
         catch (Exception ex)
         {
            MessageBox.Show(ex.Message);
         }
      }
      private void SubjectBackButton_Click(object sender, RoutedEventArgs e)
      {
         SubjectGrid.Visibility = Visibility.Hidden;
         AdminGrid.Visibility = Visibility.Visible;
      }
      private void SubjectDeleteButton_Click(object sender, RoutedEventArgs e)
      {
         try
         {
            if (SubjectDataGrid.SelectedItem == null)
               throw new ArgumentException("Выберите строку");

            var items = SubjectDataGrid.ItemsSource as List<Model.Subject>;
            var item = SubjectDataGrid.SelectedItem as Model.Subject;

            using (DataContext db = new DataContext())
            {
               if (db.User.Where(x => x.SubjectId == item.Id).Count() > 0)
                  throw new ArgumentException("Выбранный вами предмет удалить невозможно");

               var subject = db.Subject.Find(item.Id);

               db.Subject.Remove(subject);
               db.SaveChanges();
            }

            items.Remove(item);

            AddLogInfo($"Пользователь удалил предмет {item.Name}");
            SubjectDataGrid.ItemsSource = items;
            SubjectDataGrid.Items.Refresh();
         }
         catch (Exception ex)
         {
            MessageBox.Show(ex.Message);
         }
      }
      private void UserAddButton_Click(object sender, RoutedEventArgs e)
      {
         UserGrid.Visibility = Visibility.Visible;
         AdminGrid.Visibility = Visibility.Hidden;

         SurnameTextBox.Clear();
         NameTextBox.Clear();
         PatronymicTextBox.Clear();
         LogInTextBox.Clear();
         PasswordTextBox.Clear();
         RoleComboBox.SelectedItem = -1;
         RoleComboBox.Text = String.Empty;

         using (DataContext db = new DataContext())
         {
            RoleComboBox.ItemsSource = db.Role.ToList();
         }
      }
      private void UserSaveButton_Click(object sender, RoutedEventArgs e)
      {
         try
         {
            using (DataContext db = new DataContext())
            {
               if (SurnameTextBox.Text == "" || !Regex.IsMatch(SurnameTextBox.Text, @"[А-яA-z]"))
                  throw new ArgumentException("Ошибка. Вы не заполнили поле фамилия");
               if (NameTextBox.Text == "" || !Regex.IsMatch(NameTextBox.Text, @"[А-яA-z]"))
                  throw new ArgumentException("Ошибка. Вы не заполнили поле имя");
               if (PatronymicTextBox.Text == "" || !Regex.IsMatch(PatronymicTextBox.Text, @"[А-яA-z]"))
                  throw new ArgumentException("Ошибка. Вы не заполнили поле отчество");
               if (LogInTextBox.Text == "" || !Regex.IsMatch(LogInTextBox.Text, @"[A-z]"))
                  throw new ArgumentException("Ошибка. Вы не заполнили поле логин");
               if (db.User.Where(x => x.Login == LogInTextBox.Text).Count() > 0 && _changeUserInfo == false)
                  throw new ArgumentException("Данный логин уже существует");
               if (db.User.Where(x => x.Login == LogInTextBox.Text).Count() > 1 && _changeUserInfo == true)
                  throw new ArgumentException("Данный логин уже существует");
               if (PasswordTextBox.Text == "" || !Regex.IsMatch(PasswordTextBox.Text, @"[A-z0-9]"))
                  throw new ArgumentException("Ошибка. Вы не заполнили поле пароль");
               if (RoleComboBox.Text == "")
                  throw new ArgumentException("Ошибка. Вы не выбрали роль");

               var role = RoleComboBox.SelectedItem as Model.Role;
               AddLogInfo($"Добавлен новый пользователь {SurnameTextBox.Text}");
               if (role.Name == "Admin")
               {
                  if (_changeUserInfo == false)
                  {
                     db.User.Add(new User
                     {
                        Surname = SurnameTextBox.Text,
                        Name = NameTextBox.Text,
                        Patronymic = PatronymicTextBox.Text,
                        Login = LogInTextBox.Text,
                        Password = PasswordTextBox.Text,
                        RoleId = (RoleComboBox.SelectedItem as Role).Id,
                     });
                  }
                  else
                  {
                     var user = db.User.Find(_currentUserInfo.Id);
                     user.Surname = SurnameTextBox.Text;
                     user.Name = NameTextBox.Text;
                     user.Patronymic = PatronymicTextBox.Text;
                     user.Login = LogInTextBox.Text;
                     user.Password = PasswordTextBox.Text;
                     user.RoleId = (RoleComboBox.SelectedItem as Role).Id;
                     user.SubjectId = null;
                     user.GroupId = null;
                  }
               }
               else if (role.Name == "Teacher")
               {

                  if (SubjectComboBox.Text == "")
                     throw new ArgumentException("Ошибка. Вы не выбрали предмет");

                  if (_changeUserInfo == false)
                  {
                     db.User.Add(new Model.User
                     {
                        Surname = SurnameTextBox.Text,
                        Name = NameTextBox.Text,
                        Patronymic = PatronymicTextBox.Text,
                        Login = LogInTextBox.Text,
                        Password = PasswordTextBox.Text,
                        RoleId = (RoleComboBox.SelectedItem as Role).Id,
                        SubjectId = (SubjectComboBox.SelectedItem as Subject).Id,
                     });
                  }
                  else
                  {
                     var user = db.User.Find(_currentUserInfo.Id);
                     user.Surname = SurnameTextBox.Text;
                     user.Name = NameTextBox.Text;
                     user.Patronymic = PatronymicTextBox.Text;
                     user.Login = LogInTextBox.Text;
                     user.Password = PasswordTextBox.Text;
                     user.RoleId = (RoleComboBox.SelectedItem as Role).Id;
                     user.SubjectId = (SubjectComboBox.SelectedItem as Subject).Id;
                     user.GroupId = null;
                  }
               }
               else if (role.Name == "Student")
               {
                  if (GroupComboBox.Text == "")
                     throw new ArgumentException("Ошибка. Вы не выбрали группу");
                  if (_changeUserInfo == false)
                  {
                     db.User.Add(new Model.User
                     {
                        Surname = SurnameTextBox.Text,
                        Name = NameTextBox.Text,
                        Patronymic = PatronymicTextBox.Text,
                        Login = LogInTextBox.Text,
                        Password = PasswordTextBox.Text,
                        RoleId = (RoleComboBox.SelectedItem as Role).Id,
                        GroupId = (GroupComboBox.SelectedItem as Model.Group).Id,
                     });
                  }
                  else
                  {
                     var user = db.User.Find(_currentUserInfo.Id);
                     user.Surname = SurnameTextBox.Text;
                     user.Name = NameTextBox.Text;
                     user.Patronymic = PatronymicTextBox.Text;
                     user.Login = LogInTextBox.Text;
                     user.Password = PasswordTextBox.Text;
                     user.RoleId = (RoleComboBox.SelectedItem as Role).Id;
                     user.GroupId = (GroupComboBox.SelectedItem as Model.Group).Id;
                     user.SubjectId = null;
                  }
               }
               db.SaveChanges();
            }

            _changeUserInfo = false;
            UserGrid.Visibility = Visibility.Hidden;
            AdminGrid.Visibility = Visibility.Visible;
            DirectoryTabItemFill("Пользователи");
         }
         catch (Exception ex)
         {
            MessageBox.Show(ex.Message);
         }
      }
      private void UserBackButton_Click(object sender, RoutedEventArgs e)
      {
         UserGrid.Visibility = Visibility.Hidden;
         AdminGrid.Visibility = Visibility.Visible;

         GroupComboBox.IsEnabled = false;
         GroupComboBox.SelectedItem = -1;
         GroupComboBox.Text = String.Empty;
         SubjectComboBox.IsEnabled = false;
         SubjectComboBox.SelectedItem = -1;
         SubjectComboBox.Text = String.Empty;
      }
      private void UserDeleteButton_Click(object sender, RoutedEventArgs e)
      {
         try
         {
            if (UserDataGrid.SelectedItem == null)
               throw new ArgumentException("Выберите строку");

            var items = UserDataGrid.ItemsSource as List<Model.User>;
            var item = UserDataGrid.SelectedItem as Model.User;

            using (DataContext db = new DataContext())
            {
               if (db.Test.Where(x => x.UserId == item.Id).Count() > 0)
                  throw new ArgumentException("Выбранный вами пользователь не может быть удален");

               var user = db.User.Find(item.Id);

               db.User.Remove(user);
               db.SaveChanges();
            }

            items.Remove(item);

            AddLogInfo($"Пользователь удалил пользователя {item.Id}");

            UserDataGrid.ItemsSource = items;
            UserDataGrid.Items.Refresh();
         }
         catch (Exception ex)
         {
            MessageBox.Show(ex.Message);
         }
      }
      private void UserChangeButton_Click(object sender, RoutedEventArgs e)
      {
         UserGrid.Visibility = Visibility.Visible;
         AdminGrid.Visibility = Visibility.Hidden;
         _changeUserInfo = true;
         try
         {
            if (UserDataGrid.SelectedItem == null)
               throw new ArgumentException("Выберите строку");

            var items = UserDataGrid.ItemsSource as List<Model.User>;
            var item = UserDataGrid.SelectedItem as Model.User;

            _currentUserInfo.Id = item.Id;
            SurnameTextBox.Text = item.Surname;
            NameTextBox.Text = item.Name;
            PatronymicTextBox.Text = item.Patronymic;
            LogInTextBox.Text = item.Login;
            PasswordTextBox.Text = item.Password;

            using (DataContext db = new DataContext())
            {
               RoleComboBox.ItemsSource = db.Role.ToList();
               RoleComboBox.SelectedItem = db.Role.Find(item.RoleId);

               var role = db.Role.First(x => x.Id == item.RoleId);
               if (role.Name == "Admin")
               {
               }
               else if (role.Name == "Teacher")
               {
                  SubjectComboBox.ItemsSource = db.Subject.ToList();
                  SubjectComboBox.IsEnabled = true;
                  SubjectComboBox.SelectedItem = db.Subject.Find(item.SubjectId);
               }
               else if (role.Name == "Student")
               {
                  GroupComboBox.ItemsSource = db.Group.ToList();
                  GroupComboBox.IsEnabled = true;
                  GroupComboBox.SelectedItem = db.Group.Find(item.GroupId);
               }

               AddLogInfo($"Пользователь изменил данные для пользователя: {item.Id}");
            }
         }
         catch (Exception ex)
         {
            MessageBox.Show(ex.Message);
         }
      }
      private void RoleComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
      {
         using (DataContext db = new DataContext())
         {
            var role = RoleComboBox.SelectedItem as Model.Role;
            if (role != null && role.Name == "Admin")
            {
               GroupComboBox.IsEnabled = false;
               GroupComboBox.SelectedItem = -1;
               GroupComboBox.Text = String.Empty;
               SubjectComboBox.IsEnabled = false;
               SubjectComboBox.SelectedItem = -1;
               SubjectComboBox.Text = String.Empty;
            }
            else if (role != null && role.Name == "Student")
            {
               GroupComboBox.IsEnabled = true;
               SubjectComboBox.IsEnabled = false;
               SubjectComboBox.SelectedItem = -1;
               SubjectComboBox.Text = String.Empty;
               GroupComboBox.ItemsSource = db.Group.ToList();
            }
            else if (role != null && role.Name == "Teacher")
            {
               SubjectComboBox.IsEnabled = true;
               GroupComboBox.IsEnabled = false;
               GroupComboBox.SelectedItem = -1;
               GroupComboBox.Text = String.Empty;
               SubjectComboBox.ItemsSource = db.Subject.ToList();
            }
         }
      }
      private void TestEditorAddButton_Click(object sender, RoutedEventArgs e)
      {
         TestEditorGrid.Visibility = Visibility.Visible;
         TeacherGrid.Visibility = Visibility.Hidden;
      }
      private void TestEditorDeleteButton_Click(object sender, RoutedEventArgs e)
      {
         try
         {
            if (TestEditorDataGrid.SelectedItem == null)
               throw new ArgumentException("Выберите строку");

            var items = TestEditorDataGrid.ItemsSource as List<Model.Test>;
            var item = TestEditorDataGrid.SelectedItem as Model.Test;

            using (DataContext db = new DataContext())
            {
               if (db.JunctionGroupTest.Where(x => x.TestId == item.Id).Count() > 0)
                  throw new ArgumentException("Выбранный вами тест не может быть удален");

               var test = db.Test.Find(item.Id);

               db.Test.Remove(test);
               db.SaveChanges();
            }

            items.Remove(item);

            AddLogInfo($"Пользователь удалил тест: {item.Id}");
            TestEditorDataGrid.ItemsSource = items;
            TestEditorDataGrid.Items.Refresh();
         }
         catch (Exception ex)
         {
            MessageBox.Show(ex.Message);
         }
      }
      private void TestEditorBackButton_Click(object sender, RoutedEventArgs e)
      {
         TestEditorGrid.Visibility = Visibility.Hidden;
         TeacherGrid.Visibility = Visibility.Visible;
         TestNameTextBox.Text = string.Empty;
      }
      private void TestEditorSaveButton_Click(object sender, RoutedEventArgs e)
      {
         try
         {
            using (DataContext db = new DataContext())
            {
               if (TestNameTextBox.Text == "" || !Regex.IsMatch(TestNameTextBox.Text, @"[А-яA-z]"))
                  throw new ArgumentException("Ошибка. Вы не ввели название теста");
               if (db.Test.Where(x => x.Name == TestNameTextBox.Text && x.UserId == _currentUserInfo.Id).Count() > 0)
                  throw new ArgumentException("Данное название теста у преподавателя уже существует");

               db.Test.Add(new Test
               {
                  Name = TestNameTextBox.Text,
                  UserId = _currentUserInfo.Id
               });
               db.SaveChanges();
            }

            AddLogInfo($"Пользователь добавил тест: {TestNameTextBox.Text}");
            TestEditorGrid.Visibility = Visibility.Hidden;
            TeacherGrid.Visibility = Visibility.Visible;
            DirectoryTabItemFill("Редактор тестов");
         }
         catch (Exception ex)
         {
            MessageBox.Show(ex.Message);
         }
      }
      private void QuestionAddButton_Click(object sender, RoutedEventArgs e)
      {
         NumberQuestion question = new NumberQuestion();
         question.Name = $"Вопрос №{_numberQuestions.Count + 1}";
         _numberQuestions.Add(question);
         questionListBox.Items.Add(question.Name);
         questionListBox.SelectedIndex = questionListBox.Items.Count - 1;
         QuestionTextBox.Text = String.Empty;
         AnswerTextBox.Text = String.Empty;
         WrongAnswerTextBox.Text = String.Empty;
      }
      private void QuestionDeleteButton_Click(object sender, RoutedEventArgs e)
      {
         var numberQuestion = _numberQuestions.Find(x => x.Name == questionListBox.SelectedValue);
         using (DataContext db = new())
         {
            if (numberQuestion.QuestionId != null)
            {
               var question = db.Question.Find(numberQuestion.QuestionId);
               AddLogInfo($"Пользователь удалил вопрос: {question.Text}");
               db.Question.Remove(question);
               db.SaveChanges();
            }
         }
         _numberQuestions.Clear();
         questionListBox.Items.Clear();
         GetItemList();
      }
      private void QuestionSaveButton_Click(object sender, RoutedEventArgs e)
      {
         try
         {
            using (DataContext db = new DataContext())
            {
               var numberQuestion = _numberQuestions.Find(x => x.Name == questionListBox.SelectedValue);

               if (QuestionTextBox.Text == "")
                  throw new ArgumentException("Ошибка. Вы не ввели вопрос");
               if (db.Question.Where(x => x.Name == QuestionTextBox.Text && x.TestId == _currentTestId && numberQuestion.QuestionId == null).Count() > 0)
                  throw new ArgumentException("Данный вопрос уже существует в тесте");
               if (db.Question.Where(x => x.Name == QuestionTextBox.Text && x.TestId == _currentTestId && numberQuestion.QuestionId != null).Count() > 1)
                  throw new ArgumentException("Данный вопрос уже существует в тесте");
               if (AnswerTextBox.Text == "")
                  throw new ArgumentException("Ошибка. Вы не ввели ответ");
               if (WrongAnswerTextBox.Text == "")
                  throw new ArgumentException("Ошибка. Вы не ввели вопрос");

               if (numberQuestion.QuestionId == null)
               {
                  
                  db.Question.Add(new Model.Question
                  {
                     Name = QuestionTextBox.Text,
                     Text = WrongAnswerTextBox.Text,
                     Answer = AnswerTextBox.Text,
                     TestId = _currentTestId
                  });
                  db.SaveChanges();
                  AddLogInfo($"Пользователь добавил вопрос {Name} для теста {db.Test.Find(_currentTestId).Name}");
                  _numberQuestions.Find(x => x.Name == questionListBox.SelectedValue).QuestionId = db.Question.First(x => x.Name == QuestionTextBox.Text && x.TestId == _currentTestId).Id;
               }
               else if (numberQuestion.QuestionId != null)
               {
                  var question = db.Question.Find(numberQuestion.QuestionId);
                  question.Answer = AnswerTextBox.Text;
                  question.Text = WrongAnswerTextBox.Text;
                  question.Name = QuestionTextBox.Text;
                  AddLogInfo($"Пользователь обновил вопрос {Name} для теста {db.Test.Find(_currentTestId).Name}");
                  db.SaveChanges();
               }
            }
         }
         catch (Exception ex)
         {
            MessageBox.Show(ex.Message);
         }
      }
      private void TestEditorOpenButton_Click(object sender, RoutedEventArgs e)
      {
         QuestionGrid.Visibility = Visibility.Visible;
         TeacherGrid.Visibility = Visibility.Hidden;
         _numberQuestions.Clear();
         questionListBox.Items.Clear();
         GetTestId(true);
         GetItemList();
      }
      private void QuestionBackButton_Click(object sender, RoutedEventArgs e)
      {
         using (DataContext db = new DataContext()) 
         {
            var roleName = db.Role.First(x => x.Id == _currentUserInfo.RoleId).Name;
            if (roleName == "Teacher")
            {
               QuestionGrid.Visibility = Visibility.Hidden;
               TeacherGrid.Visibility = Visibility.Visible;
            }
            else if(roleName == "Admin")
            {
               QuestionGrid.Visibility = Visibility.Hidden;
               AdminGrid.Visibility = Visibility.Visible;
            }
         }
      }
      private void GetTestId(bool root)
      {
         if (root)
         {
            var items = TestEditorDataGrid.ItemsSource as List<Test>;
            var item = TestEditorDataGrid.SelectedItem as Test;
            _currentTestId = item.Id;
         }
         else
         {
            var items = TestListDataGrid.ItemsSource as List<AdminTestDTO>;
            var item = TestListDataGrid.SelectedItem as AdminTestDTO;
            _currentTestId = item.Id;
         }
      }
      private void GetItemList()
      {
         using (DataContext db = new())
         {
            var questionList = db.Question.Where(x => x.TestId == _currentTestId).ToList();
            int count = 1;
            foreach (var question in questionList)
            {
               NumberQuestion numberQuestion = new NumberQuestion();
               numberQuestion.Name = $"Вопрос №{count}";
               numberQuestion.QuestionId = question.Id;
               _numberQuestions.Add(numberQuestion);
               questionListBox.Items.Add(numberQuestion.Name);
               count++;
            }
            if (questionListBox.Items.Count > 0)
            {
               questionListBox.SelectedIndex = 0;
               var question = db.Question.Find(_numberQuestions[0].QuestionId);
               QuestionTextBox.Text = question.Name;
               AnswerTextBox.Text = question.Answer;
               WrongAnswerTextBox.Text = question.Text;
            }
            if (questionListBox.Items.Count == 0)
            {
               QuestionTextBox.Text = String.Empty;
               AnswerTextBox.Text = String.Empty;
               WrongAnswerTextBox.Text = String.Empty;
            }
         }
      }
      private void questionListBox_MouseUp(object sender, MouseButtonEventArgs e)
      {
         var numberQuestion = _numberQuestions.Find(x => x.Name == questionListBox.SelectedValue);
         using (DataContext db = new())
         {
            if (numberQuestion.QuestionId != null)
            {
               var question = db.Question.Find(numberQuestion.QuestionId);
               QuestionTextBox.Text = question.Name;
               AnswerTextBox.Text = question.Answer;
               WrongAnswerTextBox.Text = question.Text;
            }
            if (numberQuestion.QuestionId == null)
            {
               QuestionTextBox.Text = String.Empty;
               AnswerTextBox.Text = String.Empty;
               WrongAnswerTextBox.Text = String.Empty;
            }
         }
      }
      private void WrongAnswerTextBox_KeyDown(object sender, KeyEventArgs e)
      {
         if (e.Key == Key.Enter)
         {
            WrongAnswerTextBox.AppendText(Environment.NewLine);
            WrongAnswerTextBox.SelectionStart = WrongAnswerTextBox.Text.Length;
         }
      }
      private void TeacherAddTestGroupButton_Click(object sender, RoutedEventArgs e)
      {
         TeacherGrid.Visibility = Visibility.Hidden;
         JunctionGroupTestGrid.Visibility = Visibility.Visible;

         using (DataContext db = new DataContext())
         {
            TestComboBox.ItemsSource = db.Test.Where(x => x.UserId == _currentUserInfo.Id).ToList();
            GroupTestComboBox.ItemsSource = db.Group.ToList();
         }
      }
      private void TeacherDeleteTestGroupButton_Click(object sender, RoutedEventArgs e)
      {
         try
         {
            if (TeacherTestListDataGrid.SelectedItem == null)
               throw new ArgumentException("Выберите строку");

            var items = TeacherTestListDataGrid.ItemsSource as List<Model.JunctionGroupTest>;
            var item = TeacherTestListDataGrid.SelectedItem as Model.JunctionGroupTest;

            using (DataContext db = new DataContext())
            {
               var test = db.JunctionGroupTest.Find(item.Id);

               db.JunctionGroupTest.Remove(test);
               db.SaveChanges();

               AddLogInfo($"Пользователь удалил тест {item.Test} для группы {db.Test.Find(item.GroupId).Name}");
            }
            
            items.Remove(item);

            TeacherTestListDataGrid.ItemsSource = items;
            TeacherTestListDataGrid.Items.Refresh();
         }
         catch (Exception ex)
         {
            MessageBox.Show(ex.Message);
         }
      }
      private void JunctionTestGroupSaveButton_Click(object sender, RoutedEventArgs e)
      {
         try
         {
            using (DataContext db = new DataContext())
            {
               if (TestComboBox.Text == "")
                  throw new ArgumentException("Ошибка. Вы не выбрали тест");
               if (GroupTestComboBox.Text == "")
                  throw new ArgumentException("Ошибка. Вы не выбрали группу");
               int testId = (TestComboBox.SelectedItem as Test).Id;
               int groupId = (GroupTestComboBox.SelectedItem as Model.Group).Id;
               if (db.JunctionGroupTest.Any(x => x.TestId == testId && x.GroupId == groupId) == false)
               {
                  db.JunctionGroupTest.Add(new Model.JunctionGroupTest
                  {
                     TestId = (TestComboBox.SelectedItem as Test).Id,
                     GroupId = (GroupTestComboBox.SelectedItem as Model.Group).Id
                  });
                  AddLogInfo($"Пользователь добавил тест {db.Test.Find((TestComboBox.SelectedItem as Test).Id).Name} для группы {db.Group.Find((GroupTestComboBox.SelectedItem as Model.Group).Id).Name}");
                  db.SaveChanges();
               }
               else
                  throw new ArgumentException("Ошибка. Вы данный тест уже существует у группы");
            }

            JunctionGroupTestGrid.Visibility = Visibility.Hidden;
            TeacherGrid.Visibility = Visibility.Visible;
            DirectoryTabItemFill("Тесты групп");
         }
         catch (Exception ex)
         {
            MessageBox.Show(ex.Message);
         }
      }
      private void JunctionTestGroupBackButton_Click(object sender, RoutedEventArgs e)
      {
         TeacherGrid.Visibility = Visibility.Visible;
         JunctionGroupTestGrid.Visibility = Visibility.Hidden;
      }
      private void StartTestButton_Click(object sender, RoutedEventArgs e)
      {
         using (DataContext db = new DataContext())
         {
            var tests = StudentTestDataGrid.ItemsSource as List<Model.TestDTO>;
            var test = StudentTestDataGrid.SelectedItem as Model.TestDTO;
            if (db.Results.Any(x => x.UserId == _currentUserInfo.Id && x.TestId == test.TestId))
            {
               MessageBox.Show("Данный тест уже пройден");
            }
            else
            {
               StudentGrid.Visibility = Visibility.Hidden;
               StartTestGrid.Visibility = Visibility.Visible;

               var items = StudentTestDataGrid.ItemsSource as List<TestDTO>;
               var item = StudentTestDataGrid.SelectedItem as TestDTO;
               _currentTestId = item.TestId;
               NameTestLabel.Content = "Тест: " + db.Test.First(x => x.Id == _currentTestId).Name;
            }
         }
      }
      private void BackTestButton_Click(object sender, RoutedEventArgs e)
      {
         StudentGrid.Visibility = Visibility.Visible;
         StartTestGrid.Visibility = Visibility.Hidden;
      }
      private void StartTestStudentButton_Click(object sender, RoutedEventArgs e)
      {
         _startTestTime = DateTime.Now;
         TestQuestionGrid.Visibility = Visibility.Visible;
         StartTestGrid.Visibility = Visibility.Hidden;
         _questionList.Clear();
         using (DataContext db = new DataContext())
         {
            AddLogInfo($"Пользователь начал тест{db.Test.Find(_currentTestId).Name}");
         }
         GetQuestionList();
         GenerateFormQuestion(_indexQuestion);
      }
      private void GetQuestionList()
      {
         using (DataContext db = new DataContext())
         {
            _questionList = db.Question.Where(x => x.TestId == _currentTestId).ToList();
         }
      }
      private void AcceptQuestionButton_Click(object sender, RoutedEventArgs e)
      {
         if (QuestionTestListBox.SelectedValue.ToString() == _questionList[_indexQuestion].Answer)
         {
            _rightAnswers++;
         }
         _indexQuestion++;
         GenerateFormQuestion(_indexQuestion);
      }
      private void GenerateFormQuestion(int index)
      {
         if (_questionList.Count > _indexQuestion)
         {
            QuestionTestListBox.Items.Clear();
            QuestionNameLabel.Content = $"Вопрос №{index + 1}: {_questionList[index].Name}";
            var answerItems = _questionList[index].Text.Split(Environment.NewLine);
            foreach (var wrongAnswer in answerItems)
            {
               QuestionTestListBox.Items.Add(wrongAnswer);
            }
            QuestionTestListBox.Items.Insert(new Random().Next(0, answerItems.Count() - 1), _questionList[index].Answer);
         }
         else
         {
            using (DataContext db = new DataContext())
            {
               AddLogInfo($"Пользователь закончил прохождение теста {db.Test.Find(_currentTestId).Name}");
            }
            _finistTestTime = DateTime.Now;
            StudentResults();
         }
      }
      private void StudentResults()
      {
         using (DataContext db = new DataContext())
         {
            db.Results.Add(new Results
            {
               AmountOfAnswers = _questionList.Count,
               AmountOfQuestions = _rightAnswers,
               UserId = _currentUserInfo.Id,
               TestId = _currentTestId,
               Time = _finistTestTime - _startTestTime
            });
            db.SaveChanges();

            TestQuestionGrid.Visibility = Visibility.Hidden;
            StudentResultGrid.Visibility = Visibility.Visible;
            StudentNameLabel.Content = "Пользователи: " + _currentUserInfo.Surname + " " + _currentUserInfo.Name;
            StudentTestNameLabel.Content = "Тест: " + db.Test.Find(_currentTestId).Name;
            AmountOfQuestionLabel.Content = "Кол-во вопросов: " + _questionList.Count;
            AmountOfRightAnwsersLabel.Content = "Кол-во правильных ответов: " + _rightAnswers;
            TimeLabel.Content = "Затраченное время: " + (_finistTestTime - _startTestTime).ToString().Substring(0, 8);
         }
         _questionList.Clear();
      }
      private void BackStudentButton_Click(object sender, RoutedEventArgs e)
      {
         StudentResultGrid.Visibility = Visibility.Hidden;
         StudentGrid.Visibility = Visibility.Visible;
      }
      private void CheckTestResultButton_Click(object sender, RoutedEventArgs e)
      {
         using (DataContext db = new DataContext())
         {
            var tests = StudentTestDataGrid.ItemsSource as List<Model.TestDTO>;
            var test = StudentTestDataGrid.SelectedItem as Model.TestDTO;

            var result = db.Results.FirstOrDefault(x => x.UserId == _currentUserInfo.Id && x.TestId == test.TestId);
            if (result != null)
            {
               StudentGrid.Visibility = Visibility.Hidden;
               StudentResultGrid.Visibility = Visibility.Visible;
               StudentNameLabel.Content = "Пользователи: " + _currentUserInfo.Surname + " " + _currentUserInfo.Name;
               StudentTestNameLabel.Content = "Тест: " + db.Test.Find(result.TestId).Name;
               AmountOfQuestionLabel.Content = "Кол-во вопросов: " + result.AmountOfQuestions;
               AmountOfRightAnwsersLabel.Content = "Кол-во правильных ответов: " + result.AmountOfAnswers;
               TimeLabel.Content = "Затраченное время: " + (result.Time).ToString().Substring(0, 8);
            }
            else
               MessageBox.Show("Данный тест еще не был пройден");
            }
         }
      private void TestGroupResultButton_Click(object sender, RoutedEventArgs e)
      {
         List<ResultsDTO> resultsDTOs = new List<ResultsDTO>();
         TeacherGrid.Visibility = Visibility.Hidden;
         GroupResultGrid.Visibility = Visibility.Visible;

         var tests = TeacherTestListDataGrid.ItemsSource as List<Model.JunctionGroupTest>;
         var test = TeacherTestListDataGrid.SelectedItem as Model.JunctionGroupTest;
         using (DataContext db = new DataContext())
         {
            var studentsGroup = db.User.Where(x => x.GroupId == test.GroupId).ToList();
            foreach (var student in studentsGroup)
            {
               if (db.Results.Any(x => x.UserId == student.Id && x.TestId == test.TestId))
               {
                  var result = db.Results.First(x => x.UserId == student.Id && x.TestId == test.TestId);
                  ResultsDTO resultsDTO = new ResultsDTO();
                  resultsDTO.Id = result.Id;
                  resultsDTO.Surname = student.Surname;
                  resultsDTO.AmountOfQuestions = result.AmountOfQuestions;
                  resultsDTO.AmountOfAnswers = result.AmountOfAnswers;
                  resultsDTO.Time = result.Time;
                  resultsDTOs.Add(resultsDTO);
               }
            }
         }
         TestResultGroupListDataGrid.ItemsSource = resultsDTOs;
      }
      private void TestResultBackButton_Click(object sender, RoutedEventArgs e)
      {
         TeacherGrid.Visibility = Visibility.Visible;
         GroupResultGrid.Visibility = Visibility.Hidden;
      }
      private void TestResultDeleteGroupButton_Click(object sender, RoutedEventArgs e)
      {
         try
         {
            if (TestResultGroupListDataGrid.SelectedItem == null)
               throw new ArgumentException("Выберите строку");

            var tests = TestResultGroupListDataGrid.ItemsSource as List<ResultsDTO>;
            var test = TestResultGroupListDataGrid.SelectedItem as ResultsDTO;

            using (DataContext db = new DataContext())
            {
               var studentResult = db.Results.Find(test.Id);

               db.Results.Remove(studentResult);
               db.SaveChanges();
            }

            tests.Remove(test);
            AddLogInfo($"Пользователь удалил результат теста для {test.Surname}");
            TestResultGroupListDataGrid.ItemsSource = tests;
            TestResultGroupListDataGrid.Items.Refresh();
         }
         catch (Exception ex)
         {
            MessageBox.Show(ex.Message);
         }
      }
      private void AdminQuestionButton_Click(object sender, RoutedEventArgs e)
      {
         QuestionGrid.Visibility = Visibility.Visible;
         AdminGrid.Visibility = Visibility.Hidden;
         _numberQuestions.Clear();
         questionListBox.Items.Clear();
         GetTestId(false);
         GetItemList();
      }
      private void TestDeleteAdminButton_Click(object sender, RoutedEventArgs e)
      {
         try
         {
            if (TestListDataGrid.SelectedItem == null)
               throw new ArgumentException("Выберите строку");

            var tests = TestListDataGrid.ItemsSource as List<AdminTestDTO>;
            var test = TestListDataGrid.SelectedItem as AdminTestDTO;

            using (DataContext db = new DataContext())
            {
               var testItem = db.Test.Find(test.Id);

               db.Test.Remove(testItem);
               db.SaveChanges();
               AddLogInfo($"Пользователь удалил тест {testItem.Name}");
            }
            tests.Remove(test);
            
            TestListDataGrid.ItemsSource = tests;
            TestListDataGrid.Items.Refresh();
         }
         catch (Exception ex)
         {
            MessageBox.Show(ex.Message);
         }
      }
      private void SqlRequestTextBox_KeyDown(object sender, KeyEventArgs e)
      {
         if (e.Key == Key.Enter)
         {
            try
            {
               var sqlWords = SqlRequestTextBox.Text.Split(' ');
               using (SqlConnection connection = new SqlConnection(_connectionString))
               {
                  connection.Open();
                  SqlCommand command = new SqlCommand(SqlRequestTextBox.Text, connection);
                  SqlDataReader reader = command.ExecuteReader();
                  System.Data.DataTable table = new System.Data.DataTable();
                  table.Load(reader);
                  SqlRequestDataGrid.ItemsSource = table.DefaultView;
               }
               AddLogInfo($"Пользователь отправил запрос в бд {SqlRequestTextBox.Text}");
            }
            catch (Exception ex)
            {
               MessageBox.Show(ex.Message);
            }
            
         }
      }
      private void PrintResultStudentButton_Click(object sender, RoutedEventArgs e)
      {
         var wordApp = new Microsoft.Office.Interop.Word.Application();
         wordApp.Visible = false;
         try
         {
            using (DataContext db = new DataContext())
            {
               
               var tests = StudentTestDataGrid.ItemsSource as List<Model.TestDTO>;
               var test = StudentTestDataGrid.SelectedItem as Model.TestDTO;
               var result = db.Results.FirstOrDefault(x => x.UserId == _currentUserInfo.Id && x.TestId == test.TestId);

               var wordDocument = wordApp.Documents.Open($"{Environment.CurrentDirectory}/Templates/StudentResult.docx");

               ReplaceWordStub("{Surname}", _currentUserInfo.Surname, wordDocument);
               ReplaceWordStub("{TestName}", db.Test.Find(result.TestId).Name, wordDocument);
               ReplaceWordStub("{AmountOfQuestion}", result.AmountOfQuestions.ToString(), wordDocument);
               ReplaceWordStub("{AmountOfAnswers}", result.AmountOfAnswers.ToString(), wordDocument);
               ReplaceWordStub("{Time}", (result.Time).ToString().Substring(0, 8), wordDocument);

               wordDocument.SaveAs2($"{Environment.CurrentDirectory}/Documents/StudentResult.docx");
               wordApp.Visible = true;
               AddLogInfo($"Пользователь сделал экспорт результатов теста {test.TestId}");
            }
         }
         catch (Exception ex)
         {
            MessageBox.Show(ex.Message);
         }
      }
      private void ReplaceWordStub(string stubToReplace, string text, Microsoft.Office.Interop.Word.Document wordDocument)
      {
         var range = wordDocument.Content;
         range.Find.ClearFormatting();
         range.Find.Execute(FindText: stubToReplace, ReplaceWith: text);
      }
      private void LogOutStudentButton_Click(object sender, RoutedEventArgs e)
      {
         StudentGrid.Visibility = Visibility.Hidden;
         AuthorizationGrid.Visibility = Visibility.Visible;
         AddLogInfo($"Пользователь вышел из системы");
      }
      private void LogOutTeacherButton_Click(object sender, RoutedEventArgs e)
      {
         TeacherGrid.Visibility = Visibility.Hidden;
         AuthorizationGrid.Visibility = Visibility.Visible;
         AddLogInfo($"Пользователь вышел из системы");
      }
      private void LogOutAdminButton_Click(object sender, RoutedEventArgs e)
      {
         AdminGrid.Visibility = Visibility.Hidden;
         AuthorizationGrid.Visibility = Visibility.Visible;
      }
      private void BackupDbButton_Click(object sender, RoutedEventArgs e)
      {
         try
         {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
               string request = "BACKUP DATABASE AutomatedTesting " +
               $"TO DISK = '{Environment.CurrentDirectory}/sql_backup/AutomatedTesting_{DateTime.Now:yyyyMMddHHmmss}.bak' " +
               "WITH FORMAT, INIT, SKIP, NOREWIND, NOUNLOAD, STATS = 10";
               connection.Open();
               using (SqlCommand command = new SqlCommand(request, connection))
               {
                  command.ExecuteNonQuery();
               }
               AddLogInfo($"Пользователь сделал full backup");
               MessageBox.Show("Backup успешно выполнен");
            }
         }
         catch (Exception ex)
            {
               MessageBox.Show(ex.Message);
            }
      }
      private void BackupLogDbButton_Click(object sender, RoutedEventArgs e)
      {
         try
         {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
               string request = "BACKUP LOG AutomatedTesting " +
               $"TO DISK = '{Environment.CurrentDirectory}/sql_backup/AutomatedTesting_{DateTime.Now:yyyyMMddHHmmss}.trn' ";
               connection.Open();
               using (SqlCommand command = new SqlCommand(request, connection))
               {
                  command.ExecuteNonQuery();
               }
               AddLogInfo($"Пользователь сделал backup журнала транзакций");
               MessageBox.Show("Журнал транзакций успешно сохранен");
            }
         }
         catch (Exception ex)
         {
            MessageBox.Show(ex.Message);
         }
      }
      private void AddLogInfo(string log)
      {
         using (DataContext db = new DataContext()) 
         {
            db.Log.Add(new Log
            {
               LogName = log,
               Time = DateTime.Now,
               UserId = _currentUserInfo.Id,
            });

            db.SaveChanges();
         }
      }
   }
}
