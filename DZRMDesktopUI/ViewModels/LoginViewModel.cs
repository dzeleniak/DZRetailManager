using Caliburn.Micro;
using DZRMDesktopUI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DZRMDesktopUI.Library.API;
using DZRMDesktopUI.EventModels;

namespace DZRMDesktopUI.ViewModels
{
    public class LoginViewModel : Screen
    {
        private string _userName = "zeleniak.david@gmail.com";
        private string _password;
        private IAPIHelper _apiHelper;
        private IEventAggregator _events;

        public LoginViewModel(IAPIHelper apiHelper, IEventAggregator events)
        {
            _apiHelper = apiHelper;
            _events = events;
        }

        public string Username
        {
            get { return _userName; }
            set 
            {
                _userName = value;
                NotifyOfPropertyChange(() => Username);
                NotifyOfPropertyChange(() => CanLogin);
            }
        }
 
        public string Password
        {
            get { return _password; }
            set 
            {
                _password = value;
                NotifyOfPropertyChange(() => Password);
                NotifyOfPropertyChange(() => CanLogin);
            }
        }

        public bool IsErrorVisible
        {
            get 
            {
                bool output = false;

                if(ErrorMessage?.Length > 0){ output = true; }

                return output;
            }

            set { }

        }

        private string _errorMessage;

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set 
            {
                _errorMessage = value;
                NotifyOfPropertyChange(() => ErrorMessage); 
                NotifyOfPropertyChange(() => IsErrorVisible); 
            }
        }



        public bool CanLogin
        {
            get
            {
                bool output = false;

                if (Username?.Length > 0 && Password?.Length > 0)
                {
                    output = true;
                }

                return output;
            }
        }

        public async Task Login()
        {
            try
            {
                ErrorMessage = "";
                var result = await _apiHelper.Authenticate(Username, Password);

                //Capture more information about the user
                await _apiHelper.GetLoggedInUserInfo(result.Access_Token);

                _events.PublishOnUIThread(new LogOnEvent());
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
    }
}
