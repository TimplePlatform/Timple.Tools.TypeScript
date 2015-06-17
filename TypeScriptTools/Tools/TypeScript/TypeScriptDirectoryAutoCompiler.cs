using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Timple.Tools.TypeScript
{
  public class TypeScriptDirectoryAutoCompiler : IDisposable
  {
    private readonly FileSystemWatcher fsWatcher;

    public TypeScriptDirectoryAutoCompiler(String path) {
      fsWatcher = new FileSystemWatcher();
      fsWatcher.Path = path;
      fsWatcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.Size;

      fsWatcher.Filter = "*.ts";

      fsWatcher.Changed += fsWatcher_Changed;
      fsWatcher.Created += fsWatcher_Changed;
      fsWatcher.Deleted += fsWatcher_Deleted;
      fsWatcher.Renamed += fsWatcher_Renamed;

      var files = Directory.GetFiles(path, "*.ts");
      foreach (var file in files)
        ThreadPool.QueueUserWorkItem(CompileScript, file);

      fsWatcher.EnableRaisingEvents = true;
    }

    private void CompileScript(object state) {
      String tsFile = (String)state;

      TypeScriptNativeCompiler c = new TypeScriptNativeCompiler(Path.GetDirectoryName(tsFile));
      try {
        c.Compile(tsFile);
      } catch (TypeScriptNativeCompilerException ex) {
        File.WriteAllLines(tsFile + ".err", ex.Errors);
      }
    }

    void fsWatcher_Renamed(object sender, RenamedEventArgs e) {
      ThreadPool.QueueUserWorkItem(CompileScript, e.FullPath);
    }

    void fsWatcher_Deleted(object sender, FileSystemEventArgs e) {
    }

    void fsWatcher_Changed(object sender, FileSystemEventArgs e) {
      ThreadPool.QueueUserWorkItem(CompileScript, e.FullPath);
    }


    public void Dispose() {
      if (fsWatcher != null)
        fsWatcher.Dispose();
    }
  }
}
