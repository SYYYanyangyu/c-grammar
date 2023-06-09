// See https://aka.ms/new-console-template for more information
/*Console.WriteLine("Hello, World!");*/

using System.Linq.Expressions;

// 在委托外面包裹一层Expression<> 就是表达式目录树
// 表达式目录树可以通过Compile()转换成一个委托
Expression<Func<int, int>> expression = num => num + 5;

// 获取express的节点类型
Console.WriteLine($"NodeType:{expression.NodeType}");
// 获取节点类型
Console.WriteLine($"Body:{expression.Body}");
Console.WriteLine($"NodeType:{expression.Body.GetType()}");
Console.WriteLine($"NodeType:{expression.Body.NodeType}");
Console.WriteLine(ExpressionType.Lambda);

if (expression.NodeType == ExpressionType.Lambda)
{
    var lambda = (LambdaExpression)expression;
    var parameter = lambda.Parameters.Single();
    Console.WriteLine($"parameter.Name:{parameter.Name}");
    Console.WriteLine($"parameter.Type:{parameter.Type}");
    Console.WriteLine($"parameter.ReturnType:{lambda.ReturnType}");
}

// Polymorphism at work #1: a Rectangle, Triangle and Circle
// can all be used wherever a Shape is expected. No cast is
// required（不需要强制转换） because an implicit conversion exists from a derived
// class to its base class.
var shapes = new List<Shape>
{
    new Rectangle(),
    new Triangle(),
    new Circle()
};

// Polymorphism at work #2: the virtual method Draw is
// invoked on each of the derived classes, not the base class.
foreach (var shape in shapes)
{
    shape.Draw();
}

DerivedClass B = new DerivedClass();
B.DoWork();  // Calls the new method.

BaseClass A = B;
A.DoWork();  // Also calls the new method.

DerivedClassA BB = new DerivedClassA();
BB.DoWork();  // Calls the new method.

BaseClassA AA = (BaseClassA)BB;
AA.DoWork();  // Calls the old method.

// Create an instance of WorkItem by using the constructor in the
// base class that takes three arguments（争论，辩论 ，参数）.
WorkItem item = new WorkItem("Fix Bugs",
                            "Fix all bugs in my code branch",
                            new TimeSpan(3, 4, 0, 0));

// Create an instance of ChangeRequest by using the constructor in
// the derived class that takes four arguments.
ChangeRequest change = new ChangeRequest("Change Base Class Design",
                                        "Add members to the class",
                                        new TimeSpan(4, 0, 0),
                                        1);

// Use the ToString method defined in WorkItem.
Console.WriteLine(item.ToString());

// Use the inherited Update method to change the title of the
// ChangeRequest object.
change.Update("Change the Design of the Base Class",
    new TimeSpan(4, 0, 0));

// ChangeRequest inherits WorkItem's override of ToString.
Console.WriteLine(change.ToString());

#region  继承
/*
 * explicitly (明确的)
 * WorkItem implicitly(含蓄地; 暗中地; 不明显地; 无疑问地; 无保留地) inherits from the Object class
 *
 */
public class WorkItem
{
    private static int currentID;

    //Properties.
    protected int ID { get; set; }
    protected string Title { get; set; }
    protected string Description { get; set; }
    protected TimeSpan jobLength { get; set; }

    // Default constructor. If a derived class(派生类) does not invoke a base-
    // class constructor explicitly, the default constructor is called
    // implicitly.
    public WorkItem()
    {
        ID = 0;
        Title = "Default title";
        Description = "Default description.";
        jobLength = new TimeSpan();
    }

    // Instance constructor（示例构造函数） that has three parameters.
    public WorkItem(string title, string desc, TimeSpan joblen)
    {
        this.ID = GetNextID();
        this.Title = title;
        this.Description = desc;
        this.jobLength = joblen;
    }

    // 静态成员用于初始化静态成员
    // Static constructor to initialize the static member, currentID. This
    // constructor is called one time, automatically, before any instance
    // of WorkItem or ChangeRequest is created, or currentID is referenced.
    // 这是一个静态构造函数 WorkItem调用一次 这个函数就执行一次
    static WorkItem() => currentID = 0;

    protected int GetNextID() => ++currentID;

    // Method Update enables you to update the title and job length of an
    // existing WorkItem object.
    public void Update(string title, TimeSpan joblen)
    {
        // this 指向的是当前类的字段
        this.Title = title;
        this.jobLength = joblen;
    }

    // Virtual method override of the ToString method that is inherited
    // from System.Object.
    public override string ToString() =>
        $"{this.ID} - {this.Title}";
}

// ChangeRequest derives from WorkItem and adds a property (originalItemID)
// and two constructors.
public class ChangeRequest : WorkItem
{
    protected int originalItemID { get; set; }

    // Constructors. Because neither constructor calls a base-class
    // constructor explicitly, the default constructor in the base class
    // is called implicitly. The base class must contain a default
    // constructor.

    // Default constructor for the derived class.
    public ChangeRequest() { }

    // Instance constructor that has four parameters.
    public ChangeRequest(string title, string desc, TimeSpan jobLen,
                         int originalID)
    {
        // The following properties and the GetNexID method are inherited
        // from WorkItem.
        this.ID = GetNextID();
        this.Title = title;
        this.Description = desc;
        this.jobLength = jobLen;

        // Property originalItemID is a member of ChangeRequest, but not
        // of WorkItem.
        this.originalItemID = originalID;
    }
}
#endregion

#region  多态
public class Shape
{
    // A few example members
    public int X { get; private set; }
    public int Y { get; private set; }
    public int Height { get; set; }
    public int Width { get; set; }

    // Virtual method
    public virtual void Draw()
    {
        Console.WriteLine("Performing base class drawing tasks");
    }
}

public class Circle : Shape
{
    public override void Draw()
    {
        // Code to draw a circle...
        Console.WriteLine("Drawing a circle");
        base.Draw();
    }
}
public class Rectangle : Shape
{
    public override void Draw()
    {
        // Code to draw a rectangle...
        Console.WriteLine("Drawing a rectangle");
        base.Draw();
    }
}
public class Triangle : Shape
{
    public override void Draw()
    {
        // Code to draw a triangle...
        Console.WriteLine("Drawing a triangle");
        // 是用来调用基类的有参数的构造函数,因为子类不能直接继承父类的构造函数
        /*
            base可以完成创建派生类实例时调用其基类构造函数或者调用基类上已被其他方法重写的方法。
            base 关键字用于从派生类中访问基类的成员的构造函数的形参：
            调用基类上已被其他方法重写的方法。
            指定创建派生类实例时应调用的基类构造函数。
        */
        base.Draw();
}
}

public class BaseClass
{
    public virtual void DoWork() { }
    public virtual int WorkProperty
    {
        get { return 0; }
    }
}
public class DerivedClass : BaseClass
{
    public override void DoWork() { }
    public override int WorkProperty
    {
        get { return 0; }
    }
}


//使用新成员隐藏基类成员
public class BaseClassA
{
    public void DoWork() { WorkField++; }
    public int WorkField;
    public int WorkProperty
    {
        get { return 0; }
    }
}

public class DerivedClassA : BaseClassA
{
    public new void DoWork() { WorkField++; }
    public new int WorkField;
    public new int WorkProperty
    {
        get { return 0; }
    }
}

// 阻止派生类重写虚拟成员
public class A
{
    public virtual void DoWork() { }
}
public class B : A
{
    public override void DoWork() { }
}

public class C : B
{
    public sealed override void DoWork() { }
}

public class D : C
{
    public new void DoWork() { }
}
#endregion

