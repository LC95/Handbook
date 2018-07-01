# 特性(Attribute)
## 1 .Net 常用特性
1. `AttributeUsage`
    - 该特性用于控制如何应用自定义特性至目标元素. 
2. `Flags`
    - 该来将枚举数值看做按位标记, 例如
    ```C#
    [Flags]
    enum Animal{
        Dog =     0x01,//00000000000000000000000000000001
        Cat =     0x02,//00000000000000000000000000000010
        Duck =    0x04,//00000000000000000000000000000100
        Chicken = 0x08 //00000000000000000000000000001000
    }
    ```
3. `DllImport`
    - 该特性可以让我们调用非托管代码, 我们可以使用DllImport特性引入对Win32 API函数的调用.
    ```C#
    using System;
    using System.Runtime.InteropServices;
    namespace InsideDotNet.Conception.AttributeAndProperty
    {
        class DllImportTest
        {
            [DllImport("User32.dll")]
            public static extern int MessageBox(int hParent, string msg, string caption, int type);
            static int Main()
            {
                return MessageBox(0, "How to use attribute in .Net", "InsideDotNet", 0)
            }
        }
    }
    ```
4. `Serializable`
    - 该特性表明了应用的元素可以被序列化
5. `Conditional`
    - 条件编译, 在调试时使用. 该特性不可应用于数据成员和属性   
## 2 自定义特性
1. 一个特性实例
    ```C#
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Method, Inherited =true)]
    public class TestAttribute : Attribute
    {
        public TestAttribute(string msg)
        {
            Console.WriteLine(msg);
        }
        public void RunTest()
        {
            Console.WriteLine("TestAttribute here");
        }
    }
    [Test("Error Here.")]
    public void CannotRun()
    {
    }
    public static void Main(string[] args)
    {
        Program g = new Program();
        g.CannotRun();
        //获取类的信息
        Type tp = typeof(Program);
        //获取被特性应用的方法信息
        MethodInfo mInfo = tp.GetMethod("CannotRun");
        //获取该特性(并非实例化)
        TestAttribute testtAttr = (TestAttribute)Attribute.GetCustomAttribute(mInfo, typeof(TestAttribute));
        testtAttr.RunTest();
        Console.ReadKey();

    }
    ```
2. 另一个实例
    ```C#
    //控制台入口
    public static void Main(string[] args)
    {
        Type type = typeof(MyTest);
        MemberInfo info = type;
        MyselfAttribute attr = (MyselfAttribute)Attribute.GetCustomAttribute(info, typeof(MyselfAttribute));
        if(attr != null)
        {
            Console.WriteLine($"Name {attr.Name}");
            Console.WriteLine($"Age {attr.Age}");
            Console.WriteLine($"Memo {attr.Memo}");
            attr.ShowName();
        }
        Console.ReadLine();
    }
    //自定义特性
    public class MyselfAttribute : Attribute
    {
        private string _name;
        private int _age;
        private string _memo;
        public MyselfAttribute()
        {

        }
        public MyselfAttribute(string name, int age, string memo)
        {
            _name = name;
            _age = age;
            _memo = memo;
        }

        public string Name
        {
            get { return _name ?? string.Empty; }
        }
        public int Age
        {
            get { return _age; }
        }
        public string Memo
        {
            get { return _memo; }
            set { _memo = value; }
        }
        public void ShowName()
        {
            Console.WriteLine($"Hello,{Name ?? "World"}");
        }
    }
    //应用特性的类
    [Myself("Emma",25,"FML")]
    public class MyTest
    {
        public void SayHello()
        {
            Console.WriteLine("Hello , my dotnet world");
        }
    }
    ```
