USE peoplefriends;

CREATE TABLE cat (
	id INT PRIMARY KEY AUTO_INCREMENT,
	animal_name CHAR(30),
    commands TEXT,
    date_of_birth DATE
);

CREATE TABLE dog (
	id INT PRIMARY KEY AUTO_INCREMENT,
	animal_name CHAR(30),
    commands TEXT,
    date_of_birth DATE
);

CREATE TABLE hamster (
	id INT PRIMARY KEY AUTO_INCREMENT,
	animal_name CHAR(30),
    commands TEXT,
    date_of_birth DATE
);

CREATE TABLE horse (
	id INT PRIMARY KEY AUTO_INCREMENT,
	animal_name CHAR(30),
    commands TEXT,
    date_of_birth DATE
);

CREATE TABLE camel (
	id INT PRIMARY KEY AUTO_INCREMENT,
	animal_name CHAR(30),
    commands TEXT,
    date_of_birth DATE
);

CREATE TABLE donkey (
	id INT PRIMARY KEY AUTO_INCREMENT,
	animal_name CHAR(30),
    commands TEXT,
    date_of_birth DATE
);

INSERT INTO cat (animal_name,commands, date_of_birth) VALUES 
	('barsik', 'meow', '2021-01-01'),
	('kosh', 'meow, stand', '2019-12-10'),
    ('frank', 'meow, wlow', '2020-02-02'),
    ('dir', 'meow', '2022-03-03'),
    ('krop', 'meow', '2018-05-05');
   
INSERT INTO dog (animal_name,commands, date_of_birth) VALUES 
	('wolf', 'flufy', '2021-01-01'),
	('ralf', 'site', '2019-12-10'),
    ('qwerty', 'left hand', '2020-02-02'),
    ('asdfg', 'right hand', '2022-03-03'),
    ('red', 'meow', '2018-05-05');
    
INSERT INTO hamster (animal_name,commands, date_of_birth) VALUES 
	('crack', 'eat', '2021-01-01'),
	('jisus', 'eat', '2019-12-10'),
    ('qwerty', 'left hand', '2020-02-02'),
    ('asdfg', 'right hand', '2022-03-03'),
    ('black', 'meow', '2018-05-05');
    
INSERT INTO horse (animal_name,commands, date_of_birth) VALUES 
	('igogo', 'eat', '2021-01-01'),
	('igaga', 'eat', '2019-12-10'),
    ('ijoho', 'left hand', '2020-02-02'),
    ('jijij', 'right hand', '2022-03-03'),
    ('aoaoaa', 'meow', '2018-05-05');
    
INSERT INTO camel (animal_name,commands, date_of_birth) VALUES 
	('qwea', 'eat', '2021-01-01'),
	('dsaf', 'eat', '2019-12-10'),
    ('gfdsdf', 'left hand', '2020-02-02'),
    ('grwtw', 'right hand', '2022-03-03'),
    ('werqr', 'meow', '2018-05-05');
    
INSERT INTO donkey (animal_name,commands, date_of_birth) VALUES 
	('stup', 'eat', '2021-01-01'),
	('tupi', 'eat', '2019-12-10'),
    ('upid', 'left hand', '2020-02-02'),
    ('task', 'right hand', '2022-03-03'),
    ('done', 'meow', '2018-05-05');
    
TRUNCATE camel;

INSERT INTO horse (animal_name, commands, date_of_birth)
SELECT animal_name, commands, date_of_birth
FROM donkey;

DROP TABLE donkey;

RENAME TABLE horse TO horse_donkey;

CREATE TABLE young_animal (
	id INT PRIMARY KEY AUTO_INCREMENT,
	animal_name CHAR(30),
    commands TEXT,
    date_of_birth DATE,
    age TEXT
);


DELIMITER $$
CREATE FUNCTION age_animal (date_b DATE)
RETURNS TEXT
DETERMINISTIC
BEGIN
    DECLARE res TEXT DEFAULT '';
	SET res = CONCAT(
            TIMESTAMPDIFF(YEAR, date_b, CURDATE()),
            ' years ',
            TIMESTAMPDIFF(MONTH, date_b, CURDATE()) % 12,
            ' month'
        );
	RETURN res;
END $$
DELIMITER ;

INSERT INTO young_animal (animal_name, commands, date_of_birth, age)
SELECT animal_name, commands, date_of_birth, age_animal(date_of_birth)
FROM cat
WHERE TIMESTAMPDIFF(YEAR, date_of_birth, CURDATE()) BETWEEN 1 AND 3
UNION ALL
SELECT animal_name, commands, date_of_birth, age_animal(date_of_birth)
FROM dog
WHERE TIMESTAMPDIFF(YEAR, date_of_birth, CURDATE()) BETWEEN 1 AND 3
UNION ALL
SELECT animal_name, commands, date_of_birth, age_animal(date_of_birth)
FROM hamster
WHERE TIMESTAMPDIFF(YEAR, date_of_birth, CURDATE()) BETWEEN 1 AND 3
UNION ALL
SELECT animal_name, commands, date_of_birth, age_animal(date_of_birth)
FROM horse_donkey
WHERE TIMESTAMPDIFF(YEAR, date_of_birth, CURDATE()) BETWEEN 1 AND 3;

SET SQL_SAFE_UPDATES = 0;

DELETE FROM cat 
WHERE TIMESTAMPDIFF(YEAR, cat.date_of_birth, CURDATE()) IN (1, 2, 3);

DELETE FROM dog 
WHERE TIMESTAMPDIFF(YEAR, date_of_birth, CURDATE()) BETWEEN 1 AND 3;

DELETE FROM hamster 
WHERE TIMESTAMPDIFF(YEAR, date_of_birth, CURDATE()) BETWEEN 1 AND 3;

DELETE FROM horse_donkey 
WHERE TIMESTAMPDIFF(YEAR, date_of_birth, CURDATE()) BETWEEN 1 AND 3;

CREATE TABLE animals (
	id INT PRIMARY KEY AUTO_INCREMENT,
	animal_name CHAR(30),
    commands TEXT,
    date_of_birth DATE,
    age TEXT,
    animal_type ENUM('cat','dog','hamster', 'horse_donkey', 'young_animals') NOT NULL
);

INSERT INTO animals (animal_name, commands, date_of_birth, age, animal_type)
SELECT animal_name, commands, date_of_birth, age_animal(date_of_birth), 'cat'
FROM cat;

INSERT INTO animals (animal_name, commands, date_of_birth, age, animal_type)
SELECT animal_name, commands, date_of_birth, age_animal(date_of_birth), 'dog'
FROM dog;

INSERT INTO animals (animal_name, commands, date_of_birth, age, animal_type)
SELECT animal_name, commands, date_of_birth, age_animal(date_of_birth), 'hamster'
FROM hamster;

INSERT INTO animals (animal_name, commands, date_of_birth, age, animal_type)
SELECT animal_name, commands, date_of_birth, age_animal(date_of_birth), 'horse_donkey'
FROM horse_donkey;

INSERT INTO animals (animal_name, commands, date_of_birth, age, animal_type)
SELECT animal_name, commands, date_of_birth, age_animal(date_of_birth), 'young_animals'
FROM young_animal;