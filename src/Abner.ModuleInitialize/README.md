### Module Initialize To ConfigureServices

#### First Step

Install-Package Abner.ModuleInitialize to your start up project and module project;

#### Second Step

In your project create a class which should implemnet interface IModuleInitializer, then configure the service like where you configure in StartUp.ConfigureService();

#### Thrid Step

In your start up project , add this line

```c#
// ���ص�ǰ����Ŀ¼���г���
services.AddModuleInitializer();
```
or
```c#
// ֻ�������õĳ���
services.AddRefrenceModuleInitializer();
```

