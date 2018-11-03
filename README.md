# WebApiClient.Tools
WebApiClient项目的工具集

## 1 WebApiClient.Tools.Swagger
> 将swagger的本地或远程json文件解析生成WebApiClient的接口定义代码文件

### 1.1 命令介绍
  -s Swagger, --swagger=Swagger          Required. swagger的json本地文件路径或远程Uri地址 <br/>
  -n Namespace, --namespace=Namespace    代码的命名空间，如WebApiClient.Swagger<br/>
  --help                                 Display this help screen.<br/>
  
### 2.1 工作流程
1. 使用NSwag解析json得到SwaggerDocument
2. 使用RazorEngine将SwaggerDocument传入cshtml编译得到html
3. AngleSharp将html的文件提取，得到WebApiClient的代码
4. 代码美化，输出到本地文件
