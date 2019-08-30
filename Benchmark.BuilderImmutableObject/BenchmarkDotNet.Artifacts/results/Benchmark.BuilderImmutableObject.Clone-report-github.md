``` ini

BenchmarkDotNet=v0.11.5, OS=ubuntu 19.04
Intel Core i5-3230M CPU 2.60GHz (Ivy Bridge), 1 CPU, 4 logical and 2 physical cores
.NET Core SDK=2.2.401
  [Host]     : .NET Core 2.1.12 (CoreCLR 4.6.27817.01, CoreFX 4.6.27818.01), 64bit RyuJIT
  DefaultJob : .NET Core 2.1.12 (CoreCLR 4.6.27817.01, CoreFX 4.6.27818.01), 64bit RyuJIT


```
| Method |     Mean |     Error |    StdDev |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|------- |---------:|----------:|----------:|-------:|------:|------:|----------:|
| Clonar | 11.55 us | 0.2300 us | 0.2461 us | 1.0986 |     - |     - |   1.71 KB |
