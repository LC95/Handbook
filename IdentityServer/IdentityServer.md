1. IdentityServerCenter 授权服务器Demo
    在Startup.cs文件中注册
    1. 配置一个Config帮助类
        1. 配置一个api集合,用来标识用来授权的api
        ```C#
        public static IEnumerable<ApiResource> GetResouces()
        {
            return new List<ApiResource>{
                new ApiResource("api","My Api")
            };
        }
        ```
        2. 配置一个客户端集合, 用来标识允许获取token的客户端
        ```C#
        public static IEnumerable<Client> GetClients() => new List<Client>{
            new Client(){
                ClientId = "client",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = {
                    new Secret("secret".Sha256())
                },
                AllowedScopes = {"api"}
            },
            new Client(){
                ClientId = "clientPwd",
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                ClientSecrets = {
                    new Secret("secret".Sha256())
                },
                AllowedScopes = {"api"}
            }
        };
        public static List<TestUser> GetTestUsers()
        {
            return new List<TestUser>{
                new TestUser{
                    SubjectId = "1",
                    Username = "jesse",
                    Password = "123456"
                }
            };
        }
        ``` 
    2. 在`ConfigureServices`中配置服务[客户端模式]
        ```C#
        services.AddIdentityServer()
        .AddDeveloperSigningCredential()
        .AddInMemoryApiResources(Config.GetResouces())//里配置了一个api的集合(其中包括了名为"api"的api)
        .AddInMemoryClients(Config.GetClients());
        //配置了一个请求"api"的客户端, 名为"client", 授权模式为户端授权
        services.AddMvc();
                
    3. 密码模式配置
        ```C#
        .AddTestUsers(Config.GetTestUsers());
        ```
    4. 在`Configure`中开启服务
        ```C#
        public void Configure(IApplicationBuilder app,      IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //启用IdentityServer
            app.UseIdentityServer();
        }
        ```
2. ClientCredentialApi 客户端模式Demo Api
    Startup.cs中配置服务
    1. 在`ConfigureServices`中配置认证服务
    ```C#
    services.AddAuthentication("Bearer")
    .AddIdentityServerAuthentication(options=>{
        options.Authority = "http://localhost:5000";
        options.RequireHttpsMetadata = false;
        options.ApiName = "api";
    });
    ```
    2. 在`Configure`中开启服务
    ```C#
    if (env.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }
    app.UseAuthentication();
    ```
    3. 在api的`Controller`中添加需要授权标签
    ```C#
    [Authorize]
    [Route("api/[controller]")]
    ```
3. ThirdPartyDemo 第三方请求客户端
    1. 首先要去认证服务器请求token
    ```C#
    var diso = DiscoveryClient.GetAsync("http://localhost:5000").Result;
    if(diso.IsError)
    {
        Console.WriteLine(diso.Error);
    }
    var tokenClient = new TokenClien(diso.TokenEndpoint,"client","secret");
    var tokenResponse = tokenClient.RequestClientCredentialsAsyn("api").Result;
    ```
    2. 去使用token请求api
    ```C#
    var httpClient = new HttpClient();
    httpClient.SetBearerToken(tokenResponse.AccessToken);
    var resposne = httpClient.GetAsyn ("http://localhost:5001/api/values").Result;
    if(resposne.IsSuccessStatusCode)
    {
        Console.WriteLine(resposne.Content.ReadAsStringAsync().Result);

    }
    ```