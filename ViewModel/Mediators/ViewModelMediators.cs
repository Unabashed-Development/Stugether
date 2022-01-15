using Model;
using System;
using System.Collections.Generic;

namespace ViewModel.Mediators
{
    public static class ViewModelMediators
    {
        #region Fields
        private static string _mainWindowPage;
        #endregion

        #region Events
        public static event Action AuthenticationStateChanged;
        public static event Action MainWindowPageChanged;
        public static event Action MatchesChanged;
        public static event Action LikesChanged;
        #endregion

        #region Properties
        public static bool Authenticated
        {
            get => Account.Authenticated;
            set
            {
                Account.Authenticated = value;
                AuthenticationStateChanged?.Invoke();
            }
        }

        public static string MainWindowPage
        {
            get => _mainWindowPage;
            set
            {
                _mainWindowPage = value;
                MainWindowPageChanged?.Invoke();
            }
        }

        public static List<Profile> Matches
        {
            get => Account.Matches;
            set
            {
                Account.Matches = value;
                MatchesChanged?.Invoke();
            }
        }

        public static List<Profile> Likes
        {
            get => Account.Likes;
            set
            {
                Account.Likes = value;
                LikesChanged?.Invoke();
            }
        }

        public static Dictionary<int, bool> ChatWindowFocus { get; set; } = new Dictionary<int, bool>();
        #endregion
    }
}
