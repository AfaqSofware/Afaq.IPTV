using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Afaq.IPTV.Models;
using Afaq.IPTV.Services.DO;
using Realms;

namespace Afaq.IPTV.Services
{
    public class RealmDbService : IDbService
    {
        private readonly Realm _realmInstance;

        public event EventHandler ActivationCodesModified;

        public RealmDbService()
        {
            _realmInstance = Realm.GetInstance();
        }

        public User GetUser(string userId)
        {
            var userDo = GetUserDOById(userId);
            if (userDo == null) return null;
            var result = new User {Username = userDo.UserName, Password = userDo.Password};
            return result;
        }

        public bool ClearUsers()
        {
            try
            {
                using (var trans = _realmInstance.BeginWrite())
                {
                    var allusers = _realmInstance.All<UserDO>();
                    foreach (var userDO in allusers.ToList())
                    {
                        foreach (var activationCode in userDO.ActivationCodes.ToList())
                        {
                            _realmInstance.Remove(activationCode);
                        }
                        _realmInstance.Remove(userDO);
                    }
                    _realmInstance.RemoveAll<UserDO>();
                    trans.Commit();
                }
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }

        public bool RemoveUser(string userId)
        {
            try
            {
                var targetUser = GetUserDOById(userId);
                if (targetUser == null) return false;

                //Since RealmDB at this time does not support Cascade delete we do it manually 
                //TODO: Use CascadeDelete from Realm once it is supported  
                using (var trans = _realmInstance.BeginWrite())
                {
                    foreach (var activationCodeDo in targetUser.ActivationCodes)
                    {
                        _realmInstance.Remove(activationCodeDo);
                    }
                    _realmInstance.Remove(GetUserDOById(userId));
                    trans.Commit();
                }

                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }

        public User GetLastSignedInUser()
        {
            try
            {
                var users = _realmInstance.All<UserDO>().ToList();
                if (!users.Any())
                {
                    return new User();
                }
                var latestLogin = users.Max(a => a.LastSignIn);
                var userDo = _realmInstance.All<UserDO>().ToList().FirstOrDefault(a => a.LastSignIn == latestLogin);
                var user = new User
                {
                    Username = userDo.UserName,
                    Password = userDo.Password,
                    LastSignIn = userDo.LastSignIn
                };
                return user;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }
        }

        public bool SaveUser(User user)
        {
            try
            {
                var userDo = GetUserDOById(user.Username);
                return userDo == null ? AddUser(user) : UpdateUser(user);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }

        public bool SaveActivationCode(string userName, ActivationCode activationCode)
        {
            try
            {
                var activationCodeDo = GetActivationCodeDOById(userName, activationCode.Id);
                return activationCodeDo == null
                    ? AddActiviationCode(userName, activationCode)
                    : UpdateActivationCode(userName, activationCode);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }

        public List<ActivationCode> GetActivationCodes(string userId)
        {
            var userDO = GetUserDOById(userId);
            var result = new List<ActivationCode>();
            if (userDO == null)
            {
                return result;
            }
            foreach (var activationCodeDo in userDO.ActivationCodes)
            {
                var activationCode = new ActivationCode()
                {
                    Id = activationCodeDo.Id,
                    IsActive = activationCodeDo.IsActive
                };
                activationCode.SetDatabaseService(this);
                result.Add(activationCode);
            }
            return result;
        }

        public bool RemoveActivationCode(string userId, ActivationCode activationCode)
        {
            try
            {
                using (var trans = _realmInstance.BeginWrite())
                {
                    var activiationCodeDO = GetActivationCodeDOById(userId, activationCode.Id);
                    _realmInstance.Remove(activiationCodeDO);
                    trans.Commit();
                }
                ActivationCodesModified?.Invoke(this,null);
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }

        private bool AddActiviationCode(string userId, ActivationCode activationCode)
        {
            try
            {
                var userDo = GetUserDOById(userId);

                if (userDo == null)
                {
                    if (userId == "Default")
                    {
                        var user = new User
                        {
                            Username = userId,
                            Password = userId
                        };
                        if (AddUser(user))
                        {
                            userDo = GetUserDOById(userId);
                        }
                    }
                }
                _realmInstance.Write(() =>
                {
                    var activiationCodeDO = _realmInstance.CreateObject<ActivationCodeDO>();
                    activiationCodeDO.Id = activationCode.Id;
                    activiationCodeDO.IsActive = activationCode.IsActive;
                    userDo.ActivationCodes.Add(activiationCodeDO);
                });
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }

        private bool UpdateActivationCode(string userId, ActivationCode activationCode)
        {
            try
            {
                using (var trans = _realmInstance.BeginWrite())
                {
                    var activationCodeDo = GetActivationCodeDOById(userId, activationCode.Id);
                    activationCodeDo.IsActive = activationCode.IsActive;
                    trans.Commit();
                }
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }

        #region Private methods

        /// <summary>
        ///     Get ActivationCodeDO from a certain user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="activiationCodeId"></param>
        /// <returns></returns>
        private ActivationCodeDO GetActivationCodeDOById(string userId, string activiationCodeId)
        {
            try
            {
                var userDo = GetUserDOById(userId);
                return userDo?.ActivationCodes.ToList().FirstOrDefault(a => a.Id == activiationCodeId);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        /// <summary>
        ///     Find a user in the database
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        private UserDO GetUserDOById(string userId)
        {
            try
            {
                return _realmInstance.All<UserDO>().ToList().FirstOrDefault(a => a.UserName == userId);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        /// <summary>
        ///     Update user in the Database
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private bool UpdateUser(User user)
        {
            try
            {
                var userDo = GetUserDOById(user.Username);
                using (var trans = _realmInstance.BeginWrite())
                {
                    userDo.Password = user.Password;
                    userDo.UserName = user.Username;
                    userDo.LastSignIn = user.LastSignIn;
                    foreach (var activationCode in user.ActivationCodes)
                    {
                        UpdateActivationCode(user.Username, activationCode);
                    }
                    trans.Commit();
                }
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }

        /// <summary>
        ///     Store a user in the database
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private bool AddUser(User user)
        {
            try
            {
                _realmInstance.Write(() =>
                {
                    var newUserDo = _realmInstance.CreateObject<UserDO>();
                    newUserDo.UserName = user.Username;
                    newUserDo.Password = user.Password;
                    newUserDo.LastSignIn = user.LastSignIn;
                    if (user.ActivationCodes == null) return;
                    foreach (var activationCode in user.ActivationCodes)
                    {
                        AddActiviationCode(user.Username, activationCode);
                    }
                });
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }

        #endregion
    }
}