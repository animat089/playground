# Detailed Performance

The sample ran for over 3 hours, to be able to collect these details:

```txt
IterationSamples.Array_For: DefaultJob [Size=10]
Runtime = .NET 6.0.24 (6.0.2423.51814), X64 RyuJIT AVX2; GC = Concurrent Workstation
Mean = 3.305 ns, StdErr = 0.023 ns (0.70%), N = 20, StdDev = 0.104 ns
Min = 3.170 ns, Q1 = 3.234 ns, Median = 3.268 ns, Q3 = 3.384 ns, Max = 3.494 ns
IQR = 0.151 ns, LowerFence = 3.008 ns, UpperFence = 3.610 ns
ConfidenceInterval = [3.215 ns; 3.395 ns] (CI 99.9%), Margin = 0.090 ns (2.74% of Mean)
Skewness = 0.5, Kurtosis = 1.75, MValue = 2.18
-------------------- Histogram --------------------
[3.169 ns ; 3.270 ns) | @@@@@@@@@@@
[3.270 ns ; 3.379 ns) | @@@@
[3.379 ns ; 3.514 ns) | @@@@@
---------------------------------------------------

IterationSamples.Array_ForEach: DefaultJob [Size=10]
Runtime = .NET 6.0.24 (6.0.2423.51814), X64 RyuJIT AVX2; GC = Concurrent Workstation
Mean = 2.659 ns, StdErr = 0.020 ns (0.76%), N = 23, StdDev = 0.096 ns
Min = 2.538 ns, Q1 = 2.575 ns, Median = 2.648 ns, Q3 = 2.721 ns, Max = 2.914 ns
IQR = 0.146 ns, LowerFence = 2.356 ns, UpperFence = 2.940 ns
ConfidenceInterval = [2.583 ns; 2.735 ns] (CI 99.9%), Margin = 0.076 ns (2.87% of Mean)
Skewness = 0.74, Kurtosis = 2.97, MValue = 2
-------------------- Histogram --------------------
[2.538 ns ; 2.627 ns) | @@@@@@@@@@
[2.627 ns ; 2.745 ns) | @@@@@@@@@@
[2.745 ns ; 2.835 ns) | @@
[2.835 ns ; 2.959 ns) | @
---------------------------------------------------

IterationSamples.Array_ForEachLinq: DefaultJob [Size=10]
Runtime = .NET 6.0.24 (6.0.2423.51814), X64 RyuJIT AVX2; GC = Concurrent Workstation
Mean = 14.529 ns, StdErr = 0.084 ns (0.58%), N = 30, StdDev = 0.459 ns
Min = 13.935 ns, Q1 = 14.236 ns, Median = 14.387 ns, Q3 = 14.804 ns, Max = 15.899 ns
IQR = 0.567 ns, LowerFence = 13.385 ns, UpperFence = 15.655 ns
ConfidenceInterval = [14.222 ns; 14.836 ns] (CI 99.9%), Margin = 0.307 ns (2.11% of Mean)
Skewness = 1.07, Kurtosis = 3.74, MValue = 2
-------------------- Histogram --------------------
[13.741 ns ; 14.030 ns) | @@
[14.030 ns ; 14.418 ns) | @@@@@@@@@@@@@@
[14.418 ns ; 14.974 ns) | @@@@@@@@@@
[14.974 ns ; 15.315 ns) | @@
[15.315 ns ; 15.630 ns) | @
[15.630 ns ; 16.093 ns) | @
---------------------------------------------------

IterationSamples.Array_ParallelForEach: DefaultJob [Size=10]
Runtime = .NET 6.0.24 (6.0.2423.51814), X64 RyuJIT AVX2; GC = Concurrent Workstation
Mean = 3.061 us, StdErr = 0.018 us (0.58%), N = 73, StdDev = 0.151 us
Min = 2.811 us, Q1 = 2.941 us, Median = 3.007 us, Q3 = 3.175 us, Max = 3.384 us
IQR = 0.234 us, LowerFence = 2.590 us, UpperFence = 3.527 us
ConfidenceInterval = [3.001 us; 3.122 us] (CI 99.9%), Margin = 0.061 us (1.98% of Mean)
Skewness = 0.58, Kurtosis = 2.08, MValue = 2.4
-------------------- Histogram --------------------
[2.802 us ; 2.899 us) | @@@@@
[2.899 us ; 2.994 us) | @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
[2.994 us ; 3.133 us) | @@@@@@@@@@@@@@@@@
[3.133 us ; 3.234 us) | @@@@@
[3.234 us ; 3.329 us) | @@@@@@@@@@@
[3.329 us ; 3.432 us) | @@@@@
---------------------------------------------------

IterationSamples.Array_ParallelForAll: DefaultJob [Size=10]
Runtime = .NET 6.0.24 (6.0.2423.51814), X64 RyuJIT AVX2; GC = Concurrent Workstation
Mean = 7.230 us, StdErr = 0.028 us (0.39%), N = 15, StdDev = 0.108 us
Min = 6.959 us, Q1 = 7.189 us, Median = 7.239 us, Q3 = 7.309 us, Max = 7.360 us
IQR = 0.120 us, LowerFence = 7.010 us, UpperFence = 7.489 us
ConfidenceInterval = [7.114 us; 7.346 us] (CI 99.9%), Margin = 0.116 us (1.60% of Mean)
Skewness = -0.99, Kurtosis = 3.29, MValue = 2
-------------------- Histogram --------------------
[6.941 us ; 7.107 us) | @@
[7.107 us ; 7.363 us) | @@@@@@@@@@@@@
---------------------------------------------------

IterationSamples.Array_ForEachAsSpan: DefaultJob [Size=10]
Runtime = .NET 6.0.24 (6.0.2423.51814), X64 RyuJIT AVX2; GC = Concurrent Workstation
Mean = 3.050 ns, StdErr = 0.028 ns (0.91%), N = 98, StdDev = 0.275 ns
Min = 2.650 ns, Q1 = 2.828 ns, Median = 2.971 ns, Q3 = 3.255 ns, Max = 3.726 ns
IQR = 0.427 ns, LowerFence = 2.187 ns, UpperFence = 3.895 ns
ConfidenceInterval = [2.956 ns; 3.145 ns] (CI 99.9%), Margin = 0.094 ns (3.09% of Mean)
Skewness = 0.69, Kurtosis = 2.52, MValue = 2.91
-------------------- Histogram --------------------
[2.611 ns ; 2.760 ns) | @@@@@@@@
[2.760 ns ; 2.916 ns) | @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
[2.916 ns ; 3.106 ns) | @@@@@@@@@@@@@@@@@@@@@@@@@
[3.106 ns ; 3.242 ns) | @@@@@@
[3.242 ns ; 3.399 ns) | @@@@@@@@@@@@@@@
[3.399 ns ; 3.468 ns) | @
[3.468 ns ; 3.624 ns) | @@@@@@@
[3.624 ns ; 3.804 ns) | @@@
---------------------------------------------------

IterationSamples.Array_ForMemoryMarshalSpanUsafe: DefaultJob [Size=10]
Runtime = .NET 6.0.24 (6.0.2423.51814), X64 RyuJIT AVX2; GC = Concurrent Workstation
Mean = 2.717 ns, StdErr = 0.019 ns (0.69%), N = 16, StdDev = 0.075 ns
Min = 2.622 ns, Q1 = 2.651 ns, Median = 2.717 ns, Q3 = 2.756 ns, Max = 2.859 ns
IQR = 0.105 ns, LowerFence = 2.493 ns, UpperFence = 2.915 ns
ConfidenceInterval = [2.641 ns; 2.794 ns] (CI 99.9%), Margin = 0.076 ns (2.81% of Mean)
Skewness = 0.37, Kurtosis = 1.89, MValue = 2
-------------------- Histogram --------------------
[2.599 ns ; 2.677 ns) | @@@@@@
[2.677 ns ; 2.782 ns) | @@@@@@@
[2.782 ns ; 2.861 ns) | @@@
---------------------------------------------------

IterationSamples.List_For: DefaultJob [Size=10]
Runtime = .NET 6.0.24 (6.0.2423.51814), X64 RyuJIT AVX2; GC = Concurrent Workstation
Mean = 42.917 ns, StdErr = 0.234 ns (0.54%), N = 23, StdDev = 1.121 ns
Min = 40.837 ns, Q1 = 42.277 ns, Median = 42.897 ns, Q3 = 43.448 ns, Max = 45.656 ns
IQR = 1.171 ns, LowerFence = 40.520 ns, UpperFence = 45.204 ns
ConfidenceInterval = [42.031 ns; 43.804 ns] (CI 99.9%), Margin = 0.886 ns (2.07% of Mean)
Skewness = 0.37, Kurtosis = 2.9, MValue = 2
-------------------- Histogram --------------------
[40.320 ns ; 41.535 ns) | @@
[41.535 ns ; 42.501 ns) | @@@@@@
[42.501 ns ; 43.536 ns) | @@@@@@@@@@@
[43.536 ns ; 44.952 ns) | @@@
[44.952 ns ; 46.173 ns) | @
---------------------------------------------------

IterationSamples.List_Foreach: DefaultJob [Size=10]
Runtime = .NET 6.0.24 (6.0.2423.51814), X64 RyuJIT AVX2; GC = Concurrent Workstation
Mean = 9.667 ns, StdErr = 0.049 ns (0.51%), N = 15, StdDev = 0.189 ns
Min = 9.269 ns, Q1 = 9.561 ns, Median = 9.689 ns, Q3 = 9.760 ns, Max = 9.996 ns
IQR = 0.199 ns, LowerFence = 9.261 ns, UpperFence = 10.059 ns
ConfidenceInterval = [9.464 ns; 9.869 ns] (CI 99.9%), Margin = 0.202 ns (2.09% of Mean)
Skewness = -0.25, Kurtosis = 2.47, MValue = 2
-------------------- Histogram --------------------
[9.168 ns ;  9.374 ns) | @
[9.374 ns ;  9.591 ns) | @@@
[9.591 ns ;  9.868 ns) | @@@@@@@@@
[9.868 ns ; 10.097 ns) | @@
---------------------------------------------------

IterationSamples.List_ForEachLinq: DefaultJob [Size=10]
Runtime = .NET 6.0.24 (6.0.2423.51814), X64 RyuJIT AVX2; GC = Concurrent Workstation
Mean = 16.423 ns, StdErr = 0.090 ns (0.55%), N = 22, StdDev = 0.421 ns
Min = 15.703 ns, Q1 = 16.124 ns, Median = 16.397 ns, Q3 = 16.700 ns, Max = 17.276 ns
IQR = 0.575 ns, LowerFence = 15.261 ns, UpperFence = 17.563 ns
ConfidenceInterval = [16.080 ns; 16.766 ns] (CI 99.9%), Margin = 0.343 ns (2.09% of Mean)
Skewness = 0.18, Kurtosis = 2.05, MValue = 2
-------------------- Histogram --------------------
[15.667 ns ; 16.072 ns) | @@@@@
[16.072 ns ; 16.646 ns) | @@@@@@@@@@
[16.646 ns ; 17.040 ns) | @@@@@@
[17.040 ns ; 17.473 ns) | @
---------------------------------------------------

IterationSamples.List_ParallelForEach: DefaultJob [Size=10]
Runtime = .NET 6.0.24 (6.0.2423.51814), X64 RyuJIT AVX2; GC = Concurrent Workstation
Mean = 3.198 us, StdErr = 0.017 us (0.54%), N = 31, StdDev = 0.096 us
Min = 3.062 us, Q1 = 3.129 us, Median = 3.176 us, Q3 = 3.233 us, Max = 3.399 us
IQR = 0.104 us, LowerFence = 2.973 us, UpperFence = 3.389 us
ConfidenceInterval = [3.135 us; 3.261 us] (CI 99.9%), Margin = 0.063 us (1.96% of Mean)
Skewness = 0.94, Kurtosis = 2.81, MValue = 2.4
-------------------- Histogram --------------------
[3.022 us ; 3.109 us) | @@@
[3.109 us ; 3.189 us) | @@@@@@@@@@@@@@@@
[3.189 us ; 3.264 us) | @@@@@@@
[3.264 us ; 3.339 us) |
[3.339 us ; 3.419 us) | @@@@@
---------------------------------------------------

IterationSamples.List_ParallelForAll: DefaultJob [Size=10]
Runtime = .NET 6.0.24 (6.0.2423.51814), X64 RyuJIT AVX2; GC = Concurrent Workstation
Mean = 7.196 us, StdErr = 0.027 us (0.37%), N = 15, StdDev = 0.103 us
Min = 7.041 us, Q1 = 7.123 us, Median = 7.152 us, Q3 = 7.282 us, Max = 7.380 us
IQR = 0.159 us, LowerFence = 6.885 us, UpperFence = 7.520 us
ConfidenceInterval = [7.085 us; 7.306 us] (CI 99.9%), Margin = 0.111 us (1.54% of Mean)
Skewness = 0.34, Kurtosis = 1.63, MValue = 2
-------------------- Histogram --------------------
[6.986 us ; 7.217 us) | @@@@@@@@@
[7.217 us ; 7.435 us) | @@@@@@
---------------------------------------------------

IterationSamples.List_ForEachAsSpanUnsafe: DefaultJob [Size=10]
Runtime = .NET 6.0.24 (6.0.2423.51814), X64 RyuJIT AVX2; GC = Concurrent Workstation
Mean = 3.192 ns, StdErr = 0.024 ns (0.75%), N = 30, StdDev = 0.131 ns
Min = 3.014 ns, Q1 = 3.094 ns, Median = 3.171 ns, Q3 = 3.278 ns, Max = 3.526 ns
IQR = 0.184 ns, LowerFence = 2.818 ns, UpperFence = 3.553 ns
ConfidenceInterval = [3.105 ns; 3.280 ns] (CI 99.9%), Margin = 0.088 ns (2.74% of Mean)
Skewness = 0.76, Kurtosis = 2.72, MValue = 2
-------------------- Histogram --------------------
[2.958 ns ; 3.032 ns) | @
[3.032 ns ; 3.142 ns) | @@@@@@@@@@@@@
[3.142 ns ; 3.263 ns) | @@@@@@@@
[3.263 ns ; 3.384 ns) | @@@@@
[3.384 ns ; 3.533 ns) | @@@
---------------------------------------------------

IterationSamples.Enumerable_For: DefaultJob [Size=10]
Runtime = .NET 6.0.24 (6.0.2423.51814), X64 RyuJIT AVX2; GC = Concurrent Workstation
Mean = 1.351 us, StdErr = 0.007 us (0.54%), N = 42, StdDev = 0.047 us
Min = 1.286 us, Q1 = 1.315 us, Median = 1.342 us, Q3 = 1.370 us, Max = 1.482 us
IQR = 0.055 us, LowerFence = 1.234 us, UpperFence = 1.452 us
ConfidenceInterval = [1.325 us; 1.377 us] (CI 99.9%), Margin = 0.026 us (1.92% of Mean)
Skewness = 1.01, Kurtosis = 3.42, MValue = 2
-------------------- Histogram --------------------
[1.281 us ; 1.315 us) | @@@@@@@@@
[1.315 us ; 1.350 us) | @@@@@@@@@@@@@@@@@
[1.350 us ; 1.388 us) | @@@@@@@@@@
[1.388 us ; 1.430 us) | @@
[1.430 us ; 1.483 us) | @@@@
---------------------------------------------------

IterationSamples.Enumerable_Foreach: DefaultJob [Size=10]
Runtime = .NET 6.0.24 (6.0.2423.51814), X64 RyuJIT AVX2; GC = Concurrent Workstation
Mean = 148.384 ns, StdErr = 0.743 ns (0.50%), N = 19, StdDev = 3.238 ns
Min = 141.165 ns, Q1 = 146.500 ns, Median = 148.482 ns, Q3 = 150.469 ns, Max = 154.628 ns
IQR = 3.969 ns, LowerFence = 140.546 ns, UpperFence = 156.423 ns
ConfidenceInterval = [145.470 ns; 151.297 ns] (CI 99.9%), Margin = 2.913 ns (1.96% of Mean)
Skewness = -0.23, Kurtosis = 2.62, MValue = 2
-------------------- Histogram --------------------
[139.572 ns ; 143.265 ns) | @
[143.265 ns ; 146.450 ns) | @@@@
[146.450 ns ; 150.712 ns) | @@@@@@@@@@
[150.712 ns ; 156.221 ns) | @@@@
---------------------------------------------------

IterationSamples.Enumerable_ForEachLinq: DefaultJob [Size=10]
Runtime = .NET 6.0.24 (6.0.2423.51814), X64 RyuJIT AVX2; GC = Concurrent Workstation
Mean = 157.138 ns, StdErr = 0.930 ns (0.59%), N = 85, StdDev = 8.575 ns
Min = 143.518 ns, Q1 = 150.906 ns, Median = 153.782 ns, Q3 = 161.930 ns, Max = 180.894 ns
IQR = 11.024 ns, LowerFence = 134.371 ns, UpperFence = 178.466 ns
ConfidenceInterval = [153.966 ns; 160.310 ns] (CI 99.9%), Margin = 3.172 ns (2.02% of Mean)
Skewness = 1.06, Kurtosis = 3.29, MValue = 2.16
-------------------- Histogram --------------------
[140.958 ns ; 144.454 ns) | @
[144.454 ns ; 149.766 ns) | @@@@@@@@@
[149.766 ns ; 154.886 ns) | @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
[154.886 ns ; 160.459 ns) | @@@@@@@@@@@@@@
[160.459 ns ; 166.872 ns) | @@@@@@@@@@@
[166.872 ns ; 173.245 ns) | @@@
[173.245 ns ; 178.365 ns) | @@@@@@@@
[178.365 ns ; 183.454 ns) | @
---------------------------------------------------

IterationSamples.Enumerable_ParallelForEach: DefaultJob [Size=10]
Runtime = .NET 6.0.24 (6.0.2423.51814), X64 RyuJIT AVX2; GC = Concurrent Workstation
Mean = 4.333 us, StdErr = 0.024 us (0.56%), N = 54, StdDev = 0.180 us
Min = 3.994 us, Q1 = 4.183 us, Median = 4.326 us, Q3 = 4.466 us, Max = 4.841 us
IQR = 0.283 us, LowerFence = 3.759 us, UpperFence = 4.890 us
ConfidenceInterval = [4.248 us; 4.418 us] (CI 99.9%), Margin = 0.085 us (1.97% of Mean)
Skewness = 0.28, Kurtosis = 2.47, MValue = 3.06
-------------------- Histogram --------------------
[3.986 us ; 4.134 us) | @@@@@@
[4.134 us ; 4.258 us) | @@@@@@@@@@@@@@@@@
[4.258 us ; 4.404 us) | @@@@@@@@
[4.404 us ; 4.529 us) | @@@@@@@@@@@@@@@@
[4.529 us ; 4.646 us) | @@@@@@
[4.646 us ; 4.778 us) |
[4.778 us ; 4.903 us) | @
---------------------------------------------------

IterationSamples.Enumerable_ParallelForAll: DefaultJob [Size=10]
Runtime = .NET 6.0.24 (6.0.2423.51814), X64 RyuJIT AVX2; GC = Concurrent Workstation
Mean = 8.572 us, StdErr = 0.043 us (0.50%), N = 21, StdDev = 0.195 us
Min = 8.241 us, Q1 = 8.424 us, Median = 8.516 us, Q3 = 8.718 us, Max = 8.948 us
IQR = 0.295 us, LowerFence = 7.982 us, UpperFence = 9.160 us
ConfidenceInterval = [8.409 us; 8.736 us] (CI 99.9%), Margin = 0.164 us (1.91% of Mean)
Skewness = 0.16, Kurtosis = 1.75, MValue = 2
-------------------- Histogram --------------------
[8.148 us ; 8.331 us) | @
[8.331 us ; 8.516 us) | @@@@@@@@@@
[8.516 us ; 8.825 us) | @@@@@@@@@
[8.825 us ; 9.041 us) | @
---------------------------------------------------

IterationSamples.Array_For: DefaultJob [Size=1000]
Runtime = .NET 6.0.24 (6.0.2423.51814), X64 RyuJIT AVX2; GC = Concurrent Workstation
Mean = 252.581 ns, StdErr = 1.325 ns (0.52%), N = 23, StdDev = 6.355 ns
Min = 241.795 ns, Q1 = 248.165 ns, Median = 251.446 ns, Q3 = 257.046 ns, Max = 265.391 ns
IQR = 8.881 ns, LowerFence = 234.843 ns, UpperFence = 270.368 ns
ConfidenceInterval = [247.556 ns; 257.607 ns] (CI 99.9%), Margin = 5.025 ns (1.99% of Mean)
Skewness = 0.34, Kurtosis = 2.07, MValue = 2
-------------------- Histogram --------------------
[240.994 ns ; 247.673 ns) | @@@@
[247.673 ns ; 253.539 ns) | @@@@@@@@@@@@
[253.539 ns ; 268.324 ns) | @@@@@@@
---------------------------------------------------

IterationSamples.Array_ForEach: DefaultJob [Size=1000]
Runtime = .NET 6.0.24 (6.0.2423.51814), X64 RyuJIT AVX2; GC = Concurrent Workstation
Mean = 235.504 ns, StdErr = 1.338 ns (0.57%), N = 50, StdDev = 9.460 ns
Min = 222.791 ns, Q1 = 228.407 ns, Median = 233.964 ns, Q3 = 240.579 ns, Max = 264.704 ns
IQR = 12.172 ns, LowerFence = 210.149 ns, UpperFence = 258.837 ns
ConfidenceInterval = [230.821 ns; 240.187 ns] (CI 99.9%), Margin = 4.683 ns (1.99% of Mean)
Skewness = 1, Kurtosis = 3.87, MValue = 2
-------------------- Histogram --------------------
[219.421 ns ; 224.217 ns) | @
[224.217 ns ; 230.957 ns) | @@@@@@@@@@@@@@@@@@
[230.957 ns ; 239.022 ns) | @@@@@@@@@@@@@@@@
[239.022 ns ; 245.549 ns) | @@@@@@@@@
[245.549 ns ; 252.147 ns) | @@@@
[252.147 ns ; 259.961 ns) |
[259.961 ns ; 266.702 ns) | @@
---------------------------------------------------

IterationSamples.Array_ForEachLinq: DefaultJob [Size=1000]
Runtime = .NET 6.0.24 (6.0.2423.51814), X64 RyuJIT AVX2; GC = Concurrent Workstation
Mean = 1.328 us, StdErr = 0.012 us (0.92%), N = 92, StdDev = 0.118 us
Min = 1.184 us, Q1 = 1.245 us, Median = 1.276 us, Q3 = 1.384 us, Max = 1.647 us
IQR = 0.139 us, LowerFence = 1.037 us, UpperFence = 1.592 us
ConfidenceInterval = [1.287 us; 1.370 us] (CI 99.9%), Margin = 0.042 us (3.14% of Mean)
Skewness = 1.28, Kurtosis = 3.54, MValue = 2.3
-------------------- Histogram --------------------
[1.150 us ; 1.219 us) | @@@@
[1.219 us ; 1.288 us) | @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
[1.288 us ; 1.366 us) | @@@@@@@@@@@@@@@
[1.366 us ; 1.440 us) | @@@@@@@@@
[1.440 us ; 1.505 us) | @@
[1.505 us ; 1.597 us) | @@@@@@@@
[1.597 us ; 1.665 us) | @@@@@
---------------------------------------------------

IterationSamples.Array_ParallelForEach: DefaultJob [Size=1000]
Runtime = .NET 6.0.24 (6.0.2423.51814), X64 RyuJIT AVX2; GC = Concurrent Workstation
Mean = 11.232 us, StdErr = 0.032 us (0.29%), N = 15, StdDev = 0.125 us
Min = 10.922 us, Q1 = 11.190 us, Median = 11.260 us, Q3 = 11.321 us, Max = 11.408 us
IQR = 0.131 us, LowerFence = 10.994 us, UpperFence = 11.517 us
ConfidenceInterval = [11.099 us; 11.366 us] (CI 99.9%), Margin = 0.134 us (1.19% of Mean)
Skewness = -0.9, Kurtosis = 3.17, MValue = 2
-------------------- Histogram --------------------
[10.856 us ; 11.181 us) | @@@@
[11.181 us ; 11.474 us) | @@@@@@@@@@@
---------------------------------------------------

IterationSamples.Array_ParallelForAll: DefaultJob [Size=1000]
Runtime = .NET 6.0.24 (6.0.2423.51814), X64 RyuJIT AVX2; GC = Concurrent Workstation
Mean = 9.094 us, StdErr = 0.037 us (0.40%), N = 14, StdDev = 0.137 us
Min = 8.895 us, Q1 = 9.022 us, Median = 9.119 us, Q3 = 9.160 us, Max = 9.349 us
IQR = 0.138 us, LowerFence = 8.816 us, UpperFence = 9.367 us
ConfidenceInterval = [8.940 us; 9.249 us] (CI 99.9%), Margin = 0.155 us (1.70% of Mean)
Skewness = 0.11, Kurtosis = 2.08, MValue = 2
-------------------- Histogram --------------------
[8.820 us ; 9.003 us) | @@@
[9.003 us ; 9.232 us) | @@@@@@@@@
[9.232 us ; 9.424 us) | @@
---------------------------------------------------

IterationSamples.Array_ForEachAsSpan: DefaultJob [Size=1000]
Runtime = .NET 6.0.24 (6.0.2423.51814), X64 RyuJIT AVX2; GC = Concurrent Workstation
Mean = 228.994 ns, StdErr = 0.978 ns (0.43%), N = 15, StdDev = 3.787 ns
Min = 223.858 ns, Q1 = 226.346 ns, Median = 227.215 ns, Q3 = 231.905 ns, Max = 236.440 ns
IQR = 5.559 ns, LowerFence = 218.007 ns, UpperFence = 240.244 ns
ConfidenceInterval = [224.946 ns; 233.043 ns] (CI 99.9%), Margin = 4.049 ns (1.77% of Mean)
Skewness = 0.48, Kurtosis = 1.81, MValue = 2
-------------------- Histogram --------------------
[223.281 ns ; 228.982 ns) | @@@@@@@@
[228.982 ns ; 237.593 ns) | @@@@@@@
---------------------------------------------------

IterationSamples.Array_ForMemoryMarshalSpanUsafe: DefaultJob [Size=1000]
Runtime = .NET 6.0.24 (6.0.2423.51814), X64 RyuJIT AVX2; GC = Concurrent Workstation
Mean = 241.874 ns, StdErr = 1.390 ns (0.57%), N = 51, StdDev = 9.925 ns
Min = 226.973 ns, Q1 = 234.728 ns, Median = 240.131 ns, Q3 = 247.194 ns, Max = 265.216 ns
IQR = 12.465 ns, LowerFence = 216.030 ns, UpperFence = 265.892 ns
ConfidenceInterval = [237.016 ns; 246.733 ns] (CI 99.9%), Margin = 4.859 ns (2.01% of Mean)
Skewness = 0.68, Kurtosis = 2.63, MValue = 2.59
-------------------- Histogram --------------------
[226.686 ns ; 234.414 ns) | @@@@@@@@@@@@
[234.414 ns ; 241.440 ns) | @@@@@@@@@@@@@@@@@
[241.440 ns ; 251.650 ns) | @@@@@@@@@@@@@@@
[251.650 ns ; 257.173 ns) | @
[257.173 ns ; 268.728 ns) | @@@@@@
---------------------------------------------------

IterationSamples.List_For: DefaultJob [Size=1000]
Runtime = .NET 6.0.24 (6.0.2423.51814), X64 RyuJIT AVX2; GC = Concurrent Workstation
Mean = 4.039 us, StdErr = 0.024 us (0.58%), N = 75, StdDev = 0.204 us
Min = 3.665 us, Q1 = 3.894 us, Median = 4.004 us, Q3 = 4.166 us, Max = 4.561 us
IQR = 0.271 us, LowerFence = 3.487 us, UpperFence = 4.573 us
ConfidenceInterval = [3.958 us; 4.120 us] (CI 99.9%), Margin = 0.081 us (2.00% of Mean)
Skewness = 0.53, Kurtosis = 2.85, MValue = 2
-------------------- Histogram --------------------
[3.602 us ; 3.738 us) | @@@@
[3.738 us ; 3.877 us) | @@@@@@@@@@@
[3.877 us ; 4.057 us) | @@@@@@@@@@@@@@@@@@@@@@@@@@
[4.057 us ; 4.185 us) | @@@@@@@@@@@@@@@@@@
[4.185 us ; 4.320 us) | @@@@@@@@@@
[4.320 us ; 4.465 us) | @@@
[4.465 us ; 4.602 us) | @@@
---------------------------------------------------

IterationSamples.List_Foreach: DefaultJob [Size=1000]
Runtime = .NET 6.0.24 (6.0.2423.51814), X64 RyuJIT AVX2; GC = Concurrent Workstation
Mean = 679.089 ns, StdErr = 7.107 ns (1.05%), N = 100, StdDev = 71.074 ns
Min = 577.557 ns, Q1 = 621.786 ns, Median = 661.272 ns, Q3 = 732.916 ns, Max = 895.332 ns
IQR = 111.130 ns, LowerFence = 455.092 ns, UpperFence = 899.610 ns
ConfidenceInterval = [654.984 ns; 703.194 ns] (CI 99.9%), Margin = 24.105 ns (3.55% of Mean)
Skewness = 0.71, Kurtosis = 2.68, MValue = 2.97
-------------------- Histogram --------------------
[557.459 ns ; 598.827 ns) | @@@@@@
[598.827 ns ; 639.022 ns) | @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
[639.022 ns ; 674.083 ns) | @@@@@@@@@@@@@
[674.083 ns ; 695.880 ns) | @@@
[695.880 ns ; 742.905 ns) | @@@@@@@@@@@@@@@@@@@@@
[742.905 ns ; 783.100 ns) | @@@@@@@@@@@@
[783.100 ns ; 825.674 ns) | @@@@@
[825.674 ns ; 866.887 ns) | @@
[866.887 ns ; 915.430 ns) | @
---------------------------------------------------

IterationSamples.List_ForEachLinq: DefaultJob [Size=1000]
Runtime = .NET 6.0.24 (6.0.2423.51814), X64 RyuJIT AVX2; GC = Concurrent Workstation
Mean = 1.767 us, StdErr = 0.016 us (0.89%), N = 100, StdDev = 0.156 us
Min = 1.506 us, Q1 = 1.638 us, Median = 1.733 us, Q3 = 1.881 us, Max = 2.143 us
IQR = 0.242 us, LowerFence = 1.274 us, UpperFence = 2.244 us
ConfidenceInterval = [1.714 us; 1.820 us] (CI 99.9%), Margin = 0.053 us (3.00% of Mean)
Skewness = 0.4, Kurtosis = 2.26, MValue = 2.44
-------------------- Histogram --------------------
[1.462 us ; 1.539 us) | @@@@@
[1.539 us ; 1.630 us) | @@@@@@@@@@@@@@@@
[1.630 us ; 1.718 us) | @@@@@@@@@@@@@@@@@@@@@@@@@@@
[1.718 us ; 1.814 us) | @@@@@@@@@@@@@@@
[1.814 us ; 1.926 us) | @@@@@@@@@@@@@@@@@@@@@
[1.926 us ; 2.019 us) | @@@@@@@@@@
[2.019 us ; 2.122 us) | @@@@@
[2.122 us ; 2.188 us) | @
---------------------------------------------------

IterationSamples.List_ParallelForEach: DefaultJob [Size=1000]
Runtime = .NET 6.0.24 (6.0.2423.51814), X64 RyuJIT AVX2; GC = Concurrent Workstation
Mean = 12.538 us, StdErr = 0.049 us (0.39%), N = 15, StdDev = 0.191 us
Min = 12.162 us, Q1 = 12.422 us, Median = 12.604 us, Q3 = 12.672 us, Max = 12.798 us
IQR = 0.249 us, LowerFence = 12.048 us, UpperFence = 13.046 us
ConfidenceInterval = [12.334 us; 12.742 us] (CI 99.9%), Margin = 0.204 us (1.63% of Mean)
Skewness = -0.47, Kurtosis = 1.97, MValue = 2
-------------------- Histogram --------------------
[12.115 us ; 12.828 us) | @@@@@@@@@@@@@@@
---------------------------------------------------

IterationSamples.List_ParallelForAll: DefaultJob [Size=1000]
Runtime = .NET 6.0.24 (6.0.2423.51814), X64 RyuJIT AVX2; GC = Concurrent Workstation
Mean = 9.805 us, StdErr = 0.040 us (0.41%), N = 15, StdDev = 0.155 us
Min = 9.544 us, Q1 = 9.714 us, Median = 9.794 us, Q3 = 9.897 us, Max = 10.120 us
IQR = 0.183 us, LowerFence = 9.440 us, UpperFence = 10.171 us
ConfidenceInterval = [9.640 us; 9.970 us] (CI 99.9%), Margin = 0.165 us (1.69% of Mean)
Skewness = 0.25, Kurtosis = 2.22, MValue = 2
-------------------- Histogram --------------------
[9.494 us ;  9.855 us) | @@@@@@@@@@
[9.855 us ; 10.202 us) | @@@@@
---------------------------------------------------

IterationSamples.List_ForEachAsSpanUnsafe: DefaultJob [Size=1000]
Runtime = .NET 6.0.24 (6.0.2423.51814), X64 RyuJIT AVX2; GC = Concurrent Workstation
Mean = 236.292 ns, StdErr = 1.306 ns (0.55%), N = 45, StdDev = 8.760 ns
Min = 224.994 ns, Q1 = 230.236 ns, Median = 234.251 ns, Q3 = 239.655 ns, Max = 258.620 ns
IQR = 9.420 ns, LowerFence = 216.106 ns, UpperFence = 253.785 ns
ConfidenceInterval = [231.688 ns; 240.897 ns] (CI 99.9%), Margin = 4.604 ns (1.95% of Mean)
Skewness = 0.92, Kurtosis = 2.92, MValue = 2
-------------------- Histogram --------------------
[221.762 ns ; 228.760 ns) | @@@@@@@
[228.760 ns ; 235.225 ns) | @@@@@@@@@@@@@@@@@@@@
[235.225 ns ; 242.051 ns) | @@@@@@@@
[242.051 ns ; 250.556 ns) | @@@@@@@
[250.556 ns ; 260.438 ns) | @@@
---------------------------------------------------

IterationSamples.Enumerable_For: DefaultJob [Size=1000]
Runtime = .NET 6.0.24 (6.0.2423.51814), X64 RyuJIT AVX2; GC = Concurrent Workstation
Mean = 9.376 ms, StdErr = 0.053 ms (0.57%), N = 50, StdDev = 0.376 ms
Min = 8.769 ms, Q1 = 9.122 ms, Median = 9.305 ms, Q3 = 9.616 ms, Max = 10.252 ms
IQR = 0.494 ms, LowerFence = 8.380 ms, UpperFence = 10.357 ms
ConfidenceInterval = [9.190 ms; 9.562 ms] (CI 99.9%), Margin = 0.186 ms (1.98% of Mean)
Skewness = 0.39, Kurtosis = 2.22, MValue = 2
-------------------- Histogram --------------------
[ 8.749 ms ;  9.065 ms) | @@@@@@@@@@
[ 9.065 ms ;  9.333 ms) | @@@@@@@@@@@@@@@@@
[ 9.333 ms ;  9.623 ms) | @@@@@@@@@@@
[ 9.623 ms ; 10.034 ms) | @@@@@@@@@@
[10.034 ms ; 10.297 ms) | @@
---------------------------------------------------

IterationSamples.Enumerable_Foreach: DefaultJob [Size=1000]
Runtime = .NET 6.0.24 (6.0.2423.51814), X64 RyuJIT AVX2; GC = Concurrent Workstation
Mean = 12.841 us, StdErr = 0.073 us (0.57%), N = 76, StdDev = 0.635 us
Min = 11.884 us, Q1 = 12.385 us, Median = 12.639 us, Q3 = 13.160 us, Max = 14.373 us
IQR = 0.775 us, LowerFence = 11.222 us, UpperFence = 14.323 us
ConfidenceInterval = [12.591 us; 13.090 us] (CI 99.9%), Margin = 0.249 us (1.94% of Mean)
Skewness = 0.7, Kurtosis = 2.44, MValue = 2.37
-------------------- Histogram --------------------
[11.844 us ; 12.280 us) | @@@@@@@@@@@@@
[12.280 us ; 12.673 us) | @@@@@@@@@@@@@@@@@@@@@@@@@@@
[12.673 us ; 13.187 us) | @@@@@@@@@@@@@@@@@@
[13.187 us ; 13.585 us) | @@@@@
[13.585 us ; 14.091 us) | @@@@@@@@@@
[14.091 us ; 14.481 us) | @@@
---------------------------------------------------

IterationSamples.Enumerable_ForEachLinq: DefaultJob [Size=1000]
Runtime = .NET 6.0.24 (6.0.2423.51814), X64 RyuJIT AVX2; GC = Concurrent Workstation
Mean = 13.837 us, StdErr = 0.079 us (0.57%), N = 54, StdDev = 0.578 us
Min = 12.977 us, Q1 = 13.380 us, Median = 13.637 us, Q3 = 14.232 us, Max = 15.455 us
IQR = 0.852 us, LowerFence = 12.102 us, UpperFence = 15.509 us
ConfidenceInterval = [13.563 us; 14.111 us] (CI 99.9%), Margin = 0.274 us (1.98% of Mean)
Skewness = 0.77, Kurtosis = 2.94, MValue = 2.43
-------------------- Histogram --------------------
[12.777 us ; 13.212 us) | @@@@
[13.212 us ; 13.614 us) | @@@@@@@@@@@@@@@@@@@@@@@
[13.614 us ; 14.082 us) | @@@@@@@@
[14.082 us ; 14.483 us) | @@@@@@@@@@@@@
[14.483 us ; 15.115 us) | @@@
[15.115 us ; 15.517 us) | @@@
---------------------------------------------------

IterationSamples.Enumerable_ParallelForEach: DefaultJob [Size=1000]
Runtime = .NET 6.0.24 (6.0.2423.51814), X64 RyuJIT AVX2; GC = Concurrent Workstation
Mean = 94.792 us, StdErr = 0.620 us (0.65%), N = 100, StdDev = 6.201 us
Min = 82.069 us, Q1 = 90.605 us, Median = 94.781 us, Q3 = 99.810 us, Max = 108.274 us
IQR = 9.205 us, LowerFence = 76.798 us, UpperFence = 113.618 us
ConfidenceInterval = [92.689 us; 96.895 us] (CI 99.9%), Margin = 2.103 us (2.22% of Mean)
Skewness = -0.09, Kurtosis = 2.21, MValue = 2
-------------------- Histogram --------------------
[ 80.316 us ;  83.772 us) | @
[ 83.772 us ;  87.278 us) | @@@@@@@@@@@@@@@@
[ 87.278 us ;  92.299 us) | @@@@@@@@@@@@@@@@@@@
[ 92.299 us ;  97.017 us) | @@@@@@@@@@@@@@@@@@@@@@@@@@@
[ 97.017 us ; 102.274 us) | @@@@@@@@@@@@@@@@@@@@@@@@@@@@@
[102.274 us ; 107.148 us) | @@@@@@@
[107.148 us ; 110.027 us) | @
---------------------------------------------------

IterationSamples.Enumerable_ParallelForAll: DefaultJob [Size=1000]
Runtime = .NET 6.0.24 (6.0.2423.51814), X64 RyuJIT AVX2; GC = Concurrent Workstation
Mean = 106.531 us, StdErr = 0.609 us (0.57%), N = 53, StdDev = 4.436 us
Min = 98.527 us, Q1 = 103.130 us, Median = 105.855 us, Q3 = 109.782 us, Max = 118.174 us
IQR = 6.652 us, LowerFence = 93.152 us, UpperFence = 119.759 us
ConfidenceInterval = [104.406 us; 108.656 us] (CI 99.9%), Margin = 2.125 us (1.99% of Mean)
Skewness = 0.39, Kurtosis = 2.37, MValue = 2.53
-------------------- Histogram --------------------
[ 97.658 us ; 101.190 us) | @@@
[101.190 us ; 104.290 us) | @@@@@@@@@@@@@@@@@@@
[104.290 us ; 107.763 us) | @@@@@@@@@
[107.763 us ; 110.863 us) | @@@@@@@@@@@@@@
[110.863 us ; 114.345 us) | @@@@@@
[114.345 us ; 119.723 us) | @@
---------------------------------------------------

IterationSamples.Array_For: DefaultJob [Size=100000]
Runtime = .NET 6.0.24 (6.0.2423.51814), X64 RyuJIT AVX2; GC = Concurrent Workstation
Mean = 28.924 us, StdErr = 0.425 us (1.47%), N = 100, StdDev = 4.253 us
Min = 22.323 us, Q1 = 24.805 us, Median = 30.540 us, Q3 = 31.680 us, Max = 37.796 us
IQR = 6.874 us, LowerFence = 14.494 us, UpperFence = 41.992 us
ConfidenceInterval = [27.482 us; 30.367 us] (CI 99.9%), Margin = 1.442 us (4.99% of Mean)
Skewness = -0.04, Kurtosis = 1.79, MValue = 3.49
-------------------- Histogram --------------------
[21.121 us ; 22.519 us) | @
[22.519 us ; 24.924 us) | @@@@@@@@@@@@@@@@@@@@@@@@@@
[24.924 us ; 27.460 us) | @@@@@@@@@@@@@@
[27.460 us ; 30.018 us) | @@@@@
[30.018 us ; 32.803 us) | @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
[32.803 us ; 35.209 us) | @@@@@@@@@@@@@@
[35.209 us ; 38.064 us) | @@@@@
---------------------------------------------------

IterationSamples.Array_ForEach: DefaultJob [Size=100000]
Runtime = .NET 6.0.24 (6.0.2423.51814), X64 RyuJIT AVX2; GC = Concurrent Workstation
Mean = 28.983 us, StdErr = 0.181 us (0.62%), N = 100, StdDev = 1.805 us
Min = 25.575 us, Q1 = 27.663 us, Median = 28.644 us, Q3 = 30.490 us, Max = 33.405 us
IQR = 2.827 us, LowerFence = 23.423 us, UpperFence = 34.730 us
ConfidenceInterval = [28.371 us; 29.595 us] (CI 99.9%), Margin = 0.612 us (2.11% of Mean)
Skewness = 0.4, Kurtosis = 2.32, MValue = 2.06
-------------------- Histogram --------------------
[25.065 us ; 25.855 us) | @@
[25.855 us ; 27.062 us) | @@@@@@@
[27.062 us ; 28.083 us) | @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
[28.083 us ; 29.230 us) | @@@@@@@@@@@@@@@@@@@@@
[29.230 us ; 30.430 us) | @@@@@@@@@@@@@
[30.430 us ; 31.451 us) | @@@@@@@@@@@@@@
[31.451 us ; 32.502 us) | @@@@@@@@@
[32.502 us ; 33.549 us) | @@@
---------------------------------------------------

IterationSamples.Array_ForEachLinq: DefaultJob [Size=100000]
Runtime = .NET 6.0.24 (6.0.2423.51814), X64 RyuJIT AVX2; GC = Concurrent Workstation
Mean = 163.966 us, StdErr = 0.944 us (0.58%), N = 75, StdDev = 8.177 us
Min = 142.533 us, Q1 = 158.095 us, Median = 162.850 us, Q3 = 169.557 us, Max = 182.438 us
IQR = 11.462 us, LowerFence = 140.902 us, UpperFence = 186.751 us
ConfidenceInterval = [160.730 us; 167.201 us] (CI 99.9%), Margin = 3.236 us (1.97% of Mean)
Skewness = 0.09, Kurtosis = 2.61, MValue = 2.67
-------------------- Histogram --------------------
[139.988 us ; 145.078 us) | @
[145.078 us ; 151.188 us) | @@
[151.188 us ; 156.610 us) | @@@@@@@@@
[156.610 us ; 161.700 us) | @@@@@@@@@@@@@@@@@@@@@
[161.700 us ; 165.753 us) | @@@@@@@@@@@
[165.753 us ; 170.843 us) | @@@@@@@@@@@@@@@@@@
[170.843 us ; 177.701 us) | @@@@@@@@@@
[177.701 us ; 183.707 us) | @@@
---------------------------------------------------

IterationSamples.Array_ParallelForEach: DefaultJob [Size=100000]
Runtime = .NET 6.0.24 (6.0.2423.51814), X64 RyuJIT AVX2; GC = Concurrent Workstation
Mean = 161.314 us, StdErr = 1.146 us (0.71%), N = 98, StdDev = 11.343 us
Min = 130.901 us, Q1 = 153.862 us, Median = 161.546 us, Q3 = 169.218 us, Max = 189.678 us
IQR = 15.356 us, LowerFence = 130.828 us, UpperFence = 192.253 us
ConfidenceInterval = [157.426 us; 165.203 us] (CI 99.9%), Margin = 3.888 us (2.41% of Mean)
Skewness = -0.06, Kurtosis = 3.12, MValue = 2.89
-------------------- Histogram --------------------
[127.671 us ; 134.901 us) | @
[134.901 us ; 141.359 us) | @@@@@
[141.359 us ; 147.759 us) | @@
[147.759 us ; 154.217 us) | @@@@@@@@@@@@@@@@@@
[154.217 us ; 159.841 us) | @@@@@@@@@@@@@
[159.841 us ; 166.299 us) | @@@@@@@@@@@@@@@@@@@@@@@@@@@
[166.299 us ; 173.193 us) | @@@@@@@@@@@@@@@@@@@@@
[173.193 us ; 179.986 us) | @@@@@@@
[179.986 us ; 184.032 us) |
[184.032 us ; 190.490 us) | @@@@
---------------------------------------------------

IterationSamples.Array_ParallelForAll: DefaultJob [Size=100000]
Runtime = .NET 6.0.24 (6.0.2423.51814), X64 RyuJIT AVX2; GC = Concurrent Workstation
Mean = 220.056 us, StdErr = 1.030 us (0.47%), N = 18, StdDev = 4.369 us
Min = 212.418 us, Q1 = 218.402 us, Median = 220.327 us, Q3 = 222.024 us, Max = 227.431 us
IQR = 3.621 us, LowerFence = 212.970 us, UpperFence = 227.456 us
ConfidenceInterval = [215.972 us; 224.139 us] (CI 99.9%), Margin = 4.084 us (1.86% of Mean)
Skewness = -0.04, Kurtosis = 2.1, MValue = 2
-------------------- Histogram --------------------
[211.657 us ; 218.091 us) | @@@@
[218.091 us ; 224.535 us) | @@@@@@@@@@@
[224.535 us ; 229.619 us) | @@@
---------------------------------------------------

IterationSamples.Array_ForEachAsSpan: DefaultJob [Size=100000]
Runtime = .NET 6.0.24 (6.0.2423.51814), X64 RyuJIT AVX2; GC = Concurrent Workstation
Mean = 29.677 us, StdErr = 0.190 us (0.64%), N = 98, StdDev = 1.876 us
Min = 26.051 us, Q1 = 28.244 us, Median = 29.323 us, Q3 = 30.733 us, Max = 34.459 us
IQR = 2.489 us, LowerFence = 24.509 us, UpperFence = 34.467 us
ConfidenceInterval = [29.033 us; 30.320 us] (CI 99.9%), Margin = 0.643 us (2.17% of Mean)
Skewness = 0.63, Kurtosis = 2.73, MValue = 2.06
-------------------- Histogram --------------------
[25.517 us ; 26.745 us) | @@@
[26.745 us ; 27.799 us) | @@@@@@@
[27.799 us ; 28.867 us) | @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
[28.867 us ; 30.255 us) | @@@@@@@@@@@@@@@@@@@@@
[30.255 us ; 31.400 us) | @@@@@@@@@@@@@@@@@
[31.400 us ; 32.431 us) | @@@@@@
[32.431 us ; 33.499 us) | @@@@@@@
[33.499 us ; 34.609 us) | @@@@
---------------------------------------------------

IterationSamples.Array_ForMemoryMarshalSpanUsafe: DefaultJob [Size=100000]
Runtime = .NET 6.0.24 (6.0.2423.51814), X64 RyuJIT AVX2; GC = Concurrent Workstation
Mean = 30.511 us, StdErr = 0.175 us (0.57%), N = 57, StdDev = 1.324 us
Min = 27.503 us, Q1 = 29.363 us, Median = 30.644 us, Q3 = 31.343 us, Max = 33.700 us
IQR = 1.981 us, LowerFence = 26.391 us, UpperFence = 34.315 us
ConfidenceInterval = [29.902 us; 31.120 us] (CI 99.9%), Margin = 0.609 us (2.00% of Mean)
Skewness = 0.22, Kurtosis = 2.42, MValue = 3.11
-------------------- Histogram --------------------
[27.052 us ; 27.955 us) | @
[27.955 us ; 28.816 us) | @@
[28.816 us ; 29.719 us) | @@@@@@@@@@@@@@@@@
[29.719 us ; 30.514 us) | @@@@@@@
[30.514 us ; 31.417 us) | @@@@@@@@@@@@@@@@@@
[31.417 us ; 32.453 us) | @@@@@@@@
[32.453 us ; 33.395 us) | @@@
[33.395 us ; 34.152 us) | @
---------------------------------------------------

IterationSamples.List_For: DefaultJob [Size=100000]
Runtime = .NET 6.0.24 (6.0.2423.51814), X64 RyuJIT AVX2; GC = Concurrent Workstation
Mean = 513.022 us, StdErr = 3.163 us (0.62%), N = 100, StdDev = 31.627 us
Min = 448.021 us, Q1 = 489.041 us, Median = 511.271 us, Q3 = 533.989 us, Max = 592.454 us
IQR = 44.949 us, LowerFence = 421.617 us, UpperFence = 601.413 us
ConfidenceInterval = [502.296 us; 523.749 us] (CI 99.9%), Margin = 10.727 us (2.09% of Mean)
Skewness = 0.38, Kurtosis = 2.75, MValue = 2.36
-------------------- Histogram --------------------
[442.399 us ; 461.123 us) | @@
[461.123 us ; 481.501 us) | @@@@@@@@@@@@@
[481.501 us ; 503.729 us) | @@@@@@@@@@@@@@@@@@@@@@@
[503.729 us ; 521.616 us) | @@@@@@@@@@@@@@@@@@@@@@@@@@@@
[521.616 us ; 541.454 us) | @@@@@@@@@@@@@@@@@
[541.454 us ; 560.193 us) | @@@@@@@@@@
[560.193 us ; 574.602 us) | @
[574.602 us ; 592.488 us) | @@@@@@
---------------------------------------------------

IterationSamples.List_Foreach: DefaultJob [Size=100000]
Runtime = .NET 6.0.24 (6.0.2423.51814), X64 RyuJIT AVX2; GC = Concurrent Workstation
Mean = 66.751 us, StdErr = 1.113 us (1.67%), N = 94, StdDev = 10.791 us
Min = 56.808 us, Q1 = 58.948 us, Median = 62.066 us, Q3 = 68.771 us, Max = 100.478 us
IQR = 9.823 us, LowerFence = 44.214 us, UpperFence = 83.505 us
ConfidenceInterval = [62.969 us; 70.534 us] (CI 99.9%), Margin = 3.782 us (5.67% of Mean)
Skewness = 1.36, Kurtosis = 3.84, MValue = 2.32
-------------------- Histogram --------------------
[56.729 us ;  62.959 us) | @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
[62.959 us ;  69.115 us) | @@@@@@@@@@@@@@@@@@@@
[69.115 us ;  73.671 us) | @@
[73.671 us ;  79.179 us) | @@@@
[79.179 us ;  85.409 us) | @@@@@@@@@
[85.409 us ;  91.790 us) | @@@@@
[91.790 us ;  96.868 us) | @
[96.868 us ; 103.593 us) | @@
---------------------------------------------------

IterationSamples.List_ForEachLinq: DefaultJob [Size=100000]
Runtime = .NET 6.0.24 (6.0.2423.51814), X64 RyuJIT AVX2; GC = Concurrent Workstation
Mean = 153.497 us, StdErr = 0.691 us (0.45%), N = 17, StdDev = 2.850 us
Min = 150.507 us, Q1 = 151.467 us, Median = 152.483 us, Q3 = 155.401 us, Max = 159.639 us
IQR = 3.934 us, LowerFence = 145.566 us, UpperFence = 161.302 us
ConfidenceInterval = [150.722 us; 156.271 us] (CI 99.9%), Margin = 2.775 us (1.81% of Mean)
Skewness = 0.85, Kurtosis = 2.28, MValue = 2
-------------------- Histogram --------------------
[150.328 us ; 155.211 us) | @@@@@@@@@@@@
[155.211 us ; 161.094 us) | @@@@@
---------------------------------------------------

IterationSamples.List_ParallelForEach: DefaultJob [Size=100000]
Runtime = .NET 6.0.24 (6.0.2423.51814), X64 RyuJIT AVX2; GC = Concurrent Workstation
Mean = 188.177 us, StdErr = 1.102 us (0.59%), N = 89, StdDev = 10.396 us
Min = 175.297 us, Q1 = 179.812 us, Median = 184.626 us, Q3 = 194.682 us, Max = 213.945 us
IQR = 14.871 us, LowerFence = 157.506 us, UpperFence = 216.988 us
ConfidenceInterval = [184.425 us; 191.929 us] (CI 99.9%), Margin = 3.752 us (1.99% of Mean)
Skewness = 0.8, Kurtosis = 2.54, MValue = 2.62
-------------------- Histogram --------------------
[172.241 us ; 176.499 us) | @
[176.499 us ; 182.611 us) | @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
[182.611 us ; 189.435 us) | @@@@@@@
[189.435 us ; 195.548 us) | @@@@@@@@@@@@@@@@@@@@
[195.548 us ; 204.814 us) | @@@@@@@@@
[204.814 us ; 210.926 us) | @@@@@@@@
[210.926 us ; 217.001 us) | @@
---------------------------------------------------

IterationSamples.List_ParallelForAll: DefaultJob [Size=100000]
Runtime = .NET 6.0.24 (6.0.2423.51814), X64 RyuJIT AVX2; GC = Concurrent Workstation
Mean = 236.376 us, StdErr = 1.739 us (0.74%), N = 99, StdDev = 17.301 us
Min = 213.666 us, Q1 = 223.377 us, Median = 230.900 us, Q3 = 247.279 us, Max = 283.484 us
IQR = 23.902 us, LowerFence = 187.524 us, UpperFence = 283.132 us
ConfidenceInterval = [230.477 us; 242.275 us] (CI 99.9%), Margin = 5.899 us (2.50% of Mean)
Skewness = 1, Kurtosis = 3.05, MValue = 2.32
-------------------- Histogram --------------------
[208.758 us ; 219.127 us) | @@@@@@@@@@
[219.127 us ; 228.944 us) | @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
[228.944 us ; 238.663 us) | @@@@@@@@@@@@@@@@@@@@
[238.663 us ; 246.559 us) | @@@@@@
[246.559 us ; 257.525 us) | @@@@@@@@@@@@
[257.525 us ; 268.674 us) | @@@@@@
[268.674 us ; 278.491 us) | @@@@@@
[278.491 us ; 288.392 us) | @@
---------------------------------------------------

IterationSamples.List_ForEachAsSpanUnsafe: DefaultJob [Size=100000]
Runtime = .NET 6.0.24 (6.0.2423.51814), X64 RyuJIT AVX2; GC = Concurrent Workstation
Mean = 22.112 us, StdErr = 0.043 us (0.19%), N = 15, StdDev = 0.165 us
Min = 21.878 us, Q1 = 21.982 us, Median = 22.096 us, Q3 = 22.233 us, Max = 22.377 us
IQR = 0.251 us, LowerFence = 21.606 us, UpperFence = 22.610 us
ConfidenceInterval = [21.935 us; 22.288 us] (CI 99.9%), Margin = 0.177 us (0.80% of Mean)
Skewness = 0.11, Kurtosis = 1.47, MValue = 2
-------------------- Histogram --------------------
[21.790 us ; 22.465 us) | @@@@@@@@@@@@@@@
---------------------------------------------------

IterationSamples.Enumerable_For: DefaultJob [Size=100000]
Runtime = .NET 6.0.24 (6.0.2423.51814), X64 RyuJIT AVX2; GC = Concurrent Workstation
Mean = 96.335 s, StdErr = 0.515 s (0.54%), N = 64, StdDev = 4.124 s
Min = 93.763 s, Q1 = 94.364 s, Median = 94.959 s, Q3 = 95.626 s, Max = 112.083 s
IQR = 1.262 s, LowerFence = 92.471 s, UpperFence = 97.519 s
ConfidenceInterval = [94.555 s; 98.114 s] (CI 99.9%), Margin = 1.779 s (1.85% of Mean)
Skewness = 2.68, Kurtosis = 9.44, MValue = 2
-------------------- Histogram --------------------
[ 93.493 s ;  96.199 s) | @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
[ 96.199 s ;  99.239 s) | @@@@@@
[ 99.239 s ; 101.661 s) | @@
[101.661 s ; 104.438 s) | @
[104.438 s ; 107.485 s) |
[107.485 s ; 110.020 s) | @
[110.020 s ; 112.726 s) | @@@
---------------------------------------------------

IterationSamples.Enumerable_Foreach: DefaultJob [Size=100000]
Runtime = .NET 6.0.24 (6.0.2423.51814), X64 RyuJIT AVX2; GC = Concurrent Workstation
Mean = 1.266 ms, StdErr = 0.004 ms (0.35%), N = 27, StdDev = 0.023 ms
Min = 1.226 ms, Q1 = 1.247 ms, Median = 1.264 ms, Q3 = 1.281 ms, Max = 1.308 ms
IQR = 0.033 ms, LowerFence = 1.197 ms, UpperFence = 1.331 ms
ConfidenceInterval = [1.250 ms; 1.282 ms] (CI 99.9%), Margin = 0.016 ms (1.29% of Mean)
Skewness = 0.31, Kurtosis = 1.94, MValue = 2
-------------------- Histogram --------------------
[1.216 ms ; 1.263 ms) | @@@@@@@@@@@@
[1.263 ms ; 1.312 ms) | @@@@@@@@@@@@@@@
---------------------------------------------------

IterationSamples.Enumerable_ForEachLinq: DefaultJob [Size=100000]
Runtime = .NET 6.0.24 (6.0.2423.51814), X64 RyuJIT AVX2; GC = Concurrent Workstation
Mean = 1.390 ms, StdErr = 0.007 ms (0.52%), N = 26, StdDev = 0.037 ms
Min = 1.344 ms, Q1 = 1.364 ms, Median = 1.380 ms, Q3 = 1.409 ms, Max = 1.494 ms
IQR = 0.045 ms, LowerFence = 1.297 ms, UpperFence = 1.477 ms
ConfidenceInterval = [1.363 ms; 1.417 ms] (CI 99.9%), Margin = 0.027 ms (1.93% of Mean)
Skewness = 1.08, Kurtosis = 3.82, MValue = 2
-------------------- Histogram --------------------
[1.327 ms ; 1.379 ms) | @@@@@@@@@@@@@
[1.379 ms ; 1.422 ms) | @@@@@@@@@@
[1.422 ms ; 1.465 ms) | @
[1.465 ms ; 1.497 ms) | @@
---------------------------------------------------

IterationSamples.Enumerable_ParallelForEach: DefaultJob [Size=100000]
Runtime = .NET 6.0.24 (6.0.2423.51814), X64 RyuJIT AVX2; GC = Concurrent Workstation
Mean = 2.532 ms, StdErr = 0.014 ms (0.57%), N = 49, StdDev = 0.101 ms
Min = 2.356 ms, Q1 = 2.463 ms, Median = 2.522 ms, Q3 = 2.604 ms, Max = 2.747 ms
IQR = 0.141 ms, LowerFence = 2.251 ms, UpperFence = 2.815 ms
ConfidenceInterval = [2.481 ms; 2.582 ms] (CI 99.9%), Margin = 0.050 ms (1.99% of Mean)
Skewness = 0.35, Kurtosis = 2.21, MValue = 2
-------------------- Histogram --------------------
[2.340 ms ; 2.422 ms) | @@@@@
[2.422 ms ; 2.505 ms) | @@@@@@@@@@@@@@@@@
[2.505 ms ; 2.578 ms) | @@@@@@@@@@@@@
[2.578 ms ; 2.676 ms) | @@@@@@@@@@
[2.676 ms ; 2.751 ms) | @@@@
---------------------------------------------------

IterationSamples.Enumerable_ParallelForAll: DefaultJob [Size=100000]
Runtime = .NET 6.0.24 (6.0.2423.51814), X64 RyuJIT AVX2; GC = Concurrent Workstation
Mean = 2.221 ms, StdErr = 0.010 ms (0.44%), N = 14, StdDev = 0.037 ms
Min = 2.154 ms, Q1 = 2.195 ms, Median = 2.218 ms, Q3 = 2.239 ms, Max = 2.299 ms
IQR = 0.044 ms, LowerFence = 2.130 ms, UpperFence = 2.305 ms
ConfidenceInterval = [2.179 ms; 2.262 ms] (CI 99.9%), Margin = 0.041 ms (1.87% of Mean)
Skewness = 0.36, Kurtosis = 2.58, MValue = 2
-------------------- Histogram --------------------
[2.134 ms ; 2.187 ms) | @
[2.187 ms ; 2.319 ms) | @@@@@@@@@@@@@
---------------------------------------------------

// * Summary *

BenchmarkDotNet v0.13.9+228a464e8be6c580ad9408e98f18813f6407fb5a, Windows 10 (10.0.19045.3636/22H2/2022Update)
11th Gen Intel Core i7-1165G7 2.80GHz, 1 CPU, 8 logical and 4 physical cores
.NET SDK 7.0.306
  [Host]     : .NET 6.0.24 (6.0.2423.51814), X64 RyuJIT AVX2
  DefaultJob : .NET 6.0.24 (6.0.2423.51814), X64 RyuJIT AVX2
```
