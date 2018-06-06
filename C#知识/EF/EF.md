# 安装包
## 为类库安装
```
Install-Package Microsoft.EntityFrameworkCore
Install-Package Microsoft.EntityFrameworkCore.SqlServer
Install-Package Microsoft.EntityFrameworkCore.Tools
Install-Package Microsoft.EntityFrameworkCore.Design
```
## 为WEB项目安装
```
Install-Package Microsoft.EntityFrameworkCore.SqlServer
Install-Package Microsoft.EntityFrameworkCore
```
添加连接字符串
```json
"ConnectionStrings": {
    "SchoolConnection": "Data Source=.;Initial Catalog=EFTest;User ID=**;Password=***;MultipleActiveResultSets=true"
  }
```
```C#
services.AddDbContext<School_TestContext>(options =>
options.UseSqlServer(Configuration.GetConnectionStrin("SchoolConnection")));
```
# 与数据库连接
## 从已有数据库中建立实体类
```
Scaffold-DbContext "Server = [ServerAddress]; Initial Catalog = [DatabaseName]; User ID = [UserId]; Password = [Pwd]" Microsoft.EntityFrameworkCore.SqlServer
```
## 建立实体类并迁移至数据库


