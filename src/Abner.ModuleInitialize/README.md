### Module Initialize To ConfigureServices

#### First Step

Install-Package Abner.ModuleInitialize to your start up project and module project;

#### Second Step

In your project create a class which should implemnet interface IModuleInitializer, then configure the service like where you configure in StartUp.ConfigureService();

#### Thrid Step

In your start up project , add this line

```c#
// 加载当前运行目录所有程序集
services.AddModuleInitializer();
```
or
```c#
// 只加载引用的程序集
services.AddRefrenceModuleInitializer();
```

