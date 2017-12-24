

import-csv "SET_StockList.csv" | ConvertTo-Json | Add-Content -Path "StockList.json"