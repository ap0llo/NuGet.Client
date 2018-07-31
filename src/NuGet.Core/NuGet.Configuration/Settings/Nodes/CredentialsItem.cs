// Copyright(c) .NET Foundation.All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;

namespace NuGet.Configuration
{
    public sealed class CredentialsItem : SettingsItem, IEquatable<CredentialsItem>
    {
        public override string Name { get; protected set; }
        protected override bool CanHaveChildren => true;
        protected override HashSet<string> AllowedAttributes => new HashSet<string>();
        public override bool IsEmpty() => Username == null && Password == null;
        public AddItem Username { get; private set; }
        public AddItem Password => IsPasswordClearText ? _clearTextPassword : _encryptedPassword;
        public bool IsPasswordClearText { get; private set; }
        private AddItem _clearTextPassword { get; set; }
        private AddItem _encryptedPassword { get; set; }

        internal CredentialsItem(XElement element, ISettingsFile origin)
            : base(element, origin)
        {
            Name = element.Name.LocalName;

            var elementDescendants = element.Elements();

            if (elementDescendants.Count() != 2)
            {
                throw new NuGetConfigurationException(string.Format(CultureInfo.CurrentCulture, Resources.UserSettings_UnableToParseConfigFile, origin.ConfigFilePath));
            }

            var parsedItems = elementDescendants.Select(e => SettingFactory.Parse(e, origin) as AddItem).Where(i => i != null);

            foreach (var item in parsedItems)
            {
                if (string.Equals(item.Key, ConfigurationConstants.UsernameToken, StringComparison.Ordinal))
                {
                    Username = item;
                }
                else if (string.Equals(item.Key, ConfigurationConstants.PasswordToken, StringComparison.Ordinal))
                {
                    _encryptedPassword = item;
                    IsPasswordClearText = false;
                }
                else if (string.Equals(item.Key, ConfigurationConstants.ClearTextPasswordToken, StringComparison.Ordinal))
                {
                    _clearTextPassword = item;
                    IsPasswordClearText = true;
                }
            }

            if (Username == null || Password == null)
            {
                throw new NuGetConfigurationException(string.Format(CultureInfo.CurrentCulture, Resources.UserSettings_UnableToParseConfigFile, origin.ConfigFilePath));
            }
        }

        public CredentialsItem(string name, string username, string password, bool isPasswordClearText)
            : base()
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));

            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentNullException(nameof(username));
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException(nameof(password));
            }

            Username = new AddItem(ConfigurationConstants.UsernameToken, username);

            if (isPasswordClearText)
            {
                _clearTextPassword = new AddItem(ConfigurationConstants.ClearTextPasswordToken, password);
            }
            else
            {
                _encryptedPassword = new AddItem(ConfigurationConstants.PasswordToken, password);
            }

            IsPasswordClearText = isPasswordClearText;
        }

        public override SettingsItem Copy()
        {
            return new CredentialsItem(Name, Username.Value, Password.Value, IsPasswordClearText);
        }

        public override bool Update(SettingsItem item)
        {
            if (base.Update(item) && item is CredentialsItem credentials)
            {
                Username = credentials.Username;
                IsPasswordClearText = credentials.IsPasswordClearText;

                if (credentials.IsPasswordClearText)
                {
                    _clearTextPassword = credentials.Password;
                    _encryptedPassword = null;
                }
                else
                {
                    _clearTextPassword = null;
                    _encryptedPassword = credentials.Password;
                }

                var element = Node as XElement;
                if (element != null)
                {
                    element.RemoveNodes();
                    XElementUtility.AddIndented(element, Username.AsXNode());
                    XElementUtility.AddIndented(element, Password.AsXNode());
                }

                return true;
            }

            return false;
        }

        public override XNode AsXNode()
        {
            if (Node != null && Node is XElement)
            {
                return Node;
            }

            var element = new XElement(Name, Username.AsXNode(), Password.AsXNode());

            foreach (var attr in Attributes)
            {
                element.SetAttributeValue(attr.Key, attr.Value);
            }

            Node = element;

            return Node;
        }

        public bool Equals(CredentialsItem other)
        {
            if (other == null)
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return string.Equals(Name, other.Name, StringComparison.Ordinal);
        }

        public bool DeepEquals(CredentialsItem other)
        {
            if (other == null)
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return string.Equals(Name, other.Name, StringComparison.Ordinal)
                && Username.DeepEquals(other.Username)
                && IsPasswordClearText == other.IsPasswordClearText
                && Password.DeepEquals(other.Password);
        }

        public override bool DeepEquals(SettingsNode other) => DeepEquals(other as CredentialsItem);
        public override bool Equals(SettingsNode other) =>  Equals(other as CredentialsItem);
        public override bool Equals(object other) => Equals(other as CredentialsItem);
        public override int GetHashCode() => Name.GetHashCode();
    }
}
