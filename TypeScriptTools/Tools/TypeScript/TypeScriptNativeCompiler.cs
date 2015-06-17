using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timple.Tools.TypeScript
{
  public class TypeScriptNativeCompiler
  {
    private readonly String workingPath;
    private static readonly Version minVersion = new Version(1, 0, 3, 0);

    public TypeScriptNativeCompiler(String workingPath = null) {
      this.workingPath = workingPath ?? Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
      if (!Directory.Exists(this.workingPath))
        Directory.CreateDirectory(this.workingPath);
    }

    public String WorkingPath { get { return workingPath; } }

    private ProcessStartInfo EnsureNativeCompiler(TypeScriptNativeCompilerConfiguration conf) {

      ProcessStartInfo psi = new ProcessStartInfo();
      psi.Arguments = "-v";
      psi.WorkingDirectory = conf.NativeBasePath;
      psi.RedirectStandardError = psi.RedirectStandardOutput = true;
      psi.FileName = "tsc.exe";
      psi.UseShellExecute = false;

      using (Process p = new Process()) {
        p.StartInfo = psi;

        p.Start();

        Version v = null;

        p.OutputDataReceived += (sender, e) => {
          if (e.Data == null)
            return;

          String[] parts = e.Data.Split(' ');
          Version.TryParse(parts[1], out v);
        };

        p.BeginErrorReadLine();
        p.BeginOutputReadLine();

        p.WaitForExit();

        if (null == v)
          throw new ArgumentException("Native TypeScript Compiler (tsc.exe) not found");

        if (v < minVersion)
          throw new Exception(String.Format("Unsupported Compiler version {0} (min version: {1})", v, minVersion));

        return psi;
      }

    }


    public void Compile(String tsFile, TypeScriptNativeCompilerConfiguration configuration = null) {
      var conf = configuration ?? new TypeScriptNativeCompilerConfiguration() {
        CodePage = null,
        RemoveComments = false,
        TargetVersion = "ES5"
      };

      var psi = EnsureNativeCompiler(conf);

      psi.Arguments = "\"" + tsFile + "\"" + conf.BuildArguments();

      using (Process p = new Process()) {
        p.StartInfo = psi;

        p.Start();

        List<String> errors = new List<string>();
        List<String> infos = new List<string>();

        p.ErrorDataReceived += (sender, e) => {
          if (String.IsNullOrEmpty(e.Data))
            return;

          errors.Add(e.Data);
        };

        p.OutputDataReceived += (sender, e) => {
          if (String.IsNullOrEmpty(e.Data))
            return;

          infos.Add(e.Data);
        };

        p.BeginOutputReadLine();
        p.BeginErrorReadLine();


        p.WaitForExit();

        if (p.ExitCode != 0)
          throw new TypeScriptNativeCompilerException(errors);

      }

    }

  }
}
