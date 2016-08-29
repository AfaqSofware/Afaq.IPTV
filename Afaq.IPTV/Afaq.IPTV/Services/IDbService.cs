
using System;
using System.Collections.Generic;
using Afaq.IPTV.Models;

namespace Afaq.IPTV.Services
{
    public interface IDbService
    {
        event EventHandler ActivationCodesModified; 

        /// <summary>
        ///     Retrieve user from the Database
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        User GetUser(string userId);


        /// <summary>
        /// Retrieves the last user which logged in to the system
        /// </summary>
        /// <returns></returns>
        User GetLastSignedInUser();



        /// <summary>
        /// Saves the user, if it is a new user then create it. 
        /// </summary>
        /// <param name="user"></param>
        /// <returns> true if is saved otherwise it is false</returns>
        bool SaveUser(User user);



        /// <summary>
        ///     Delete a user from the database
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>true if is deleted otherwise it is false</returns>
        bool RemoveUser(string userId);


        /// <summary>
        /// Removes all users from the databse
        /// </summary>
        bool ClearUsers();

        /// <summary>
        /// Get activation codes for a user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        List<ActivationCode> GetActivationCodes(string userId);

        /// <summary>
        /// Saves the activation code if it's new then add it. 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="activationCode"></param>
        /// <returns>True if added successfully otherwise false</returns>
        bool SaveActivationCode(string userName, ActivationCode activationCode);


        /// <summary>
        /// Remove an activation code from a user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="activationCode"></param>
        /// <returns>True if activation code is removed otherwise false</returns>
        bool RemoveActivationCode(string userId, ActivationCode activationCode);


    }
}