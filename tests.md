# Тесты
---

### Без NGINX
---
Server Software:        Kestrel\
Server Hostname:        localhost\
Server Port:            5001

Document Path:          /persons\
Document Length:        642 bytes

Concurrency Level:      10\
Complete requests:      100\
Failed requests:        0\
Non-2xx responses:      100\
Total transferred:      64700 bytes\
HTML transferred:       46520 bytes



Connection Times (ms)
| |min  |mean[+/-sd] |median |  max|
|-|-|-|-|-|
Connect:|        0|    0  | 0.5|     0     |  2|
Processing:|     1 |   7  | 3.6 |     6   |   37|
Waiting:    |    1  |  6 |  3.0  |    5  |    30|
Total:       |   1   | 7|   3.6   |   7 |     38|

Percentage of the requests served within a certain time (ms)
|||
|-|-|
  50% |     7|
  66%     | 9|
  75%     | 9|
  80%     | 9|
  90%    | 10|
  95%   |  10|
  98%  |   10|
  99% |    38|
 100%|     38 (longest request)|


## NGINX One Server
---
 Server Software:        warehouse_App\
Server Hostname:        localhost\
Server Port:            50000

Document Path:          /persons\
Document Length:        198 bytes

Concurrency Level:      10\
Complete requests:      100\
Failed requests:        0\
Non-2xx responses:      100\
Total transferred:      39300 bytes\
HTML transferred:       19800 bytes


Connection Times (ms)

| | min | mean[+/-sd]| median   |max|
|-|-|-|-|-|
Connect:      |  0  |  0  | 0.5   |   0  |     1|
Processing:   |  1  |  6  | 2.3   |   6  |    13|
Waiting:      |  1  |  5  | 2.5   |   5  |    12|
Total:       |   1  |  6  | 2.3   |   6  |    13|

Percentage of the requests served within a certain time (ms)
|||
|-|-|
  50%|      6|
  66% |     7|
  75%  |    7|
  80%   |   7|
  90%    | 10|
  95%     |12|
  98%|     13|
  99% |    13|
 100%  |   13 (longest request)|


 ## Nginx No Balance
 ---

 Server Software:        warehouse_App\
Server Hostname:        localhost\
Server Port:            50000

Document Path:          /persons\
Document Length:        198 bytes

Concurrency Level:      10\
Complete requests:      100\
Failed requests:        0\
Non-2xx responses:      100\
Total transferred:      39300 bytes\
HTML transferred:       19800 bytes


Connection Times (ms)
| |min|  mean[+/-sd]| median   |max|
|-|-|-|-|-|
Connect:   |     0   | 0|   0.5   |   0|       2|
Processing: |    1  |  6 |  1.5  |    7 |      9|
Waiting:     |   0 |   4  | 2.0 |     4  |     9|
Total:        |  2|    7   |1.5|      7   |   10|

Percentage of the requests served within a certain time (ms)
|||
|-|-|
  50%|      7|
  66% |     8|
  75%  |    8|
  80%   |   8|
  90%    |  9|
  95%     | 9|
  98% |    10|
  99%  |  10|
 100%   |  10 (longest request)|


## NGINX balance
---

Server Software:        warehouse_App\
Server Hostname:        localhost\
Server Port:            50000

Document Path:          /persons\
Document Length:        198 bytes

Concurrency Level:      10\
Complete requests:      100\
Failed requests:        0\
Non-2xx responses:      100\
Total transferred:      39300 bytes\
HTML transferred:       19800 bytes


Connection Times (ms)
|      |min|  mean[+/-sd]| median   |max|
|-|-|-|-|-|
Connect:    |    0|    0  | 0.5|      0   |    2|
Processing:|     1 |   5  | 1.7 |     6  |     8|
Waiting:  |      0  |  3  | 1.8  |    3 |      7|
Total:   |       1   | 6 |  1.8   |   7|       8|

Percentage of the requests served within a certain time (ms)
|||
|-|-|
  50%|      7|
  66% |     7|
  75%  |    7|
  80%   |   7|
  90%    |  8|
  95%     | 8|
  98%      |8|
  99%  |    8|
 100%   |   8 (longest request)|




|Параметр|Без|Один|3 без баланса|3 с балансом|
|-|-|-|-|-|
|Time taken for tests|2.160 | 2.115|2.105 | 2.103  |
| Requests per second| 46.30|47.29 | 47.51 |47.55 |
|Time per request |215.976 |211.466 |210.492 |210.302 |
| Time per request  (mean, across all concurrent requests)|21.598 |21.147 |21.049 | 21.030|
|Transfer rate | 29.25|  18.15| 18.23| 18.25 |




