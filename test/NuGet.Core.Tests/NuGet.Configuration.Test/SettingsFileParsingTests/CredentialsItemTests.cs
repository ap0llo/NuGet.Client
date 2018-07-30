// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.IO;
using System.Linq;
using FluentAssertions;
using NuGet.Test.Utility;
using Xunit;

namespace NuGet.Configuration.Test
{
    public class CredentialsItemTests
    {
        // TODO-PATO
        //        [Fact]
        //        public void CallingUpdateSubsectionsRemovesSubsectionSettingMetadata()
        //        {
        //            // Arrange
        //            var nugetConfigPath = "NuGet.Config";
        //            var config = @"<?xml version=""1.0"" encoding=""utf-8""?>
        //<configuration>
        //  <section>
        //    <subsection>
        //      <add key=""key1"" value=""value1"" meta1=""data1"" />
        //    </subsection>
        //  </section>
        //</configuration>";

        //            var settingValue = new SettingValue("key2", "value2", isMachineWide: false);

        //            var valueLookUp = new Dictionary<string, SettingValue>()
        //            {
        //                { "key2",  settingValue}
        //            };

        //            using (var mockBaseDirectory = TestDirectory.Create())
        //            {
        //                ConfigurationFileTestUtility.CreateConfigurationFile(nugetConfigPath, mockBaseDirectory, config);
        //                var settings = new Settings(mockBaseDirectory);

        //                // Act
        //                settings.UpdateSubsections("section", "subsection", valueLookUp.Values.ToList());
        //                var settingValues = settings.GetNestedSettingValues("section", "subsection");

        //                // Assert
        //                settingValues.Should().NotBeNull();
        //                settingValues.Count.Should().Be(valueLookUp.Count);
        //                foreach (var value in settingValues)
        //                {
        //                    var matchingValue = valueLookUp[value.Key];
        //                    matchingValue.Should().NotBeNull();
        //                    value.Value.ShouldBeEquivalentTo(matchingValue.Value);
        //                    value.AdditionalData.ShouldBeEquivalentTo(matchingValue.AdditionalData);
        //                }
        //            }
        //        }

        //        [Fact]
        //        public void CallingUpdateSubsectionsRemovesSubsectionAndSectionIfEmpty()
        //        {
        //            // Arrange
        //            var nugetConfigPath = "NuGet.Config";
        //            var config = @"<?xml version=""1.0"" encoding=""utf-8""?>
        //<configuration>
        //  <section>
        //    <subsection>
        //      <add key=""key1"" value=""value1"" meta1=""data1"" />
        //    </subsection>
        //  </section>
        //</configuration>";
        //            var valueLookUp = new Dictionary<string, SettingValue>();

        //            using (var mockBaseDirectory = TestDirectory.Create())
        //            {
        //                ConfigurationFileTestUtility.CreateConfigurationFile(nugetConfigPath, mockBaseDirectory, config);
        //                var settings = new Settings(mockBaseDirectory);

        //                // Act
        //                settings.UpdateSubsections("section", "subsection", valueLookUp.Values.ToList());
        //                var settingValues = settings.GetNestedSettingValues("section", "subsection");

        //                // Assert
        //                settingValues.Should().NotBeNull();
        //                settingValues.Should().BeEmpty();
        //            }
        //        }

        //        [Fact]
        //        public void CallingUpdateSubsectionsRemovesSubsectionButLeavesOtherSubsections()
        //        {
        //            // Arrange
        //            var nugetConfigPath = "NuGet.Config";
        //            var config = @"<?xml version=""1.0"" encoding=""utf-8""?>
        //<configuration>
        //  <section>
        //    <subsection>
        //      <add key=""key1"" value=""value1"" meta1=""data1"" />
        //    </subsection>
        //    <subsection2>
        //      <add key=""key2"" value=""value2"" meta1=""data1"" />
        //    </subsection2>
        //  </section>
        //</configuration>";

        //            var settingValue = new SettingValue("key2", "value2", isMachineWide: false);
        //            settingValue.AdditionalData.Add("meta1", "data1");

        //            var valueLookUp = new Dictionary<string, SettingValue>()
        //            {
        //                { "key2",  settingValue}
        //            };

