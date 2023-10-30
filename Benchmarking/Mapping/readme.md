# Detailed Performance

The sample ran for over 3 hours, to be able to collect these details:

```txt
// * Detailed results *
MappingSamples.XmlDocToModelMapping: DefaultJob
Runtime = .NET 6.0.24 (6.0.2423.51814), X64 RyuJIT AVX2; GC = Concurrent Workstation
Mean = 10.373 us, StdErr = 0.047 us (0.46%), N = 39, StdDev = 0.295 us
Min = 9.971 us, Q1 = 10.150 us, Median = 10.317 us, Q3 = 10.492 us, Max = 11.291 us
IQR = 0.342 us, LowerFence = 9.638 us, UpperFence = 11.004 us
ConfidenceInterval = [10.204 us; 10.541 us] (CI 99.9%), Margin = 0.168 us (1.62% of Mean)
Skewness = 1.01, Kurtosis = 3.64, MValue = 2
-------------------- Histogram --------------------
[ 9.857 us ; 10.247 us) | @@@@@@@@@@@@@@@@@
[10.247 us ; 10.518 us) | @@@@@@@@@@@@@
[10.518 us ; 10.910 us) | @@@@@@@@
[10.910 us ; 11.177 us) |
[11.177 us ; 11.405 us) | @
---------------------------------------------------

MappingSamples.ModelToModelMapping: DefaultJob
Runtime = .NET 6.0.24 (6.0.2423.51814), X64 RyuJIT AVX2; GC = Concurrent Workstation
Mean = 2.260 us, StdErr = 0.012 us (0.52%), N = 24, StdDev = 0.057 us
Min = 2.165 us, Q1 = 2.222 us, Median = 2.243 us, Q3 = 2.290 us, Max = 2.431 us
IQR = 0.068 us, LowerFence = 2.121 us, UpperFence = 2.392 us
ConfidenceInterval = [2.216 us; 2.304 us] (CI 99.9%), Margin = 0.044 us (1.95% of Mean)
Skewness = 0.89, Kurtosis = 4.08, MValue = 2
-------------------- Histogram --------------------
[2.139 us ; 2.196 us) | @@
[2.196 us ; 2.267 us) | @@@@@@@@@@@
[2.267 us ; 2.319 us) | @@@@@@@@@
[2.319 us ; 2.405 us) | @
[2.405 us ; 2.457 us) | @
---------------------------------------------------

MappingSamples.AutoMapperMapping: DefaultJob
Runtime = .NET 6.0.24 (6.0.2423.51814), X64 RyuJIT AVX2; GC = Concurrent Workstation
Mean = 2.583 us, StdErr = 0.013 us (0.49%), N = 54, StdDev = 0.093 us
Min = 2.410 us, Q1 = 2.527 us, Median = 2.558 us, Q3 = 2.626 us, Max = 2.829 us
IQR = 0.099 us, LowerFence = 2.378 us, UpperFence = 2.775 us
ConfidenceInterval = [2.539 us; 2.627 us] (CI 99.9%), Margin = 0.044 us (1.70% of Mean)
Skewness = 0.77, Kurtosis = 3.04, MValue = 2
-------------------- Histogram --------------------
[2.378 us ; 2.450 us) | @
[2.450 us ; 2.521 us) | @@@@@@@@@@
[2.521 us ; 2.585 us) | @@@@@@@@@@@@@@@@@@@@@@@
[2.585 us ; 2.655 us) | @@@@@@@@@@
[2.655 us ; 2.747 us) | @@@@@@
[2.747 us ; 2.831 us) | @@@@
---------------------------------------------------

MappingSamples.XsltXMLMapping: DefaultJob
Runtime = .NET 6.0.24 (6.0.2423.51814), X64 RyuJIT AVX2; GC = Concurrent Workstation
Mean = 18.056 us, StdErr = 0.076 us (0.42%), N = 15, StdDev = 0.294 us
Min = 17.695 us, Q1 = 17.836 us, Median = 17.966 us, Q3 = 18.246 us, Max = 18.656 us
IQR = 0.410 us, LowerFence = 17.221 us, UpperFence = 18.861 us
ConfidenceInterval = [17.741 us; 18.370 us] (CI 99.9%), Margin = 0.314 us (1.74% of Mean)
Skewness = 0.54, Kurtosis = 2.03, MValue = 2
-------------------- Histogram --------------------
[17.652 us ; 18.358 us) | @@@@@@@@@@@@@
[18.358 us ; 18.773 us) | @@
---------------------------------------------------

MappingSamples.XsltJsonMapping: DefaultJob
Runtime = .NET 6.0.24 (6.0.2423.51814), X64 RyuJIT AVX2; GC = Concurrent Workstation
Mean = 8.359 us, StdErr = 0.043 us (0.51%), N = 23, StdDev = 0.206 us
Min = 8.007 us, Q1 = 8.231 us, Median = 8.307 us, Q3 = 8.448 us, Max = 8.822 us
IQR = 0.217 us, LowerFence = 7.905 us, UpperFence = 8.774 us
ConfidenceInterval = [8.196 us; 8.522 us] (CI 99.9%), Margin = 0.163 us (1.95% of Mean)
Skewness = 0.55, Kurtosis = 2.73, MValue = 2
-------------------- Histogram --------------------
[7.936 us ; 8.161 us) | @@
[8.161 us ; 8.387 us) | @@@@@@@@@@@
[8.387 us ; 8.708 us) | @@@@@@@@
[8.708 us ; 8.917 us) | @@
---------------------------------------------------

MappingSamples.XsltTextMapping: DefaultJob
Runtime = .NET 6.0.24 (6.0.2423.51814), X64 RyuJIT AVX2; GC = Concurrent Workstation
Mean = 3.338 us, StdErr = 0.009 us (0.27%), N = 13, StdDev = 0.032 us
Min = 3.282 us, Q1 = 3.326 us, Median = 3.332 us, Q3 = 3.349 us, Max = 3.396 us
IQR = 0.023 us, LowerFence = 3.290 us, UpperFence = 3.384 us
ConfidenceInterval = [3.299 us; 3.376 us] (CI 99.9%), Margin = 0.038 us (1.15% of Mean)
Skewness = 0.3, Kurtosis = 2.42, MValue = 2
-------------------- Histogram --------------------
[3.264 us ; 3.413 us) | @@@@@@@@@@@@@
---------------------------------------------------
```
