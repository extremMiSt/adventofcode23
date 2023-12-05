using System.Net.Sockets;

string file = File.ReadAllText("./day05/input.txt");
string[] sections = file.Split("\n\n");
List<Mapping>[] maps= new List<Mapping>[sections.Length-1];
for (int i = 0; i < maps.Length; i++){
    maps[i] = [];
}

string[] seedS = sections[0].Split(" ");
long[] seed = new long[seedS.Length-1];
for (int i = 1; i < seedS.Length; i++){
    seed[i-1] = long.Parse(seedS[i]);
}

for (int i = 1; i < sections.Length; i++){
    string[] ranges = sections[i].Split("\n");
    for (int j = 1; j < ranges.Length; j++){
        string[] rs = ranges[j].Split(" ");
        Mapping r = new(long.Parse(rs[0]), long.Parse(rs[1]), long.Parse(rs[2]));
        maps[i-1].Add(r);
    }
    maps[i-1].Sort();
    foreach (Mapping item in maps[i-1]){
        Console.Write(item);
        Console.Write(", ");
    }
    Console.WriteLine();
}

long lowest = long.MaxValue;

for (int i = 0; i < seed.Length; i++){
    long v = seed[i];
    Console.WriteLine("----SEED:" + v);
    for (int j = 0; j < maps.Length; j++){
        long v1 =v;
        foreach (Mapping range in maps[j]){
            long n = range.Map(v);
            if(n != v1){
                v1 = n;
                break;
            }
        }
        Console.WriteLine(j + " from " + v + " to " + v1);
        v = v1;
    }
    if (v < lowest){
        lowest = v;
    }
}
Console.WriteLine(lowest);


class Range(long start, long span, int type){

    private long Start {get;} = start;
    private long Span {get;} = span;
    private long Type {get;} = type; 
}


class Mapping(long dest, long source, long ranges): IComparable<Mapping>{
    private long Source { get; } = source;
    private long Dest { get; } = dest;
    private long Ranges { get; } = ranges;

    public int CompareTo(Mapping? other)
    {
        if (other is not null){
            long d = this.Source - other.Source;
            return d==0?0:(d<0?-1:1);
        }
        return 1;
    }

    public long Map(long input){
        if(input >= Source && input < Source+Ranges){
            long dist = input-Source;
            return Dest+dist;
        }else{
            return input;
        }
    }

    public override string ToString(){
        return "["+Dest+ ", " + Source + ", " + Ranges+"]";
    }

}
