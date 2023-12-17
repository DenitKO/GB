# GB_final_01.2023
Final work at first part of education

# `Итоговая аттестация. Практическое задание`

## `Информация о проекте`
Необходимо организовать систему учета для питомника, в котором живут домашние и вьючные животные.

## `Задание:`

### 1. Использование команды cat в Linux.
- Создать два текстовых файла: "Pets"(Домашние животные) и "Pack animals"(вьючные животные), используя команду `cat` в терминале Linux. В первом файле перечислить собак, кошек и хомяков. Во втором — лошадей, верблюдов и ослов. 
- Далее объединить содержимое этих двух файлов в один и просмотреть его содержимое. 
- Затем переименовать получившийся файл в "Human Friends"

мы можем использовать cat и затем заполнить файл уже изнутри, но проще создать и наполнить его командой **echo "sometext" > somefile**

> *echo "dog, cat, hamster" > pets*

> *echo "horse, camel, donkey" > Pack\ animals*

> *cat pets Pack\ animals > animals*

> *mv animals Human\ Friends*

### 2. Создать директорию, переместить файл туда.

> *mkdir testdir*

> *mv Human\ Friends testdir/*

> *ls*

### 3. Подключить дополнительный репозиторий MySQL. Установить любой пакет из этого репозитория.

Обновляем информацию о пакетах чтобы убрать лишнее:

> *sudo apt update*

Скачиваем конфигуратор mysql:

> *wget https://dev.mysql.com/get/mysql-apt-config_0.8.28-1_all.deb*

устанавливаем компоненты mysql с помощью конфигуратора:

> *sudo dpkg -i mysql-apt-config_0.8.28-1_all.deb*

В процессе установки жмем Ок, чтобы выполнить полную установку

Обновляем информацию о пакетах и видим подключенный репозиторий mysql:

> *sudo apt-get update*

Устанавливаем mysql-server:

> *sudo apt-get install -y mysql-server*

Проверяем результат установки:

> *systemctl status mysql*

### 4. Установить и удалить deb-пакет с помощью dpkg.

> *wget https://dev.mysql.com/get/Downloads/Connector-J/mysql-connector-j_8.2.0-1ubuntu22.04_all.deb*

> *sudo dpkg -i mysql-connector-j_8.2.0-1ubuntu22.04_all.deb*

> *sudo dpkg -r mysql-connector-j*

> *sudo apt-get autoremove*

### 5. Выложить историю команд в терминале ubuntu.

![Консоль](Images/1.png)
