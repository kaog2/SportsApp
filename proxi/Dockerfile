FROM nginx:latest

COPY ./default.conf /etc/nginx/conf.d/default.conf
COPY ./uwsgi_params /etc/nginx/uwsgi_params

USER root

RUN mkdir -p /app/static
RUN chmod 755 /app/static

RUN mkdir -p /app/media
RUN chmod 755 /app/media

USER root
