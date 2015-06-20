# Timple.Tools.TypeScript

Simple collection of tools to generate TypeScript client files for WebApi controllers


## Expose Objects to TypeScript
You can expose POCO objects to TypeScript

```CSharp
using (var sw = new StreamWriter(File.Create("test.ts"))) {
    ITypeScriptTranslator translator = new TypeScriptPipelineTranslator(sw);
    translator.Translate(typeof(MyClass));
}
```

This will generate a test.ts file with `MyClass` object fields translated to it, including all its hierarchy

## Expose ApiControllers to TypeScript
You can generate TypeScript Client for your WebApi Controllers
```CSharp
using (var sw = new StreamWriter(File.Create("test.ts"))) {
    ITypeScriptTranslator translator = new TypeScriptPipelineTranslator(sw);
    translator.Translate(typeof(MyController));
}
```
This will generate a test.ts file exposing `MyController` routes.
You can also use `ApiControllerClientBuilder` to generate for assemblies.
```CSharp
using (var sw = new StreamWriter(File.Create("test.ts"))) {
    ITypeScriptTranslator translator = new TypeScriptPipelineTranslator(sw);
    ApiControllerClientBuilder clientBuilder = new ApiControllerClientBuilder(translator);
    clientBuilder.GenerateForAssemblies(AppDomain.CurrentDomain.GetAssemblies());
}
```

## TypeScriptDirectoryAutoCompiler
Watch a directory, and its subdirectories, for any typescript (.ts) file to compile. Any change automatically compiles the file.

```CSharp
var autoCompiler = new TypeScriptDirectoryAutoCompiler("myscriptsdir");
// remember to dispose the object after use
```