        //            using (var mockBaseDirectory = TestDirectory.Create())
        //            {
        //                ConfigurationFileTestUtility.CreateConfigurationFile(nugetConfigPath, mockBaseDirectory, config);
        //                var settings = new Settings(mockBaseDirectory);

        //                // Act
        //                settings.UpdateSubsections("section", "subsection", valueLookUp.Values.ToList());
        //                var settingValues = settings.GetNestedSettingValues("section", "subsection2");

        //                // Assert
        //                settingValues.Should().NotBeNull();
        //                settingValues.Count.Should().Be(valueLookUp.Count);
        //                foreach (var value in settingValues)
        //                {
        //                    var matchingValue = valueLookUp[value.Key];
        //                    matchingValue.Should().NotBeNull();
        //                    value.Value.ShouldBeEquivalentTo(matchingValue.Value);
        //                    value.AdditionalData.ShouldBeEquivalentTo(matchingValue.AdditionalData);
        //                }
        //            }
        //        }

        //        [Fact]
        //        public void CallingUpdateSubsectionsRemovesSubsectionButLeavesOtherSections()
        //        {
        //            // Arrange
        //            var nugetConfigPath = "NuGet.Config";
        //            var config = @"<?xml version=""1.0"" encoding=""utf-8""?>
        //<configuration>
        //  <section>
        //    <subsection>
        //      <add key=""key1"" value=""value1"" meta1=""data1"" />
        //    </subsection>
        //  </section>
        //  <section2>
        //    <subsection2>
        //      <add key=""key2"" value=""value2"" meta1=""data1"" />
        //    </subsection2>
        //  </section2>
        //</configuration>";

        //            var settingValue = new SettingValue("key2", "value2", isMachineWide: false);
        //            settingValue.AdditionalData.Add("meta1", "data1");

        //            var valueLookUp = new Dictionary<string, SettingValue>()
        //            {
        //                { "key2",  settingValue}
        //            };

        //            using (var mockBaseDirectory = TestDirectory.Create())
        //            {
        //                ConfigurationFileTestUtility.CreateConfigurationFile(nugetConfigPath, mockBaseDirectory, config);
        //                var settings = new Settings(mockBaseDirectory);

        //                // Act
        //                settings.UpdateSubsections("section", "subsection", valueLookUp.Values.ToList());
        //                var settingValues = settings.GetNestedSettingValues("section2", "subsection2");

        //                // Assert
        //                settingValues.Should().NotBeNull();
        //                settingValues.Count.Should().Be(valueLookUp.Count);
        //                foreach (var value in settingValues)
        //                {
        //                    var matchingValue = valueLookUp[value.Key];
        //                    matchingValue.Should().NotBeNull();
        //                    value.Value.ShouldBeEquivalentTo(matchingValue.Value);
        //                    value.AdditionalData.ShouldBeEquivalentTo(matchingValue.AdditionalData);
        //                }
        //            }
        //        }

        //        [Fact]
        //        public void CallingUpdateSubsectionsRemovesSubsectionAndSectionInConfigFileIfEmpty()
        //        {
        //            // Arrange
        //            var nugetConfigPath = "NuGet.Config";
        //            var config = @"<?xml version=""1.0"" encoding=""utf-8""?>
        //<configuration>
        //  <section>
        //    <subsection>
        //      <add key=""key1"" value=""value1"" meta1=""data1"" />
        //    </subsection>
        //  </section>
        //</configuration>";
        //            var valueLookUp = new Dictionary<string, SettingValue>();

        //            using (var mockBaseDirectory = TestDirectory.Create())
        //            {
        //                ConfigurationFileTestUtility.CreateConfigurationFile(nugetConfigPath, mockBaseDirectory, config);
        //                var settings = new Settings(mockBaseDirectory);

        //                // Act
        //                settings.UpdateSubsections("section", "subsection", valueLookUp.Values.ToList());
        //                var settingValues = settings.GetNestedSettingValues("section", "subsection");

        //                // Assert
        //                var result = @"<?xml version=""1.0"" encoding=""utf-8""?>
        //<configuration>
        //</configuration>";

        //                Assert.Equal(result.Replace("\r\n", "\n"), File.ReadAllText(Path.Combine(mockBaseDirectory, nugetConfigPath)).Replace("\r\n", "\n"));
        //            }
        //        }

