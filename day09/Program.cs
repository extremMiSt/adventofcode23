StreamReader reader = new(File.OpenRead("./day09/input.txt")); 

long sum = 0;
long sum2 = 0;

while(!reader.EndOfStream){
    string? s = reader.ReadLine();
    if(s is not null){
        long[] line = parseLong(s.Split(" "));
        List<long> last = [line.Last()];
        List<long> first = [line.First()];
        Tuple<long[], bool> deriv = derive(line);
        last.Add(deriv.Item1.Last());
        first.Add(deriv.Item1.First());
        while(!deriv.Item2){
            deriv = derive(deriv.Item1);
            last.Add(deriv.Item1.Last());
            first.Add(deriv.Item1.First());
        }
        foreach (long item in last){
            sum += item;
        }
        first.Reverse();
        long temp = first.First();
        for (int i = 1; i < first.Count; i++){
            temp = first[i] - temp;
        }
        sum2 += temp;
    }
}
Console.WriteLine("part 1: " + sum);
Console.WriteLine("part 2: " + sum2);

static long[] parseLong(string[] s){
    long[] ret = new long[s.Length];
    for (int i = 0; i < ret.Length; i++){
        ret[i] = long.Parse(s[i]);
    }
    return ret;
}

static Tuple<long[], bool> derive(long[] s){
    bool done = true;
    long[] ret = new long[s.Length-1];
    for (int i = 0; i < ret.Length; i++){
        ret[i] = s[i+1] - s[i];
        done &= ret[i] == 0;
    }
    return new(ret, done);
}