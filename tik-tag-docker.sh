#!/bin/bash


outputversion=0.3.0

echo "docker tag tik.computation.akkaseed sripirom/tik.computation.akkaseed:$outputversion";
docker tag tik.computation.akkaseed sripirom/tik.computation.akkaseed:$outputversion;

echo "docker tag tik.processservice.online sripirom/tik.processservice.online:$outputversion";
docker tag tik.processservice.online sripirom/tik.processservice.online:$outputversion;

echo "docker tag tik.processservice.identity sripirom/tik.processservice.identity:$outputversion";
docker tag tik.processservice.identity sripirom/tik.processservice.identity:$outputversion;

echo "docker tag tik.webportal sripirom/tik.webportal:$outputversion";
docker tag tik.webportal sripirom/tik.webportal:$outputversion;

echo "docker tag tik.websignalr sripirom/tik.websignalr:$outputversion";
docker tag tik.websignalr sripirom/tik.websignalr:$outputversion;

echo "docker tag tik.elasticsearch sripirom/tik.elasticsearch:$outputversion";
docker tag tik.elasticsearch sripirom/tik.elasticsearch:$outputversion;