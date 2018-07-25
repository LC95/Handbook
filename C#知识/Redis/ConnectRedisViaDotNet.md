# .Net连接Redis

## 一个DEMO
DEMO：
```CSharp
static void Main(string[] args)
{
    string host = "localhost";
    ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(host);//连接REDIS

    IDatabase db = redis.GetDatabase();
    db.StringSet("User","LC95");
    db.StringAppend("User","is a good coder");//写入字符串

    string user = db.StringGet("User");//读取字符串
    Console.WriteLine(user);
}
```