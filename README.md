## .NET Format

Format C# document using Roslyn

## Installation

```
brew tap wk-j/tab
brew install dotnet-format
```

## Usage

```bash
dotnet-format tests/Hello.cs
find . -name "*.cs" | dotnet-format
```