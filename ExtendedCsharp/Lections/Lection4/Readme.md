# Разработка приложения на С# (лекции)

## Содержание 

## Урок 4. Коллекции (часть 2)

[Dictionary<TKey,TValue>](#dictionarytkeytvalue)

[Особенности работы с Dictionary](#особенности-работы-с-dictionary)

- [Основные методы Dictionary<TKey, TValue>:](#основные-методы-dictionarytkey-tvalue)

[HashSet<T>](#hashset)

[Особенности работы с HashSet](#особенности-работы-с-hashset)

[Другие коллекции из System.Collections.Generic](#другие-коллекции-из-systemcollectionsgeneric)

[LINQ](#linq)

[Особенности работы с LINQ](#linq)

### Dictionary<TKey,TValue>

Dictionary удобен тем, что доступ к любому элементу с большой вероятностью можно получить за константную скорость O(1) и в более редких случаях O(N).

- GetHashCode() - метод позволяет получить хеш-код объекта – специальное значение, результат работы однонаправленной функции. Типом этого значения является int. Алгоритм генерации хеш-кода может отличаться в разных версиях .Net. Платформа старается обеспечить равномерное распределение хеш-кодов в диапазоне int, но не гарантирует уникальность значений. Возможна ситуация, когда два объекта имеют одинаковый хеш-код: значение int находится в диапазоне от -2,147,483,648 до 2,147,483,647, и этого диапазона может не хватить в условиях, когда количество объектов превышает это значение.

- Equals(object) - позволяет сравнить текущий экземпляр объекта с экземпляром, переданным в метод. Если они идентичны, метод вернет true, иначе false. Для reference-типов Equals будет сравнивать, являются ли объекты одним экземпляром, путем сравнения ссылок на эти объекты (метод ReferenceEquals); для value-типов метод будет сравнивать значения.

```C#
struct SomeStruct
{
    public int Field;
}
static void Main(string[] args)
{
    var a = new object();
    var b = new object();
    var c = a;
    Console.WriteLine(a.Equals(b)); // False
    Console.WriteLine(a.Equals(c)); // True

    var sa = new SomeStruct();
    var sb = new SomeStruct();

    sa.Field = 10;


    Console.WriteLine(sa.Equals(sb)); // Fasle

    sb.Field = 10;

    Console.WriteLine(sa.Equals(sb)); // True
}
```
Теперь когда мы разобрались как работают эти два метода, настало время объяснить как устроен Dictionary. Словарь содержит массив типа int называемый “корзины” или buckets, а также массив entries - Entry[] предназначенный для хранения

```C#
private uint hashCode;

public int next;

public TKey key;

public TValue value;
```

Каждый раз когда мы добавляем пару Ключ-Значение (TKey-TValue) в словарь происходит следующее: определяется номер позиции в массиве buckets по следующей формуле:

```C#
int bucketNum = (value.GetHashCode() & 0x7fffffff) % buckets.Length;
```
логическое умножение на 0x7fffffff требуется чтобы инвертировать значение хеш кода в случае если оно отрицательно

%bucket.Length требуется для распределения хеш кодов по диапазону корзин от 0 до buckets.Length

Затем создается новая структура Entry и ее свойства Key и Value устанавливаются равными TKey, TValue. Структура затем добавляется в массив entries (в следующую свободную позицию), а ссылка на ее индекс присваивается buckets[bucketNum].

```C#
entries[nextEntry] = new Entry(TKey,TValue);
buckets[bucketNum] = nextEntry;
nextEntry++;
```
Когда мы получаем элемент из словаря, происходит следующее, мы получаем номер корзины по формуле:
```C#
int bucketNum = (value.GetHashCode() & 0x7fffffff) % buckets.Length;
```

далее получаем номер корзины

```C#
int entryNum = buckets[bucketNum];
```

И получаем элемент из корзины.

```C#
return entries[entryNum];
```

Как видите операция выполняется за константное время. Но как быть в ситуациях когда у двух объектов один хеш-код и

```C#
int bucketNum = (value.GetHashCode() & 0x7fffffff) % buckets.Length;
```

укажет на одну и ту же позицию в массиве buckets. Такая ситуация называется коллизией и разрешается следующим образом. Поняв что buckets[bucketNum], уже ссылается на какой-то элемент, словарь поступит следующим образом: он создаст новый Entry

```C#
entries[nextEntry] = new Entry(TKey,TValue);
```

а потом присвоит его индекс но не в массив buckets[bucketNum] (там уже лежит индекс предыдущего Entry) а в

```C#
entries[buckets[bucketNum]].Next = nextEntry;
```
То есть entry может ссылаться на следующее entry с ключом TKey имеющим аналогичный индекс в массиве buckets. Теперь давайте разберем то как происходит извлечение элемента из словаря в случае если имеет место коллизия. первый шаг универсален - мы получаем номер корзины по формуле

```C#
int bucketNum = (value.GetHashCode() & 0x7fffffff
%buckets.Length;
```

после этого мы переходим в entries[bucketNum]

```C#
var entry = entries[buckets[bucketNum]];
```
и проверяем его значение Next Если Next не равняется -1 (означает что следующей элемента нет) значит нам нужно выбрать TValue перебрав текущий и возможно следующие Entry. В процессе перебора мы определяем искомый TValue сравнивая TKey каждого Entry(с помощью метода Equals) с тем что передан в индексатор. В коде это можно упрощенно выразить следующим образом.:

```C#
var entry = entries[buckets[bucketNum]];
while(entry.Next>=0)
{
if(entry.TKey.Equals(TKey))
return entry.TValue;
else
entry = entries[entry.Next];
}
```

Как уже упоминалось сложность поиска в этом случае O(N) Словарь поддерживает следующие интерфейсы: IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable, IDictionary, ICollection, IReadOnlyDictionary<TKey, TValue>, IReadOnlyCollection<KeyValuePair<TKey,TValue>>,ISerializable,IDeserializationCallback where TKey : notnull

Класс Dictionary имеет множество конструкторов, опишем лишь некоторые из них:
- Dictionary() - конструктор по умолчанию
- Dictionary(IEnumerable<TKey, TValue>) - создает словарь из элементов
полученных из энумератора объекта, класс которого поддерживает
соответствующий интерфейс
- Dictionary(IDictionary<TKey, TValue>) - создает словарь на основе объекта
класс которого поддерживает интерфейс IDictionary<TKey, TValue>
- Dictionary(IEqualityComparer<TKey>) - создает словарь компаратором предопределяющим поведение при сравнении элементов (требуется в случаях коллизии) 

Помимо этого класс Dictionary имеет конструкторы позволяющие определять размер внутреннего хранилища выделенного под хранение элементов в сочетании с уже описанными выше параметрами, а также конструкторы с информацией о сериализации и десериализции объекта. Свойства Dictionary<TKey,TValue>:
- Comparer - возвращает IEqualityComparer. Если IEqualityComparer не был явно задан в конструкторе от в этом качестве выступает EualityComarer<TKey>.Default который реализует сравнение путем вызова метода Equals.
- Count - получает количество элементов Dictionary
- Item[TKey] - получает/записывает элемент из словаря с помощью переданного в индексатор ключа
- Keys - коллекция ключей
- Values - коллекция значений

```C#
static void Main(string[] args)
{
    var capitals = new Dictionary<string, string>();

    capitals["Россия"] = "Москва";
    capitals["Бразилия"] = "Бразилиа";
    capitals["Австралия"] = "Камберра";
    capitals["Германия"] = "Берлин";

    Console.WriteLine("Столицы:");
    foreach (string capital in capitals.Values)
    {
        Console.Write(capital + ", ");
    }

    Console.WriteLine();
    Console.WriteLine("Страны:");
    foreach (string country in capitals.Keys)
    {
        Console.Write(country + ", ");
    }

    Console.WriteLine();
    Console.WriteLine("Столицы стран:");
    foreach (KeyValuePair<string, string> capital in capitals)
    {
        Console.WriteLine($"Столица страны {capital.Key} = {capital.Value}.");
    }
}
```

dictionary[TKey] = value; - создаст значение если его нет

value = dictionary[TKey]; - вызовет ошибку если значения TKey нет в словаре

[🔝 Наверх](#содержание)

#### Основные методы Dictionary<TKey, TValue>:

- Add(TKey, TValue) - добавляет значение TValue для ключа TKey в словарь. Если значение уже есть в словаре, операция приведет к ошибке времени выполнения.

```C#
var capitals = new Dictionary<string, string>();


capitals.Add("Россия", "Москва");
capitals.Add("Бразилия", "Бразилиа");
capitals.Add("Австралия", "Камберра");
capitals.Add("Германия", "Берлин");

Console.WriteLine("Столицы стран:");
foreach (KeyValuePair<string, string> capital in capitals)
{
    Console.WriteLine($"Столица страны {capital.Key} = {capital.Value}.");
}
```
- Clear() - очищает словарь, удаляя из него все ключи и значения

```C#
var capitals = new Dictionary<string, string>();


capitals.Add("Россия", "Москва");
capitals.Add("Бразилия", "Бразилиа");
capitals.Add("Австралия", "Камберра");
capitals.Add("Германия", "Берлин");

capitals.Clear();

Console.WriteLine("Столицы стран:"); // Столицы стран:
foreach (KeyValuePair<string, string> capital in capitals)
{
    Console.WriteLine($"Столица страны {capital.Key} = {capital.Value}."); //
}
```

- Contains(TKey) - метод возвращает true если указанный ключ имеется в словаре
- Contains(TValue) - метод возвращает true если указанное значение имеется в словаре

```C#
var capitals = new Dictionary<string, string>();


capitals.Add("Россия", "Москва");
capitals.Add("Бразилия", "Бразилиа");
capitals.Add("Австралия", "Камберра");
capitals.Add("Германия", "Берлин");

capitals.Clear();

Console.WriteLine($"В словаре есть ключ Россия? - " + capitals.ContainsKey("Россия")); // True
Console.WriteLine($"В словаре есть ключ Франция? - " + capitals.ContainsKey("Франция")); // False
Console.WriteLine($"В словаре есть значение Москва? - " + capitals.ContainsKey("Москва")); // True
Console.WriteLine($"В словаре есть ключ Париж? - " + capitals.ContainsKey("Париж")); // False
```

- EnsureCapacity(int) - увеличивает(если требуется) размер внутреннего хранилищи словаря до указанного в параметре размера
- Equals(object) - возвращает true если переданный в метод объект равняется объекту переданному в метод. Критерии равенства можно задавать самостоятельно переопределяя этот метод в своих классах. В коллекции Stack, метод Equals возвращает true в случае если сравниваемый объект является тем же что объект метод которого мы вызываем.
- GetEnumerator() - возвращает энумератор, с помощью него мы выводим элементы стека в цикле foreach. При этом свойство Current энумератора равняется паре ключ-значение.
- GetHashCode() - возвращает хеш-код объекта
- Remove(TKey) - удаляет элемент с указанным ключом из словаря.Метод возвращает true в случае успеха и false если элемент не найден

```C#
var capitals = new Dictionary<string, string>();


capitals.Add("Россия", "Москва");
capitals.Add("Бразилия", "Бразилиа");
capitals.Add("Австралия", "Камберра");
capitals.Add("Германия", "Берлин");

capitals.Remove("Австралия");

Console.WriteLine("Столицы стран:"); // Столицы стран:
foreach (KeyValuePair<string, string> capital in capitals)
{
    Console.WriteLine($"Столица страны {capital.Key} = {capital.Value}."); //
}
```
- Remove(TKey, out TValue) - удаляет элемент с указанным ключом из словаря, при этом копирую значение TValue в out параметр. true/false возвращается в зависимости от результата удаления (как и в прошлом методе)

```C#
var capitals = new Dictionary<string, string>();

capitals.Add("Россия", "Москва");
capitals.Add("Бразилия", "Бразилиа");
capitals.Add("Австралия", "Камберра");
capitals.Add("Германия", "Берлин");

if (capitals.Remove("Австралия", out string removed))
{
    Console.WriteLine("Удалённая столица-" + removed); // Удалённая столица-Камберра
}

if (capitals.Remove("Кения", out string removed2)) //
{
    Console.WriteLine("Удалённая столица-" + removed2);
}

Console.WriteLine("Столицы стран:");
foreach (KeyValuePair<string, string> capital in capitals)
{
    Console.WriteLine($"Столица страны {capital.Key} = {capital.Value}.");
}
```

- TrimExcess() - урезает размер памяти используемой для хранения элементов до актуального размера (равного уже имеющемуся количеству элементов)
- TrimExcess(int) - урезает размер памяти используемой для хранения элементов до указанного размера
- TryAdd(TKey,TValue) - метод аналогичен методы Add за исключением ошибки при попытки добавления уже имеющегося элемента. Метод TryAdd возвращает false если добавление не удалось завершить из-за наличия в словаре элемента с TKey, тогда как метод Add в такой ситуации приведет к возникновению ошибки времени выполнения

```C#
var capitals = new Dictionary<string, string>();

capitals.Add("Россия", "Москва");
capitals.Add("Бразилия", "Бразилиа");
capitals.Add("Австралия", "Камберра");
capitals.Add("Германия", "Берлин");

if (capitals.TryAdd("Австралия", "Вашингтон") == false)
{
    Console.WriteLine("Элемент уже есть в словаре"); // Элемент уже есть в словаре
}
else
    Console.WriteLine("Элемент добавлен");

if (capitals.TryAdd("Австрия", "Вена") == false)
{
    Console.WriteLine("Элемент уже есть в словаре");
}
else
    Console.WriteLine("Элемент добавлен"); // Элемент добавлен

Console.WriteLine("Столицы стран:");
foreach (KeyValuePair<string, string> capital in capitals)
{
    Console.WriteLine($"Столица страны {capital.Key} = {capital.Value}.");
}
```

-  TryGetValue(TKey, out TValue) - метод пытается получить значение из словаря в out параметр TValue. В случае успеха возвращает true.

```C#
var capitals = new Dictionary<string, string>();

capitals.Add("Россия", "Москва");
capitals.Add("Бразилия", "Бразилиа");
capitals.Add("Австралия", "Камберра");
capitals.Add("Германия", "Берлин");

if (capitals.TryGetValue("Австралия", out string value))
{
    Console.WriteLine(value); // Камберра
}
else
    Console.WriteLine("Элемент не найден");

if (capitals.TryGetValue("Австрия", out value))
{
    Console.WriteLine(value);
}
else
    Console.WriteLine("Элемент не найден"); // Элемент не найден
```

[🔝 Наверх](#содержание)

### Особенности работы с Dictionary

Словарь применяется везде где нужен быстрый доступ к элементам по ключу. Другое название словаря - хеш-таблица или hashtable. Если доступ по ключу не нужен

```C#
static void Main(string[] args)
{
    var s = "Текст с повторяющимися повторяющимися повторяющимися словами. Выведи количество повторов вместе со словами";

    StringBuilder sb = new StringBuilder();
    Dictionary<string, int> count = new Dictionary<string, int>();

    foreach (var myChar in s)
    {
        if (" ,.-".Contains(myChar))
        {
            if (sb.Length > 0)
            {
                if (count.ContainsKey(sb.ToString()))
                {
                    count[sb.ToString()]++;
                }
                else
                    count[sb.ToString()] = 1;
                sb.Clear();
            }
        }
        else
            sb.Append(myChar);
    }
    /*
     * вторая проверка после окончания цикла нужна
     * так как в конце предложения может не оказаться проблема
     * или знака препинания и условие в цикле не сработает
     */
    if (count.ContainsKey(sb.ToString()))
    {
        count[sb.ToString()]++;
    }
    else
        count[sb.ToString()] = 1;

    foreach (var pair in count)
    {
        Console.WriteLine($"Слово '{pair.Key}' повторяется {pair.Value} раз");
    }
}
```
Аналогичный код, сделанный с помощью списка занял бы значительно больше времени так как нам пришлось бы каждый раз искать ключ за O(N) времени (помните поиск в списке). Здесь же поиск происходит за O(1) и программа работает ровно столько времени сколько требуется для того чтобы перебрать нашу строку и собрать из нее отдельные слова.

Не всегда Dictionary является идеальным примером. Если мы знаем весь диапазон значений которыми мы оперируем то бывают случаи когда значительно проще использовать простой массив для операции подсчета

Давайте покажем это на примере подсчета количества повторов символов в строке с английским текстом. Условимся что буквы могут быть только английские и заглавные буквы равняются строчным.

```C#
var s = "To use or not to use Dictionary";

Dictionary<char, int> count = new Dictionary<char, int>();

foreach (var myChar in s)
{
    if (!Char.IsAsciiLetter(myChar))
        continue;
    var lowerChar = Char.ToLower(myChar);
    if(count.ContainsKey(lowerChar))
        count[lowerChar]++;
    else 
        count[lowerChar] = 1;
}

foreach (var pair in count)
{
    Console.WriteLine($"{pair.Key} = {pair.Value}");
}
```

Мы перебираем наш текст и если символ не является буквой мы пропускаем виток итерации, в противном случае мы приводим символ к нижнему регистру и записываем его в словарь. По завершению цикла мы выводим на экран наш словарь с частотой каждого из символов.

Задача казалось бы решена, но как уже было сказано раньше, если можно обойтись более простой структурой данных то лучше так и сделать: мы знаем что в английском алфавите 26 буквы и каждой из буквы можно назначить номер от 0 до 25 в том порядке в котором они следуют в алфавите. Тогда мы могли бы вести подсчет букв в массиве из 26 элементов. Давайте попробуем такой вариант

```C#
var s = "To use or not to use Dictionary";

            int[] count = new int[26];

            foreach (var myChar in s)
            {
                if (!Char.IsAsciiLetter(myChar))
                    continue;

                var lowerChar = Char.ToLower(myChar);

                int pos = ((byte)lowerChar) - ((byte)'a');

                count[pos]++;
            }

            for (int i = 0; i < 26; i++)
            {
                if (count[i] > 0)
                    Console.WriteLine($"{(char)(i+(byte)'a')}={count[i]}");
            }
```

Обратите внимание. Результат подсчета равен тому что мы получили в предыдущем примере, но теперь бонусом мы получили отсортированный по алфавиту результат. 

Поясним как работает алгоритм - в ASCII а именно им кодируется английский алфавит, каждому символу соответствует определенный номер от 0 до 127. Все буквы идут последовательно. В C# мы можем привести char к byte получа его порядковый номер в кодировке ASCII(int pos = ...). Буквы алфавита начинаются не с нуля и символ ‘a’ имеет порядковый номер 97. Наш массив для подсчета имеет размерность 26 поэтому чтобы рассчитать позицию конкретной буквы нам нужно отнять от нее порядковый номер ‘a’ что мы и делаем в (16) строке. Получив номер ячейки в массиве мы просто увеличиваем значение в этой конкретной ячейке на 1. Массив инициализируется значениями по умолчанию (для int это 0) поэтому в самом начале подсчета количество каждой из букв равно 0. В последнем цикле мы перебираем наш массив обрабатывая значения в тех ячейках где оно не равно нулю преобразовывая номер ячейки в букву алфавита с помощью операции явного приведения.

Всегда старайтесь выбирать как можно более простую структуру данных если это возможно, так как они зачастую дают константную скорость выполнения требуемой операции и занимают меньше объема в памяти.

[🔝 Наверх](#содержание)

### HashSet<T>

Коллекция HashSet или множество представляет из себя хранилище элементов работающее по аналогии с Dictionary но в отличие от словаря hashset хранит только значения. Коллекция позволяет быстро добавить, удалить или определить имеется ли в коллекции заданный элемент. Стоимость операции O(1) но в редких случаях коллизий O(N)

HashSet поддерживает следующие интерфейсы: ICollection<T>, IEnumerable<T>, IEnumerable, ISet<T>, IReadOnlyCollection<T>, IReadOnlySet<T>, ISerializable, IDeserializationCallback

Класс HashSet имеет множество конструкторов, опишем лишь некоторые из них:

- HashSet() - конструктор по умолчанию
- HashSet(IEnumerable<T>) - создает множество из элементов полученных из энумератора объекта, класс которого поддерживает соответствующий интерфейс
- HashSet(ISet<T) - создает множество на основе объекта класс которого поддерживает интерфейс ISet<T>
- HashSet(IEqualityComparer<T>) - создает множество с компаратором предопределяющим поведение при сравнении элементов (требуется в случаях коллизии)

Свойства HashSet<T>:
- Comparer - возвращает IEqualityComparer. Если IEqualityComparer не был явно задан в конструкторе от в этом качестве выступает EqualityComarer<T>.Default который реализует сравнение путем вызова метода Equals.
- Count - хранит количество элементов множества

#### Основные методы HashSet<T>:

- Add(T) - добавляет элемент в множество. Если элемент добавлен в множество то метод возвращает true, если же элемент уже присутствует в множестве - метод возвращает false

- Clear() - очищает множество, удаляя все его элементы

- Contains(T) - проверяет наличие элемента в множестве

- CopyTo(T[]) - копирует элементы множества в массив T[]
- CopyTo(T[], int start) - копирует элементы множества в массив T[] располагая их с позиции start
- CopyTo(T[], int start, int count) - копирует элементы в количестве count множества в массив T[] располагая их с позиции start

- EnsureCapacity(int) - метод работает также как и для словаря или например списка - он убеждается что размер внутреннего хранилища достаточен чтобы вместить количество элементов, переданное в метод. Вы наверняка заметили что мы повторяем описание этого метода от коллекции к коллекции - все потому что в нагруженных приложениях такая казалось бы мелочь как заблаговременное выделение достаточного количества памяти играет важную роль которой не стоит пренебрегать.
- ExceptWith(IEnumerable<T>) - удаляет из множества элементы которые есть в переданном в метод IEnumerable<T>.

```C#
var set1 = new HashSet<int>(new int[]{ 1, 2, 3, 4, 5, 6, 7, 8, 9, 0});
var set2 = new HashSet<int>(new int[]{ 5, 6, 7, 8, 9, 0});

set1.ExceptWith(set2);

foreach (var s in set1)
    Console.Write(s+" "); // 1 2 3 4
```
Вызов метода удаляет из множества set1 все элементы хранящиеся в set2

- GetEnumerator() - возвращает энумератор (помните интерфейс IEnumerable?)
- GetHashcode() - возвращает хеш-код объекта.
- IntersectWith(IEnumerable<T>) - (пересечение)модифицирует множество таким образом что в нем остаются только те элементы которые являются общими с IEnumerable переданный в метод

```C#
var set1 = new HashSet<int>(new int[]{ 1, 2, 3, 4, 5, 6, 7, 8, 9, 0});
var set2 = new HashSet<int>(new int[]{ -1, 5, 6, 7, 8, 9, 0, 10});

set1.IntersectWith(set2);

foreach (var s in set1)
    Console.Write(s+" "); // 5 6 7 8 9 0
```

IsProperSubsetOf(IEnumerator<T>) - (подмножество)определяет является ли множество подмножеством представленным интерфейсом IEnumerable при условии что они различаются по количеству элементов (не равны)

```C#
var set1 = new HashSet<int>(new int[]{ 1, 2, 3, 4, 5, 6, 7, 8, 9, 0});
var set2 = new HashSet<int>(new int[]{ 5, 6, 7, 8, 9, 0 });
var set3 = new HashSet<int>(new int[]{ 5, 6, 7, 8, 9, 0 });

System.Console.WriteLine(set2.IsProperSubsetOf(set1)); // True
System.Console.WriteLine(set2.IsProperSubsetOf(set3)); // False
```
Множестве set2 является подмножеством set1 так как все элементы входящие в set2 имеются в set1, но множество set2 не является подмножеством set3 так как они не отличаются по длине , хоть их элементы и совпадают.

IsProperSupersetOf(IEnumerable<T>) - (надмножество)определяет является ли множество переданной в метод (представленное IEnumerable) подмножеством множества при условии что они различаются по количеству элементов 

```C#
var set1 = new HashSet<int>(new int[]{ 1, 2, 3, 4, 5, 6, 7, 8, 9, 0});
var set2 = new HashSet<int>(new int[]{ 5, 6, 7, 8, 9, 0 });
var set3 = new HashSet<int>(new int[]{ 5, 6, 7, 8, 9, 0 });
var set4 = new HashSet<int>(new int[]{ 1, 2, 3, 4, 5, 6, 7, 8, 9, 0});

System.Console.WriteLine(set1.IsProperSupersetOf(set2)); // True
System.Console.WriteLine(set3.IsProperSupersetOf(set4)); // False
```

Множестве set2 является подмножеством set1 так как все элементы входящие в set2 имеются в set1, но множество set4 не является подмножеством set1 так как они не отличаются по длине , хоть их элементы и совпадают.

- IsSubsetOf(IEnumerable<T>) -(подмножество) определяет является ли множество подмножеством представленным интерфейсом IEnumerable. Метод не учитывает длину сравниваемых множеств в случае если они совпадают (в отличие от IsProperSubsetOf)

```C#
var set1 = new HashSet<int>(new int[]{ 1, 2, 3, 4, 5, 6, 7, 8, 9, 0});
var set2 = new HashSet<int>(new int[]{ 5, 6, 7, 8, 9, 0 });
var set3 = new HashSet<int>(new int[]{ 5, 6, 7, 8, 9, 0 });
var set4 = new HashSet<int>(new int[]{ 1, 2, 3, 4, 5, 6, 7, 8, 9, 0});

System.Console.WriteLine(set2.IsSubsetOf(set1)); // True
System.Console.WriteLine(set3.IsSubsetOf(set4)); // True
```

- IsSupersetOf(IEnumerable<T>) - (надмножество)определяет является ли множество переданной в метод (представленное IEnumerable) подмножеством множества без оглядки на длину как в методе IsProperSupersetOf

```C#
var set1 = new HashSet<int>(new int[]{ 1, 2, 3, 4, 5, 6, 7, 8, 9, 0});
var set2 = new HashSet<int>(new int[]{ 5, 6, 7, 8, 9, 0 });
var set3 = new HashSet<int>(new int[]{ 5, 6, 7, 8, 9, 0 });
var set4 = new HashSet<int>(new int[]{ 1, 2, 3, 4, 5, 6, 7, 8, 9, 0});

System.Console.WriteLine(set1.IsSupersetOf(set2)); // True
System.Console.WriteLine(set1.IsSupersetOf(set4)); // True
```

Overlaps(IEnumerable<T>) - (перекрытие)метод возвращает true если множество имеет общие элементы с IEnumerable<T> переданным в метод.

```C#
var set1 = new HashSet<int>(new int[]{ 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 });
var set2 = new HashSet<int>(new int[]{ 20, 30, 40, 1, 3, 4 });
var set3 = new HashSet<int>(new int[]{ 20, 30, 40 });

System.Console.WriteLine(set1.Overlaps(set2)); // True
System.Console.WriteLine(set1.Overlaps(set3)); // False
```

Remove(T) - удаляет элемент из множества возвращая true в случае успеха. Если же элемент отсутствует, метод возвращает false
```C#
var set1 = new HashSet<int>(new int[]{ 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 });
var set2 = new HashSet<int>(new int[]{ 20, 30, 40, 1, 3, 4 });
var set3 = new HashSet<int>(new int[]{ 20, 30, 40 });

System.Console.WriteLine(set1.Remove(1)); // True
System.Console.WriteLine(set1.Remove(1)); // False
```

RemoveWhere(Predicate<T>) - удаляет из множества элементы совпадающие с предикатом. Метод возвращает количество удаленных элементов.

```C#
static bool IsEven(int i)
{
    return i % 2 == 0;
}

var set1 = new HashSet<int>(new int[]{ 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 });
var set2 = new HashSet<int>(new int[]{ 20, 30, 40, 1, 3, 4 });
var set3 = new HashSet<int>(new int[]{ 20, 30, 40 });

System.Console.WriteLine(set1.RemoveWhere(IsEven)); // 5

foreach (var i in set1)
{
    System.Console.Write(i + " "); // 1 3 5 7 9
}
```

С помощью уже известного нам предиката мы удаляем из множества set1 все четные элементы. в количестве 5 штук

 - SetEquals(IEnumerable<T>) -(тождество) метод возвращает true если множество равно набору представленному IEnumerable иными словами если количество элементов в обоих коллекциях одинаково и сами элементы равны.

- SymmetricExceptWith(IEnumerable<T>) - модифицирует текущее множество таким образом чтобы в нем были только элементы содержащиеся либо в одном либо в другом множестве но не в обоих
```C#
var set1 = new HashSet<int>(new int[]{ 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 });
var set2 = new HashSet<int>(new int[]{ 1, 2, 3, 10, 11, 12, 13, 14, 15 });

set1.SymmetricExceptWith(set2);

foreach (var i in set1)
{
    System.Console.Write(i + " "); // 12 11 10 4 5 6 7 8 9 0 13 14 15
}
```
**Обратите внимание, тут это хорошо видно, множество не гарантирует сохранение порядка следования элементов.**

- TrimExcess() - обрезает размер внутреннего хранилища до размеров множества.
- UnionWith(IEnumerable<T>) - (объединение)модифицирует очередь добавляя в нее все элементы коллекции представленной IEnumerable (разумеется если они еще не находятся в множестве)

[🔝 Наверх](#содержание)

### Особенности работы с HashSet

-

[🔝 Наверх](#содержание)

### Другие коллекции из System.Collections.Generic

-

[🔝 Наверх](#содержание)

### LINQ

-

[🔝 Наверх](#содержание)

### Особенности работы с LINQ

-

[🔝 Наверх](#содержание)