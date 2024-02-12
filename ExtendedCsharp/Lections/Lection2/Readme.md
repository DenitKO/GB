# Разработка приложения на С# (лекции)

## Урок 2. Интерфейсы и обощения

### Интерфейсы

Интерфейсы в чем-то похожи на абстрактные классы, но, в отличие от последних, не могут
содержать реализацию и хранить состояние

поддерживают (могут содержать) методы, свойства, индексаторы и события.

Важно понимать, когда нужно использовать абстрактный класс, а когда интерфейс:
если требуется использовать одинаковым образом группу объектов, не связанных
наследованием, то интерфейс будет единственным решением. 

C# содержит предопределенные интерфейсы, реализовав которые можно наделить
ваш класс поддержкой различных алгоритмов платформы.

<pre><code>
[модификатор доступа] interface IИмяИнтерфейса
{
    //методы, свойства, события, индексаторы
}
[модификатор доступа] class ИмяИмяКласса:IИмяИнтерфейса
{
    //методы, свойства, события, индексаторы
}
</code></pre>

Пример таких интерфейсов это: ICloneable, IConvertible, IDisposable, IConvertible,
IComparable.

<pre><code>
public interface IComparable
{
    int CompareTo (object? obj);
}

public CompareTo(object? obj)
{
    if (obj == null)
        return -1;
    return this.Birthday.CompareTo((obj as Person).Birthday);
}
</code></pre>

Интерфейс не нужно описывать, так как он уже описан и является частью
библиотеки System.

Чтобы сортировка работала правильно, метод CompareTo должен
работать следующим образом: возвращать -1, если текущий объект
меньше переданного, 0, если объекты равны, и 1, если текущий объект
больше переданного в метод.


**Я всё равно описал.**

<pre><code>
internal class ComparablePerson : IComparable
{
    public ComparablePerson(int age, double height, double weight)
    {
        this.age = age;
        this.height = height;
        this.weight = weight;
    }

    public int age;
    public double height;
    public double weight;

    public int CompareTo(object? obj)
    {
        ComparablePerson p = obj as ComparablePerson;

        if (p != null)
        {
            if (age > p.age)
            {
                return -1;
            }
            else if (age < p.age)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        else
        {
            throw new Exception("Параметр должен быть типа ComparablePerson");
        }
    }
}
</code></pre>

**И вот метод для вставки в Main**
<pre><code>
public static void ComparablePerson()
{
    ComparablePerson[] persons = new ComparablePerson[10];

    Random r = new Random();

    for (int i = 0; i < persons.Length; i++)
    {
        persons[i] = new ComparablePerson(r.Next(1, 95), r.Next(30, 190), r.Next(20, 95));

        Console.WriteLine($"age: {persons[i].age} height: {persons[i].height} weight: {persons[i].age}");
    }

    Array.Sort(persons);

    Console.WriteLine(new string('-', 30));

    for (int i = 0; i < persons.Length; i++)
    {
        Console.WriteLine($"age: {persons[i].age} height: {persons[i].height} weight: {persons[i].age}");
    }

    Console.ReadKey();
}
</code></pre>

### Индексаторы

C# позволяет добавлять индексаторы в классы, что дает возможность обращаться к
ним тем же способом, что и к элементам массива, то есть с использованием индекса
– квадратных скобок.

<pre><code>
[модификатор доступа] тип this[type index]
{
    get{}//код получения значения по индексу
    set{}//код установки значения по индексу
}

public intarface IFamily
{
    Person this[int index]
    {
        get;
    }
    int Count { get;}
}
</code></pre>

реализация интерфеса в классе Person

<pre><code>
private Person[] Family;
public int Count {get {return 1 + (Family?.Length ?? 0);}}

public Person this[int index]
{
    get{
        if (index <=b) return this;
        if (Family is null)
            return null;
        if (Family.Length >= index)
            return Family[index-1]
        return null;
    }
}
</code></pre>

Обратите внимание на использование оператора “?”, идущего после Family и
оператора "null-объединения - ??” идущего после Length – с помощью “?” выражение Family?.Length
безопасно выполняется и возвращает null, если массив Family еще не
инициализирован. Если же он инициализирован, то возвращается длина массива. В
свою очередь, оператор “??” анализирует значение Family?.Length, и если оно равно
null, возвращает 0

??= забыли упомянуть о "операторе присваивания объединения со значением Null",
работает с 8 версии

### Индексаторы

C# позволяет добавлять индексаторы в классы, что дает возможность обращаться к ним тем же способом что и к элементам массива, то есть с использованием индекса – квадратных скобок.

<pre><code>
[модификатор доступа] тип this[type index]
{
    get{}//код получения значения по индексу
    set{}//код установки значения по индексу
}
</code></pre>

сделаем интерфейс IFamily, который будет добавлять классу, реализующему его, возможность работать с ним как с массивом значений

<pre><code>
namespace Lection2
{
    public interface IFamily
    {
        Person this[int index] { get; }
        int Count { get; }
    }
}
</code></pre>

пример интерфейса IFamily. С помощью индексатора мы будем обращаться к членам семьи, а свойство Count нам пригодится для определения количества ее членов

    private Person[] Family;
    public int Count { get { return 1 + (Family?.Length ?? 0); } }

    public Person this[int index] {
        get {
            if (index <= 0) return this;
            if (Family is null)
                return null;
            if (Family.Length >= index)
                return Family[index-1];
            return null;
        }
    }

### Переопределение операторов

    public static operator оператор(операнд/операнды)
    {
        код возвращающий результат операции
    }

Переопределять можно следующие операторы:

+x,-x,!x,~x,++,--, true, false

x+y, x-y, x*y, x/y, x%y

x&y, x|y, x^y,

x==y, x!=y, x<y, x>y, x<=y, x>=y

Операторы true и false, == и !=, < и >, <= и >= могут быть переопределены только в
паре.


### Реализация явного и неявного приведения

-

### Анонимные типы

-

### Записи (record)

-

## Обобщения (Generics)

-

### Generic наследование

-

### Generic методы

-

### Generic интерфейсы

-

### Generic интерфейсы, ковариантность, контравариантность и инвариантность

-