        //        [Fact]
        //        public void CallingUpdateSubsectionsRemovesSubsectionAndSectionInConfigFileButLeavesOtherSections()
        //        {
        //            // Arrange
        //            var nugetConfigPath = "NuGet.Config";
        //            var config = @"<?xml version=""1.0"" encoding=""utf-8""?>
        //<configuration>
        //  <section>
        //    <subsection>
        //      <add key=""key1"" value=""value1"" meta1=""data1"" />
        //    </subsection>
        //  </section>
        //  <section2>
        //    <subsection2>
        //      <add key=""key1"" value=""value1"" meta1=""data1"" />
        //    </subsection2>
        //  </section2>
        //</configuration>";
        //            var valueLookUp = new Dictionary<string, SettingValue>();

        //            using (var mockBaseDirectory = TestDirectory.Create())
        //            {
        //                ConfigurationFileTestUtility.CreateConfigurationFile(nugetConfigPath, mockBaseDirectory, config);
        //                var settings = new Settings(mockBaseDirectory);

        //                // Act
        //                settings.UpdateSubsections("section", "subsection", valueLookUp.Values.ToList());
        //                var settingValues = settings.GetNestedSettingValues("section", "subsection");

        //                // Assert
        //                var result = @"<?xml version=""1.0"" encoding=""utf-8""?>
        //<configuration>
        //  <section2>
        //    <subsection2>
        //      <add key=""key1"" value=""value1"" meta1=""data1"" />
        //    </subsection2>
        //  </section2>
        //</configuration>";

        //                Assert.Equal(result.Replace("\r\n", "\n"), File.ReadAllText(Path.Combine(mockBaseDirectory, nugetConfigPath)).Replace("\r\n", "\n"));
        //            }
        //        }

        //        [Fact]
        //        public void CallingUpdateSubsectionsRemovesSubsectionAndSectionInConfigFileButLeavesOtherSubsections()
        //        {
        //            // Arrange
        //            var nugetConfigPath = "NuGet.Config";
        //            var config = @"<?xml version=""1.0"" encoding=""utf-8""?>
        //<configuration>
        //  <section>
        //    <subsection>
        //      <add key=""key1"" value=""value1"" meta1=""data1"" />
        //    </subsection>
        //    <subsection2>
        //      <add key=""key1"" value=""value1"" meta1=""data1"" />
        //    </subsection2>
        //  </section>
        //</configuration>";
        //            var valueLookUp = new Dictionary<string, SettingValue>();

        //            using (var mockBaseDirectory = TestDirectory.Create())
        //            {
        //                ConfigurationFileTestUtility.CreateConfigurationFile(nugetConfigPath, mockBaseDirectory, config);
        //                var settings = new Settings(mockBaseDirectory);

        //                // Act
        //                settings.UpdateSubsections("section", "subsection", valueLookUp.Values.ToList());
        //                var settingValues = settings.GetNestedSettingValues("section", "subsection");

        //                // Assert
        //                var result = @"<?xml version=""1.0"" encoding=""utf-8""?>
        //<configuration>
        //  <section>
        //    <subsection2>
        //      <add key=""key1"" value=""value1"" meta1=""data1"" />
        //    </subsection2>
        //  </section>
        //</configuration>";

        //                Assert.Equal(result.Replace("\r\n", "\n"), File.ReadAllText(Path.Combine(mockBaseDirectory, nugetConfigPath)).Replace("\r\n", "\n"));
        //            }
        //        }

        //        [Fact]
        //        public void CallingUpdateSubsectionsUpdatesSubsectionsIntoNestedSettingsWhenPresent()
        //        {
        //            // Arrange
        //            var nugetConfigPath = "NuGet.Config";
        //            var config1 = @"<?xml version=""1.0"" encoding=""utf-8""?>
        //<configuration>
        //  <section>
        //    <subsection>
        //      <add key=""key1"" value=""value1"" meta1=""data1"" meta2=""data2"" />
        //    </subsection>
        //  </section>
        //</configuration>";

