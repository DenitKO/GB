### ООП - Объектно ориентированное программирование(**Object oriented programming)**

ООП строится на 3-х основных принципах:
● Инкапсуляция – способность объекта скрывать свою внутреннюю
реализацию, объединяя в себя данные и методы; ИЛИ

Инкапсуляция - механизм языка, позволяющий объединить данные и методы, работающие с этими данными в единый объект, и скрыть детали реализации от пользователя.
● Наследование – возможность создавать новые объекты (наследник/child) на
основе уже существующих (родитель/parent). Объекты-потомки наследуют
поведение и свойства объектов родителей;
● Полиморфизм – способность выполнять действия с объектом вне
зависимости от его типа.

Абстрактный класс родитель Shape:

- метод CalculateArea
- метод SetColor

Классы наследники Square, Circle:

- метод CalculateArea унаследованный от родителя и у каждого реализован по своему, и не обязательно знать особенности их реализации, метод вернёт нам площадь этих объектов(Инкапсуляция)
- метод SetColor можно реализовать лишь единожды в классе Shape. “цвет” одинаков вне зависимости от типа фигуры. (Наследование)
- много уже созданных квадратов и кругов. Вам не важен тип фигуры, важно знать только ее площадь. **Полиморфизм** позволяет привести любой из этих объектов к классу Shape, и нам будет достаточно вызвать метод CalculateArea, не вдаваясь в подробности того, какой фигурой на самом деле является объект

### SOLID

- S – single responsibility – один объект одно предназначение.
- O – open close principle - класс может быть открыт к расширениям, но закрыт к модификациям.
- L – Liskov substitution principle – принцип подстановки
- I – interface segregation - разделение интерфейсов.
- D – dependency inversion - инверсия зависимости.

### DI

DI - dependency injection - Инжектор (-: 

### KISS

Keep it short and simple – код должен быть прост и понятен.

### DRY

Don’t repeat yourself

### IoC

IoC - Inversion of control - Don’t call us, we’ll call you. Различные источники приводят различные паттерны, к которым может быть применен IoC. И скорее всего они все правы и просто дополняют друг друга. Вот некоторые их этих паттернов: factory, service locator, template method, observer, strategy.

### Свойства класса
<pre><code>
[модификатор доступа] Тип ИмяСвойства
{
    [модификатор доступа] get;
    [модификатор доступа] set/init; 
    /* инит, по сути, создаёт константу в которую можно занести другое значение, при инициализации, кроме дефолтного. */
}
</pre></code>

### Required

required - ключевое слово используемое для обозначения обязательного для инициализации поля

inline инициализация - в методах get, set/init

### Модификаторы доступа

● public – к типу или члену, помеченному public, можно получить доступ из любого другого кода текущей или другой сборки.

● private – тип или член, помеченный словом private, доступен только коду текущего класса или структуры.

● protected – тип или член, помеченный словом protected, доступен только коду текущего класса или коду классов-наследников.

● internal – к типу или члену, помеченному internal, можно получить доступ из любого другого кода текущей, но не другой сборки.

● protected internal – к типу или члену, помеченному protected internal, можно получить доступ из любого другого кода текущей сборки или из классов
потомков, объявленных в других сборках.

● private internal – тип или член, помеченный словом protected, доступен только коду текущего класса или коду классов-наследников текущей сборки.

модификатор sealed запрещает наследовать