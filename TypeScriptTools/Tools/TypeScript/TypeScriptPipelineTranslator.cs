using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timple.Tools.TypeScript.Translators;

namespace Timple.Tools.TypeScript
{
  public class TypeScriptPipelineTranslator : ITypeScriptTranslator
  {
    private readonly StreamWriter writer;
    private readonly Dictionary<String, String> referenceByFullName;
    private readonly LinkedList<ITypeScriptPipelineTranslator> translators;
    private readonly Stack<Type> translatingStack;

    public TypeScriptPipelineTranslator(StreamWriter writer) {
      this.writer = writer;
      referenceByFullName = new Dictionary<string, string>();
      translators = new LinkedList<ITypeScriptPipelineTranslator>();
      translatingStack = new Stack<Type>();
      ScanTranslators();
    }

    private void ScanTranslators() {
      var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes().Where(xx => xx.IsClass && typeof(ITypeScriptPipelineTranslator).IsAssignableFrom(xx)));
      foreach (var type in types) {
        if (type == typeof(DefaultPipelineTranslator))
          continue;

        ITypeScriptPipelineTranslator trans = (ITypeScriptPipelineTranslator)Activator.CreateInstance(type);
        trans.Prepare(this);
      }

      var defaultTrans = new DefaultPipelineTranslator();
      defaultTrans.Prepare(this);
    }

    public void AddTranslator(ITypeScriptPipelineTranslator trans) {
      translators.AddFirst(trans);
    }

    public void AddTailTranslator(ITypeScriptPipelineTranslator trans) {
      translators.AddLast(trans);
    }


    public String Translate(Type tp) {
      String vl;

      try {
        translatingStack.Push(tp);

        if (tp.IsGenericParameter)
          return tp.Name;

        if (tp.FullName != null && referenceByFullName.TryGetValue(tp.FullName, out vl))
          return vl;

        foreach (var trans in translators) {
          if (!trans.Accepts(tp))
            continue;

          vl = trans.Translate(tp, this);
          RegisterType(tp.FullName ?? vl, vl);
          return vl;
        }

        throw new ArgumentException("No translator for " + tp.FullName);
      } finally {
        translatingStack.Pop();
      }
    }

    public StreamWriter Writer {
      get {
        return writer;
      }
    }


    public void RegisterType(string fullName, string typeScriptName) {
      if (referenceByFullName.ContainsKey(fullName))
        return;
      referenceByFullName.Add(fullName, typeScriptName);
    }


    public Stack<Type> TranslatingStack {
      get { return translatingStack; }
    }
  }
}
