using SimpleManager.Commands;
using SimpleManager.Context;
using SimpleManager.Handlers;
using SimpleManager.Models;
using SimpleManager.Views.OtherViews;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace SimpleManager.ViewModels.TableViewModels
{
    class AuthVM : BaseVM
    {

        #region Properties

        /// <summary>
        /// Возвращает или задает текущего авторизованного пользователя
        /// </summary>
        public static Employee CurrentUser { get; set; }

        public string Login { get; set; }
        public string Password { get; set; }

        #endregion

        private bool authBttnPressed = false;

        #region Commands

        /* Асинхронно выполняет запрос к БД с целью авторизации пользователя.
         * В новом потоке генерирует сообщение для пользователя о ходе авторизации.
         * Кнопка становится временно недоступна до итога авторизации.
         * В случае провала авторизации выдает соответствующее сообщение.
         */
        public DelegateCommand Authorizate => new DelegateCommand(async (obj) =>
        {
            authBttnPressed = true;
            new Thread(() => MessageBox.Show("Выполняется авторизация, пожалуйста, подождите")).Start();
            await Task.Run(() => CurrentUser = SimpleManagerContext.DataBase.Employees
                .Where(employe => employe.Password == Password && employe.Login == Login)
                .FirstOrDefault());
            authBttnPressed = false;
            if (CurrentUser == null)
            {
                MessageBox.Show("Неправильный логин или пароль");
                return;
            }
            NavigationHandler.NavigationService.Navigate(new MenuView());

        }, (obj) => !string.IsNullOrEmpty(Login) && !string.IsNullOrEmpty(Password) && !authBttnPressed);

        public DelegateCommand Exit => new DelegateCommand((obj) =>
        {
            Application.Current.Shutdown();
        });

        #endregion
    }
}
