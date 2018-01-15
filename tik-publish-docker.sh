#!/bin/bash
outputversion=0.3.0

echo "docker push sripirom/tik.computation.akkaseed:$outputversion";
docker push sripirom/tik.computation.akkaseed:$outputversion;

echo "docker push sripirom/tik.processservice.online:$outputversion";
docker push sripirom/tik.processservice.online:$outputversion

echo "docker push sripirom/tik.processservice.identity:$outputversion";
docker push sripirom/tik.processservice.identity:$outputversion

echo "docker push sripirom/tik.webportal:$outputversion";
docker push sripirom/tik.webportal:$outputversion

echo "docker push sripirom/tik.websignalr:$outputversion";
docker push sripirom/tik.websignalr:$outputversion

echo "docker push sripirom/tik.elasticsearch:$outputversion";
docker push sripirom/tik.elasticsearch:$outputversion
