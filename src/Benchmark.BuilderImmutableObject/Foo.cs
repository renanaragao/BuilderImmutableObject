using System;
namespace Benchmark.BuilderImmutableObject
{
    public class Foo
    {
        [Obsolete]
        public Foo()
        {
            
        }

        public Foo(int id, decimal altura, string nome, DateTime nascimento, decimal peso, bool ativo)
        {
            this.Id = id;
            this.Altura = altura;
            this.Nome = nome;
            this.Nascimento = nascimento;
            this.Peso = peso;
            this.Ativo = ativo;

        }
        public int Id { get; private set; }
        public decimal Altura { get; private set; }
        public string Nome { get; private set; }
        public DateTime Nascimento { get; private set; }
        public decimal Peso { get; private set; }
        public bool Ativo { get; private set; }
    }
}