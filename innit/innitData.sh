#!/bin/bash

for file in ./data/commonstocks/*; do
    echo $file
    curl -u elastic:password -s -XPOST 'http://localhost:9200/_bulk?pretty' -H 'Content-Type: application/x-ndjson' --data-binary @$file
done

for file in ./data/commonstockInfos/*; do
    echo $file
    curl -u elastic:password -s -XPOST 'http://localhost:9200/_bulk?pretty' -H 'Content-Type: application/x-ndjson' --data-binary @$file
done


for file in ./data/eod2016/*; do
    echo $file
    curl -u elastic:password -s -XPOST 'http://localhost:9200/_bulk?pretty' -H 'Content-Type: application/x-ndjson' --data-binary @$file
done