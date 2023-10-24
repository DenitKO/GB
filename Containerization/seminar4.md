Задание: необходимо создать Dockerfile, основанный на любом образе (вы в праве выбрать самостоятельно).
В него необходимо поместить приложение, написанное на любом известном вам языке программирования (Python, Java, C, С#, C++).
При запуске контейнера должно запускаться самостоятельно написанное приложение.

mkdir dockerfile

cd dockerfile/

nano Dockerfile

### добавляем код в файл

FROM ubuntu: latest

RUN apt-get update && apt-get install -y curl wget && rm -rf /var/lib/apt/lists/*

WORKDIR /app

COPY . .

CMD["bash"]