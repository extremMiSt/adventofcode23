using System.Net.Sockets;

string file = File.ReadAllText("./day05/example1.txt");
string[] sections = file.Split("\n\n");
HashSet<Range>[] maps= new HashSet<Range>[sections.Length-1];
for (int i = 0; i < maps.Length; i++){
    maps[i] = [];
}

string[] seedS = sections[0].Split(" ");
long[] seed = new long[seedS.Length-1];
for (int i = 1; i < seed.Length; i++){
    seed[i-1] = long.Parse(seedS[i]);
}

for (int i = 1; i < sections.Length; i++){
    string[] ranges = sections[i].Split("\n");
    for (int j = 1; j < ranges.Length; j++){
        string[] rs = ranges[j].Split(" ");
        Range r = new(long.Parse(rs[0]), long.Parse(rs[1]), long.Parse(rs[2]));
        maps[i-1].Add(r);
    }
}




class Range(long source, long dest, long ranges){
    private long Source { get; } = source;
    private long Dest { get; } = dest;
    private long Ranges { get; } = ranges;

    public long Map(long input){
        if(input >= Source && input < Source+Ranges){
            long dist = input-Source;
            return Dest+dist;
        }else{
            return input;
        }
    }
}
