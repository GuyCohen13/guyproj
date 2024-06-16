using guyproj.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace guyproj.ViewModel
{
    public class SharedViewModel
    {
        private List<User> _usersList;

        public SharedViewModel()
        {
            _usersList = new List<User>();
        }

        public List<User> UsersList
        {
            get { return _usersList; }
            set { _usersList = value; }
        }
    }
}
