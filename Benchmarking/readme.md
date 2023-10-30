# Benchmarking
Here, we are trying to shocase the results of the benchmarking projects we have done with .NET 6.0

## Iterations
For detailed results/histograms, please visit the internal folders...

| Method                          | Size   | Mean                  | Error                 | StdDev                | Median                | Allocated |
|-------------------------------- |------- |----------------------:|----------------------:|----------------------:|----------------------:|----------:|
| Array_For                       | 10     |              3.305 ns |             0.0904 ns |             0.1041 ns |              3.268 ns |         - |
| Array_ForEach                   | 10     |              2.659 ns |             0.0762 ns |             0.0964 ns |              2.648 ns |         - |
| Array_ForEachLinq               | 10     |             14.529 ns |             0.3067 ns |             0.4591 ns |             14.387 ns |         - |
| Array_ParallelForEach           | 10     |          3,061.447 ns |            60.7074 ns |           151.1824 ns |          3,006.808 ns |    2233 B |
| Array_ParallelForAll            | 10     |          7,229.673 ns |           115.8755 ns |           108.3900 ns |          7,238.739 ns |    4088 B |
| Array_ForEachAsSpan             | 10     |              3.050 ns |             0.0943 ns |             0.2750 ns |              2.971 ns |         - |
| Array_ForMemoryMarshalSpanUsafe | 10     |              2.717 ns |             0.0763 ns |             0.0750 ns |              2.717 ns |         - |
| List_For                        | 10     |             42.917 ns |             0.8863 ns |             1.1209 ns |             42.897 ns |         - |
| List_Foreach                    | 10     |              9.667 ns |             0.2024 ns |             0.1894 ns |              9.689 ns |         - |
| List_ForEachLinq                | 10     |             16.423 ns |             0.3429 ns |             0.4211 ns |             16.397 ns |         - |
| List_ParallelForEach            | 10     |          3,198.051 ns |            62.6695 ns |            95.7029 ns |          3,175.838 ns |    2248 B |
| List_ParallelForAll             | 10     |          7,195.895 ns |           110.5638 ns |           103.4214 ns |          7,151.733 ns |    4088 B |
| List_ForEachAsSpanUnsafe        | 10     |              3.192 ns |             0.0876 ns |             0.1311 ns |              3.171 ns |         - |
| Enumerable_For                  | 10     |          1,350.864 ns |            25.8946 ns |            47.3497 ns |          1,342.399 ns |         - |
| Enumerable_Foreach              | 10     |            148.384 ns |             2.9133 ns |             3.2381 ns |            148.482 ns |      48 B |
| Enumerable_ForEachLinq          | 10     |            157.138 ns |             3.1719 ns |             8.5753 ns |            153.782 ns |      48 B |
| Enumerable_ParallelForEach      | 10     |          4,332.722 ns |            85.1749 ns |           179.6628 ns |          4,326.372 ns |   13272 B |
| Enumerable_ParallelForAll       | 10     |          8,572.327 ns |           163.7715 ns |           194.9583 ns |          8,515.767 ns |    8944 B |
| Array_For                       | 1000   |            252.581 ns |             5.0254 ns |             6.3555 ns |            251.446 ns |         - |
| Array_ForEach                   | 1000   |            235.504 ns |             4.6829 ns |             9.4598 ns |            233.964 ns |         - |
| Array_ForEachLinq               | 1000   |          1,328.343 ns |            41.7186 ns |           117.6680 ns |          1,275.596 ns |         - |
| Array_ParallelForEach           | 1000   |         11,232.466 ns |           133.6022 ns |           124.9716 ns |         11,259.811 ns |    3008 B |
| Array_ParallelForAll            | 1000   |          9,094.441 ns |           154.8922 ns |           137.3079 ns |          9,118.927 ns |    4088 B |
| Array_ForEachAsSpan             | 1000   |            228.994 ns |             4.0487 ns |             3.7871 ns |            227.215 ns |         - |
| Array_ForMemoryMarshalSpanUsafe | 1000   |            241.874 ns |             4.8587 ns |             9.9250 ns |            240.131 ns |         - |
| List_For                        | 1000   |          4,039.103 ns |            80.7694 ns |           204.1145 ns |          4,003.878 ns |         - |
| List_Foreach                    | 1000   |            679.089 ns |            24.1048 ns |            71.0737 ns |            661.272 ns |         - |
| List_ForEachLinq                | 1000   |          1,767.243 ns |            53.0574 ns |           156.4410 ns |          1,732.949 ns |         - |
| List_ParallelForEach            | 1000   |         12,537.859 ns |           204.1425 ns |           190.9550 ns |         12,604.243 ns |    3062 B |
| List_ParallelForAll             | 1000   |          9,805.137 ns |           165.2939 ns |           154.6160 ns |          9,794.130 ns |    4088 B |
| List_ForEachAsSpanUnsafe        | 1000   |            236.292 ns |             4.6044 ns |             8.7603 ns |            234.251 ns |         - |
| Enumerable_For                  | 1000   |      9,376,201.906 ns |       185,923.9682 ns |       375,575.5852 ns |      9,304,848.438 ns |       8 B |
| Enumerable_Foreach              | 1000   |         12,840.549 ns |           249.3168 ns |           634.5909 ns |         12,638.539 ns |      48 B |
| Enumerable_ForEachLinq          | 1000   |         13,836.919 ns |           274.0778 ns |           578.1232 ns |         13,637.368 ns |      48 B |
| Enumerable_ParallelForEach      | 1000   |         94,791.969 ns |         2,102.9212 ns |         6,200.5106 ns |         94,780.597 ns |   36805 B |
| Enumerable_ParallelForAll       | 1000   |        106,531.350 ns |         2,124.9327 ns |         4,435.5259 ns |        105,854.639 ns |    8990 B |
| Array_For                       | 100000 |         28,924.359 ns |         1,442.3246 ns |         4,252.7266 ns |         30,539.943 ns |         - |
| Array_ForEach                   | 100000 |         28,982.862 ns |           612.2109 ns |         1,805.1178 ns |         28,644.241 ns |         - |
| Array_ForEachLinq               | 100000 |        163,965.682 ns |         3,235.5524 ns |         8,176.6539 ns |        162,850.183 ns |         - |
| Array_ParallelForEach           | 100000 |        161,314.208 ns |         3,888.4214 ns |        11,342.7077 ns |        161,545.935 ns |    2965 B |
| Array_ParallelForAll            | 100000 |        220,055.671 ns |         4,083.5432 ns |         4,369.3455 ns |        220,326.550 ns |    4127 B |
| Array_ForEachAsSpan             | 100000 |         29,676.614 ns |           643.1837 ns |         1,876.1969 ns |         29,323.129 ns |         - |
| Array_ForMemoryMarshalSpanUsafe | 100000 |         30,510.544 ns |           609.0184 ns |         1,323.9559 ns |         30,643.756 ns |         - |
| List_For                        | 100000 |        513,022.265 ns |        10,726.5252 ns |        31,627.4016 ns |        511,271.045 ns |       1 B |
| List_Foreach                    | 100000 |         66,751.237 ns |         3,782.3101 ns |        10,791.1436 ns |         62,065.790 ns |         - |
| List_ForEachLinq                | 100000 |        153,496.522 ns |         2,774.8259 ns |         2,849.5419 ns |        152,483.398 ns |         - |
| List_ParallelForEach            | 100000 |        188,176.940 ns |         3,751.7073 ns |        10,395.9632 ns |        184,625.769 ns |    3063 B |
| List_ParallelForAll             | 100000 |        236,376.111 ns |         5,898.9636 ns |        17,300.6391 ns |        230,900.439 ns |    4095 B |
| List_ForEachAsSpanUnsafe        | 100000 |         22,111.632 ns |           176.6654 ns |           165.2529 ns |         22,095.584 ns |         - |
| Enumerable_For                  | 100000 | 96,334,535,182.812 ns | 1,779,235,487.1824 ns | 4,123,648,793.9520 ns | 94,959,102,700.000 ns |     912 B |
| Enumerable_Foreach              | 100000 |      1,266,066.674 ns |        16,303.7374 ns |        22,855.5629 ns |      1,263,549.414 ns |      49 B |
| Enumerable_ForEachLinq          | 100000 |      1,389,768.983 ns |        26,753.7074 ns |        36,620.7767 ns |      1,380,484.570 ns |      50 B |
| Enumerable_ParallelForEach      | 100000 |      2,531,858.331 ns |        50,412.8259 ns |       100,679.8606 ns |      2,521,822.266 ns |  134595 B |
| Enumerable_ParallelForAll       | 100000 |      2,220,776.507 ns |        41,422.0419 ns |        36,719.5612 ns |      2,218,110.547 ns |    9007 B |

