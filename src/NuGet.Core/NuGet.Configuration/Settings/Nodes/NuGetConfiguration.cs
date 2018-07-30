// Copyright(c) .NET Foundation.All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace NuGet.Configuration
{
    public sealed class NuGetConfiguration : SettingsCollection<SettingsSection>, IEquatable<NuGetConfiguration>, ISettingsCollection
    {
        public override string Name => ConfigurationConstants.Configuration;

        protected override HashSet<string> AllowedAttributes => new HashSet<string>();

        private Dictionary<string, SettingsSection> Sections => ChildrenSet.Select(c=> c.Value).ToDictionary(c => c.Name);

        protected override bool CanBeCleared => false;

        public NuGetConfiguration()
            : base()
        {
        }

        internal NuGetConfiguration(ISettingsFile origin)
            : base()
        {
            var defaultSource = new SourceItem(NuGetConstants.FeedName, NuGetConstants.V3FeedUrl, protocolVersion: "3");
            var section = new SettingsSection(ConfigurationConstants.PackageSources, children: defaultSource);
            ChildrenSet.Add(section, section);

            Node = AsXNode();
            Origin = origin;
        }

        internal NuGetConfiguration(XElement element, ISettingsFile origin)
            : base(element, origin)
        {
        }

        public NuGetConfiguration(params SettingsSection[] sections)
            : base(new HashSet<SettingsSection>(sections))
        {
        }

        public void MergeSectionsInto(Dictionary<string, SettingsSection> sectionsContainer)
        {
            // loop through the current element's sections: merge any overlapped sections, add any missing section
            foreach (var section in Sections)
            {
                var newSection = new SettingsSection(section.Value);

                if (sectionsContainer.TryGetValue(newSection.Name, out var settingsSection))
                {
                    settingsSection.Merge(newSection);
                }
                else
                {
                    sectionsContainer.Add(section.Key, newSection);
                }
            }
        }

        public SettingsSection GetSection(string sectionName)
        {
            if (Sections.TryGetValue(sectionName, out var section))
            {
                return section;
            }

            return null;
        }

        public bool Equals(NuGetConfiguration other)
        {
            if (other == null)
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return Children.SequenceEqual(other.Children);
        }

        public bool DeepEquals(NuGetConfiguration other) => Equals(other);
        public override bool DeepEquals(SettingsNode other) => Equals(other as NuGetConfiguration);
        public override bool Equals(SettingsNode other) => Equals(other as NuGetConfiguration);
        public override bool Equals(object other) => Equals(other as NuGetConfiguration);
        public override int GetHashCode() => ChildrenSet.GetHashCode();
    }
}
