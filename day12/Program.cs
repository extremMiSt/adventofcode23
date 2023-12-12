StreamReader reader = new(File.OpenRead("./day12/input.txt")); 

int sum = 0;
long sum2 = 0;
while(!reader.EndOfStream){
    string? s = reader.ReadLine();
    if(s is not null){
        int c = (new Problem(s)).Solve();
        long c2 = Part2.Solve2(s);
        sum+=c;
        sum2+=c2;
    }
}
Console.WriteLine(sum);
Console.WriteLine(sum2);
