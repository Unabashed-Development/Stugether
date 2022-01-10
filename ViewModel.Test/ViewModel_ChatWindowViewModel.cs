using System;
using System.Collections.Generic;
using System.Text;
using Gateway;
using Model;
using NUnit.Framework;

namespace ViewModel.Test
{
    public class ViewModel_ChatWindowViewModel
    {
        [Test]
        public void ChatWindowViewModel_Send()
        {
            const int sender = 1;
            const int receiver = 12;
            Profile own = ProfileDataAccess.LoadProfile(sender);
            Profile other = ProfileDataAccess.LoadProfile(receiver);
            Account.UserID = sender;
            Profile.LoggedInProfile = own;

            ChatWindowViewModel vm = new ChatWindowViewModel(other);

            Assert.IsTrue(vm.Title == other.FirstName + " " + other.LastName);
            vm.SendMessageContent = "Test ViewModel_ChatWindowViewModel";
            Assert.DoesNotThrow(() => { vm.SendChatMessageCommand.Execute(null); });
            Assert.IsTrue(vm.SendMessageContent == "");

            vm.ChatWindowHasFocus = true;
            Assert.DoesNotThrow(vm.SeenChatMessages);
        }

        [Test]
        public void ChatWindowViewModel_GetMessages()
        {
            const int sender = 12;
            const int receiver = 1;
            Profile own = ProfileDataAccess.LoadProfile(sender);
            Profile other = ProfileDataAccess.LoadProfile(receiver);
            Account.UserID = sender;
            Profile.LoggedInProfile = own;

            ChatWindowViewModel vm = new ChatWindowViewModel(other);

            Assert.DoesNotThrow(() =>
            {
                vm.RefreshChats(true);
            });
            if (vm.ChatMessages.Count == 0)
            {
                Assert.Warn("No messages have been loaded. This could be because this user doesn't have any messages, or because there is something wrong");
            }
        }
    }
}
