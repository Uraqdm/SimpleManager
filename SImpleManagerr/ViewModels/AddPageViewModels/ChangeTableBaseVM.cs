using SimpleManager.Commands;
using SimpleManager.Handlers;
using SimpleManager.ViewModels.TableViewModels;

namespace SimpleManager.ViewModels.AddPageViewModels
{
    abstract class ChangeTableBaseVM : BaseVM
    {
        #region Commands

        public DelegateCommand Cancel { get; }

        #endregion

        #region Constructor

        public ChangeTableBaseVM()
        {
            Cancel = new DelegateCommand(obj => 
            {
                NavigationHandler.NavigationService.GoBack();
            } );
        }

        #endregion 
    }
}
