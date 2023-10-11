global using static C_新语法.QHelper;
using C_新语法;
using System.IO;
using static System.Net.WebRequestMethods;
namespace C_新语法;
internal class Program
{
    private static void Main(string[] args)
    {
        //Console.WriteLine("Hello, World!");
        //using QHelper a = new QHelper();
        //Console.WriteLine(a.GetName());

        //{
        //    using var fs = System.IO.File.OpenWrite("D:/1.txt");
        //    using var write = new StreamWriter(fs);
        //}
        //using (var fs = System.IO.File.OpenWrite("D:/1.txt"))
        //{
        //    using (var write = new StreamWriter(fs))
        //    {
        //        write.WriteLine("hello");
        //    }
        //}


        //var s = System.IO.File.ReadAllText("D:/1.txt");
        //Console.WriteLine(s);
        //Student t = GetDate();

        //if (t.Name == null)
        //{
        //    Console.WriteLine("姓名为空");
        //}
        //else
        //    Console.WriteLine(t.Name.ToLower());

        //Dog dog1 = new Dog(1, "花花");
        //Dog dog2 = new Dog(2, "彩票");
        //Console.WriteLine(dog1.ToString());
        //Console.WriteLine(dog1.Name);
        Person p1 = new Person(1, "安家费", 14);
        Person p2 = new Person(1, "安家费", 14);
        Person p3 = new Person(3, "一天热天", 78);
        //Console.WriteLine(p1.ToString());
        //Console.WriteLine(p2 == p3);
        //Console.WriteLine(p1 == p2);
        //Console.WriteLine(object.ReferenceEquals(p1, p2));

        
        Person p4 = p1 with { };
        Console.WriteLine(p4.ToString());
        Console.WriteLine(p1 == p4);
        Console.WriteLine(Object.ReferenceEquals(p1, p4));
    }
    static Student GetDate()
    {
        Student student = new Student("1231231");
        return student;
    }
}
