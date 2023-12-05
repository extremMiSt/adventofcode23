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
}

long lowest = long.MaxValue;

for (int i = 0; i < seed.Length; i++){
    long v = seed[i];
    for (int j = 0; j < maps.Length; j++){
        long v1 =v;
        foreach (Mapping range in maps[j]){
            long n = range.Map(v);
            if(n != v1){
                v1 = n;
                break;
            }
        }
        v = v1;
    }
    if (v < lowest){
        lowest = v;
    }
}
Console.WriteLine(lowest);


List<Range> seeds = [];
for (int i = 0; i < seed.Length; i+=2){
    seeds.Add(new(seed[i], seed[i+1]));
}

for (int i = 0; i < maps.Length; i++){
    List<Range> done = [];
    foreach(Mapping m in maps[i]){
        List<Range> ss = [];
        foreach (Range sd in seeds){
            Range[] split = m.Split(sd);
            if(split[1] is not null){
                done.Add(split[1]);
            }
            if(split[0] is not null){
                ss.Add(split[0]);
            }
            if(split[2] is not null){
                ss.Add(split[2]);
            }
        }
        seeds = ss;
    }
    done.AddRange(seeds);
    seeds = done;
}

Range low = new(long.MaxValue,1);
foreach (Range ss in seeds){
    if(ss.Start < low.Start){
        low = ss;
    }
}

Console.WriteLine(low.Start);

class Range(long start, long span){
    public long Start {get;} = start;
    public long Span {get;} = span;
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

    public Range[] Split(Range r){
        Range[] rs = new Range[3];
        if(r.Start < this.Source){
            long end = Math.Min(r.Start+r.Span, this.Source-1);
            if(end - r.Start >= 0){
                rs[0] = new(r.Start, end - r.Start+1);
            }
        }
        if(r.Start+r.Span-1 >= this.Source && r.Start <= this.Source+this.Ranges-1){
            long start = Math.Max(this.Source, r.Start);
            long end = Math.Min(r.Start+r.Span-1, this.Source+this.Ranges-1);
            if(end - start >= 0){
                rs[1] = new(this.Map(start), end - start+1);
            }
        }
        if(r.Start+r.Span-1 >= this.Source+this.Ranges-1){
            long start = Math.Max(this.Source+this.Ranges, r.Start);
            long end = r.Start+r.Span-1;
            if(end - start >= 0){
                rs[2] = new(start, end-start+1);
            } 
        }
        return rs;
    }
}
