using iKino.API.Models;
using iKino.API.Services.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iKino.API.Domain
{
    public class User
    {
        protected User()
        {
        }

        public User(string username, string mail, string role = Roles.User)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("Username can not be empty.", nameof(username));

            if (username.Length > 50)
                throw new ArgumentException("Username can not have more than 50 characters.", nameof(username));


            Username = username;
            Mail = mail;
            Role = role;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid UserId { get; protected set; }

        public string Username { get; protected set; }
        public string Password { get; protected set; }
        public string Mail { get; protected set; }
        public string Role { get; protected set; }
        public bool IsActive { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime UpdatedAt { get; protected set; }

        public void Activate()
        {
            if (IsActive)
                return;

            IsActive = true;
            UpdatedAt = DateTime.UtcNow;
        }

        public void DeActivate()
        {
            if (!IsActive)
                return;

            IsActive = false;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetRole(string role)
        {
            Role = role;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetPassword(string password, IHashService hashService)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password can not be empty.", nameof(password));
            if (password.Length > 100)
                throw new ArgumentException("Password can not have more than 100 characters.", nameof(password));

            Password = hashService.Hash(password);
            UpdatedAt = DateTime.UtcNow;
        }
    }
}