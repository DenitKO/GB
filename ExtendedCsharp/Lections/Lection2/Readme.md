# Разработка приложения на С# (лекции)

## Урок 2. Интерфейсы и обощения

## Содержание

[Интерфейсы](#интерфейсы)

[Индексаторы](#индексаторы)

[Переопределение операторов](#переопределение-операторов)

[Реализация явного и неявного приведения](#реализация-явного-и-неявного-приведения)

[Анонимные типы](#анонимные-типы)

[Записи](#записи-record)

[Обобщения (Generics)](#обобщения-generics)

[Generic наследование](#generic-наследование)

[Generic ограничения](#generic-ограничения)

[Generic методы](#generic-методы)

[Generic интерфейсы](#generic-интерфейсы)

[Generic интерфейсы, ковариантность, контравариантность и инвариантность](#generic-интерфейсы-ковариантность-контравариантность-и-инвариантность)


### Интерфейсы

Интерфейсы в чем-то похожи на абстрактные классы, но, в отличие от последних, не могут
содержать реализацию и хранить состояние

поддерживают (могут содержать) методы, свойства, индексаторы и события.

Важно понимать, когда нужно использовать абстрактный класс, а когда интерфейс:
если требуется использовать одинаковым образом группу объектов, не связанных
наследованием, то интерфейс будет единственным решением. 

C# содержит предопределенные интерфейсы, реализовав которые можно наделить
ваш класс поддержкой различных алгоритмов платформы.


```C# 
[модификатор доступа] interface IИмяИнтерфейса
{
    //методы, свойства, события, индексаторы
}
[модификатор доступа] class ИмяИмяКласса:IИмяИнтерфейса
{
    //методы, свойства, события, индексаторы
}
```


Пример таких интерфейсов это: ICloneable, IConvertible, IDisposable, IConvertible,
IComparable.
IEnumerable для ForEach. В котором нужно обязательно реализовать метод GetEnumerator() с типом IEnumerator который в свою очередь обязан реализовавывать свойство object Current { get; }, и два метода - bool MoveNext(), и void Reset().



```C# 
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
```


Интерфейс не нужно описывать, так как он уже описан и является частью
библиотеки System.

Чтобы сортировка работала правильно, метод CompareTo должен
работать следующим образом: возвращать -1, если текущий объект
меньше переданного, 0, если объекты равны, и 1, если текущий объект
больше переданного в метод.


**Я всё равно описал.**


```C#
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
```


**И вот метод для вставки в Main**

```C#
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
```

[🔝 Наверх](#содержание)

### Индексаторы

C# позволяет добавлять индексаторы в классы, что дает возможность обращаться к
ним тем же способом, что и к элементам массива, то есть с использованием индекса
– квадратных скобок.

```C#
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
```

реализация интерфеса в классе Person

```C#
private Person[] Family;
public int Count {get {return 1 + (Family?.Length ?? 0);}}

public Person this[int index]
{
    get{
        if (index <= 0) return this;
        if (Family is null)
            return null;
        if (Family.Length >= index)
            return Family[index-1];
        return null;
    }
}
```

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

```C#
[модификатор доступа] тип this[type index]
{
    get{}//код получения значения по индексу
    set{}//код установки значения по индексу
}
```

сделаем интерфейс IFamily, который будет добавлять классу, реализующему его, возможность работать с ним как с массивом значений

```C#
namespace Lection2
{
    public interface IFamily
    {
        Person this[int index] { get; }
        int Count { get; }
    }
}
```

пример интерфейса IFamily. С помощью индексатора мы будем обращаться к членам семьи, а свойство Count нам пригодится для определения количества ее членов

```C#
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
```

[🔝 Наверх](#Содержание)

### Переопределение операторов

Пользовательские классы могут перегружать предопределенные операторы языка таким образом, чтобы их можно было применять в сочетании с этими классами.

```C#
public static NameOfClass operator переопределяемый_оператор(операнд/операнды)
{
    код возвращающий результат операции
}
```

Переопределять можно следующие операторы:

+x,-x,!x,~x,++,--, true, false

x+y, x-y, x*y, x/y, x%y

x&y, x|y, x^y,

x==y, x!=y, x<y, x>y, x<=y, x>=y

Операторы true и false, == и !=, < и >, <= и >= могут быть переопределены только в
паре.

напишем класс Fibonacci, который будет подсчитывать числа Фибоначчи с помощью оператора унарного инкремента “++”, применяемого к экземпляру этого класса

```C#
internal class Fibonacci
{
    public int Value { get; private set; } = 1;
    private int _valuePrev = 0;

    public static Fibonacci operator ++(Fibonacci f)
    {
        var temp = f.Value; 
        f.Value = f.Value + f._valuePrev;
        f._valuePrev = temp;

        return f;
    }

    public static Fibonacci operator +(Fibonacci f, int count)
    {
        for (int i = 0; i < count; i++)
        {
            f++;
        }
        return f;
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}

internal class Programm
{
    static void Main(string[] args)
    {
        var a = new Fibonacci();

        //1 1 2 3 5 8 13 21
        for (int i = 1; i <= 8; i++)
        {
            Console.Write($"{a} "); // 1 1 2 3 5 8 13 21 
            a++;
        }

        Console.WriteLine();

        var b = new Fibonacci();

        b = b + 7;

        Console.WriteLine(b); // 21
    }
}
```

[🔝 Наверх](#Содержание)


### Реализация явного и неявного приведения

Для пользовательских классов можно определять операции явного и неявного приведения.

```C#
public static implicit/explicit operator ИмяВозвращаемого типа(параметр)
    {
        return результат;
    }
```

Ключевое слово implicit следует использовать для реализации оператора неявного приведения типа, тогда как explicit для явного.

Важно помнить, что методы неявного преобразования стандартных типов C# никогда не приводят к возникновению исключительных ситуаций (ошибок), соответственно, при разработке своего неявного преобразования следует придерживаться такого же принципа. Если вы по какой-либо причине думаете, что ошибки преобразования все-таки возможны, то такое преобразование следует реализовать как явное.

[🔝 Наверх](#Содержание)

### Анонимные типы

Анонимные типы являют собой простой способ собрать в одном месте значения для последующего использования

```C#
var name = new {имя_свойства = значение,...}
```

Это как будто мы создаём класс только с полями, поэтому нужно указывать имена переменных. Т.к. в классе одинаковое название полей/свойств недопустимо.

### Записи (record)

Если классы предназначены для описания поведения и иерархии наследования, то записи или record предназначены для хранения данных. В общем виде объявление записей выглядит следующим образом:

```C#
[модификатор доступа] record [class/struct] Имя(тип имя)
{
    //поля
}
```

Обратите внимание на круглые скобки, идущие за ключевым словом record – в них можно указать список свойств записи, доступных только для чтения. Это можно сравнить с конструктором класса, но, в отличие от записей, в объекте класса эти параметры остаются локальными и не становятся его частью, если, конечно, не присвоить их каким-либо заранее объявленным полям. Поля record, объявленные в заголовке record (в круглых скобках), являются иммутабельными – это значит, что значения, переданные через конструктор, нельзя будет изменить после создания экземпляра record. Поля, находящиеся в фигурных скобках, являются
мутабельными.

Тип record умеет быть как value, так и reference-типом. По умолчанию все записи являются ссылочными, если для них явно не указать модификатор struct при объявлении. Модификатор class является опциональным и может дополнительно указывать разработчику на то, что запись является reference-типом.

```C#
record class ReferenceRecord(int a)
{
    public int b = 0;
}

record class ValueRecord(int a)
{
    public int b = 0;
}
```

В коде ReferenceRecord являются reference-типом, тогда как ValueRecord – value-типом. Как мы помним из материала предыдущих лекций, value-типы при присваивании и передаче в функцию копируются, тогда как reference-типы передаются по ссылке.
```C#
var record = new Record(10);

var record1 = new ReferenceRecord(10);
var record2 = record1;

record2.b = 20;
System.Console.WriteLine(record1.b); // 20


var record3 = new ValueRecord(10);
var record4 = record1;

record4.b = 20;
System.Console.WriteLine(record3.b); // 0
```

Как видно из кода, после присваивания значения record1 переменной record2 и последующего изменения поля record2.b, при выводе на печать поля record2.a выводится измененное значение. Тогда как схожий код, но уже оперирующий объектами типа record struct, приводит к копированию значения, и измененное значение record4.b никак не влияет на сканированный оригинал
record3.b (строки 21-25). Главное отличие записей от структур и классов заключается в том, что компилятор автоматически реализует для них интерфейс IEquatable, позволяющий сравнивать их с учетом значений, хранящихся в них.

```C#
record Record(int a)
{
    public int b { get; set; } = 0;
}

Static void Main(string[] args)
{
    var record2 = new Record(10);
    var record1 = new Record(10);

    System.Console.WriteLine(record1 == record2); // True

    record1.b = 20;

    System.Console.WriteLine(record1 == record2); // False
    }
```

пока все значения полей первого экземпляра равны значениям полей второго экземпляра, операция сравнения будет возвращать true

```C#
System.Console.WriteLine(record1); // Record { a = 10, b = 20 }
```

Вторым важным методом, автоматически реализуемым компилятором, является ToString(). В отличие от классов, где ToString, если его не переопределить, возвращает имя класса, в записях ToString() возвращает список полей и их значений.

Еще одним важным свойством записей является возможность их копирования (даже record class) с одновременным изменением их полей – копирование с недеструктивной мутацией. Для этого служит ключевое слово with, после которого уже знакомым нам синтаксисом inline инициализации можно задать значение полей, которые мы хотим изменить.

```C#
record Record(int a)
{
    public int b { get; set; } = 0;
}

Static void Main(string[] args)
{
    var record1 = new Record(10);

    Record record2 = record1 with { b = 10 };
    Record record3 = record1 with { b = 15 };

    System.Console.WriteLine(record1); // Record { a = 10, b = 0 }
    System.Console.WriteLine(record2); // Record { a = 10, b = 10 }
    System.Console.WriteLine(record3); // Record { a = 15, b = 0 }
}
```

Иллюстрация показывает пример копирования записей с мутацией отдельных полей.
При это стоит заметить, что происходит именно копирование, в независимости от того является ли Рекорд value или reference типом.

[🔝 Наверх](#Содержание)

## Обобщения (Generics)

Обобщения – это способ создать “универсальную” логику в классе или методе,
откладывая уточнение спецификации до момента вызова конструктора класса. Это
достигается путем применения обобщенных параметров.

```C#
[модификатор доступа] class имя<T1,T2,..,Tn>
{
    T1 Имя1; //переменная
    T1 Имя2 {get;set;} //свойство
    T2 Имя(Tn имя) //метод
    {
    }
}
```

Где T1, T2, Tn – имена типов (их может быть произвольное количество). Обобщения активно используются C#, в следующей лекции у нас будет достаточно таких примеров (Коллекции). Мы уже встречались с обобщениями в таком типе данных, как кортежи.

```C#
class SimpleTuple<T1, T2>
{
    public T1 Item1 { get; init; }
    public T2 Item2 { get; init; }

    public SimpleTuple(T1 item1, T2 item2)
    {
        this.Item1 = item1;
        this.Item2 = item2;
    }

    public override string ToString()
    {
        return $"{{{Item1}, {Item2}}}";
    }
}

SimpleTuple<int, string> simpleTuple1 = new SimpleTuple<int, string>(10, "ABC");
Console.WriteLine(simpleTuple1); // {10, ABC}
```

Важно понимать, что при создании экземпляра класса с параметрами Value-типа для каждого уникального сочетания .Net создаст свой уникальный класс, на основе которого будет создан объект. Тогда как для Reference-типов .Net обойдется обобщенной реализацией.

[🔝 Наверх](#Содержание)

### Generic наследование

Обобщенные классы можно наследовать.

```C#
class SimpleTuple1<T1> : SimpleTuple<T1, int>
{
    public SimpleTuple1(T1 item1, int item2) : base(item1, item2)
    {
    }
}
class SimpleTuple2 : SimpleTuple<string, int>
{
    public SimpleTuple2(string item1, int item2) : base(item1, item2)
    {
    }
}
```

Два примера наследования демонстрируют частичное (<T1,int>) и полное (<string,int>) замещение обобщенных параметров родительского класса.


### Generic ограничения

Объявления обобщенных типов могут содержать ограничения, задающие типы, которые могут быть использованы для создания его экземпляров.

```C#
[модификатор доступа] class имя<T1,T2,..,Tn> where ограничения
{
    T1 Имя1; //переменная
    T1 Имя2 {get;set;} //свойство
    T2 Имя(Tn имя) //метод
    {
    }
}
```

Ограничения призваны сузить диапазон значений, которые может использовать обобщенный класс, при этом расширяя возможности: чем строже ограничение, тем больше класс будет знать о типе своих параметров.

Другими **НОРМАЛЬНЫМИ** словами, когда мы не ограничиваем дженерик, компилятор считает что перед нами тип object, когда при ограничении каким то классом или интерфейсом он видит его как этот класс или интерфейс и соответсвенно, его поля и методы.

Основные ограничения в блоке where:
- where T: struct – аргумент обобщенного типа должен быть value-типом (например, это может быть структура или же целочисленная переменная),
- where T: class – аргумент должен быть reference-типом,
- where T: new() – аргумент должен иметь конструктор по умолчанию,
- where T: ИмяКласса – аргумент должен принадлежать потомкам определенного класса,
- where T: ИмяИнтерфейса – аргумент должен поддерживать определенный интерфейс.


```C#
class Utility<T> where T:struct
{
    public static void Swap(ref T v1, ref T v2)
    {
        T temp = v1;
        v1 = v2;
        v2 = temp;
    }
}

int a = 10, b = 20;
char c1 = 'B', c2 = 'B';

Utility<int>.Swap(ref a, ref b);

Console.WriteLine($"a={a}, b = {b}"); // a=20, b = 10

Utility<char>.Swap(ref c1, ref c2);

Console.WriteLine($"c1={c1}, c2 = {c2}"); // c1=B, c2 = B
```

Постарайтесь применить к своему обобщенному классу как можно больше ограничений. Если ограничение не нарушает ваш замысел, при этом помогает избежать его неправильного использования, то обязательно примените его.



[🔝 Наверх](#Содержание)

### Generic методы

Помимо классов обобщения могут быть объявлены также и в методах

```C#
class Utility2
{
    public static void Swap<T>(ref T v1, ref T v2)
    {
        T temp = v1;
        v1 = v2;
        v2 = temp;
    }
}

int a = 10, b = 20;
char c1 = 'B', c2 = 'B';
string s1 = "ABC", s2 = "BCD";

Utility2.Swap<int>(ref a, ref b);

Console.WriteLine($"a={a}, b = {b}"); // a=20, b = 10

Utility2.Swap(ref c1, ref c2);

Console.WriteLine($"c1={c1}, c2 = {c2}"); // c1=B, c2 = B

Utility2.Swap(ref s1, ref s2);

System.Console.WriteLine($"c1={s1}, s2 = {s2}"); // c1=BCD, s2 = ABC
```

Как видно в коде, мы убрали обобщенный аргумент из объявления класса Utility и переместили его в объявление метода Swap. Обратите внимание на то, как изменился вызов функции Swap. В строке 20 мы явно указываем тип обобщенного параметра, однако это не обязательно: поскольку компилятор может определить тип параметра из аргументов нашего метода, мы можем опустить явное указание этого типа в угловых скобках после имени функции.

[🔝 Наверх](#Содержание)

### Generic интерфейсы

Обобщенные интерфейсы позволяют избежать проблем, связанных с упаковкой-распаковкой value-типов при их имплементации в классах, работающих с таковыми. Это значительно ускоряет работу многих алгоритмов. Возьмем, например, уже знакомый нам интерфейс IComparable.

```C#
internal struct Metric : IComparable
{
    public int Month;
    public int Temperature;
    public int Days;

    public int CompareTo(object? obj)
    {
        var metric = (Metric)obj;

        int res = this.Month.CompareTo(metric.Month);
        if (res != 0) 
        {
            return res;
        }
        else
        {
            return this.Temperature.CompareTo(metric.Temperature);
        }
    }

    public override string ToString()
    {
        return $"{{{Month}:{Temperature}:{Days}}}";
    }
}

Metric[] tempratures = new Metric[]
            {
                new Metric{ Month = 1, Temperature = -1, Days = 10 },
                new Metric{ Month = 8, Temperature = 22, Days = 1 },
                new Metric{ Month = 1, Temperature = -10, Days = 2 },
                new Metric{ Month = 2, Temperature = -1, Days = 3 },
                new Metric{ Month = 5, Temperature = 10, Days = 4 },
                new Metric{ Month = 1, Temperature = -2, Days = 5 },
                new Metric{ Month = 2, Temperature = -30, Days = 1 },
                new Metric{ Month = 1, Temperature = 2, Days = 3 },
            };

            Array.Sort(tempratures);

            foreach (var t in tempratures)
            {
                Console.Write(t + " ");
            }
```

Реализуя интерфейс, нужно самостоятельно привести входной параметр к нужному типу. Если тип, реализующий IComparable, является value-типом, то каждый раз при вызове CompareTo его аргумент будет упакован в object, а в самом методе распакован для проведения процедуры сравнения. Обобщенный интерфейс IComparable решает эту проблему.

короче так как CompareTo принимает дженерики, или реализует дженерики можно не прибегать к явному преобразованию тут

```C#
internal struct Metric : IComparable

public int CompareTo(object? obj)
    {
        var metric = (Metric)obj;
        ...
    }
```

а просто сразу использовать нужный тип

```C#
internal struct Metric : IComparable<Metric>

public int CompareTo(Metric metric)
    {
        ...
    }
```

теперь мы используем обобщенный интерфейс IComparable<T>, унаследовав его с аргументом, соответствующим типу нашей структуры – IComparable<Metric>. Также изменился метод CompareTo: теперь он не использует object, и нам не требуется дорогостоящая распаковка, чтобы перейти к сравнению

[🔝 Наверх](#Содержание)

### Generic интерфейсы, ковариантность, контравариантность и инвариантность

#### От [Программирование - это просто](https://www.youtube.com/watch?v=Zxfyj-m0c7w)
- Ковариантность интерфейса **\<out T>** позволяет приводить дочерний класс обобщения к родительскому
Она накладывает определённые ограничения.


```C#
public class Animal{}
public class Cat : Animal {}
public interface IStudent<T> {}
public class Student<T> : IStudent<T>
```

```C#
//так можно 
public interface IStudent<out T> 
{
    // Выходной тип данных может быть T, а входной должен быть другим.
    T Move (int distance); <--
}

IStudent<Animal> student1 = new Student<Animal>();
// позволяет приводить дочерний класс обобщения к родительскому
IStudent<Animal> student2 = new Student<Cat>(); <--

//так нельзя
public interface IStudent<out T> 
{
    // В ковариантном интерфейсе
    // Входной тип данных не может быть того же типа что и обобщение
    // Parameter must be input-safe.
    void Set(T animal); <--
}

IStudent<Cat> student = new Student<Animal>;
```

- Контрвариант интерфейса **\<in T>** наоборот. Позволяет приводить общий класс к дочернему. Тут обратная аналогия, думаю избыточные примеры неуместны.

```C#
//так можно 
public interface IStudent<in T> 
{
    void Set(T animal); <--
}

IStudent<Cat> student = new Student<Animal>; <--

//так нельзя
public interface IStudent<in T> 
{
    T Move (int distance); <--
}

IStudent<Animal> student2 = new Student<Cat>(); <--
```

#### От [Метанита](https://metanit.com/sharp/tutorial/3.27.php)
- Ковариантность: позволяет использовать более конкретный тип, чем заданный изначально

- Контравариантность: позволяет использовать более универсальный тип, чем заданный изначально

- Инвариантность: позволяет использовать только заданный тип

**По умолчанию все обобщенные интерфейсы являются инвариантными.**

#### От [ExtremCode](https://www.youtube.com/watch?v=BvmvcHroPRg)

Вот мы пробуем например сделать что то такое

```C#
List<object> list = new List<string>();
```

нам выдаст ошибку **Cannot [implicitly](#реализация-явного-и-неявного-приведения) convert type 'System.Collection.Generic.List<string>' to 'System.Collection.Generic.List<object>'**

Что значит. Не можно неявно конвертировать тип такой то в такой то

Все обобщенные интерфейсы по умолчанию инвариантны. Инвариантность обязует вас использовать лишь тот тип, который вы указали в обобщении.

Ковариантность позволяет использовать в обобщениях более конкретные типы данных, чем мы указывали изначально

```C#
IList<object> objects = new MyList<string>();
```
IEnumerable - ковариантный обобщенный интерфейс "из коробки"

```C#
abstract class Animal{}
class Cat : Animal{}
static void Main(){
    List<Cat> list = new List<Cat>();
    IEnumerable<Cat> cats = list;
    IEnumerable<Animal> animal = cats;
    // Почему List<T> не ковариантен, а инвариантен?
    // Потому что это актуально для обобщенных интерфейсов (Еще на самом деле для делегатов)
}
```
еще пример

```C#
abstract class Engine{}
class V8Engine : Engine {}
interface ICar<out T> where T : Engine{
    T GetEngine();
}
class Lada : ICar<V8Engine>
{
    public V8Engine GetEngine(){
        return new V8Engine();
    }
}
static void Main(){
    Lada lada = new Lada();
    ICar<V8Engine> vEightCar = lada;
    ICar<Engine> someCar = lada; // тут была бы ошибка "cannot implicitly" если бы не <out T>
}
```
контрвариантность же наоборот, позволяет приводить общие типы, взамен конкретных

с примером для контрвариантности <in T> уже сложнее, реализация интерфейса IComparable

попробуем запилить основу для стек зоопарка

```C#
abstract class Animal{}
class Cat : Animal{}
interface IPushable<in T> where T : Animal
{
    void push <T obj>;
}

class Stack<T> : IPushable<T> where T : Animal
{
    public void Push(T obj){};
}

static void Main(string[] args)
{
    IPushable<Cat> cats = new Stack<Animal>();
    cats.Push(new Cat());
}
```
Ковариантность и контравариантность не очень безопасны, потому что они плевать хотели на типизацию
Во время компиляции ошибочное приведение типов невозможно, только в рантайме.

#### От GeekBrains

Ковариантность и контравариантность – это возможность использовать объекты-наследники или же объекты-родители вместо изначального объекта. Ковариантность позволяет использовать объект унаследованного типа вместо указанного изначально. Ковариантность похожа на полиморфизм. Контравариантность наоборот позволяет использовать объект родительского типа вместо изначально указанного. Инвариантность – это невозможность использовать объект иного типа.

Ковариантный интерфейс
```C#
interface ICovariant<out T>
{
    T GetDefault();
}

class SomeClass<T> : ICovariant<T>
{
    public T GetDefault()
    {
        return default(T);
    }
}

ICovariant<string> str1 = new SomeClass<string>();
ICovariant<object> obj1 = str1;

ICovariant<object> obj2 = new SomeClass<string>();
// ICovariant<string> str2 = obj2; // так нельзя
```

ковариантные значения могут быть только возвращаемыми значениями, но никак не параметрами метода. Если мы попытаемся использовать T в качестве параметра метода, то получим ошибку.




```C#
interface ICovariant<out T>
{
    T GetDefault();
    void WrongFunction(T arg); <-- неверно
}
```
Контрвариантный интерфейс
```C#
interface IContrvariant<in T>
{
    void DoSomething(T arg);
}

class SomeClass2<T> : IContrvariant<T>
{
    public void DoSomething(T arg)
    {              
    }
}

IContrvariant<object> obj2 = new SomeClass2<object>();
IContrvariant<string> str2 = obj2;

IContrvariant<string> str1 = new SomeClass2<string>();
// IContrvariant<object> obj1 = str1; // так нельзя
```

Контравариантность работает только в одну сторону – от родителей к потомкам.

```C#
interface IContrvariant<in T>
{
    T <-- DoSomething(T arg);
}
```
 инвариантный интерфейс
```C#
interface IInvariant<T>
{
    T GetDefault();
    void DoSomething(T arg);
}

class SomeClass3<T> : IInvariant<T>
{
    public void DoSomething(T arg)
    {              
    }

    public T GetDefault()
    {
        return default(T);
    }
}

IInvariant<object> obj2 = new SomeClass3<object>();
// IInvariant<string> str2 = obj2; // так нельзя

IInvariant<string> str1 = new SomeClass3<string>();
// IInvariant<object> obj1 = str1; // так нельзя
```

В коде видно, что инвариантный интерфейс не позволяет использовать себя с интерфейсом наследника или с интерфейсом потомка. Параметр T интерфейса может быть как входящим (параметр метода), так и возвращаемым.

[🔝 Наверх](#Содержание)