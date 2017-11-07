#!/bin/bash
FILES=/Users/Tikclicker/SripiromDev/GitHub/TouchIntegrationKit/sampleData/ElasticSearchData/*
for f in $FILES
do
  echo "Processing $f file..."
  curl -XPOST 192.168.99.100:32817/set_stock_eod/eod/_bulk --data-binary  @$f
  # take action on each file. $f store current file name
  # cat $f
done