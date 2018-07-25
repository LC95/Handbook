# 代码演示

```Csharp
class Program
{
    static void Main(string[] args)
    {
        var dogs = new List<Dog>();
       
        dogs.Add(new Dog() { Name = "dog0" });
        dogs.Add(new Dog() { Name = "dog1" });
        //协变
        IEnumerable<Animal> animals = dogs;
        //逆变
        Action<IList<Dog>> outputName = new Action<IEnumerable<Animal>>((t) =>
        {
            foreach (var animal in t)
            {
                Console.WriteLine(animal.Name);
            }
        });
        outputName(dogs);
        Console.Read();
    }
}
class Animal
{
    public string Name { get; set; }
}
class Dog : Animal
{   
}
```