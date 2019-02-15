## .NET Format

[![NuGet](https://img.shields.io/nuget/v/wk.DotNetFormat.svg)](https://www.nuget.org/packages/wk.DotNetFormat)

Format C# document using Roslyn

## Installation

```bash
dotnet tool install wk.DotNetFormat
```

## Usage

```bash
wk-dotnet-format tests/Hello.cs
find . -name "*.cs" | wk-dotnet-format
```