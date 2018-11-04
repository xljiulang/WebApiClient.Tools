# WebApiClient.Tools
[WebApiClient](https://github.com/dotnetcore/WebApiClient)项目的工具集

## 1 WebApiClient.Tools.Swagger
> 将swagger的本地或远程json文件解析生成WebApiClient的接口定义代码文件

### 1.1 命令介绍
```
  -s Swagger, --swagger=Swagger          Required. swagger的json本地文件路径或远程Uri地址
  -n Namespace, --namespace=Namespace    代码的命名空间，如WebApiClient.Swagger
  --help                                 Display this help screen.
```
### 1.2 工作流程
1. 使用NSwag解析json得到SwaggerDocument
2. 使用RazorEngine将SwaggerDocument传入cshtml模板编译得到html
3. 使用AngleSharp将html的文本代码提取，得到WebApiClient的声明式代码
4. 代码美化，输出到本地文件
