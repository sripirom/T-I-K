docker run -e ELASTIC_PASSWORD=password --network=tik_default --name elastic docker.elastic.co/elasticsearch/elasticsearch-platinum:6.1.1


#No password
docker run -e ELASTIC_PASSWORD=password --network=tik_default -p 9200:9200 -p 9300:9300 --name elastic docker.elastic.co/elasticsearch/elasticsearch-platinum:6.1.1


docker run --restart=always -e ELASTIC_PASSWORD=password --network=tik_default -p 9200:9200 -p 9300:9300 --name elastic tik.elastic