        //            var config2 = @"<?xml version=""1.0"" encoding=""utf-8""?>
        //<configuration>
        //  <section>
        //    <subsection>
        //      <add key=""key2"" value=""value2"" meta1=""data1"" />
        //    </subsection>
        //  </section>
        //</configuration>";

        //            var settingValue1 = new SettingValue("key1", "value1", isMachineWide: false);
        //            settingValue1.AdditionalData.Add("meta1", "data1");
        //            settingValue1.AdditionalData.Add("meta2", "data2");

        //            var settingValue2 = new SettingValue("key2", "value2", isMachineWide: false);
        //            settingValue2.AdditionalData.Add("meta1", "data1");

        //            var valueLookUp = new Dictionary<string, SettingValue>()
        //            {
        //                { "key1",  settingValue1},
        //                { "key2",  settingValue2}
        //            };

        //            using (var mockBaseDirectory = TestDirectory.Create())
        //            using (var mockChildDirectory = TestDirectory.Create(mockBaseDirectory))
        //            {
        //                ConfigurationFileTestUtility.CreateConfigurationFile(nugetConfigPath, mockBaseDirectory, config1);
        //                ConfigurationFileTestUtility.CreateConfigurationFile(nugetConfigPath, mockChildDirectory, config2);

        //                var configPaths = new List<string> { Path.Combine(mockChildDirectory, nugetConfigPath), Path.Combine(mockBaseDirectory, nugetConfigPath) };
        //                var settings = Settings.LoadSettingsGivenConfigPaths(configPaths);

        //                // Act
        //                settings.UpdateSubsections("section", "subsection", valueLookUp.Values.ToList());
        //                var settingValues = settings.GetNestedSettingValues("section", "subsection");

        //                // Assert
        //                settingValues.Should().NotBeNull();
        //                settingValues.Count.Should().Be(valueLookUp.Count);
        //                foreach (var settingValue in settingValues)
        //                {
        //                    var matchingValue = valueLookUp[settingValue.Key];
        //                    matchingValue.Should().NotBeNull();
        //                    settingValue.Value.ShouldBeEquivalentTo(matchingValue.Value);
        //                    settingValue.AdditionalData.ShouldBeEquivalentTo(matchingValue.AdditionalData);
        //                }
        //            }
        //        }

        //        [Fact]
        //        public void CallingUpdateSubsectionsRemovesSubsectionsFromNestedSettingsWhenEmpty()
        //        {
        //            // Arrange
        //            var nugetConfigPath = "NuGet.Config";
        //            var config1 = @"<?xml version=""1.0"" encoding=""utf-8""?>
        //<configuration>
        //  <section>
        //    <subsection>
        //      <add key=""key1"" value=""value1"" meta1=""data1"" meta2=""data2"" />
        //    </subsection>
        //  </section>
        //</configuration>";

        //            var config2 = @"<?xml version=""1.0"" encoding=""utf-8""?>
        //<configuration>
        //  <section>
        //    <subsection>
        //      <add key=""key2"" value=""value2"" meta1=""data1"" />
        //    </subsection>
        //  </section>
        //</configuration>";

        //            var valueLookUp = new Dictionary<string, SettingValue>();

        //            using (var mockBaseDirectory = TestDirectory.Create())
        //            using (var mockChildDirectory = TestDirectory.Create(mockBaseDirectory))
        //            {
        //                ConfigurationFileTestUtility.CreateConfigurationFile(nugetConfigPath, mockBaseDirectory, config1);
        //                ConfigurationFileTestUtility.CreateConfigurationFile(nugetConfigPath, mockChildDirectory, config2);

        //                var configPaths = new List<string> { Path.Combine(mockChildDirectory, nugetConfigPath), Path.Combine(mockBaseDirectory, nugetConfigPath) };
        //                var settings = Settings.LoadSettingsGivenConfigPaths(configPaths);

        //                // Act
        //                settings.UpdateSubsections("section", "subsection", valueLookUp.Values.ToList());
        //                var settingValues = settings.GetNestedSettingValues("section", "subsection");

        //                // Assert
        //                settingValues.Should().NotBeNull();
        //                settingValues.Should().BeEmpty();
        //            }
        //        }

