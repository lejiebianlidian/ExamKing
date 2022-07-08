FROM mysql:8.0

ENV MYSQL_ALLOW_EMPTY_PASSWORD yes

COPY ./docker/backend/mysql/init/examking.sql /docker-entrypoint-initdb.d/
RUN chmod a+x /docker-entrypoint-initdb.d/examking.sql