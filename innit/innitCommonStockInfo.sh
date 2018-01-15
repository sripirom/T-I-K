#!/bin/bash

for file in ./data/commonstockInfos/*; do
    echo $file
    curl -s -XPOST 'http://localhost:9200/_bulk?pretty' -H 'Content-Type: application/x-ndjson' --data-binary @$file
done

