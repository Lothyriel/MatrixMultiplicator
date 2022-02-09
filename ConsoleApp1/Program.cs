using System.Diagnostics;
var sexo = Stopwatch.StartNew();
var putaria = new int[1000000 * 10];
for (int numero = 0; numero < 1000000; numero++)
{
    for (int i = 1; i <= 10; i++)
    {
        putaria[i] = i * numero;
    }
}
Console.WriteLine(sexo.ElapsedMilliseconds);
sexo.Stop();