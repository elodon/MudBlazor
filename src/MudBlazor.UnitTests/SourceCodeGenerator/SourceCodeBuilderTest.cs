﻿// Copyright (c) MudBlazor 2021
// MudBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Linq;
using FluentAssertions;
using MudBlazor.SourceCodeGenerator;
using MudBlazor.SourceCodeGenerator.Models;
using NUnit.Framework;

namespace MudBlazor.UnitTests.SourceCodeGenerator;

[TestFixture]
public class SourceCodeBuilderTest
{
    [Test]
    public void Build_ShouldReturnValidSwitchStatement_WhenMembersGiven()
    {
        // Arrange
        var enumMembers = new[] {new EnumMember("One", "Description")};
        var enumData = new EnumData("ClassName", "EnumName", "EnumNamespace", "public", enumMembers);

        // Act
        var generatedCode = SourceCodeBuilder.Build(ref enumData).Replace("\r", "");
        // Assert
        const string Expected = """
// Copyright (c) MudBlazor 2021
// MudBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

// <auto-generated>
// This code was auto generated by a source code generator.
// </auto-generated>

// Disable obsolete warnings for generated code
#pragma warning disable CS0618

namespace EnumNamespace;

/// <summary>
/// Extension methods for <see cref="EnumName"/> enum.
/// </summary>
public static class ClassName {
    
    /// <summary>
    /// Returns the value of the <see cref="System.ComponentModel.DescriptionAttribute"/> attribute.
    /// If no description attribute was found the default ToString method will be used.
    /// </summary>
    public static string ToDescriptionString(this EnumName mudEnum)
    {
        return mudEnum switch
        {
            EnumName.One => "Description",
            _ => mudEnum.ToString().ToLower(),
        };

    }

}
""";
        generatedCode.Should().Be(Expected);
    }
    
    [Test]
    public void Build_ShouldReturnOnlyDefaultSwitchStatement_WhenMembersEmpty()
    {
        // Arrange
        var enumMembers = Enumerable.Empty<EnumMember>();
        var enumData = new EnumData("ClassName", "EnumName", "EnumNamespace", "public", enumMembers);

        // Act
        var generatedCode = SourceCodeBuilder.Build(ref enumData).Replace("\r", "");

        // Assert
        const string Expected = """
// Copyright (c) MudBlazor 2021
// MudBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

// <auto-generated>
// This code was auto generated by a source code generator.
// </auto-generated>

// Disable obsolete warnings for generated code
#pragma warning disable CS0618

namespace EnumNamespace;

/// <summary>
/// Extension methods for <see cref="EnumName"/> enum.
/// </summary>
public static class ClassName {
    
    /// <summary>
    /// Returns the value of the <see cref="System.ComponentModel.DescriptionAttribute"/> attribute.
    /// If no description attribute was found the default ToString method will be used.
    /// </summary>
    public static string ToDescriptionString(this EnumName mudEnum)
    {
        return mudEnum switch
        {
            _ => mudEnum.ToString().ToLower(),
        };

    }

}
""";
        generatedCode.Should().Be(Expected);
    }
}
