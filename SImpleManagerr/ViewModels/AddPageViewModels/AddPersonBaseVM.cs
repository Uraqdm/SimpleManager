using SimpleManager.Models;

namespace SimpleManager.ViewModels.AddPageViewModels
{
    abstract class AddPersonBaseVM: BaseVM
    {
        #region Properties

        protected Person model;
        public string FirstName
        {
            get => model.FirstName;
            set
            {
                model.FirstName = value;
                OnPropertyChanged();
            }
        }
        public string MiddleName
        {
            get => model.MiddleName;
            set
            {
                model.MiddleName = value;
                OnPropertyChanged();
            }
        }
        public string LastName
        {
            get => model.LastName;
            set
            {
                model.LastName = value;
                OnPropertyChanged();
            }
        }
        public string PhoneNumber
        {
            get => model.PhoneNumber;
            set
            {
                model.PhoneNumber = value;
                OnPropertyChanged();
            }
        }
        public string Email
        {
            get => model.Email;
            set
            {
                model.Email = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Protected virtual methods

        /// <summary>
        /// Проверяет поля FirstName, LastName, Email, PhoneNumber на null или на значение пустой строки.
        /// </summary>
        /// <returns>false - проверка не пройдена, есть null или пустая строка. true - проверка пройдена, в полях есть значения</returns>
        protected virtual bool NullOrEmpty()
        {
            if (!string.IsNullOrEmpty(FirstName) && !string.IsNullOrEmpty(LastName) && !string.IsNullOrEmpty(PhoneNumber) && !string.IsNullOrEmpty(Email))
            {
                return true;
            }
            else return false;
        }

        #endregion
    }
}