        //        [Fact]
        //        public void CallingUpdateSubsectionsRemovesSubsectionsFromNestedSettingsInConfigFileWhenEmpty()
        //        {
        //            // Arrange
        //            var nugetConfigPath = "NuGet.Config";
        //            var config1 = @"<?xml version=""1.0"" encoding=""utf-8""?>
        //<configuration>
        //  <section>
        //    <subsection>
        //      <add key=""key1"" value=""value1"" meta1=""data1"" meta2=""data2"" />
        //    </subsection>
        //  </section>
        //</configuration>";

        //            var config2 = @"<?xml version=""1.0"" encoding=""utf-8""?>
        //<configuration>
        //  <section>
        //    <subsection>
        //      <add key=""key2"" value=""value2"" meta1=""data1"" />
        //    </subsection>
        //  </section>
        //</configuration>";

        //            var valueLookUp = new Dictionary<string, SettingValue>();

        //            using (var mockBaseDirectory = TestDirectory.Create())
        //            using (var mockChildDirectory = TestDirectory.Create(mockBaseDirectory))
        //            {
        //                ConfigurationFileTestUtility.CreateConfigurationFile(nugetConfigPath, mockBaseDirectory, config1);
        //                ConfigurationFileTestUtility.CreateConfigurationFile(nugetConfigPath, mockChildDirectory, config2);

        //                var configPaths = new List<string> { Path.Combine(mockChildDirectory, nugetConfigPath), Path.Combine(mockBaseDirectory, nugetConfigPath) };
        //                var settings = Settings.LoadSettingsGivenConfigPaths(configPaths);

        //                // Act
        //                settings.UpdateSubsections("section", "subsection", valueLookUp.Values.ToList());
        //                var settingValues = settings.GetNestedSettingValues("section", "subsection");

        //                // Assert
        //                var result1 = @"<?xml version=""1.0"" encoding=""utf-8""?>
        //<configuration>
        //</configuration>";

        //                var result2 = @"<?xml version=""1.0"" encoding=""utf-8""?>
        //<configuration>
        //</configuration>";

        //                Assert.Equal(result1.Replace("\r\n", "\n"), File.ReadAllText(Path.Combine(mockBaseDirectory, nugetConfigPath)).Replace("\r\n", "\n"));
        //                Assert.Equal(result2.Replace("\r\n", "\n"), File.ReadAllText(Path.Combine(mockBaseDirectory, nugetConfigPath)).Replace("\r\n", "\n"));
        //            }
        //        }

        //        [Fact]
        //        public void CallingUpdateSubsectionsRemovesSubsectionsFromNestedSettingsInConfigFileButLeavesOtherSubsections()
        //        {
        //            // Arrange
        //            var nugetConfigPath = "NuGet.Config";
        //            var config1 = @"<?xml version=""1.0"" encoding=""utf-8""?>
        //<configuration>
        //  <section>
        //    <subsection>
        //      <add key=""key1"" value=""value1"" meta1=""data1"" meta2=""data2"" />
        //    </subsection>
        //    <subsection1>
        //      <add key=""key1"" value=""value1"" meta1=""data1"" meta2=""data2"" />
        //    </subsection1>
        //  </section>
        //</configuration>";

        //            var config2 = @"<?xml version=""1.0"" encoding=""utf-8""?>
        //<configuration>
        //  <section>
        //    <subsection>
        //      <add key=""key2"" value=""value2"" meta1=""data1"" />
        //    </subsection>
        //  </section>
        //</configuration>";

        //            var valueLookUp = new Dictionary<string, SettingValue>();

        //            using (var mockBaseDirectory = TestDirectory.Create())
        //            using (var mockChildDirectory = TestDirectory.Create(mockBaseDirectory))
        //            {
        //                ConfigurationFileTestUtility.CreateConfigurationFile(nugetConfigPath, mockBaseDirectory, config1);
        //                ConfigurationFileTestUtility.CreateConfigurationFile(nugetConfigPath, mockChildDirectory, config2);

        //                var configPaths = new List<string> { Path.Combine(mockChildDirectory, nugetConfigPath), Path.Combine(mockBaseDirectory, nugetConfigPath) };
        //                var settings = Settings.LoadSettingsGivenConfigPaths(configPaths);

        //                // Act
        //                settings.UpdateSubsections("section", "subsection", valueLookUp.Values.ToList());
        //                var settingValues = settings.GetNestedSettingValues("section", "subsection");

