class DotNetFilter < Formula
  desc "Format C# document using Roslyn"
  homepage "https://github.com/wk-j/dotnet-format"
  url "https://github.com/wk-j/dotnet-format/releases/download/0.1.0/dotnet-format.zip"
  version "0.1.0"
  sha256 "279f9e151cf39a7f5bc35e814340bb7477c102cd2d28bba7af650a26ec2b99c0"

  # depends_on "mono" => :recommended

  def install
    libexec.install Dir["*"]

    # To match non-mono install, create an `omnisharp' shell script.
    (bin/"dotnet-format").write <<-EOS.undent
      #!/usr/bin/env sh
      mono /usr/local/opt/dotnet-format/DotNetFormat.exe $@
    EOS
  end

  def post_install
    system "/bin/chmod +x #{bin}/dotnet-format"
  end

end