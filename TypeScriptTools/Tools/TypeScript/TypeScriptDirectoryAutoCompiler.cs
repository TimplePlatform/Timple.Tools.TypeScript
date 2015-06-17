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

    public TypeScriptDirectoryAutoCompiler(String path, bool recursive = true) {
      fsWatcher = new FileSystemWatcher();
      fsWatcher.Path = path;
      fsWatcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.Size;

      fsWatcher.Filter = "*.ts";
      fsWatcher.IncludeSubdirectories = recursive;

      fsWatcher.Changed += fsWatcher_Changed;
      fsWatcher.Created += fsWatcher_Changed;
      fsWatcher.Deleted += fsWatcher_Deleted;
      fsWatcher.Renamed += fsWatcher_Renamed;

      ThreadPool.QueueUserWorkItem(CompileScript, Directory.GetFiles(path, "*.ts"));

      if (recursive) {
        Stack<String> dirs = new Stack<string>(Directory.GetDirectories(path));

        while (dirs.Count > 0) {
          var dir = dirs.Pop();

          ThreadPool.QueueUserWorkItem(CompileScript,Directory.GetFiles(dir, "*.ts"));
          
          var subDirs = Directory.GetDirectories(dir);
          foreach (var d in subDirs)
            dirs.Push(d);
        }
      }

      fsWatcher.EnableRaisingEvents = true;
    }

    private void CompileScript(object state) {
      String[] tsFiles = state as String[];

      if (tsFiles == null)
        tsFiles = new string[] { (string)state };

      TypeScriptNativeCompiler c = new TypeScriptNativeCompiler();

      foreach (var tsFile in tsFiles) {
        try {
          c.Compile(tsFile);
        } catch (TypeScriptNativeCompilerException ex) {
          File.WriteAllLines(tsFile + ".err", ex.Errors);
        }
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
