

& docker tag tik.computation.akkaseed sripirom/tik.computation.akkaseed:0.1

& docker tag tik.processservice.online sripirom/tik.processservice.online:0.1

& docker tag tik.processservice.identity sripirom/tik.processservice.identity:0.1

& docker tag tik.webportal sripirom/tik.webportal:0.1


docker push sripirom/tik.computation.akkaseed:0.1
docker push sripirom/tik.processservice.online:0.1
docker push sripirom/tik.processservice.identity:0.1
docker push sripirom/tik.webportal:0.1