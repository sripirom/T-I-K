

#$files = Get-ChildItem "/Users/Tikclicker/SripiromDev/GitHub/TouchIntegrationKit/sampleData/ElasticSearchData"

#for ($i=0; $i -lt $files.Count; $i++) 
#{
   & curl -XPOST 192.168.99.100:32817/set_stock_eod/eod/_bulk --data-binary  /Users/Tikclicker/SripiromDev/GitHub/TouchIntegrationKit/sampleData/ElasticSearchData/EOD_1975-05-06.json
    #Write-Output $files[$i].FullName

#}