        //                // Assert
        //                var result1 = @"<?xml version=""1.0"" encoding=""utf-8""?>
        //<configuration>
        //  <section>
        //    <subsection1>
        //      <add key=""key1"" value=""value1"" meta1=""data1"" meta2=""data2"" />
        //    </subsection1>
        //  </section>
        //</configuration>";

        //                var result2 = @"<?xml version=""1.0"" encoding=""utf-8""?>
        //<configuration>
        //</configuration>";

        //                Assert.Equal(result1.Replace("\r\n", "\n"), File.ReadAllText(Path.Combine(mockBaseDirectory, nugetConfigPath)).Replace("\r\n", "\n"));
        //                Assert.Equal(result2.Replace("\r\n", "\n"), File.ReadAllText(Path.Combine(mockChildDirectory, nugetConfigPath)).Replace("\r\n", "\n"));
        //            }
        //        }

        //        [Fact]
        //        public void CallingUpdateSubsectionsRemovesSubsectionsFromNestedSettingsInConfigFileButLeavesOtherSections()
        //        {
        //            // Arrange
        //            var nugetConfigPath = "NuGet.Config";
        //            var config1 = @"<?xml version=""1.0"" encoding=""utf-8""?>
        //<configuration>
        //  <section>
        //    <subsection>
        //      <add key=""key1"" value=""value1"" meta1=""data1"" meta2=""data2"" />
        //    </subsection>
        //  </section>
        //  <section3>
        //    <subsection4>
        //      <add key=""key1"" value=""value1"" meta1=""data1"" meta2=""data2"" />
        //    </subsection4>
        //  </section3>
        //</configuration>";

        //            var config2 = @"<?xml version=""1.0"" encoding=""utf-8""?>
        //<configuration>
        //  <section>
        //    <subsection>
        //      <add key=""key2"" value=""value2"" meta1=""data1"" />
        //    </subsection>
        //  </section>
        //  <section2>
        //    <subsection3>
        //      <add key=""key1"" value=""value1"" meta1=""data1"" meta2=""data2"" />
        //    </subsection3>
        //  </section2>
        //</configuration>";

        //            var valueLookUp = new Dictionary<string, SettingValue>();

        //            using (var mockBaseDirectory = TestDirectory.Create())
        //            using (var mockChildDirectory = TestDirectory.Create(mockBaseDirectory))
        //            {
        //                ConfigurationFileTestUtility.CreateConfigurationFile(nugetConfigPath, mockBaseDirectory, config1);
        //                ConfigurationFileTestUtility.CreateConfigurationFile(nugetConfigPath, mockChildDirectory, config2);

        //                var configPaths = new List<string> { Path.Combine(mockChildDirectory, nugetConfigPath), Path.Combine(mockBaseDirectory, nugetConfigPath) };
        //                var settings = Settings.LoadSettingsGivenConfigPaths(configPaths);

        //                // Act
        //                settings.UpdateSubsections("section", "subsection", valueLookUp.Values.ToList());
        //                var settingValues = settings.GetNestedSettingValues("section", "subsection");

        //                // Assert
        //                var result1 = @"<?xml version=""1.0"" encoding=""utf-8""?>
        //<configuration>
        //  <section3>
        //    <subsection4>
        //      <add key=""key1"" value=""value1"" meta1=""data1"" meta2=""data2"" />
        //    </subsection4>
        //  </section3>
        //</configuration>";

        //                var result2 = @"<?xml version=""1.0"" encoding=""utf-8""?>
        //<configuration>
        //  <section2>
        //    <subsection3>
        //      <add key=""key1"" value=""value1"" meta1=""data1"" meta2=""data2"" />
        //    </subsection3>
        //  </section2>
        //</configuration>";

        //                Assert.Equal(result1.Replace("\r\n", "\n"), File.ReadAllText(Path.Combine(mockBaseDirectory, nugetConfigPath)).Replace("\r\n", "\n"));
        //                Assert.Equal(result2.Replace("\r\n", "\n"), File.ReadAllText(Path.Combine(mockChildDirectory, nugetConfigPath)).Replace("\r\n", "\n"));
        //            }
        //  }
    }
}
