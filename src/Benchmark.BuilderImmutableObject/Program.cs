using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using BuilderImmutableObject;

namespace Benchmark.BuilderImmutableObject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<Clone>();
        }
    }

    [MemoryDiagnoser]
    public class Clone
    {
        private readonly Foo foo;

        public Clone()
        {
            foo = new Foo
            (
                id: 34,
                altura: 56.78m,
                nome: "Ricardo Alves",
                nascimento: DateTime.Now,
                peso: 45.67m,
                ativo: true
            );
        }

        [Benchmark]
        public Foo Clonar() => foo.Set(x => x.Nome, "novo nome").Build();
    }
}
